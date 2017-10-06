using System;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using CommandLine;
using NLog;
using NLog.Config;
using NLog.Targets;
using Sdl.Community.SdlPluginInstaller.Model;
using Sdl.Community.SdlPluginInstaller.Properties;

namespace Sdl.Community.SdlPluginInstaller
{
    static class Program
    {
        private static readonly DeployOptions Dpo = new DeployOptions();
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            WindowDPIAwarness.SetPerMonitorDpiAwareness(ProcessDpiAwareness.Process_DPI_Unaware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            InitializeLoggingConfiguration();

            var logger = LogManager.GetLogger("log");

            if (args.Length == 0)
            {
               Application.Run(new PluginForm(logger));
            }

            else
            {
                var parser = new Parser();

                if (parser.ParseArguments(args, Dpo))
                {
                    var processStartInfo = new ProcessStartInfo("Sdl.Community.SdlPluginInstaller.Cmd.exe",
                        string.Join(" ", args))
                    {UseShellExecute = false};

                    var process = new Process
                    {
                        StartInfo = processStartInfo,

                    };
                    process.Start();
                }
                else
                {
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

            
            }
            
                
              

            
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

            logger.Error(e.Exception, "Unhandled exception");

        }
    }
}
