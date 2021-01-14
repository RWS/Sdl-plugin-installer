using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using CommandLine;
using Sdl.Community.SdlPluginInstaller.Model;
using Sdl.Community.SdlPluginInstaller.Services;

namespace Sdl.Community.SdlPluginInstaller.Cmd
{
    public class Program
    {
        private static readonly DeployOptions Dpo = new DeployOptions();
        private static readonly List<PluginPackageInfo> PluginPackages = new List<PluginPackageInfo>();
        private static StudioVersionService _studioVersionService;
        private static List<StudioVersion> _installedStudioVersions;
        private static bool _shown40;
        private static bool _shown80;
        
        private static void Main(string[] args)
        {   
            var parser = new Parser();

            if (parser.ParseArguments(args, Dpo))
            {

                if (!string.IsNullOrEmpty(Dpo.SourcePlugin))
                {
                    var pluginPackageInfo = PluginPackageInfo.CreatePluginPackageInfo(Dpo.SourcePlugin);
                    PluginPackages.Add(pluginPackageInfo);
                }
                if (!string.IsNullOrEmpty(Dpo.FolderPath))
                {

                    var pluginsPath = Directory.GetFiles(Dpo.FolderPath, "*.sdlplugin", SearchOption.AllDirectories);
                    foreach (var plugin in pluginsPath)
                    {
                        var pluginInfo = PluginPackageInfo.CreatePluginPackageInfo(plugin);
                        PluginPackages.Add(pluginInfo);
                    }

                }
                _studioVersionService = new StudioVersionService();
                _installedStudioVersions = _studioVersionService.GetInstalledStudioVersions();

                var backgroundWorker = new BackgroundWorker
                {
                    WorkerSupportsCancellation = true,
                    WorkerReportsProgress = true
                };

                backgroundWorker.ProgressChanged += BackgroundWorker_ProgressChanged;
                backgroundWorker.DoWork += BackgroundWorker_DoWork;
                backgroundWorker.RunWorkerCompleted += BackgroundWorker_RunWorkerCompleted;

                if (!backgroundWorker.IsBusy)
                {

                    backgroundWorker.RunWorkerAsync();

                    while (backgroundWorker.IsBusy)
                    {
                      
                        Thread.Sleep(100);
                    }

                    if (Dpo.CloseConsole)
                    {
                        Console.WriteLine(@"Please press q to close the window.");
                        var key = Console.ReadKey();
                        if (key.KeyChar.Equals('q'))
                        {
                            Environment.Exit(0);
                        }
                    }

                }
            }
        }

        private static void BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
          
            var progress = e.ProgressPercentage;
            
            if (progress == 40 && !_shown40)
            {
                _shown40 = true;
                Console.WriteLine(@"40% installed.");
               
            }
            if (progress == 80 && !_shown80)
            {
                _shown80 = true;
                Console.WriteLine(@"80% installed.");

            }

        }

        private static void BackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            Console.WriteLine(@"Installation finished.");
        }

        private static void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            
            var backgroundWorker = sender as BackgroundWorker;
           

            foreach (var plugin in PluginPackages)
            {
               
                var notSupportedVersions=_studioVersionService.GetNotSupportedStudioVersions(plugin);
                var selectedVersion=string.Empty;

                switch (Dpo.Version)
                {
                    case SelectedVersion.Studio2:
                        selectedVersion = "Studio2";
                        break;
                    case SelectedVersion.Studio3:
                        selectedVersion = "Studio3";
                        break;
                    case SelectedVersion.Studio4:
                        selectedVersion = "Studio4";
                        break;
                    case SelectedVersion.Studio5:
                        selectedVersion = "Studio5";
                        break;
                    case SelectedVersion.Studio6:
                        selectedVersion = "Studio6";
                        break;
                }

                    var notSupported = notSupportedVersions.FirstOrDefault(v => v.Version == selectedVersion);
                    if (notSupported !=null)
                    {
                        Console.WriteLine(@"Selected studio version {0} is not compatible with the plugin version {1}",notSupported.PublicVersion,plugin.Version);
                       
                    }
                    else
                    {
                        Console.WriteLine(@"Deploy started for following plugin: {0}", plugin.PluginName);

                        _shown40 = false;
                        _shown80 = false;

                        var installService = new InstallService(plugin, _installedStudioVersions);
                        var deployLocation = new Environment.SpecialFolder();
                        switch (Dpo.DeployLocation)
                        {
                            case DeployDestination.Local:
                                deployLocation = Environment.SpecialFolder.LocalApplicationData;
                                break;
                            case DeployDestination.ProgramData:
                                deployLocation = Environment.SpecialFolder.CommonApplicationData;
                                break;
                            case DeployDestination.Roaming:

                                deployLocation = Environment.SpecialFolder.ApplicationData;
                                break;
                        }

                        if (backgroundWorker != null)
                            installService.DeployPackage(backgroundWorker.ReportProgress, deployLocation);
                    }
 
            }

        }
    }
}
