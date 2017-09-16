using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.Serialization.Formatters.Binary;
using System.Drawing;

namespace VolumeBalancer
{
    static class UserSettings
    {
        private static string _mainFocusApplication;
        private static Color _trayIconColor;
        private static Color _formIconColor;
        private static int[] _customColors;
        private static bool _balanceSystemSound;
        private static Hotkey _hotkeyIncreaseFocusApplicationVolume;
        private static Hotkey _hotkeyIncreaseOtherApplicationVolume;
        private static Hotkey _hotkeyResetBalance;
        private static Hotkey _hotkeyActivateMainFocusApplication;
        private static Hotkey _hotkeyActivateTemporaryFocusApplication;
        private static Hotkey _hotkeyResetAllVolumes;

        private static readonly Color DEFAULT_COLOR = Color.FromArgb(0, 128, 255);


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


        public static Color getTrayIconColor()
        {
            return _trayIconColor;
        }
        public static void setTrayIconColor(Color trayIconColor)
        {
            _trayIconColor = trayIconColor;
            Properties.Settings.Default.trayIconColor = trayIconColor;
            Properties.Settings.Default.Save();
        }


        public static Color getFormIconColor()
        {
            return _formIconColor;
        }
        public static void setFormIconColor(Color formIconColor)
        {
            _formIconColor = formIconColor;
            Properties.Settings.Default.formIconColor = formIconColor;
            Properties.Settings.Default.Save();
        }


        public static int[] getCustomColors()
        {
            return _customColors;
        }
        public static void setCustomColors(int[] customColors)
        {
            _customColors = customColors;
            string converted = String.Join(",", _customColors.Select(i => i.ToString()).ToArray());
            Properties.Settings.Default.customColors = converted;
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
            _trayIconColor = Properties.Settings.Default.trayIconColor;
            if (_trayIconColor.A == 0)
                _trayIconColor = DEFAULT_COLOR;

            _formIconColor = Properties.Settings.Default.formIconColor;
            if (_formIconColor.A == 0)
                _formIconColor = DEFAULT_COLOR;

            if (_customColors == null)
                _customColors = new int[16];

            string[] customColors = Properties.Settings.Default.customColors.Split(',');
            if (customColors.Length != 16)
            {
                _customColors[0] = Helper.ColorToInt(DEFAULT_COLOR);
                _customColors[1] = Helper.ColorToInt(Color.Black);
                for (int i = 2; i < _customColors.Length; i++)
                    _customColors[i] = Helper.ColorToInt(Color.White);
            }
            else
            {
                for (int i = 0; i < _customColors.Length; i++)
                    _customColors[i] = Int32.Parse(customColors[i]);
            }

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
