using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Oqtane.Modules;
using Oqtane.Models;
using Oqtane.Infrastructure;
using Oqtane.Interfaces;
using Oqtane.Enums;
using Oqtane.Repository;
using RLTechnologies.Module.Banner.Repository;
using System.Threading.Tasks;

namespace RLTechnologies.Module.Banner.Manager
{
    public class BannerManager : MigratableModuleBase, IInstallable, IPortable, ISearchable
    {
        private readonly IBannerRepository _BannerRepository;
        private readonly IDBContextDependencies _DBContextDependencies;

        public BannerManager(IBannerRepository BannerRepository, IDBContextDependencies DBContextDependencies)
        {
            _BannerRepository = BannerRepository;
            _DBContextDependencies = DBContextDependencies;
        }

        public bool Install(Tenant tenant, string version)
        {
            return Migrate(new BannerContext(_DBContextDependencies), tenant, MigrationType.Up);
        }

        public bool Uninstall(Tenant tenant)
        {
            return Migrate(new BannerContext(_DBContextDependencies), tenant, MigrationType.Down);
        }

        public string ExportModule(Oqtane.Models.Module module)
        {
            string content = "";
            List<Models.Banner> Banners = _BannerRepository.GetBanners(module.ModuleId).ToList();
            if (Banners != null)
            {
                content = JsonSerializer.Serialize(Banners);
            }
            return content;
        }

        public void ImportModule(Oqtane.Models.Module module, string content, string version)
        {
            List<Models.Banner> Banners = null;
            if (!string.IsNullOrEmpty(content))
            {
                Banners = JsonSerializer.Deserialize<List<Models.Banner>>(content);
            }
            if (Banners != null)
            {
                foreach(var Banner in Banners)
                {
                    _BannerRepository.AddBanner(new Models.Banner { ModuleId = module.ModuleId, Name = Banner.Name });
                }
            }
        }

        public Task<List<SearchContent>> GetSearchContentsAsync(PageModule pageModule, DateTime lastIndexedOn)
        {
           var searchContentList = new List<SearchContent>();

           foreach (var Banner in _BannerRepository.GetBanners(pageModule.ModuleId))
           {
               if (Banner.ModifiedOn >= lastIndexedOn)
               {
                   searchContentList.Add(new SearchContent
                   {
                       EntityName = "RLTechnologiesBanner",
                       EntityId = Banner.BannerId.ToString(),
                       Title = Banner.Name,
                       Body = Banner.Name,
                       ContentModifiedBy = Banner.ModifiedBy,
                       ContentModifiedOn = Banner.ModifiedOn
                   });
               }
           }

           return Task.FromResult(searchContentList);
        }
    }
}
