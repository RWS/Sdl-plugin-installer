using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Sdl.Community.SdlPluginInstaller.Model;
using Microsoft.Win32;
using Sdl.Core.PluginFramework.PackageSupport;

namespace Sdl.Community.SdlPluginInstaller.Services
{
    public class StudioVersionService
    {
        private const string InstallLocation64Bit =@"SOFTWARE\Wow6432Node\SDL\";
        private const string InstallLocation32Bit = @"SOFTWARE\SDL";
        private const string CentrallProcessor = @"HARDWARE\DESCRIPTION\System\CentralProcessor\0";
        private readonly PluginPackageInfo _pluginPackage;

        private readonly Dictionary<string, string> _supportedStudioVersions = new Dictionary<string, string>
        {
            {"Studio2", "SDL Studio 2011"},
            {"Studio3", "SDL Studio 2014"},
            {"Studio4", "SDL Studio 2015"}
        };
        private readonly List<StudioVersion> _installedStudioVersions; 

        public bool Is64Bit { get; private set; }

        public StudioVersionService(PluginPackageInfo pluginPackage)
        {
            Is64Bit = IsMachine64Bit();
            _installedStudioVersions = new List<StudioVersion>();
            _pluginPackage = pluginPackage;

            Initialize();
        }
        public List<StudioVersion> GetInstalledStudioVersions()
        {
            return _installedStudioVersions;
        }

        public List<StudioVersion> GetNotSupportedStudioVersions()
        {
            return _installedStudioVersions.Where(
                x => x.ExecutableVersion.CompareTo(_pluginPackage.MinRequiredProductVersion) < 0).ToList();
        }

        private void Initialize()
        {
            var registryPath = Is64Bit ? InstallLocation64Bit : InstallLocation32Bit;
            var sdlRegistryKey = Registry.LocalMachine.OpenSubKey(registryPath);

            if (sdlRegistryKey == null) return;
            foreach (var supportedStudioVersion in _supportedStudioVersions)
            {
                FindAndCreateStudioVersion(registryPath, supportedStudioVersion.Key, supportedStudioVersion.Value);
            }
            
        }

        private bool IsMachine64Bit()
        {
            var centralProcessorKey = Registry.LocalMachine.OpenSubKey(CentrallProcessor);
            if (centralProcessorKey != null)
            {
                var value = (int)centralProcessorKey.GetValue("Platform Specific Field 1");
                return value == 64;
            }
            return false;
        }

        private void FindAndCreateStudioVersion(string registryPath, string studioVersion, string studioPublicVersion)
        {
            var studioKey = Registry.LocalMachine.OpenSubKey(string.Format(@"{0}\{1}", registryPath, studioVersion));
            if (studioKey != null)
            {
                CreateStudioVersion(studioKey, studioVersion, studioPublicVersion);
            }
        }

        private void CreateStudioVersion(RegistryKey studioKey,string version, string publicVersion)
        {
            var installLocation = studioKey.GetValue("InstallLocation").ToString();
            var fullVersion = GetStudioFullVersion(installLocation);


            _installedStudioVersions.Add(new StudioVersion()
            {
                Version = version,
                PublicVersion = publicVersion,
                InstallPath = installLocation,
                ExecutableVersion = new Version(fullVersion)
            });
        }

        private static string GetStudioFullVersion(string installLocation)
        {
            var assembly = Assembly.LoadFile(string.Format(@"{0}\{1}", installLocation, "SDLTradosStudio.exe"));
            var versionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            string fullVersion = versionInfo.FileVersion;
            return fullVersion;
        }

       
    }
}
