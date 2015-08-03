using System;
using System.IO;
using Sdl.Core.PluginFramework.PackageSupport;
using System.IO.Packaging;

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
        /// Uses System.IO.Packaging ZipPackage to return the maxversion from the manifest in the compressed sdlplugin file.
        /// </summary>
        /// <param name="pluginPackagePath">The path to the plugin file.</param>
        /// <returns>The version corresponding to the maxversion, or null if none is found.</returns>
        private static Version GetMaxVersion(string pluginPackagePath)
        {
            string manifestFilename = "pluginpackage.manifest.xml";
            string contents = null;
            Package archive = ZipPackage.Open(pluginPackagePath);
            Uri manifestUri = new Uri("/" + manifestFilename, UriKind.Relative);
            PackagePart manifest = archive.GetPart(manifestUri);
            using(StreamReader reader = new StreamReader(manifest.GetStream()))
            {
                contents = reader.ReadToEnd();
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
