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
                    MainForm mainForm = new MainForm();
                    Application.Run();
                }
            }
        }
    }
}
