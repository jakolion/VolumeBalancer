using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;
using System.Management;
using NAudio.CoreAudioApi;
using NAudio.CoreAudioApi.Interfaces;

namespace VolumeBalancer
{
    public partial class MainForm : Form, IAudioSessionEventsHandler
    {
        private List<AudioApp> _audioAppList = new List<AudioApp>();
        private bool _guiUpdateByEventIsRunning = false;
        private Thread _updateApplicationListThread;
        public bool updateApplicationListThreadAbort;

        const int HOTKEY_INCREASE_CHAT_APPLICATION_VOLUME = 1;
        const int HOTKEY_INCREASE_OTHER_APPLICATION_VOLUME = 2;
        const int HOTKEY_RESET_BALANCE = 3;
        const int HOTKEY_RESET_ALL_VOLUMES = 4;


        public MainForm()
        {
            InitializeComponent();
        }


        #region main functions

        // update the application list
        private void UpdateApplicationList()
        {
            // clear the list
            _audioAppList.Clear();
            //_audioAppList.TrimExcess();

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
                if (session.State != AudioSessionState.AudioSessionStateExpired && ProcessExists(session.GetProcessID))
                {
                    if (session.IsSystemSoundsSession)
                    {
                        _audioAppList.Add(new AudioApp(session, "System Sound"));
                    }
                    else
                    {
                        // get full path of process if possible
                        string applicationPath = GetProcessPath(session.GetProcessID);
                        if (applicationPath == "")
                            applicationPath = Process.GetProcessById((int)session.GetProcessID).ProcessName;

                        // add new audio app to list if not already existing
                        if (!AudioApplicationIsRunning(applicationPath))
                            _audioAppList.Add(new AudioApp(session, applicationPath));
                    }
                    session.RegisterEventClient(this);
                }
            }

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
        private void SetHotkeys()
        {
            SetHotkey(HOTKEY_INCREASE_CHAT_APPLICATION_VOLUME, UserSettings.getHotkeyIncreaseChatVolume());
            SetHotkeyTextBox(textBoxHotkeyIncreaseChatApplicationVolume, UserSettings.getHotkeyIncreaseChatVolume());

            SetHotkey(HOTKEY_INCREASE_OTHER_APPLICATION_VOLUME, UserSettings.getHotkeyIncreaseOtherApplicationVolume());
            SetHotkeyTextBox(textBoxHotkeyIncreaseOtherApplicationVolume, UserSettings.getHotkeyIncreaseOtherApplicationVolume());

            SetHotkey(HOTKEY_RESET_BALANCE, UserSettings.getHotkeyResetBalance());
            SetHotkeyTextBox(textBoxHotkeyResetBalance, UserSettings.getHotkeyResetBalance());

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
                if (hotkey.getModifierKeys() == Keys.Shift)
                    modifierId += MOD_SHIFT;

                if (hotkey.getModifierKeys() == Keys.Control)
                    modifierId += MOD_CONTROL;

                if (hotkey.getModifierKeys() == Keys.LWin || hotkey.getModifierKeys() == Keys.RWin)
                    modifierId += MOD_WIN;

                if (hotkey.getModifierKeys() == Keys.Alt)
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
                case HOTKEY_INCREASE_CHAT_APPLICATION_VOLUME:
                    UserSettings.setHotkeyIncreaseChatVolume(hotkey);
                    break;

                case HOTKEY_INCREASE_OTHER_APPLICATION_VOLUME:
                    UserSettings.setHotkeyIncreaseOtherApplicationVolume(hotkey);
                    break;

                case HOTKEY_RESET_BALANCE:
                    UserSettings.setHotkeyResetBalance(hotkey);
                    break;

                case HOTKEY_RESET_ALL_VOLUMES:
                    UserSettings.setHotkeyResetAllVolumes(hotkey);
                    break;
            }
        }


        // records an hotkey pressed on textbox
        private void HotkeyPressed(TextBox textBox, KeyEventArgs e, int hotkeyId)
        {
            if (e.KeyCode != Keys.Back && e.KeyCode != Keys.Delete)
            {
                // get modifier keys
                Keys modifierKeys = e.Modifiers;

                // remove modifier keys
                Keys pressedKey = e.KeyData ^ modifierKeys;

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
            else
            {
                // create a new hotkey object
                Hotkey hotkey = new Hotkey(Keys.None, Keys.None);

                // save the hotkey
                SaveHotkeyInUserSettings(hotkeyId, hotkey);

                // apply the new hotkey
                SetHotkey(hotkeyId, hotkey);

                // write the new hotkey to the textbox
                SetHotkeyTextBox(textBox, hotkey);
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
        private void GetApplicationVolumes(out float chatApplicationVolume, out float highestOtherApplicationVolume)
        {
            chatApplicationVolume = 0;
            highestOtherApplicationVolume = 0;

            // get highest volume of chat application and other application
            for (int i = 0; i < _audioAppList.Count; i++)
            {
                AudioApp app = _audioAppList[i];
                AudioSessionControl session = app.session;
                if (app.path == UserSettings.getChatApplication())
                {
                    chatApplicationVolume = session.SimpleAudioVolume.Volume;
                }
                else
                {
                    if (session.SimpleAudioVolume.Volume > highestOtherApplicationVolume)
                        highestOtherApplicationVolume = session.SimpleAudioVolume.Volume;
                }
            }

            // return if chat application isn't running
            if (!AudioApplicationIsRunning(UserSettings.getChatApplication())) return;

            // return if only the audio application is running
            if (AudioApplicationIsRunning(UserSettings.getChatApplication()) && _audioAppList.Count == 1) return;

            // set the loudest application (also chat) to 1
            if (chatApplicationVolume < 1 && highestOtherApplicationVolume < 1)
            {
                float multiplier = 0;
                if (chatApplicationVolume >= highestOtherApplicationVolume)
                {
                    multiplier = 1 + (100 / chatApplicationVolume * (1 - chatApplicationVolume) / 100);
                }
                else if (highestOtherApplicationVolume > chatApplicationVolume)
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


        // updates the balance slider
        private void UpdateBalanceSlider()
        {
            // get highest volume of chat application and other application
            float chatApplicationVolume = 0;
            float highestOtherApplicationVolume = 0;
            GetApplicationVolumes(out chatApplicationVolume, out highestOtherApplicationVolume);

            // check if chat application is running
            if (AudioApplicationIsRunning(UserSettings.getChatApplication()))
            {
                // check if only the chat aplication is running
                if (_audioAppList.Count == 1)
                {
                    // chat application is the only audio application running
                    // so the slider can only be moved to the right side
                    trackBarBalance.Value = trackBarBalance.Maximum - (int)Math.Round((trackBarBalance.Maximum / 2) * chatApplicationVolume);

                    // disable label chat
                    labelBalanceChatApplication.Enabled = false;
                }
                else
                {
                    // calc position of the slider
                    if (chatApplicationVolume > highestOtherApplicationVolume)
                    {
                        trackBarBalance.Value = (int)Math.Round((trackBarBalance.Maximum / 2) * highestOtherApplicationVolume);
                    }
                    else if (highestOtherApplicationVolume > chatApplicationVolume)
                    {
                        trackBarBalance.Value = trackBarBalance.Maximum - (int)Math.Round((trackBarBalance.Maximum / 2) * chatApplicationVolume);
                    }
                    else
                    {
                        trackBarBalance.Value = trackBarBalance.Maximum / 2;
                    }

                    // enable label chat
                    labelBalanceChatApplication.Enabled = true;
                }

                // enable label other
                labelBalanceOtherApplications.Enabled = true;

                // enable balance group
                groupBoxBalance.Enabled = true;
            }
            else
            {
                // check if a chat application is set and not empty
                if (UserSettings.getChatApplication() == "")
                {
                    // disable label other
                    labelBalanceOtherApplications.Enabled = false;

                    // disable label chat
                    labelBalanceChatApplication.Enabled = false;

                    // disable balance group and move slider to the center
                    groupBoxBalance.Enabled = false;
                    trackBarBalance.Value = trackBarBalance.Maximum / 2;
                }
                else
                {
                    // chat application is not running

                    // disable label other
                    labelBalanceOtherApplications.Enabled = false;

                    // enable label chat
                    labelBalanceChatApplication.Enabled = true;

                    // enable balance group
                    groupBoxBalance.Enabled = true;

                    // so the slider can only be moved to the left side
                    trackBarBalance.Value = (int)Math.Round((trackBarBalance.Maximum / 2) * highestOtherApplicationVolume);
                }
            }
        }


        // checks if a process is running
        private bool ProcessExists(uint processId)
        {
            try
            {
                var process = Process.GetProcessById((int)processId);
                return true;
            }
            catch (ArgumentException)
            {
                return false;
            }
        }


        // get path of process
        private string GetProcessPath(uint processId)
        {
            const uint PROCESS_QUERY_LIMITED_INFORMATION = 0x1000;
            IntPtr hProcess = OpenProcess(PROCESS_QUERY_LIMITED_INFORMATION, false, processId);
            if (hProcess != IntPtr.Zero)
            {
                try
                {
                    StringBuilder buffer = new StringBuilder(1024);
                    uint size = (uint)buffer.Capacity;
                    if (QueryFullProcessImageName(hProcess, 0, buffer, ref size))
                    {
                        return buffer.ToString();
                    }
                }
                finally
                {
                    CloseHandle(hProcess);
                }
            }
            return string.Empty;
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


        // increase chat volume
        private void IncreaseChatVolume()
        {
            // move slider to the left (to chat aplication)
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
        
        private void MainForm_Load(object sender, EventArgs e)
        {

            // read user settings
            UserSettings.readSettings();

            // set the chat application
            textBoxChatApplication.Text = UserSettings.getChatApplication();

            // pre-set the audio application drop down list
            UpdateDropDownListAudioApplications();

            // set the balance
            trackBarBalance.Value = (int)(trackBarBalance.Maximum / 100 * UserSettings.getBalancePosition());

            // set the hotkeys
            SetHotkeys();

            // start thread for polling audio applications
            _updateApplicationListThread = new Thread(UpdateApplicationListJob);
            _updateApplicationListThread.Start();

            // show the GUI if the chat application is not stored in the user settings
            if (UserSettings.getChatApplication() == "")
                Show();
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
                string newChatApplication = comboAudioApplications.SelectedItem.ToString();
                textBoxChatApplication.Text = newChatApplication;
                UserSettings.setChatApplication(newChatApplication);
                comboAudioApplications.SelectedIndex = 0;
                ResetBalance();
            }
        }


        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Applications (*.exe)|*.exe";
            ofd.FilterIndex = 1;
            ofd.Multiselect = false;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                textBoxChatApplication.Text = ofd.FileName;
                UserSettings.setChatApplication(ofd.FileName);
                ResetBalance();
            }
        }


        private void trackBarBalance_ValueChanged(object sender, EventArgs e)
        {
            // skip if chat application name isn't set
            if (UserSettings.getChatApplication() == "") return;

            // check if an event is updating the UI
            if (!_guiUpdateByEventIsRunning)
            {
                // get highest volume of chat application and other application
                float chatApplicationVolume = 0;
                float highestOtherApplicationVolume = 0;
                GetApplicationVolumes(out chatApplicationVolume, out highestOtherApplicationVolume);

                int center = trackBarBalance.Maximum / 2;
                int value = trackBarBalance.Value;
                float newChatApplicationVolume = 0;
                float newHighestOtherApplicationVolume = 0;

                // check position of slider
                if (value < center)
                {
                    // slider is on chat application side
                    newChatApplicationVolume = 1;
                    newHighestOtherApplicationVolume = (1f / center * value);
                }
                else if (value > center && AudioApplicationIsRunning(UserSettings.getChatApplication()))
                {
                    // slider is on other application side
                    newChatApplicationVolume = (1f / center * (trackBarBalance.Maximum - value));
                    newHighestOtherApplicationVolume = 1;
                }
                else
                {
                    // slider is centered
                    newChatApplicationVolume = 1;
                    newHighestOtherApplicationVolume = 1;
                }

                // calc multiplier for being able to keep the volume balance between all applications
                float multiplier = 100 / highestOtherApplicationVolume * newHighestOtherApplicationVolume / 100;

                // loop through audio applications and set session volume
                for (int i = 0; i < _audioAppList.Count; i++)
                {
                    AudioApp app = _audioAppList[i];
                    if (app.path == UserSettings.getChatApplication())
                    {
                        app.session.SimpleAudioVolume.Volume = newChatApplicationVolume;
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


        private void buttonIncreaseChatApplicationVolume_Click(object sender, EventArgs e)
        {
            // increase chat volume
            IncreaseChatVolume();
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


        private void textBoxShortcutIncreaseChatVolume_KeyDown(object sender, KeyEventArgs e)
        {
            HotkeyPressed((TextBox)sender, e, HOTKEY_INCREASE_CHAT_APPLICATION_VOLUME);
        }


        private void textBoxHotkeyIncreaseOtherApplicationsVolume_KeyDown(object sender, KeyEventArgs e)
        {
            HotkeyPressed((TextBox)sender, e, HOTKEY_INCREASE_OTHER_APPLICATION_VOLUME);
        }


        private void textBoxHotkeyResetBalance_KeyDown(object sender, KeyEventArgs e)
        {
            HotkeyPressed((TextBox)sender, e, HOTKEY_RESET_BALANCE);
        }


        private void textBoxHotkeyResetAllVolumes_KeyDown(object sender, KeyEventArgs e)
        {
            HotkeyPressed((TextBox)sender, e, HOTKEY_RESET_ALL_VOLUMES);
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
                if ((int)m.WParam == HOTKEY_INCREASE_CHAT_APPLICATION_VOLUME)
                    IncreaseChatVolume();

                else if ((int)m.WParam == HOTKEY_INCREASE_OTHER_APPLICATION_VOLUME)
                    IncreaseOtherApplicationVolume();

                else if ((int)m.WParam == HOTKEY_RESET_BALANCE)
                    ResetBalance();

                else if ((int)m.WParam == HOTKEY_RESET_ALL_VOLUMES)
                    ResetAllAudioApplicationVolume();
            }
            base.WndProc(ref m);
        }

        #endregion


        #region imports

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern IntPtr OpenProcess(uint processAccess, bool bInheritHandle, uint processId);

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        static extern bool QueryFullProcessImageName(IntPtr hProcess, uint dwFlags, [Out, MarshalAs(UnmanagedType.LPTStr)] StringBuilder lpExeName, ref uint lpdwSize);

        [DllImport("kernel32.dll", SetLastError = true)]
        //[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
        //[SuppressUnmanagedCodeSecurity]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool CloseHandle(IntPtr hObject);

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
