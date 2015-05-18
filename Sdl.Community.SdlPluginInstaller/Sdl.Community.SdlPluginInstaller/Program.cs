using System;
using System.IO;
using System.Windows.Forms;
using NLog;
using NLog.Config;
using NLog.Targets;

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
            InitializeLoggingConfiguration();

            if (args.Length == 0) return;

            Application.ThreadException += Application_ThreadException;
            Application.Run(new InstallerForm(args[0]));
        }

        private static void InitializeLoggingConfiguration()
        {
           var loggingConfiguration = new LoggingConfiguration();
            var fileTarget = new FileTarget();
            loggingConfiguration.AddTarget("file", fileTarget);
            fileTarget.CreateDirs = true;
            fileTarget.FileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                @"SDL Community\PluginInstaller\Log\community-installer.log");
            fileTarget.Layout = "${longdate}|${level:uppercase=true}|${logger}|${message}|${exception:format=ToString}";

            var rule = new LoggingRule("*", LogLevel.Trace, fileTarget);
            loggingConfiguration.LoggingRules.Add(rule);

            LogManager.Configuration = loggingConfiguration;

        }


        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            var logger = LogManager.GetLogger("log");

            logger.ErrorException("Unhandled exception", e.Exception);

        }
    }
}
