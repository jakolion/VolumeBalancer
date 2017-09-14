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
        private static string _mainFocusApplication;
        private static string _trayIcon;
        private static bool _balanceSystemSound;
        private static Hotkey _hotkeyIncreaseFocusApplicationVolume;
        private static Hotkey _hotkeyIncreaseOtherApplicationVolume;
        private static Hotkey _hotkeyResetBalance;
        private static Hotkey _hotkeyActivateMainFocusApplication;
        private static Hotkey _hotkeyActivateTemporaryFocusApplication;
        private static Hotkey _hotkeyResetAllVolumes;


        public static string getMainFocusApplication()
        {
            return _mainFocusApplication;
        }
        public static void setMainFocusApplication(string mainFocusApplication)
        {
            _mainFocusApplication = mainFocusApplication;
            Properties.Settings.Default.mainFocusApplication = mainFocusApplication;
            Properties.Settings.Default.Save();
        }


        public static string getTrayIcon()
        {
            return _trayIcon;
        }
        public static void setTrayIcon(string trayIcon)
        {
            _trayIcon = trayIcon;
            Properties.Settings.Default.trayIcon = trayIcon;
            Properties.Settings.Default.Save();
        }


        public static bool getBalanceSystemSound()
        {
            return _balanceSystemSound;
        }
        public static void setBalanceSystemSound(bool balanceSystemSound)
        {
            _balanceSystemSound = balanceSystemSound;
            Properties.Settings.Default.balanceSystemSound = balanceSystemSound;
            Properties.Settings.Default.Save();
        }


        public static Hotkey getHotkeyIncreaseFocusApplicationVolume()
        {
            return _hotkeyIncreaseFocusApplicationVolume;
        }
        public static void setHotkeyIncreaseFocusApplicationVolume(Hotkey hotkey)
        {
            _hotkeyIncreaseFocusApplicationVolume = hotkey;
            Properties.Settings.Default.hotkeyIncreaseFocusApplicationVolume = HotkeyToString(hotkey);
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


        public static Hotkey getHotkeyActivateMainFocusApplication()
        {
            return _hotkeyActivateMainFocusApplication;
        }
        public static void setHotkeyActivateMainFocusApplication(Hotkey hotkey)
        {
            _hotkeyActivateMainFocusApplication = hotkey;
            Properties.Settings.Default.hotkeyActivateMainFocusApplication = HotkeyToString(hotkey);
            Properties.Settings.Default.Save();
        }


        public static Hotkey getHotkeySetAndActivateTemporaryFocusApplication()
        {
            return _hotkeyActivateTemporaryFocusApplication;
        }
        public static void setHotkeySetAndActivateTemporaryFocusApplication(Hotkey hotkey)
        {
            _hotkeyActivateTemporaryFocusApplication = hotkey;
            Properties.Settings.Default.hotkeyActivateTemporaryFocusApplication = HotkeyToString(hotkey);
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
            _mainFocusApplication = Properties.Settings.Default.mainFocusApplication;
            _trayIcon = Properties.Settings.Default.trayIcon;
            _balanceSystemSound = Properties.Settings.Default.balanceSystemSound;
            _hotkeyIncreaseFocusApplicationVolume = StringToHotkey(Properties.Settings.Default.hotkeyIncreaseFocusApplicationVolume);
            _hotkeyIncreaseOtherApplicationVolume = StringToHotkey(Properties.Settings.Default.hotkeyIncreaseOtherApplicationVolume);
            _hotkeyResetBalance = StringToHotkey(Properties.Settings.Default.hotkeyResetBalance);
            _hotkeyActivateMainFocusApplication = StringToHotkey(Properties.Settings.Default.hotkeyActivateMainFocusApplication);
            _hotkeyActivateTemporaryFocusApplication = StringToHotkey(Properties.Settings.Default.hotkeyActivateTemporaryFocusApplication);
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
