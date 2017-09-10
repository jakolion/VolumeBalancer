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
        private static Hotkey _hotkeyIncreaseChatVolume;

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
            return _hotkeyIncreaseChatVolume;
        }

        public static void setHotkeyIncreaseChatVolume(Hotkey hotkey)
        {
            _hotkeyIncreaseChatVolume = hotkey;
            Properties.Settings.Default.hotkeyIncreaseChatVolume = HotkeyToString(hotkey);
            Properties.Settings.Default.Save();
        }

        public static void readSettings()
        {
            _chatApplication = Properties.Settings.Default.chatApplication;
            _balancePosition = Properties.Settings.Default.balancePosition;
            _hotkeyIncreaseChatVolume = StringToHotkey(Properties.Settings.Default.hotkeyIncreaseChatVolume);
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
