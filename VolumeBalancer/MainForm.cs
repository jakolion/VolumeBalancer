using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;
using NAudio.CoreAudioApi;
using NAudio.CoreAudioApi.Interfaces;

namespace VolumeBalancer
{
    public partial class MainForm : Form, IAudioSessionEventsHandler
    {
        private NotifyIcon trayIcon;
        private List<AudioApp> _audioAppList = new List<AudioApp>();
        private string _currentFocusApplication;
        private bool _guiUpdateByEventIsRunning = false;
        private Thread _updateApplicationListThread;
        public bool updateApplicationListThreadAbort;

        const string AUTOSTART_PATH = "Software\\Microsoft\\Windows\\CurrentVersion\\Run";
        const string AUTOSTART_NAME = "VolumeBalancer";

        const string TRAYICON_BLACK = "iconBlack";
        const string TRAYICON_GREY = "iconGrey";
        const string TRAYICON_WHITE = "iconWhite";

        const int HOTKEY_INCREASE_FOCUS_APPLICATION_VOLUME = 1;
        const int HOTKEY_INCREASE_OTHER_APPLICATION_VOLUME = 2;
        const int HOTKEY_RESET_BALANCE = 3;
        const int HOTKEY_ACTIVATE_MAIN_FOCUS_APPLICATION = 4;
        const int HOTKEY_ACTIVATE_TEMPORARY_FOCUS_APPLICATION = 5;
        const int HOTKEY_RESET_ALL_VOLUMES = 6;


        public MainForm()
        {
            InitializeComponent();

            // read user settings
            UserSettings.readSettings();

            // we need initialize the form before being able to interact with the controls
            Hide();

            // create a tray menu
            ContextMenu trayMenu = new ContextMenu();
            MenuItem version = new MenuItem("Version " + Application.ProductVersion);
            version.Enabled = false;
            trayMenu.MenuItems.Add(version);
            trayMenu.MenuItems.Add("Configuration ...", OnTrayMenuConfigureClicked);
            trayMenu.MenuItems.Add("-");
            trayMenu.MenuItems.Add("Exit", OnTrayMenuExitClicked);

            // create a tray icon
            trayIcon = new NotifyIcon();
            trayIcon.Text = Application.ProductName;
            SetTrayIcon(UserSettings.getTrayIcon());

            // add tray menu to icon
            trayIcon.ContextMenu = trayMenu;
            trayIcon.Visible = true;
            trayIcon.MouseClick += OnTrayIconClicked;
            trayIcon.MouseDoubleClick += OnTrayIconClicked;

            // set the focus application
            _currentFocusApplication = UserSettings.getMainFocusApplication();
            textBoxMainFocusApplication.Text = UserSettings.getMainFocusApplication();

            // pre-set the audio application drop down list
            UpdateDropDownListAudioApplications();

            // set the balance
            trackBarBalance.Value = (int)(trackBarBalance.Maximum / 100 * UserSettings.getBalancePosition());

            // set the hotkeys
            SetAllHotkeys();

            // start thread for polling audio applications
            _updateApplicationListThread = new Thread(UpdateApplicationListJob);
            _updateApplicationListThread.Start();

            // check autostart
            CheckAutostartStatus();

            // show the GUI if the focus application is not saved in the user settings
            if (UserSettings.getMainFocusApplication() == "")
                Show();
        }


        #region main functions

        // set tray icon
        private void SetTrayIcon(string trayIconName)
        {
            if (Properties.Resources.ResourceManager.GetObject(trayIconName) == null)
                trayIconName = TRAYICON_GREY;

            trayIcon.Icon = (Icon)Properties.Resources.ResourceManager.GetObject(trayIconName);

            // select radio button
            switch (trayIconName)
            {
                case TRAYICON_BLACK:
                    radioButtonTrayIconColorBlack.Select();
                    break;

                case TRAYICON_GREY:
                    radioButtonTrayIconColorGrey.Select();
                    break;

                case TRAYICON_WHITE:
                    radioButtonTrayIconColorWhite.Select();
                    break;
            }
        }


        // check autostart and set radio buttons
        private void CheckAutostartStatus()
        {
            // get autostart path
            string autostartPath = Helper.RegistryReadCurrentUser(AUTOSTART_PATH, AUTOSTART_NAME);

            // autostart path does not exist
            if (autostartPath == null)
            {
                radioButtonAutostartDisabled.Select();
                return;
            }

            // check if autostart path is correct
            string currentPath = Helper.GetProcessPath((uint)Process.GetCurrentProcess().Id);
            if (currentPath != autostartPath)
            {
                // path is different, update the path
                Helper.RegistryWriteCurrentUser(AUTOSTART_PATH, AUTOSTART_NAME, currentPath);
            }

            radioButtonAutostartEnabled.Select();
        }


        // enable autostart
        private void EnableAutostart()
        {
            string currentPath = Helper.GetProcessPath((uint)Process.GetCurrentProcess().Id);
            Helper.RegistryWriteCurrentUser(AUTOSTART_PATH, AUTOSTART_NAME, currentPath);
            CheckAutostartStatus();
        }


        // disable autostart
        private void DisableAutostart()
        {
            Helper.RegistryDeleteCurrentUser(AUTOSTART_PATH, AUTOSTART_NAME);
            CheckAutostartStatus();
        }


        // update the application list
        private void UpdateApplicationList()
        {
            bool currentFocusApplicationIsRunning = false;

            // clear the list
            _audioAppList.Clear();

            // get current sessions
            MMDeviceEnumerator deviceEnumerator = new MMDeviceEnumerator();
            MMDevice device = deviceEnumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
            SessionCollection sessions = device.AudioSessionManager.Sessions;

            // return if no sessions are available
            if (sessions == null) return;

            // loop through sessions
            for (int i = 0; i < sessions.Count; i++)
            {
                AudioSessionControl session = sessions[i];
                // check if session is not expired and process exists
                if (session.State != AudioSessionState.AudioSessionStateExpired && Helper.ProcessExists(session.GetProcessID))
                {
                    if (session.IsSystemSoundsSession)
                    {
                        _audioAppList.Add(new AudioApp(session, "System Sound"));
                    }
                    else
                    {
                        // get full path of process if possible
                        string applicationPath = Helper.GetProcessPath(session.GetProcessID);
                        if (applicationPath == "")
                            applicationPath = Process.GetProcessById((int)session.GetProcessID).ProcessName;

                        // add new audio app to list if not already existing
                        if (!AudioApplicationIsRunning(applicationPath))
                            _audioAppList.Add(new AudioApp(session, applicationPath));

                        // check if current focus application is running
                        if (applicationPath == _currentFocusApplication)
                            currentFocusApplicationIsRunning = true;
                    }
                    session.RegisterEventClient(this);
                }
            }

            // switch to main focus application if temporary focus application exits
            if (_currentFocusApplication != UserSettings.getMainFocusApplication() && !currentFocusApplicationIsRunning)
                ActivateMainFocusApplication();

            // update temp focus application text box
            UpdateTempFocusApplicationTextBox();

            // update GUI
            UpdateBalanceSlider();
        }


        // update drop down list with audio applications
        private void UpdateDropDownListAudioApplications()
        {
            comboAudioApplications.Items.Clear();
            comboAudioApplications.Sorted = true;
            comboAudioApplications.Items.AddRange(_audioAppList.ToArray());
            comboAudioApplications.Sorted = false;
            comboAudioApplications.Items.Insert(0, "Select a running audio application");
            comboAudioApplications.SelectedIndex = 0;
        }


        // set all hotkeys
        private void SetAllHotkeys()
        {
            SetHotkey(HOTKEY_INCREASE_FOCUS_APPLICATION_VOLUME, UserSettings.getHotkeyIncreaseFocusApplicationVolume());
            SetHotkeyTextBox(textBoxHotkeyIncreaseFocusApplicationVolume, UserSettings.getHotkeyIncreaseFocusApplicationVolume());

            SetHotkey(HOTKEY_INCREASE_OTHER_APPLICATION_VOLUME, UserSettings.getHotkeyIncreaseOtherApplicationVolume());
            SetHotkeyTextBox(textBoxHotkeyIncreaseOtherApplicationVolume, UserSettings.getHotkeyIncreaseOtherApplicationVolume());

            SetHotkey(HOTKEY_RESET_BALANCE, UserSettings.getHotkeyResetBalance());
            SetHotkeyTextBox(textBoxHotkeyResetBalance, UserSettings.getHotkeyResetBalance());

            SetHotkey(HOTKEY_ACTIVATE_MAIN_FOCUS_APPLICATION, UserSettings.getHotkeyActivateMainFocusApplication());
            SetHotkeyTextBox(textBoxHotkeyActivateMainFocusApplication, UserSettings.getHotkeyActivateMainFocusApplication());

            SetHotkey(HOTKEY_ACTIVATE_TEMPORARY_FOCUS_APPLICATION, UserSettings.getHotkeyActivateTemporaryFocusApplication());
            SetHotkeyTextBox(textBoxHotkeyActivateTemporaryFocusApplication, UserSettings.getHotkeyActivateTemporaryFocusApplication());

            SetHotkey(HOTKEY_RESET_ALL_VOLUMES, UserSettings.getHotkeyResetAllVolumes());
            SetHotkeyTextBox(textBoxHotkeyResetAllVolumes, UserSettings.getHotkeyResetAllVolumes());
        }


        // set a specific hotkey
        private void SetHotkey(int hotkeyId, Hotkey hotkey)
        {
            // unregister old hotkey
            UnregisterHotKey(this.Handle, hotkeyId);

            // check if hotkey is cleared
            if (hotkey.getModifierKeys() != Keys.None && hotkey.getPressedKey() != Keys.None)
            {
                // calculate modifier id
                int modifierId = 0;
                if ((hotkey.getModifierKeys() & Keys.Shift) != Keys.None)
                    modifierId += MOD_SHIFT;

                if ((hotkey.getModifierKeys() & Keys.Control) != Keys.None)
                    modifierId += MOD_CONTROL;

                if ((hotkey.getModifierKeys() & Keys.LWin) != Keys.None || (hotkey.getModifierKeys() & Keys.RWin) != Keys.None)
                    modifierId += MOD_WIN;

                if ((hotkey.getModifierKeys() & Keys.Alt) != Keys.None)
                    modifierId += MOD_ALT;

                // set hotkey
                RegisterHotKey(this.Handle, hotkeyId, modifierId, (int)hotkey.getPressedKey());
            }
        }


        // set the textbox text for a specific hotkey
        private void SetHotkeyTextBox(TextBox textBox, Hotkey hotkey)
        {
            // combine key data
            Keys keys = hotkey.getModifierKeys() | hotkey.getPressedKey();

            // convert the key data for the text box
            var converter = new KeysConverter();
            textBox.Text = converter.ConvertToString(keys);
        }


        // save a specific hotkey in user settings
        private void SaveHotkeyInUserSettings(int hotkeyId, Hotkey hotkey)
        {
            switch (hotkeyId)
            {
                case HOTKEY_INCREASE_FOCUS_APPLICATION_VOLUME:
                    UserSettings.setHotkeyIncreaseFocusApplicationVolume(hotkey);
                    break;

                case HOTKEY_INCREASE_OTHER_APPLICATION_VOLUME:
                    UserSettings.setHotkeyIncreaseOtherApplicationVolume(hotkey);
                    break;

                case HOTKEY_RESET_BALANCE:
                    UserSettings.setHotkeyResetBalance(hotkey);
                    break;

                case HOTKEY_ACTIVATE_MAIN_FOCUS_APPLICATION:
                    UserSettings.setHotkeyActivateMainFocusApplication(hotkey);
                    break;

                case HOTKEY_ACTIVATE_TEMPORARY_FOCUS_APPLICATION:
                    UserSettings.setHotkeyActivateTemporaryFocusApplication(hotkey);
                    break;

                case HOTKEY_RESET_ALL_VOLUMES:
                    UserSettings.setHotkeyResetAllVolumes(hotkey);
                    break;
            }
        }


        // records an hotkey pressed on textbox
        private void HotkeyPressed(TextBox textBox, KeyEventArgs e, int hotkeyId)
        {
            // get modifier keys
            Keys modifierKeys = e.Modifiers;

            // get non-modifier keys
            Keys pressedKey = e.KeyData ^ modifierKeys;

            if (modifierKeys == Keys.None && (pressedKey == Keys.Back || pressedKey == Keys.Delete))
            {
                // delete the hotkey

                // create a new hotkey object
                Hotkey hotkey = new Hotkey(Keys.None, Keys.None);

                // save the hotkey
                SaveHotkeyInUserSettings(hotkeyId, hotkey);

                // apply the new hotkey
                SetHotkey(hotkeyId, hotkey);

                // write the new hotkey to the textbox
                SetHotkeyTextBox(textBox, hotkey);
            }
            else
            {
                // check if at least one modifier and another key was pressed
                if (
                    modifierKeys != Keys.None &&
                    pressedKey != Keys.None &&
                    pressedKey != Keys.Menu &&
                    pressedKey != Keys.ShiftKey &&
                    pressedKey != Keys.ControlKey)
                {
                    // create a new hotkey object
                    Hotkey hotkey = new Hotkey(modifierKeys, pressedKey);

                    // save the hotkey
                    SaveHotkeyInUserSettings(hotkeyId, hotkey);

                    // apply the new hotkey
                    SetHotkey(hotkeyId, hotkey);

                    // write the new hotkey to the textbox
                    SetHotkeyTextBox(textBox, hotkey);
                }
            }

            e.Handled = true;
            e.SuppressKeyPress = true;
        }


        // returns true if the application path can be found
        // in the current audio application list
        private bool AudioApplicationIsRunning(string applicationPath)
        {
            if (applicationPath == "") return false;
            return _audioAppList.FirstOrDefault(x => x.ToString() == applicationPath) != null;
        }


        // get application volumes
        // sets loudest application to 1
        private void GetApplicationVolumes(out float focusApplicationVolume, out float highestOtherApplicationVolume)
        {
            focusApplicationVolume = 0;
            highestOtherApplicationVolume = 0;

            // get highest volume of focus application and other application
            for (int i = 0; i < _audioAppList.Count; i++)
            {
                AudioApp app = _audioAppList[i];
                AudioSessionControl session = app.session;
                if (app.path == _currentFocusApplication)
                {
                    focusApplicationVolume = session.SimpleAudioVolume.Volume;
                }
                else
                {
                    if (session.SimpleAudioVolume.Volume > highestOtherApplicationVolume)
                        highestOtherApplicationVolume = session.SimpleAudioVolume.Volume;
                }
            }

            // return if focus application isn't running
            if (!AudioApplicationIsRunning(_currentFocusApplication)) return;

            // return if only the audio application is running
            if (AudioApplicationIsRunning(_currentFocusApplication) && _audioAppList.Count == 1) return;

            // set the loudest application (also focus application) to 1
            if (focusApplicationVolume < 1 && highestOtherApplicationVolume < 1)
            {
                float multiplier = 0;
                if (focusApplicationVolume >= highestOtherApplicationVolume)
                {
                    multiplier = 1 + (100 / focusApplicationVolume * (1 - focusApplicationVolume) / 100);
                }
                else if (highestOtherApplicationVolume > focusApplicationVolume)
                {
                    multiplier = 1 + (100 / highestOtherApplicationVolume * (1 - highestOtherApplicationVolume) / 100);
                }

                for (int i = 0; i < _audioAppList.Count; i++)
                {
                    AudioApp app = _audioAppList[i];
                    AudioSessionControl session = app.session;
                    session.SimpleAudioVolume.Volume *= multiplier;
                }

            }
        }


        // activate main focus application
        private void ActivateMainFocusApplication()
        {
            // set new focus application
            _currentFocusApplication = UserSettings.getMainFocusApplication();

            // update temp focus application text box
            UpdateTempFocusApplicationTextBox();

            // update GUI
            UpdateBalanceSlider();
        }


        // activate temporary focus application
        private void ActivateTemporaryFocusApplication()
        {
            // set new focus application to topmost application
            string processPath = Helper.GetProcessPath(Helper.GetTopmostProcessId());
            if (AudioApplicationIsRunning(processPath))
            {
                // set new focus application
                _currentFocusApplication = processPath;

                // update temp focus application text box
                UpdateTempFocusApplicationTextBox();

                // update GUI
                UpdateBalanceSlider();
            }
        }


        // update the balance slider
        private void UpdateBalanceSlider()
        {
            // get highest volume of focus application and other application
            float focusApplicationVolume = 0;
            float highestOtherApplicationVolume = 0;
            GetApplicationVolumes(out focusApplicationVolume, out highestOtherApplicationVolume);

            // check if focus application is running
            if (AudioApplicationIsRunning(_currentFocusApplication))
            {
                // check if only the focus aplication is running
                if (_audioAppList.Count == 1)
                {
                    // focus application is the only audio application running
                    // so the slider can only be moved to the right side
                    trackBarBalance.Value = trackBarBalance.Maximum - (int)Math.Round((trackBarBalance.Maximum / 2) * focusApplicationVolume);

                    // disable label focus
                    labelBalanceFocusApplication.Enabled = false;
                }
                else
                {
                    // calc position of the slider
                    if (focusApplicationVolume > highestOtherApplicationVolume)
                    {
                        trackBarBalance.Value = (int)Math.Round((trackBarBalance.Maximum / 2) * highestOtherApplicationVolume);
                    }
                    else if (highestOtherApplicationVolume > focusApplicationVolume)
                    {
                        trackBarBalance.Value = trackBarBalance.Maximum - (int)Math.Round((trackBarBalance.Maximum / 2) * focusApplicationVolume);
                    }
                    else
                    {
                        trackBarBalance.Value = trackBarBalance.Maximum / 2;
                    }

                    // enable label focus
                    labelBalanceFocusApplication.Enabled = true;
                }

                // enable label other
                labelBalanceOtherApplications.Enabled = true;

                // enable balance group
                groupBoxBalance.Enabled = true;
            }
            else
            {
                // check if a focus application is set and not empty
                if (UserSettings.getMainFocusApplication() == "")
                {
                    // disable label other
                    labelBalanceOtherApplications.Enabled = false;

                    // disable label focus
                    labelBalanceFocusApplication.Enabled = false;

                    // disable balance group and move slider to the center
                    groupBoxBalance.Enabled = false;
                    trackBarBalance.Value = trackBarBalance.Maximum / 2;
                }
                else
                {
                    // focus application is not running

                    // disable label other
                    labelBalanceOtherApplications.Enabled = false;

                    // enable label focus
                    labelBalanceFocusApplication.Enabled = true;

                    // enable balance group
                    groupBoxBalance.Enabled = true;

                    // so the slider can only be moved to the left side
                    trackBarBalance.Value = (int)Math.Round((trackBarBalance.Maximum / 2) * highestOtherApplicationVolume);
                }
            }
        }


        // update the temp focus application text box
        private void UpdateTempFocusApplicationTextBox()
        {
            if (_currentFocusApplication == UserSettings.getMainFocusApplication())
            {
                labelTemporaryFocusApplication.Enabled = false;
                textBoxTemporaryFocusApplication.Text = "";
                textBoxTemporaryFocusApplication.Enabled = false;
            }
            else
            {
                labelTemporaryFocusApplication.Enabled = true;
                textBoxTemporaryFocusApplication.Text = _currentFocusApplication;
                textBoxTemporaryFocusApplication.Enabled = true;
            }
        }


        // reset balance
        private void ResetBalance()
        {
            // store old track bar value
            int oldValue = trackBarBalance.Value;

            // set balance slider to the center
            trackBarBalance.Value = trackBarBalance.Maximum / 2;

            // force reset of balance if value hasn't changed
            if (oldValue == trackBarBalance.Value)
                trackBarBalance_ValueChanged(null, null);
        }


        // increase focus volume
        private void IncreaseFocusApplicationVolume()
        {
            // move slider to the left (to focus aplication)
            if (trackBarBalance.Value > trackBarBalance.Minimum)
                trackBarBalance.Value--;
        }


        // increase other apps volume
        private void IncreaseOtherApplicationVolume()
        {
            // move slider to the right (to other aplications)
            if (trackBarBalance.Value < trackBarBalance.Maximum)
                trackBarBalance.Value++;
        }


        // reset all audio application volumes
        private void ResetAllAudioApplicationVolume()
        {
            // loop through audio applications and set session volume
            for (int i = 0; i < _audioAppList.Count; i++)
            {
                AudioApp app = _audioAppList[i];
                app.session.SimpleAudioVolume.Volume = 1;
            }
        }


        // poll for new audio applications
        private void UpdateApplicationListJob()
        {
            const long DELAY = 2 * TimeSpan.TicksPerSecond; // 2 seconds
            updateApplicationListThreadAbort = false;
            long ticksTarget = DateTime.Now.Ticks;
            long ticks;
            while (!updateApplicationListThreadAbort)
            {
                ticks = DateTime.Now.Ticks;
                if (ticks >= ticksTarget)
                {
                    try
                    {
                        this.BeginInvoke(new Action(delegate ()
                        {
                            UpdateApplicationList();
                        }));
                    }
                    catch { }
                    ticksTarget = DateTime.Now.Ticks + DELAY;
                    GC.Collect();
                }
                System.Threading.Thread.Sleep(100);
            }
        }

        #endregion


        #region gui events

        private void OnTrayIconClicked(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                OnTrayMenuConfigureClicked(sender, e);
        }


        private void OnTrayMenuConfigureClicked(object sender, EventArgs e)
        {
            // show main form and bring to front
            Show();
            Activate();
        }


        private void OnTrayMenuExitClicked(object sender, EventArgs e)
        {
            // end thread
            updateApplicationListThreadAbort = true;

            // release the icon resource
            trayIcon.Dispose();

            // release form
            Dispose();

            // exit
            Application.Exit();
        }


        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // hide window instead of closing it
            Hide();
            e.Cancel = true;
        }


        private void comboAudioApplications_DropDown(object sender, EventArgs e)
        {
            UpdateDropDownListAudioApplications();
        }


        private void comboAudioApplications_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboAudioApplications.SelectedIndex > 0)
            {
                string newFocusApplication = comboAudioApplications.SelectedItem.ToString();
                textBoxMainFocusApplication.Text = newFocusApplication;
                UserSettings.setMainFocusApplication(newFocusApplication);
                _currentFocusApplication = newFocusApplication;
                comboAudioApplications.SelectedIndex = 0;
                ActivateMainFocusApplication();
            }
        }


        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Applications (*.exe)|*.exe";
                ofd.FilterIndex = 1;
                ofd.Multiselect = false;
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    textBoxMainFocusApplication.Text = ofd.FileName;
                    UserSettings.setMainFocusApplication(ofd.FileName);
                    _currentFocusApplication = ofd.FileName;
                    ActivateMainFocusApplication();
                }
            }
        }


        private void trackBarBalance_ValueChanged(object sender, EventArgs e)
        {
            // skip if focus application name isn't set
            if (UserSettings.getMainFocusApplication() == "") return;

            // check if an event is updating the UI
            if (!_guiUpdateByEventIsRunning)
            {
                // get highest volume of focus application and other application
                float focusApplicationVolume = 0;
                float highestOtherApplicationVolume = 0;
                GetApplicationVolumes(out focusApplicationVolume, out highestOtherApplicationVolume);

                int center = trackBarBalance.Maximum / 2;
                int value = trackBarBalance.Value;
                float newFocusApplicationVolume = 0;
                float newHighestOtherApplicationVolume = 0;

                // check position of slider
                if (value < center)
                {
                    // slider is on focus application side
                    newFocusApplicationVolume = 1;
                    newHighestOtherApplicationVolume = (1f / center * value);
                }
                else if (value > center && AudioApplicationIsRunning(_currentFocusApplication))
                {
                    // slider is on other application side
                    newFocusApplicationVolume = (1f / center * (trackBarBalance.Maximum - value));
                    newHighestOtherApplicationVolume = 1;
                }
                else
                {
                    // slider is centered
                    newFocusApplicationVolume = 1;
                    newHighestOtherApplicationVolume = 1;
                }

                // calc multiplier for being able to keep the volume balance between all applications
                float multiplier = 100 / highestOtherApplicationVolume * newHighestOtherApplicationVolume / 100;

                // loop through audio applications and set session volume
                for (int i = 0; i < _audioAppList.Count; i++)
                {
                    AudioApp app = _audioAppList[i];
                    if (app.path == _currentFocusApplication)
                    {
                        app.session.SimpleAudioVolume.Volume = newFocusApplicationVolume;
                    }
                    else
                    {
                        // if highestOtherApplicationVolume is 0, multiplier is infinity
                        if (highestOtherApplicationVolume == 0)
                            app.session.SimpleAudioVolume.Volume = newHighestOtherApplicationVolume;
                        else
                            app.session.SimpleAudioVolume.Volume *= multiplier;
                    }
                }
            }

            // save new position of balance in user settings
            uint balancePosition = (uint)(100f / trackBarBalance.Maximum * trackBarBalance.Value);
            UserSettings.setBalancePosition(balancePosition);
        }


        private void buttonIncreaseFocusApplicationVolume_Click(object sender, EventArgs e)
        {
            // increase focus volume
            IncreaseFocusApplicationVolume();
        }


        private void buttonResetBalance_Click(object sender, EventArgs e)
        {
            // reset balance
            ResetBalance();
        }


        private void buttonIncreaseOtherApplicationVolume_Click(object sender, EventArgs e)
        {
            // increase other apps volume
            IncreaseOtherApplicationVolume();
        }


        private void textBoxShortcutIncreaseFocusApplicationVolume_KeyDown(object sender, KeyEventArgs e)
        {
            HotkeyPressed((TextBox)sender, e, HOTKEY_INCREASE_FOCUS_APPLICATION_VOLUME);
        }


        private void textBoxHotkeyIncreaseOtherApplicationsVolume_KeyDown(object sender, KeyEventArgs e)
        {
            HotkeyPressed((TextBox)sender, e, HOTKEY_INCREASE_OTHER_APPLICATION_VOLUME);
        }


        private void textBoxHotkeyResetBalance_KeyDown(object sender, KeyEventArgs e)
        {
            HotkeyPressed((TextBox)sender, e, HOTKEY_RESET_BALANCE);
        }


        private void textBoxHotkeyActivateMainFocusApplication_KeyDown(object sender, KeyEventArgs e)
        {
            HotkeyPressed((TextBox)sender, e, HOTKEY_ACTIVATE_MAIN_FOCUS_APPLICATION);
        }


        private void textBoxHotkeyActivateTemporaryFocusApplication_KeyDown(object sender, KeyEventArgs e)
        {
            HotkeyPressed((TextBox)sender, e, HOTKEY_ACTIVATE_TEMPORARY_FOCUS_APPLICATION);
        }


        private void textBoxHotkeyResetAllVolumes_KeyDown(object sender, KeyEventArgs e)
        {
            HotkeyPressed((TextBox)sender, e, HOTKEY_RESET_ALL_VOLUMES);
        }


        private void radioButtonTrayIconColorBlack_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            if (rb.Checked)
            {
                UserSettings.setTrayIcon(TRAYICON_BLACK);
                SetTrayIcon(TRAYICON_BLACK);
            }
        }


        private void radioButtonTrayIconColorGrey_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            if (rb.Checked)
            {
                UserSettings.setTrayIcon(TRAYICON_GREY);
                SetTrayIcon(TRAYICON_GREY);
            }
        }


        private void radioButtonTrayIconColorWhite_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            if (rb.Checked)
            {
                UserSettings.setTrayIcon(TRAYICON_WHITE);
                SetTrayIcon(TRAYICON_WHITE);
            }
        }


        private void radioButtonAutostartEnabled_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            if (rb.Checked)
            {
                EnableAutostart();
            }
        }
            

        private void radioButtonAutostartDisabled_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = (RadioButton)sender;
            if (rb.Checked)
            {
                DisableAutostart();
            }
        }


        #endregion


        #region external events

        public void OnVolumeChanged(float volume, bool isMuted)
        {
            try
            {
                this.BeginInvoke(new Action(delegate ()
                {
                    _guiUpdateByEventIsRunning = true;
                    UpdateBalanceSlider();
                    _guiUpdateByEventIsRunning = false;
                }));
            }
            catch { }
            GC.Collect();
        }

        public void OnStateChanged(AudioSessionState state) { }

        public void OnDisplayNameChanged(string displayName) { }

        public void OnIconPathChanged(string iconPath) { }

        public void OnChannelVolumeChanged(uint channelCount, IntPtr newVolumes, uint channelIndex) { }

        public void OnGroupingParamChanged(ref Guid groupingId) { }

        public void OnSessionDisconnected(AudioSessionDisconnectReason disconnectReason) { }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_HOTKEY)
            {
                switch ((int)m.WParam)
                {
                    case HOTKEY_INCREASE_FOCUS_APPLICATION_VOLUME:
                        IncreaseFocusApplicationVolume();
                        break;

                    case HOTKEY_INCREASE_OTHER_APPLICATION_VOLUME:
                        IncreaseOtherApplicationVolume();
                        break;

                    case HOTKEY_RESET_BALANCE:
                        ResetBalance();
                        break;

                    case HOTKEY_ACTIVATE_MAIN_FOCUS_APPLICATION:
                        ActivateMainFocusApplication();
                        break;

                    case HOTKEY_ACTIVATE_TEMPORARY_FOCUS_APPLICATION:
                        ActivateTemporaryFocusApplication();
                        break;

                    case HOTKEY_RESET_ALL_VOLUMES:
                        ResetAllAudioApplicationVolume();
                        break;
                }
            }
            base.WndProc(ref m);
        }

        #endregion


        #region imports

        [DllImport("user32.dll")]
        private static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vk);

        [DllImport("user32.dll")]
        private static extern bool UnregisterHotKey(IntPtr hWnd, int id);

        const int MOD_ALT = 0x0001;
        const int MOD_CONTROL = 0x0002;
        const int MOD_SHIFT = 0x0004;
        const int MOD_WIN = 0x0008;
        const int WM_HOTKEY = 0x0312;

        #endregion

    }

}
