using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace VolumeBalancer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // single-instance check
            bool newMutexCreated = true;
            using (Mutex mutex = new Mutex(true, "VolumeBalancer", out newMutexCreated))
            {
                if (newMutexCreated)
                {
                    // start application
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    TrayApp trayApp = new TrayApp();
                    Application.Run();
                }
            }
        }
    }

    class TrayApp : MainForm
    {
        private NotifyIcon trayIcon;
        private ContextMenu trayMenu;

        public TrayApp()
        {
            // create a tray menu
            trayMenu = new ContextMenu();
            MenuItem version = new MenuItem("Version: " + Application.ProductVersion);
            version.Enabled = false;
            trayMenu.MenuItems.Add(version);
            trayMenu.MenuItems.Add("Configuration ...", OpenGui);
            trayMenu.MenuItems.Add("-");
            trayMenu.MenuItems.Add("Exit", OnExit);

            // create a tray icon
            trayIcon = new NotifyIcon();
            trayIcon.Text = Application.ProductName;
            trayIcon.Icon = Properties.Resources.iconWhite;

            // add tray menu to icon
            trayIcon.ContextMenu = trayMenu;
            trayIcon.Visible = true;
            trayIcon.MouseClick += OnMouseClick;
            trayIcon.MouseDoubleClick += OnMouseClick;
        }

        private void OnMouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
                OpenGui(sender, e);
        }

        private void OpenGui(object sender, EventArgs e)
        {
            // show form and bring to front
            Show();
            Activate();
        }

        private void OnExit(object sender, EventArgs e)
        {
            // exit
            updateApplicationListThreadAbort = true;
            Dispose(true);
            Application.Exit();
        }

        protected override void Dispose(bool isDisposing)
        {
            if (isDisposing)
            {
                // release the icon resource
                trayIcon.Dispose();
            }
            base.Dispose(isDisposing);
        }
    }
}
