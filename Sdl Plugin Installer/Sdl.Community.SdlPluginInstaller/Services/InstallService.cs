using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Sdl.Community.SdlPluginInstaller.Model;
using Sdl.Versioning;

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
                        IsPluginInstalledForVersion(studioVersion, _pluginPackageInfo.PluginName))
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
                RemovePluginForStudioVersion(studioVersion, _pluginPackageInfo.PluginName);
            }
        }

        public void DeployPackage(Action<int> reportProgress, Environment.SpecialFolder folder)
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
                        var pluginfolder =
                               Path.Combine(Environment.GetFolderPath(folder), string.Format((Versions.PluginPackagePath), studioVersion.PluginSubPath));
                        var path =
                               Path.Combine(pluginfolder, string.Format("{0}.sdlplugin", _pluginPackageInfo.PluginName));

                        
                        if (!Directory.Exists(pluginfolder))
                        {
                            Directory.CreateDirectory(pluginfolder);
                        }

                        using (var fsDest = new FileStream(path, FileMode.Create))
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

        private bool IsPluginInstalledForVersion(StudioVersion studioVersion, string pluginName)
        {
            return
                _pluginFolderLocations.Select(
                    pluginFolderLocation =>
                        Path.Combine(Environment.GetFolderPath(pluginFolderLocation),
                        string.Format(Versions.PluginPackagePath, studioVersion.PluginSubPath),
                        string.Format("{0}.sdlplugin", pluginName))).Any(File.Exists);
        }

       

        private void RemovePluginForStudioVersion(StudioVersion studioVersion, string pluginName)
        {
            foreach (var pluginFolderLocation in _pluginFolderLocations)
            {
                var packagePluginPath = Path.Combine(
                    Environment.GetFolderPath(pluginFolderLocation),
                    string.Format(Versions.PluginPackagePath, studioVersion.PluginSubPath),
                    string.Format("{0}.sdlplugin", pluginName));

                var unpackedFolder = Path.Combine(
                    Environment.GetFolderPath(pluginFolderLocation),
                    string.Format(Versions.PluginUnpackPath, studioVersion.PluginSubPath),
                    pluginName);

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
