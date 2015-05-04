using System;
using System.IO;
using Sdl.Core.PluginFramework.PackageSupport;

namespace Sdl.Community.SdlPluginInstaller.Model
{
    public class PluginPackageInfo
    {
        public string Description { get; set; }
        public string PluginName { get; set; }
        public Version MinRequiredProductVersion { get; set; }
        public string Path { get; set; }

        private PluginPackageInfo()
        {
            
        }

        public static PluginPackageInfo CreatePluginPackageInfo(string pluginPackagePath)
        {
            var packageInfo = new PluginPackageInfo();
            using (var pluginPackage = new PluginPackage(pluginPackagePath, FileAccess.ReadWrite))
            {
                packageInfo.PluginName = pluginPackage.PackageManifest.PlugInName;
                packageInfo.Description = pluginPackage.PackageManifest.Description;
                packageInfo.Path = pluginPackage.FilePath;
                packageInfo.MinRequiredProductVersion = pluginPackage.PackageManifest.MinRequiredProductVersion;
            }
            return packageInfo;
        }
    }
}
