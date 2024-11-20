using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Collections.Generic;
using Oqtane.Modules;
using RLTechnologies.Module.Banner.Models;

namespace RLTechnologies.Module.Banner.Repository
{
    public class BannerRepository : IBannerRepository, ITransientService
    {
        private readonly IDbContextFactory<BannerContext> _factory;

        public BannerRepository(IDbContextFactory<BannerContext> factory)
        {
            _factory = factory;
        }

        public IEnumerable<Models.Banner> GetBanners(int ModuleId)
        {
            using var db = _factory.CreateDbContext();
            return db.Banner.Where(item => item.ModuleId == ModuleId).ToList();
        }

        public Models.Banner GetByModule(int ModuleId)
        {
            using var db = _factory.CreateDbContext();

            return db.Banner.FirstOrDefault(x => x.ModuleId == ModuleId);
        }

        public Models.Banner GetBanner(int BannerId)
        {
            return GetBanner(BannerId, true);
        }

        public Models.Banner GetBanner(int BannerId, bool tracking)
        {
            using var db = _factory.CreateDbContext();
            if (tracking)
            {
                return db.Banner.Find(BannerId);
            }
            else
            {
                return db.Banner.AsNoTracking().FirstOrDefault(item => item.BannerId == BannerId);
            }
        }

        public Models.Banner AddBanner(Models.Banner Banner)
        {
            using var db = _factory.CreateDbContext();
            db.Banner.Add(Banner);
            db.SaveChanges();
            return Banner;
        }

        public Models.Banner UpdateBanner(Models.Banner Banner)
        {
            using var db = _factory.CreateDbContext();
            db.Entry(Banner).State = EntityState.Modified;
            db.SaveChanges();
            return Banner;
        }

        public void DeleteBanner(int BannerId)
        {
            using var db = _factory.CreateDbContext();
            Models.Banner Banner = db.Banner.Find(BannerId);
            db.Banner.Remove(Banner);
            db.SaveChanges();
        }
    }
}
