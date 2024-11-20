using System.Collections.Generic;
using System.Threading.Tasks;

namespace RLTechnologies.Module.Banner.Repository
{
    public interface IBannerRepository
    {
        IEnumerable<Models.Banner> GetBanners(int ModuleId);
        Models.Banner GetByModule(int ModuleId);
        Models.Banner GetBanner(int BannerId);
        Models.Banner GetBanner(int BannerId, bool tracking);
        Models.Banner AddBanner(Models.Banner Banner);
        Models.Banner UpdateBanner(Models.Banner Banner);
        void DeleteBanner(int BannerId);
    }
}
