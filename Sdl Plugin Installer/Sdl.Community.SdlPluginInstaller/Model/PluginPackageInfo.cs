using System;
using System.IO;
using Sdl.Core.PluginFramework.PackageSupport;
using System.IO.Packaging;
using System.Xml;

namespace Sdl.Community.SdlPluginInstaller.Model
{
    public class PluginPackageInfo
    {
        public string Description { get; set; }
        public string PluginName { get; set; }
        public Version MinRequiredProductVersion { get; set; }
        public Version MaxRequiredProductVersion { get; set; }
        public string Path { get; set; }
        public string Author { get; set; }
        public Version Version { get; set; }

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
            var manifestFilename = "pluginpackage.manifest.xml";
            string contents;
            using (var archive = Package.Open(pluginPackagePath, FileMode.Open))
            {
                var manifestUri = new Uri("/" + manifestFilename, UriKind.Relative);
                var manifest = archive.GetPart(manifestUri);
                using (var reader = new StreamReader(manifest.GetStream()))
                {
                    contents = reader.ReadToEnd();
                }
            }
            string result = null;
            System.Xml.XmlDocument xmlDoc = new System.Xml.XmlDocument();
            xmlDoc.LoadXml(contents);
            try
            {
                foreach (XmlAttribute attribute in xmlDoc["PluginPackage"]["RequiredProduct"].Attributes)
                {
                    if (attribute.Name.Equals("maxversion"))
                    {
                        result = attribute.Value;
                    }
                }
            }
            catch
            {
                // ignored
            }

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
                packageInfo.MaxRequiredProductVersion = pluginPackage.PackageManifest.MaxRequiredProductVersion;
                packageInfo.Author = pluginPackage.PackageManifest.Author;
                packageInfo.Version = pluginPackage.PackageManifest.Version;
            }

            packageInfo.MaxRequiredProductVersion = GetMaxVersion(pluginPackagePath);
            return packageInfo;
        }
    }
}
