using System.Collections.Generic;
using System.Threading.Tasks;

namespace RLTechnologies.Module.Banner.Services
{
    public interface IBannerService 
    {
        Task<List<Models.Banner>> GetBannersAsync(int ModuleId);

        Task<Models.Banner> GetByModuleAsync(int ModuleId);

        Task<Models.Banner> GetBannerAsync(int BannerId, int ModuleId);

        Task<Models.Banner> AddBannerAsync(Models.Banner Banner);

        Task<Models.Banner> UpdateBannerAsync(Models.Banner Banner);

        Task DeleteBannerAsync(int BannerId, int ModuleId);
    }
}
