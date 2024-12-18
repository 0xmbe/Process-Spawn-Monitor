using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Process_Spawn_Monitor
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            if (Environment.OSVersion.Version.Major >= 6)    // Fix for DPI scaling
                SetProcessDPIAware();

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());
        }

        [System.Runtime.InteropServices.DllImport("user32.dll")]    // Fix for DPI scaling
        private static extern bool SetProcessDPIAware();    // Fix for DPI scaling
    }
}
