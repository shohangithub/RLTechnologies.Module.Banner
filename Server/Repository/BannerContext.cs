using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Oqtane.Modules;
using Oqtane.Repository;
using Oqtane.Infrastructure;
using Oqtane.Repository.Databases.Interfaces;

namespace RLTechnologies.Module.Banner.Repository
{
    public class BannerContext : DBContextBase, ITransientService, IMultiDatabase
    {
        public virtual DbSet<Models.Banner> Banner { get; set; }

        public BannerContext(IDBContextDependencies DBContextDependencies) : base(DBContextDependencies)
        {
            // ContextBase handles multi-tenant database connections
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Models.Banner>().ToTable(ActiveDatabase.RewriteName("RLTechnologiesBanner"));
        }
    }
}
