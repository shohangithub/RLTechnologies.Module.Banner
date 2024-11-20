using Oqtane.Models;
using Oqtane.Modules;

namespace RLTechnologies.Module.Banner
{
    public class ModuleInfo : IModule
    {
        public ModuleDefinition ModuleDefinition => new ModuleDefinition
        {
            Name = "Banner",
            Description = "Banner",
            Version = "1.0.0",
            ServerManagerType = "RLTechnologies.Module.Banner.Manager.BannerManager, RLTechnologies.Module.Banner.Server.Oqtane",
            ReleaseVersions = "1.0.0",
            Dependencies = "RLTechnologies.Module.Banner.Shared.Oqtane",
            PackageName = "RLTechnologies.Module.Banner" 
        };
    }
}
