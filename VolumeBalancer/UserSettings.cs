using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolumeBalancer
{
    static class UserSettings
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
