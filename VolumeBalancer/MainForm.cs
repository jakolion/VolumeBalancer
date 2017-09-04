﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Management;
using NAudio.CoreAudioApi;
using NAudio.CoreAudioApi.Interfaces;

namespace VolumeBalancer
{
    public partial class MainForm : Form, IAudioSessionEventsHandler
    {
        private MMDevice _device;
        private List<AudioApp> _audioAppList = new List<AudioApp>();
        private bool _guiUpdateByEventIsRunning = false;

        public MainForm()
        {
            InitializeComponent();

            // get default audio endpoint
            MMDeviceEnumerator deviceEnumerator = new MMDeviceEnumerator();
            _device = deviceEnumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
            
            // load the application list for the first time
            UpdateApplicationList();
        }

        private void UpdateApplicationList()
        {
            // clear the list and combo box
            _audioAppList.Clear();
            comboChatApplication.Items.Clear();

            // get current sessions
            SessionCollection sessions = _device.AudioSessionManager.Sessions;

            // return if no sessions are available
            if (sessions == null) return;

            // loop through sessions
            for (int i = 0; i < sessions.Count; i++)
            {
                AudioSessionControl session = sessions[i];
                if (ProcessExists(session.GetProcessID)) // is this necessary?
                {
                    if (session.IsSystemSoundsSession)
                    {
                        _audioAppList.Add(new AudioApp(session, "System Sound"));
                    }
                    else
                    {
                        string applicationName = GetProcessPath(Process.GetProcessById((int)session.GetProcessID));
                        if (applicationName == "")
                            applicationName = Process.GetProcessById((int)session.GetProcessID).ProcessName;

                        _audioAppList.Add(new AudioApp(session, applicationName));
                    }
                    session.RegisterEventClient(this);
                }
            }

            // put the applications to the combo box and select the first item
            comboChatApplication.Items.AddRange(_audioAppList.ToArray());
            if (comboChatApplication.Items.Count > 0)
                comboChatApplication.SelectedIndex = 0;
        }

        // get application volumes
        // sets loudest application to 1
        void GetApplicationVolumes(out float chatApplicationVolume, out float highestOtherApplicationVolume)
        {
            // get highest volume of chat application and other application
            chatApplicationVolume = 0;
            highestOtherApplicationVolume = 0;
            AudioApp selectedChatApplication = (AudioApp)comboChatApplication.SelectedItem;
            for (int i = 0; i < _audioAppList.Count; i++)
            {
                AudioApp app = _audioAppList[i];
                AudioSessionControl session = app._session;
                if (app._name == selectedChatApplication._name)
                {
                    chatApplicationVolume = session.SimpleAudioVolume.Volume;
                }
                else
                {
                    if (session.SimpleAudioVolume.Volume > highestOtherApplicationVolume)
                        highestOtherApplicationVolume = session.SimpleAudioVolume.Volume;
                }
            }

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
                    AudioSessionControl session = app._session;
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
        }

        // checks if a process is running
        // (is this check necessary?)
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
        public string GetProcessPath(Process process)
        {
            try
            {
                return process.MainModule.FileName;
            }
            catch
            {
                string query = "SELECT ExecutablePath FROM Win32_Process WHERE ProcessId = " + process.Id;
                ManagementObjectSearcher searcher = new ManagementObjectSearcher(query);
                foreach (ManagementObject item in searcher.Get())
                {
                    if (item["ExecutablePath"] != null)
                    {
                        string path = item["ExecutablePath"].ToString();
                        return path;
                    }
                }
            }
            return "";
        }

        // reset all sessions
        void ResetAllSessions()
        {
            // store old track bar value
            int oldValue = trackBarBalance.Value;

            // set balance slider to the center
            trackBarBalance.Value = trackBarBalance.Maximum / 2;

            // force reset of balance if value hasn't changed
            if (oldValue == trackBarBalance.Value)
                trackBarBalance_ValueChanged(null, null);
        }



        #region GUI events

        private void buttonReset_Click(object sender, EventArgs e)
        {
            // reset
            ResetAllSessions();
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

        private void trackBarBalance_ValueChanged(object sender, EventArgs e)
        {
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
                else if (trackBarBalance.Value > center)
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
                AudioApp selectedChatApplication = (AudioApp)comboChatApplication.SelectedItem;
                for (int i = 0; i < _audioAppList.Count; i++)
                {
                    AudioApp app = _audioAppList[i];
                    if (app._name == selectedChatApplication._name)
                    {
                        app._session.SimpleAudioVolume.Volume = newChatApplicationVolume;
                    }
                    else
                    {
                        // if highestOtherApplicationVolume is 0, multiplier is infinity
                        if (highestOtherApplicationVolume == 0)
                            app._session.SimpleAudioVolume.Volume = newHighestOtherApplicationVolume;
                        else
                            app._session.SimpleAudioVolume.Volume *= multiplier;
                    }

                }
            }
        }

        private void comboChatApplication_SelectedIndexChanged(object sender, EventArgs e)
        {
            ResetAllSessions();
        }

        #endregion



        #region events

        public void OnVolumeChanged(float volume, bool isMuted)
        {
            try
            {
                this.Invoke(new Action(delegate ()
                {
                    _guiUpdateByEventIsRunning = true;
                    UpdateGui();
                    _guiUpdateByEventIsRunning = false;
                }));
            }
            catch
            {
            }
        }

        public void OnDisplayNameChanged(string displayName)
        {
        }

        public void OnIconPathChanged(string iconPath)
        {
        }

        public void OnChannelVolumeChanged(uint channelCount, IntPtr newVolumes, uint channelIndex)
        {
        }

        public void OnGroupingParamChanged(ref Guid groupingId)
        {
        }

        public void OnStateChanged(AudioSessionState state)
        {
        }

        public void OnSessionDisconnected(AudioSessionDisconnectReason disconnectReason)
        {
        }

        #endregion
    }

    // class for storing a session and it's readable name
    public class AudioApp
    {
        public AudioSessionControl _session { get; }
        public string _name { get; }

        public AudioApp(AudioSessionControl session, string name)
        {
            _session = session;
            _name = name;
        }

        public override string ToString()
        {
            return _name;
        }
    }
}
