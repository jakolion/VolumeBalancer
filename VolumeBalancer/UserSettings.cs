using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;

namespace VolumeBalancer
{
    static class UserSettings
    {
        private static string _chatApplication;
        private static uint _balancePosition;
        private static Hotkey _hotkeyIncreaseChatApplicationVolume;
        private static Hotkey _hotkeyIncreaseOtherApplicationVolume;
        private static Hotkey _hotkeyResetBalance;
        private static Hotkey _hotkeyResetAllVolumes;


        public static string getChatApplication()
        {
            return _chatApplication;
        }


        public static void setChatApplication(string chatApplication)
        {
            _chatApplication = chatApplication;
            Properties.Settings.Default.chatApplication = chatApplication;
            Properties.Settings.Default.Save();
        }


        public static uint getBalancePosition()
        {
            return _balancePosition;
        }


        public static void setBalancePosition(uint balancePosition)
        {
            _balancePosition = balancePosition;
            Properties.Settings.Default.balancePosition = balancePosition;
            Properties.Settings.Default.Save();
        }


        public static Hotkey getHotkeyIncreaseChatVolume()
        {
            return _hotkeyIncreaseChatApplicationVolume;
        }


        public static void setHotkeyIncreaseChatVolume(Hotkey hotkey)
        {
            _hotkeyIncreaseChatApplicationVolume = hotkey;
            Properties.Settings.Default.hotkeyIncreaseChatApplicationVolume = HotkeyToString(hotkey);
            Properties.Settings.Default.Save();
        }


        public static Hotkey getHotkeyIncreaseOtherApplicationVolume()
        {
            return _hotkeyIncreaseOtherApplicationVolume;
        }


        public static void setHotkeyIncreaseOtherApplicationVolume(Hotkey hotkey)
        {
            _hotkeyIncreaseOtherApplicationVolume = hotkey;
            Properties.Settings.Default.hotkeyIncreaseOtherApplicationVolume = HotkeyToString(hotkey);
            Properties.Settings.Default.Save();
        }


        public static Hotkey getHotkeyResetBalance()
        {
            return _hotkeyResetBalance;
        }


        public static void setHotkeyResetBalance(Hotkey hotkey)
        {
            _hotkeyResetBalance = hotkey;
            Properties.Settings.Default.hotkeyResetBalance = HotkeyToString(hotkey);
            Properties.Settings.Default.Save();
        }


        public static Hotkey getHotkeyResetAllVolumes()
        {
            return _hotkeyResetAllVolumes;
        }


        public static void setHotkeyResetAllVolumes(Hotkey hotkey)
        {
            _hotkeyResetAllVolumes = hotkey;
            Properties.Settings.Default.hotkeyResetAllVolumes = HotkeyToString(hotkey);
            Properties.Settings.Default.Save();
        }


        public static void readSettings()
        {
            _chatApplication = Properties.Settings.Default.chatApplication;
            _balancePosition = Properties.Settings.Default.balancePosition;
            _hotkeyIncreaseChatApplicationVolume = StringToHotkey(Properties.Settings.Default.hotkeyIncreaseChatApplicationVolume);
            _hotkeyIncreaseOtherApplicationVolume = StringToHotkey(Properties.Settings.Default.hotkeyIncreaseOtherApplicationVolume);
            _hotkeyResetBalance = StringToHotkey(Properties.Settings.Default.hotkeyResetBalance);
            _hotkeyResetAllVolumes = StringToHotkey(Properties.Settings.Default.hotkeyResetAllVolumes);
        }


        private static string HotkeyToString(Hotkey hotkey)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                new BinaryFormatter().Serialize(ms, hotkey);
                return Convert.ToBase64String(ms.ToArray());
            }
        }


        private static Hotkey StringToHotkey(string base64String)
        {
            if (base64String.Length == 0) return new Hotkey(Keys.None, Keys.None);

            byte[] bytes = Convert.FromBase64String(base64String);
            using (MemoryStream ms = new MemoryStream(bytes, 0, bytes.Length))
            {
                ms.Write(bytes, 0, bytes.Length);
                ms.Position = 0;
                return (Hotkey)new BinaryFormatter().Deserialize(ms);
            }
        }
    }
}
