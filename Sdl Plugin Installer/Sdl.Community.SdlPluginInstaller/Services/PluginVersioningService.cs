using Sdl.Community.SdlPluginInstaller.Model;
using Sdl.Versioning;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sdl.Community.SdlPluginInstaller.Services
{
    public class PluginVersioningService
    {
        public StudioVersionService VersioningService;

        private List<StudioVersion> _installedStudioVersions;

        public PluginVersioningService()
        {
            VersioningService = new StudioVersionService();
            _installedStudioVersions = new List<StudioVersion>();
            Initialize();
        }

        public List<StudioVersion> GetNotSupportedStudioVersions(PluginPackageInfo pluginPackage)
        {
            return _installedStudioVersions.Where(x => !StudioVersionSuported(pluginPackage.MinRequiredProductVersion,
                                        pluginPackage.MaxRequiredProductVersion, x.ExecutableVersion)).ToList();

        }

        public bool StudioVersionSuported(Version minVersion, Version maxVersion, Version studioVersion)
        {
            return studioVersion >= minVersion && (maxVersion == null || studioVersion <= maxVersion);

        }


        private void Initialize()
        {
            _installedStudioVersions = VersioningService.GetInstalledStudioVersions();
        }

    }
}
