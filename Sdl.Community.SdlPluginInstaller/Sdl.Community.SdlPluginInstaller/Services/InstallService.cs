using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using Sdl.Community.SdlPluginInstaller.Model;
using Sdl.Core.PluginFramework.PackageSupport;

namespace Sdl.Community.SdlPluginInstaller.Services
{
    public class InstallService
    {
        private readonly PluginPackageInfo _pluginPackageInfo;
        public List<StudioVersion> StudioVersions { get; set; }

        private readonly List<Environment.SpecialFolder> _pluginFolderLocations = new List<Environment.SpecialFolder>
        {
           Environment.SpecialFolder.ApplicationData,
           Environment.SpecialFolder.LocalApplicationData,
           Environment.SpecialFolder.CommonApplicationData
        };

        public InstallService(PluginPackageInfo pluginPackageInfo)
            : this(pluginPackageInfo, new List<StudioVersion>())
        {
            
        }
        public InstallService(PluginPackageInfo pluginPackageInfo,List<StudioVersion> studioVersions)
        {
            _pluginPackageInfo = pluginPackageInfo;
            StudioVersions = studioVersions;
        }

        public bool IsPluginInstalled()
        {
            return
                StudioVersions.Select(
                    studioVersion =>
                        IsPluginInstalledForVersion(studioVersion.ExecutableVersion.Major.ToString(),
                            _pluginPackageInfo.PluginName))
                    .Any(isPluginInstalled => isPluginInstalled);
        }

        public List<string> GetStudioProcesses()
        {
            var processes = Process.GetProcessesByName("SDLTradosStudio");

            return processes.Select(process => process.ProcessName).ToList();
        }

        public void RemoveInstalledPlugin()
        {
            foreach (var studioVersion in StudioVersions)
            {
                RemovePluginForStudioVersion(studioVersion.ExecutableVersion.Major.ToString(),
                   _pluginPackageInfo.PluginName);
            }
        }

        public void DeployPackage(Action<int> reportProgress)
        {
            var bytesRead = 0;
            const int bytesPerChunk = 1000;
            using (var fs = new FileStream(_pluginPackageInfo.Path, FileMode.Open, FileAccess.Read))
            {
                var totalLength = fs.Length*StudioVersions.Count;
                foreach (var studioVersion in StudioVersions)
                {
                    fs.Seek(0, SeekOrigin.Begin);
                    var br = new BinaryReader(fs);
                    {
                        var destination = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                            @"SDL\SDL Trados Studio", studioVersion.ExecutableVersion.Major.ToString(),
                            @"Plugins\Packages",
                            string.Format("{0}.sdlplugin", _pluginPackageInfo.PluginName));

                        using (var fsDest = new FileStream(destination, FileMode.Create))
                        {
                            var bw = new BinaryWriter(fsDest);

                            for (int i = 0; i < fs.Length; i += bytesPerChunk)
                            {
                                byte[] buffer = br.ReadBytes(bytesPerChunk);
                                bw.Write(buffer);
                                bytesRead += bytesPerChunk;
                                var progress = 0;
                                if (bytesRead > totalLength)
                                {
                                    progress = 100;
                                }
                                else
                                {
                                    progress = (int) (((double) bytesRead/totalLength)*100);
                                }
                                reportProgress(progress);
                            }
                        }
                    }
                }
            }
        }

        private bool IsPluginInstalledForVersion(string version, string pluginName)
        {
            return
                _pluginFolderLocations.Select(
                    pluginFolderLocation =>
                        Path.Combine(Environment.GetFolderPath(pluginFolderLocation), @"SDL\SDL Trados Studio", version,
                           @"Plugins\Packages", string.Format("{0}.sdlplugin", pluginName))).Any(File.Exists);
        }

       

        private void RemovePluginForStudioVersion(string version, string pluginName)
        {
            foreach (var pluginFolderLocation in _pluginFolderLocations)
            {
                var packagePluginPath = Path.Combine(Environment.GetFolderPath(pluginFolderLocation),
                    @"SDL\SDL Trados Studio", version,
                    @"Plugins\Packages", string.Format("{0}.sdlplugin", pluginName));
               var unpackedFolder = Path.Combine(Environment.GetFolderPath(pluginFolderLocation),
                    @"SDL\SDL Trados Studio", version,
                    @"Plugins\Unpacked",pluginName);
                if (Directory.Exists(unpackedFolder))
                {
                    Directory.Delete(unpackedFolder, true);
                }
                if (File.Exists(packagePluginPath))
                {
                    File.Delete(packagePluginPath);
                }
            }
        }
    }
}
