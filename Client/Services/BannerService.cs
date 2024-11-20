using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Oqtane.Services;
using Oqtane.Shared;

namespace RLTechnologies.Module.Banner.Services
{
    public class BannerService : ServiceBase, IBannerService
    {
        public BannerService(HttpClient http, SiteState siteState) : base(http, siteState) { }

        private string Apiurl => CreateApiUrl("Banner");

        public async Task<List<Models.Banner>> GetBannersAsync(int ModuleId)
        {
            List<Models.Banner> Banners = await GetJsonAsync<List<Models.Banner>>(CreateAuthorizationPolicyUrl($"{Apiurl}?moduleid={ModuleId}", EntityNames.Module, ModuleId), Enumerable.Empty<Models.Banner>().ToList());
            return Banners.OrderBy(item => item.Name).ToList();
        }

        public async Task<Models.Banner> GetByModuleAsync(int ModuleId)
        {
            return await GetJsonAsync<Models.Banner>(CreateAuthorizationPolicyUrl($"{Apiurl}/GetByModule?moduleid={ModuleId}", EntityNames.Module, ModuleId));
        }

        public async Task<Models.Banner> GetBannerAsync(int BannerId, int ModuleId)
        {
            return await GetJsonAsync<Models.Banner>(CreateAuthorizationPolicyUrl($"{Apiurl}/{BannerId}", EntityNames.Module, ModuleId));
        }

        public async Task<Models.Banner> AddBannerAsync(Models.Banner Banner)
        {
            return await PostJsonAsync<Models.Banner>(CreateAuthorizationPolicyUrl($"{Apiurl}", EntityNames.Module, Banner.ModuleId), Banner);
        }

        public async Task<Models.Banner> UpdateBannerAsync(Models.Banner Banner)
        {
            return await PutJsonAsync<Models.Banner>(CreateAuthorizationPolicyUrl($"{Apiurl}/{Banner.BannerId}", EntityNames.Module, Banner.ModuleId), Banner);
        }

        public async Task DeleteBannerAsync(int BannerId, int ModuleId)
        {
            await DeleteAsync(CreateAuthorizationPolicyUrl($"{Apiurl}/{BannerId}", EntityNames.Module, ModuleId));
        }
    }
}
