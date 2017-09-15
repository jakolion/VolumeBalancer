using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Timers;
using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;
using NAudio.CoreAudioApi;
using NAudio.CoreAudioApi.Interfaces;

namespace VolumeBalancer
{
    public partial class MainForm : Form, IAudioSessionEventsHandler
    {
        private NotifyIcon _trayIcon;
        private List<HotkeyListElement> _hotkeyList = new List<HotkeyListElement>();
        private List<AudioApp> _audioAppList = new List<AudioApp>();
        private string _currentFocusApplication;
        private bool _guiUpdateByEventIsRunning = false;
        private System.Timers.Timer _highlightHotkeyResetTimer;
        public bool abortAllThreads = false;

        const string AUTOSTART_PATH = "Software\\Microsoft\\Windows\\CurrentVersion\\Run";
        const string AUTOSTART_NAME = "VolumeBalancer";
        
        const string TRAYICON_NAME = "icon_128";


        public MainForm()
        {
            InitializeComponent();

            // set language to English/US
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");

            // read user settings
            UserSettings.readSettings();

            // we need initialize the form before being able to interact with the controls
            Hide();

            // create a tray menu
            ContextMenu trayMenu = new ContextMenu();
            MenuItem version = new MenuItem(Application.ProductName + " " + GetVersion());
            version.Enabled = false;
            trayMenu.MenuItems.Add(version);
            trayMenu.MenuItems.Add("Configuration ...", OnTrayMenuConfigureClicked);
            trayMenu.MenuItems.Add("-");
            trayMenu.MenuItems.Add("Exit", OnTrayMenuExitClicked);

            // create a tray icon
            _trayIcon = new NotifyIcon();
            _trayIcon.Text = Application.ProductName;
            SetTrayIcon(UserSettings.getTrayIconColor());

            // add tray menu to icon
            _trayIcon.ContextMenu = trayMenu;
            _trayIcon.Visible = true;
            _trayIcon.MouseClick += OnTrayIconClicked;
            _trayIcon.MouseDoubleClick += OnTrayIconClicked;

            // set the focus application
            _currentFocusApplication = UserSettings.getMainFocusApplication();
            textBoxMainFocusApplication.Text = UserSettings.getMainFocusApplication();

            // pre-set the audio application drop down list
            UpdateDropDownListAudioApplications();

            // set Color based on configuration
            panelTrayIconColor.BackColor = UserSettings.getTrayIconColor();

            // set checkbox for balancing system sound
            checkBoxBalanceSystemSounds.Checked = UserSettings.getBalanceSystemSound();

            // define and set the hotkeys
            DefineHotkeys();
            SetAllHotkeys();

            // check autostart
            CheckAutostartStatus();

            // start timer for polling audio applications
            System.Timers.Timer updateApplicationListTimer = new System.Timers.Timer();
            updateApplicationListTimer.Interval = 2000;
            updateApplicationListTimer.Elapsed += new ElapsedEventHandler(UpdateApplicationListJob);
            updateApplicationListTimer.AutoReset = true;
            updateApplicationListTimer.Start();

            // create timer for reseting the hotkey highlight
            _highlightHotkeyResetTimer = new System.Timers.Timer();
            _highlightHotkeyResetTimer.Interval = 3000;
            _highlightHotkeyResetTimer.Elapsed += new ElapsedEventHandler(HighlightHotkeyReset);
            _highlightHotkeyResetTimer.AutoReset = false;

            // show the GUI if the focus application is not saved in the user settings
            if (UserSettings.getMainFocusApplication() == "")
                Show();
        }


        #region main functions

        // set tray icon
        private void SetTrayIcon(Color color)
        {
            _trayIcon.Icon = Helper.SetIconColor((Bitmap)Properties.Resources.ResourceManager.GetObject(TRAYICON_NAME), color);
            
        }


        // check autostart and set radio buttons
        private void CheckAutostartStatus()
        {
            // get autostart path
            string autostartPath = Helper.RegistryReadCurrentUser(AUTOSTART_PATH, AUTOSTART_NAME);

            // autostart path does not exist
            if (autostartPath == null)
            {
                checkBoxAutostart.Checked = false;
                return;
            }

            // check if autostart path is correct
            string currentPath = Helper.GetProcessPath((uint)Process.GetCurrentProcess().Id);
            if (currentPath != autostartPath)
            {
                // path is different, update the path
                Helper.RegistryWriteCurrentUser(AUTOSTART_PATH, AUTOSTART_NAME, currentPath);
            }

            checkBoxAutostart.Checked = true;
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
                        if (UserSettings.getBalanceSystemSound())
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

            // update balance slider
            UpdateBalanceSlider();
        }


        // update drop down list with audio applications
        private void UpdateDropDownListAudioApplications()
        {
            // create new audio application list without system sound
            List<string> audioAppList = new List<string>();

            // loop through audio applications
            for (int i = 0; i < _audioAppList.Count; i++)
            {
                if (!_audioAppList[i].session.IsSystemSoundsSession)
                {
                    audioAppList.Add(_audioAppList[i].ToString());
                }
            }

            // build combo box
            comboAudioApplications.Items.Clear();
            comboAudioApplications.Sorted = true;
            comboAudioApplications.Items.AddRange(audioAppList.ToArray());
            comboAudioApplications.Sorted = false;
            comboAudioApplications.Items.Insert(0, "Select a running audio application");
            comboAudioApplications.SelectedIndex = 0;
        }


        // define hotkeys
        private void DefineHotkeys()
        {
            int i = 1;

            _hotkeyList.Add(new HotkeyListElement(
                i++,
                textBoxHotkeyIncreaseFocusApplicationVolume,
                labelHotkeyIncreaseFocusApplicationVolume,
                UserSettings.getHotkeyIncreaseFocusApplicationVolume,
                UserSettings.setHotkeyIncreaseFocusApplicationVolume,
                IncreaseFocusApplicationVolume
                ));

            _hotkeyList.Add(new HotkeyListElement(
                i++,
                textBoxHotkeyIncreaseOtherApplicationVolume,
                labelHotkeyIncreaseOtherApplicationsVolume,
                UserSettings.getHotkeyIncreaseOtherApplicationVolume,
                UserSettings.setHotkeyIncreaseOtherApplicationVolume,
                IncreaseOtherApplicationVolume
                ));

            _hotkeyList.Add(new HotkeyListElement(
                i++,
                textBoxHotkeyResetBalance,
                labelHotkeyResetBalance,
                UserSettings.getHotkeyResetBalance,
                UserSettings.setHotkeyResetBalance,
                ResetBalance
                ));

            _hotkeyList.Add(new HotkeyListElement(
                i++,
                textBoxHotkeyActivateMainFocusApplication,
                labelHotkeyActivateMainFocusApplication,
                UserSettings.getHotkeyActivateMainFocusApplication,
                UserSettings.setHotkeyActivateMainFocusApplication,
                ActivateMainFocusApplication
                ));

            _hotkeyList.Add(new HotkeyListElement(
                i++,
                textBoxHotkeySetAndActivateTemporaryFocusApplication,
                labelHotkeySetAndActivateTemporaryFocusApplication,
                UserSettings.getHotkeySetAndActivateTemporaryFocusApplication,
                UserSettings.setHotkeySetAndActivateTemporaryFocusApplication,
                SetAndActivateTemporaryFocusApplication
                ));

            _hotkeyList.Add(new HotkeyListElement(
                i++,
                textBoxHotkeyResetAllVolumes,
                labelHotkeyResetAllVolumes,
                UserSettings.getHotkeyResetAllVolumes,
                UserSettings.setHotkeyResetAllVolumes,
                ResetAllVolumes
                ));

        }


        // set all hotkeys
        private void SetAllHotkeys()
        {
            foreach (HotkeyListElement hle in _hotkeyList)
            {
                SetHotkey(hle.id, hle.getHotkey());
                SetHotkeyTextBox(hle.textBox, hle.getHotkey());
            }
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
            foreach (HotkeyListElement hle in _hotkeyList)
            {
                if (hotkeyId == hle.id)
                {
                    hle.setHotkey(hotkey);
                    return;
                }
            }
        }


        // records an hotkey pressed on textbox
        private void HotkeyInTextBoxPressed(TextBox textBox, KeyEventArgs e)
        {
            // get modifier keys
            Keys modifierKeys = e.Modifiers;

            // get non-modifier keys
            Keys pressedKey = e.KeyData ^ modifierKeys;

            // get hotkey id via current text box
            int hotkeyId = -1;
            foreach (HotkeyListElement hle in _hotkeyList)
            {
                if (textBox == hle.textBox)
                {
                    hotkeyId = hle.id;
                    break;
                }
            }

            // check if hotkey id was found
            if (hotkeyId < 0)
                return;

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


        // highlights a hotkey text box
        private void HighlightHotkey(HotkeyListElement hle)
        {
            // reset all label and text box colors
            foreach (HotkeyListElement hleSearch in _hotkeyList)
            {
                if (hleSearch.label.ForeColor != SystemColors.ControlText)
                    hleSearch.label.ForeColor = SystemColors.ControlText;

                if (hleSearch.textBox.ForeColor != SystemColors.ControlText)
                    hleSearch.textBox.ForeColor = SystemColors.ControlText;
            }

            // highlight new label and textbox
            hle.label.ForeColor = Color.Red;
            // backcolor has to be set in order to change the forecolor in a readonly text box
            hle.textBox.BackColor = SystemColors.Control;
            hle.textBox.ForeColor = Color.Red;

            // reset timer
            _highlightHotkeyResetTimer.Stop();
            _highlightHotkeyResetTimer.Start();
        }


        // reset hotkey label and text color
        private void HighlightHotkeyReset(object source, ElapsedEventArgs e)
        {
            foreach (HotkeyListElement hle in _hotkeyList)
            {
                if (hle.label.ForeColor != SystemColors.ControlText)
                    hle.label.ForeColor = SystemColors.ControlText;

                if (hle.textBox.ForeColor != SystemColors.ControlText)
                    hle.textBox.ForeColor = SystemColors.ControlText;
            }
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
        private void SetAndActivateTemporaryFocusApplication()
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
                // check if only the focus application is running
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
                    // check if no audio application is running
                    if (_audioAppList.Count == 0)
                    {
                        // disable balance group
                        groupBoxBalance.Enabled = false;
                    }
                    else
                    {
                        // one or more audio applications running
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
        private void ResetAllVolumes()
        {
            // loop through audio applications and set session volume
            for (int i = 0; i < _audioAppList.Count; i++)
            {
                AudioApp app = _audioAppList[i];
                app.session.SimpleAudioVolume.Volume = 1;
            }
        }


        // poll for new audio applications
        private void UpdateApplicationListJob(object source, ElapsedEventArgs e)
        {
            try
            {
                this.BeginInvoke(new Action(delegate ()
                {
                    UpdateApplicationList();
                }));
            }
            catch { }
            GC.Collect();
        }


        // get version
        private string GetVersion()
        {
            string version = Application.ProductVersion;
            char charToFind = '.';
            int first = version.IndexOf(charToFind);
            if (first < 0)
                return version;

            int second = version.IndexOf(charToFind, first + 1);
            if (second < 0)
                return version;

            return version.Substring(0, second);
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
            abortAllThreads = true;

            // release the icon resource
            _trayIcon.Dispose();

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
            HotkeyInTextBoxPressed((TextBox)sender, e);
        }


        private void textBoxHotkeyIncreaseOtherApplicationsVolume_KeyDown(object sender, KeyEventArgs e)
        {
            HotkeyInTextBoxPressed((TextBox)sender, e);
        }


        private void textBoxHotkeyResetBalance_KeyDown(object sender, KeyEventArgs e)
        {
            HotkeyInTextBoxPressed((TextBox)sender, e);
        }


        private void textBoxHotkeyActivateMainFocusApplication_KeyDown(object sender, KeyEventArgs e)
        {
            HotkeyInTextBoxPressed((TextBox)sender, e);
        }


        private void textBoxHotkeySetAndActivateTemporaryFocusApplication_KeyDown(object sender, KeyEventArgs e)
        {
            HotkeyInTextBoxPressed((TextBox)sender, e);
        }


        private void textBoxHotkeyResetAllVolumes_KeyDown(object sender, KeyEventArgs e)
        {
            HotkeyInTextBoxPressed((TextBox)sender, e);
        }

        private void panelTrayIconColor_MouseClick(object sender, MouseEventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();

            if (colorDialog.ShowDialog() == DialogResult.OK)
                ((Panel)sender).BackColor = colorDialog.Color;
        }

        private void panelTrayIconColor_BackColorChanged(object sender, EventArgs e)
        {
            Panel panelTemp = (Panel)sender;

            UserSettings.setTrayIconColor(panelTemp.BackColor);
            SetTrayIcon(panelTemp.BackColor);
        }

        private void checkBoxAutostart_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            if (cb.Checked)
                EnableAutostart();
            else
                DisableAutostart();
        }


        private void checkBoxBalanceSystemSounds_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            UserSettings.setBalanceSystemSound(cb.Checked);
            UpdateApplicationList();
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
                foreach (HotkeyListElement hlePressed in _hotkeyList)
                {
                    if ((int)m.WParam == hlePressed.id)
                    {
                        // check if a hotkey text box is selected
                        foreach (HotkeyListElement hleSelected in _hotkeyList)
                        {
                            if (hleSelected.textBox.Focused)
                            {
                                // check if the pressed hotkey equals the currently selected text box
                                if (hlePressed.id == hleSelected.id)
                                    return;

                                // otherwise mark the text box field
                                HighlightHotkey(hlePressed);
                                return;
                            }
                        }

                        // no hotkey text box selected, we can execute the command
                        hlePressed.executeHotkey();
                        return;
                    }
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
