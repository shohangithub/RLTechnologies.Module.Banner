using Microsoft.AspNetCore.Builder; 
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Oqtane.Infrastructure;
using RLTechnologies.Module.Banner.Repository;
using RLTechnologies.Module.Banner.Services;

namespace RLTechnologies.Module.Banner.Startup
{
    public class ServerStartup : IServerStartup
    {
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // not implemented
        }

        public void ConfigureMvc(IMvcBuilder mvcBuilder)
        {
            // not implemented
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IBannerService, ServerBannerService>();
            services.AddDbContextFactory<BannerContext>(opt => { }, ServiceLifetime.Transient);
        }
    }
}
