using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NLog;
using Sdl.Community.SdlPluginInstaller.Model;
using Sdl.Versioning;

namespace Sdl.Community.SdlPluginInstaller.Services
{
    public class PluginManagementService
    {
        private readonly Logger _logger;
        private bool _needsAdminRights;

        public PluginManagementService(Logger logger)
        {
            _logger = logger;
        }

        private readonly List<string> _pluginFolderLocations = new List<string>
        {
           Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
               Versions.PluginPackagePath),
            Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
               Versions.PluginPackagePath),
             Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
               Versions.PluginPackagePath)
        };

        public List<PluginPackageInfo> GetInstalledPlugins(StudioVersion studioVersion)
        {
            var installedPlugins = new List<PluginPackageInfo>();


            var pluginPath = _pluginFolderLocations.Select(
                pluginFolderLocation =>
                    string.Format(pluginFolderLocation, studioVersion.PluginSubPath));

            //check the folder exists before iterating on it.
            var ExistingPluginPath = new List<String>();

            foreach (var plugin in pluginPath)
            {
                if (Directory.Exists(plugin))
                {
                    ExistingPluginPath.Add(plugin);
                }
            }

            {
                foreach (var plugin in ExistingPluginPath.Select(path => Directory.GetFiles(path, "*.sdlplugin"))
                    .Where(plugin => plugin.Length != 0))
                {
                    installedPlugins.AddRange(GetPluginPackages(plugin));
                }
            }

            return installedPlugins;
        }

        public List<PluginPackageInfo> GetPluginPackages(string[] pluginPath)
        {
            var pluginList = new List<PluginPackageInfo>();
            foreach (var path in pluginPath)
            {
                try
                {
                    pluginList.Add(PluginPackageInfo.CreatePluginPackageInfo(path));
                }
                catch (UnauthorizedAccessException exception)
                {
                    _needsAdminRights = true;
                    _logger.Error(exception, "Admin rights are required");
                }
                catch (Exception ex)
                {
                    _logger.Error(ex, "Error at getting plugins packages ");
                }
            }
            return pluginList;
        }

        public bool NeedsAdminRights()
        {
            return _needsAdminRights;
        }
    }
}
