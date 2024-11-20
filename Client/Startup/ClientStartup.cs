using Microsoft.Extensions.DependencyInjection;
using Oqtane.Services;
using RLTechnologies.Module.Banner.Services;

namespace RLTechnologies.Module.Banner.Startup
{
    public class ClientStartup : IClientStartup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IBannerService, BannerService>();
        }
    }
}
