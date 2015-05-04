using System;
using System.Windows.Forms;

namespace Sdl.Community.SdlPluginInstaller
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (args.Length == 0) return;

            Application.Run(new InstallerForm(args[0]));
        }
    }
}
