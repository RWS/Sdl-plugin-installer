using System;
using System.IO;
using Sdl.Core.PluginFramework.PackageSupport;
using Ionic.Zip;

namespace Sdl.Community.SdlPluginInstaller.Model
{
    public class PluginPackageInfo
    {
        public string Description { get; set; }
        public string PluginName { get; set; }
        public Version MinRequiredProductVersion { get; set; }
        public Version MaxRequiredProductVersion { get; set; }
        public string Path { get; set; }

        private PluginPackageInfo()
        {
            
        }

        /// <summary>
        /// Uses DotNetZip library to return the maxversion from the manifest in the compressed sdlplugin file.
        /// </summary>
        /// <param name="pluginPackagePath">The path to the plugin file.</param>
        /// <returns>The version corresponding to the maxversion, or null if none is found.</returns>
        private static Version GetMaxVersion(string pluginPackagePath)
        {
            string manifestFilename = "pluginpackage.manifest.xml";
            string contents = null;

            using (MemoryStream ms = new MemoryStream())
            {
                using (ZipFile zip = new ZipFile(pluginPackagePath))
                {
                    foreach (ZipEntry e in zip)
                    {
                        if (e.FileName.Equals(manifestFilename))
                        {
                            e.Extract(ms);
                            ms.Position = 0;
                            using (StreamReader reader = new StreamReader(ms))
                            {
                                contents = reader.ReadToEnd();
                            }
                        }
                    }
                }
            }

            string result = null;
            System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
            xmlDoc.LoadXml(contents);
            try
            {
                result = xmlDoc["PluginPackage"]["RequiredProduct"].Attributes["maxversion"].Value;
            }
            catch { }

            if (result == null) return null;
            return new Version(result);
        }
        
        public static PluginPackageInfo CreatePluginPackageInfo(string pluginPackagePath)
        {
            var packageInfo = new PluginPackageInfo();
            using (var pluginPackage = new PluginPackage(pluginPackagePath, FileAccess.Read))
            {
                packageInfo.PluginName = pluginPackage.PackageManifest.PlugInName;
                packageInfo.Description = pluginPackage.PackageManifest.Description;
                packageInfo.Path = pluginPackage.FilePath;
                packageInfo.MinRequiredProductVersion = pluginPackage.PackageManifest.MinRequiredProductVersion;
            }
            
            packageInfo.MaxRequiredProductVersion = GetMaxVersion(pluginPackagePath);
            return packageInfo;
        }
    }
}
