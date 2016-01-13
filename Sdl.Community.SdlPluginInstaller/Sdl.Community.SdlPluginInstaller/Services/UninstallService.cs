using System;
using System.IO;
using Sdl.Community.SdlPluginInstaller.Model;

namespace Sdl.Community.SdlPluginInstaller.Services
{
    public class UninstallService
    {
      
        public void UninstallPlugin(PluginPackageInfo pluginInfo)
        {

            if (File.Exists(pluginInfo.Path))
            {
                File.Delete(pluginInfo.Path); 
            }

            var path = pluginInfo.Path.Replace("Packages", "Unpacked");
            var folderPath = path.Remove(path.LastIndexOf(@"\", StringComparison.Ordinal));
            var pathCombined = Path.Combine(folderPath, pluginInfo.PluginName);
            if (Directory.Exists(pathCombined))
            {
                Directory.Delete(pathCombined,true);
            }
       
        }
    }
}
