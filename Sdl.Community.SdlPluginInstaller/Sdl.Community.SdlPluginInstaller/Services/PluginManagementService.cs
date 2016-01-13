using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Sdl.Community.SdlPluginInstaller.Model;

namespace Sdl.Community.SdlPluginInstaller.Services
{
    public class PluginManagementService
    {
        private readonly List<string> _pluginFolderLocations = new List<string>
        {
           Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
               @"SDL\SDL Trados Studio\{0}\Plugins\Packages"),
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
               @"SDL\SDL Trados Studio\{0}\Plugins\Packages"),
             Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
               @"SDL\SDL Trados Studio\{0}\Plugins\Packages")
        };
     
        public List<PluginPackageInfo> GetInstalledPlugins(StudioVersion studioVersion)
        {
            var installedPlugins = new List<PluginPackageInfo>();


            var pluginPath = _pluginFolderLocations.Select(
                pluginFolderLocation =>
                    string.Format(pluginFolderLocation, studioVersion.ExecutableVersion.Major));

            foreach (var plugin in pluginPath.Select(path => Directory.GetFiles(path, "*.sdlplugin"))
                .Where(plugin => plugin.Length != 0))
            {
                installedPlugins.AddRange(GetPluginPackages(plugin));
            }

            return installedPlugins;
        }

        public List<PluginPackageInfo> GetPluginPackages(string[] pluginPath)
        {
            return pluginPath.Select(PluginPackageInfo.CreatePluginPackageInfo).ToList();
        }
    }
}
