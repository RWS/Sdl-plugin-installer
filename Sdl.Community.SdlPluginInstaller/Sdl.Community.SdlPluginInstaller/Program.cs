using System;
using System.IO;
using System.Windows.Forms;
using NLog;
using NLog.Config;
using NLog.Targets;
using Sdl.Community.SdlPluginInstaller.Model;
using Sdl.Community.SdlPluginInstaller.Properties;

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
            var logger = LogManager.GetLogger("log");


            var pluginPackageInfo = PluginPackageInfo.CreatePluginPackageInfo(args[0]);

            if (string.IsNullOrEmpty(pluginPackageInfo.PluginName))
            {
                MessageBox.Show(
                    string.Format(
                        "There is no data in the package manifest. Please ask the plugin developer to add relevant information to the package manifest."),
                    Resources.Program_Main_Invalid_package, MessageBoxButtons.OK);
                return;
            }


            Application.ThreadException += Application_ThreadException;
            Application.Run(new InstallerForm(pluginPackageInfo, logger));
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
