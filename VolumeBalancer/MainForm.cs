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
        public Thread _updateApplicationListThread;


        public MainForm()
        {
            InitializeComponent();

            // read user settings
            UserSettings.readSettings();
            textBoxChatApplication.Text = UserSettings.getChatApplication();
            trackBarBalance.Value = (int)(trackBarBalance.Maximum / 100 * UserSettings.getBalancePosition());

            // start thread for polling audio applications
            _updateApplicationListThread = new Thread(UpdateApplicationListJob);
            _updateApplicationListThread.Start();

            Show();
        }


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
                        if (!audioApplicationIsRunning(applicationPath))
                            _audioAppList.Add(new AudioApp(session, applicationPath));
                    }
                    session.RegisterEventClient(this);
                }
            }


            // put the applications to the combo box and select the first item
            // prevent the update if the combo box is dropped down
            if (!comboAudioApplications.DroppedDown)
            {
                comboAudioApplications.Items.Clear();
                //comboAudioApplications.Items.Add("Select a running audio application");
                comboAudioApplications.Sorted = true;
                comboAudioApplications.Items.AddRange(_audioAppList.ToArray());
                comboAudioApplications.Sorted = false;
                comboAudioApplications.Items.Insert(0, "Select a running audio application");
                if (comboAudioApplications.Items.Count > 0)
                    comboAudioApplications.SelectedIndex = 0;
            }

            // update GUI
            UpdateGui();
        }


        // returns true if the application path can be found
        // in the current audio application list
        bool audioApplicationIsRunning(string applicationPath)
        {
            return _audioAppList.FirstOrDefault(x => x.ToString() == applicationPath) != null;
        }


        // get application volumes
        // sets loudest application to 1
        void GetApplicationVolumes(out float chatApplicationVolume, out float highestOtherApplicationVolume)
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
            if (!audioApplicationIsRunning(UserSettings.getChatApplication())) return;

            // return if only the audio application is running
            if (audioApplicationIsRunning(UserSettings.getChatApplication()) && _audioAppList.Count == 1) return;

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


        // updates the gui
        void UpdateGui()
        {
            // get highest volume of chat application and other application
            float chatApplicationVolume = 0;
            float highestOtherApplicationVolume = 0;
            GetApplicationVolumes(out chatApplicationVolume, out highestOtherApplicationVolume);

            // check if chat application is running
            if (audioApplicationIsRunning(UserSettings.getChatApplication()))
            {
                // check if only the chat aplication is running
                if (_audioAppList.Count == 1)
                {
                    // chat application is the only audio application running
                    // so the slider can only be moved to the right side
                    trackBarBalance.Value = trackBarBalance.Maximum - (int)Math.Round((trackBarBalance.Maximum / 2) * chatApplicationVolume);

                    // disable label chat
                    labelChat.Enabled = false;
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
                    labelChat.Enabled = true;
                }

                // enable label other
                labelOther.Enabled = true;

                // enable balance group
                groupBoxBalance.Enabled = true;
            }
            else
            {
                // check if a chat application is set and not empty
                if (UserSettings.getChatApplication() == "")
                {
                    // disable label other
                    labelOther.Enabled = false;

                    // disable label chat
                    labelChat.Enabled = false;

                    // disable balance group and move slider to the center
                    groupBoxBalance.Enabled = false;
                    trackBarBalance.Value = trackBarBalance.Maximum / 2;
                }
                else
                {
                    // chat application is not running

                    // disable label other
                    labelOther.Enabled = false;

                    // enable label chat
                    labelChat.Enabled = true;

                    // enable balance group
                    groupBoxBalance.Enabled = true;

                    // so the slider can only be moved to the left side
                    trackBarBalance.Value = (int)Math.Round((trackBarBalance.Maximum / 2) * highestOtherApplicationVolume);
                }
            }
        }


        // checks if a process is running
        bool ProcessExists(uint processId)
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
        string GetProcessPath(uint processId)
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
        void ResetBalance()
        {
            // store old track bar value
            int oldValue = trackBarBalance.Value;

            // set balance slider to the center
            trackBarBalance.Value = trackBarBalance.Maximum / 2;

            // force reset of balance if value hasn't changed
            if (oldValue == trackBarBalance.Value)
                trackBarBalance_ValueChanged(null, null);
        }


        // poll for new audio applications
        // because events are not working on every pc
        void UpdateApplicationListJob()
        {
            while (true)
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
                System.Threading.Thread.Sleep(2000);
            }
        }


        #region GUI events


        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            // hide window instead of closing it
            Hide();
            e.Cancel = true;
        }


        private void comboAudioApplications_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboAudioApplications.SelectedIndex > 0)
            {
                string newChatApplication = comboAudioApplications.SelectedItem.ToString();
                textBoxChatApplication.Text = newChatApplication;
                UserSettings.setChatApplication(newChatApplication);
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
                float newChatApplicationVolume = 0;
                float newHighestOtherApplicationVolume = 0;

                // check position of slider
                if (trackBarBalance.Value < center)
                {
                    // slider is on chat application side
                    newChatApplicationVolume = 1;
                    newHighestOtherApplicationVolume = (1f / center * trackBarBalance.Value);
                }
                else if (trackBarBalance.Value > center && audioApplicationIsRunning(UserSettings.getChatApplication()))
                {
                    // slider is on other application side
                    newChatApplicationVolume = (1f / center * (trackBarBalance.Maximum - trackBarBalance.Value));
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


        private void buttonReset_Click(object sender, EventArgs e)
        {
            // reset balance
            ResetBalance();
        }


        private void buttonIncreaseOther_Click(object sender, EventArgs e)
        {
            // move slider to the right (to other aplications)
            if (trackBarBalance.Value < trackBarBalance.Maximum)
                trackBarBalance.Value++;
        }


        private void buttonIncreaseChat_Click(object sender, EventArgs e)
        {
            // move slider to the left (to chat aplication)
            if (trackBarBalance.Value > trackBarBalance.Minimum)
                trackBarBalance.Value--;
        }


        #endregion


        #region events

        public void OnVolumeChanged(float volume, bool isMuted)
        {
            try
            {
                this.BeginInvoke(new Action(delegate ()
                {
                    _guiUpdateByEventIsRunning = true;
                    UpdateGui();
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

        #endregion


        // class for storing a session and it's readable name
        private class AudioApp
        {
            public AudioSessionControl session { get; }
            public string path { get; }

            public AudioApp(AudioSessionControl session, string path)
            {
                this.session = session;
                this.path = path;
            }

            public override string ToString()
            {
                return path;
            }
        }


        // class for storing user settings
        private static class UserSettings
        {
            private static string _chatApplication;
            private static uint _balancePosition;

            public static string getChatApplication()
            {
                return _chatApplication;
            }

            public static void setChatApplication(string chatApplication)
            {
                _chatApplication = chatApplication;
                saveSettings();
            }

            public static uint getBalancePosition()
            {
                return _balancePosition;
            }

            public static void setBalancePosition(uint balancePosition)
            {
                _balancePosition = balancePosition;
                saveSettings();
            }

            public static void readSettings()
            {
                _chatApplication = Properties.Settings.Default.chatApplication;
                _balancePosition = Properties.Settings.Default.balancePosition;
            }

            private static void saveSettings()
            {
                Properties.Settings.Default.chatApplication = _chatApplication;
                Properties.Settings.Default.balancePosition = _balancePosition;
                Properties.Settings.Default.Save();
            }
        }
    }

}
