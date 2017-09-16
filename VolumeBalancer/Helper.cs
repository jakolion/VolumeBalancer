using System;
using Microsoft.Win32;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Drawing;

namespace VolumeBalancer
{
    static class Helper
    {
        // checks if a process is running
        static public bool ProcessExists(uint processId)
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


        // change non-transparent pixels of bitmap to color
        static public Icon SetIconColor(Bitmap bmpTemp, Color targetColor)
        {
            for (int i = 0; i < bmpTemp.Width; i++)
            {
                for (int j = 0; j < bmpTemp.Height; j++)
                {
                    if (bmpTemp.GetPixel(i, j).A > 10)
                    {
                        bmpTemp.SetPixel(i, j, targetColor);
                    }
                }
            }

            // icon created by GetHicon() must be destroyed due to handle leakage!
            Icon tmpIcon = Icon.FromHandle(bmpTemp.GetHicon());
            Icon returnIcon = tmpIcon.Clone() as Icon;
            DestroyIcon(tmpIcon.Handle); 

            return returnIcon;
        }


        // convert a color to int for custom colors
        static public int ColorToInt(Color color)
        {
            // ToArgb() byte order:    AARRGGBB
            // CustomColor byte order: RRGGBB00
            return (color.R) | (color.G << 8) | (color.B << 16);
        }


        // get path of process
        static public string GetProcessPath(uint processId)
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

        
        // get topmost process id
        static public uint GetTopmostProcessId()
        {
            uint pid;
            IntPtr hwnd = GetForegroundWindow();
            GetWindowThreadProcessId(hwnd, out pid);
            return pid;
        }


        // read from registry
        static public string RegistryReadCurrentUser(string path, string key)
        {
            try
            {
                string value = null;
                using (RegistryKey regKey = Registry.CurrentUser.OpenSubKey(path))
                {
                    if (regKey == null)
                        return null;

                    value = (string)regKey.GetValue(key, null);
                }
                return value;
            }
            catch
            {
                return null;
            }
        }


        // write to registry
        static public bool RegistryWriteCurrentUser(string path, string key, string value)
        {
            try
            {
                using (RegistryKey regKey = Registry.CurrentUser.OpenSubKey(path, true))
                {
                    if (regKey == null)
                        return false;

                    regKey.SetValue(key, value, RegistryValueKind.String);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }


        // delete registry key
        static public bool RegistryDeleteCurrentUser(string path, string key)
        {
            try
            {
                using (RegistryKey regKey = Registry.CurrentUser.OpenSubKey(path, true))
                {
                    if (regKey == null)
                        return false;

                    regKey.DeleteValue(key);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }


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
        public static extern IntPtr GetWindowThreadProcessId(IntPtr hWnd, out uint ProcessId);

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();
        
        [DllImport("user32.dll", CharSet = CharSet.None, ExactSpelling = false)]
        private static extern bool DestroyIcon(IntPtr handle);

        #endregion
    }
}
