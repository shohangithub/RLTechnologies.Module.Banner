using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Oqtane.Enums;
using Oqtane.Infrastructure;
using Oqtane.Models;
using Oqtane.Security;
using Oqtane.Shared;
using RLTechnologies.Module.Banner.Models;
using RLTechnologies.Module.Banner.Repository;

namespace RLTechnologies.Module.Banner.Services
{
    public class ServerBannerService : IBannerService
    {
        private readonly IBannerRepository _BannerRepository;
        private readonly IUserPermissions _userPermissions;
        private readonly ILogManager _logger;
        private readonly IHttpContextAccessor _accessor;
        private readonly Alias _alias;

        public ServerBannerService(IBannerRepository BannerRepository, IUserPermissions userPermissions, ITenantManager tenantManager, ILogManager logger, IHttpContextAccessor accessor)
        {
            _BannerRepository = BannerRepository;
            _userPermissions = userPermissions;
            _logger = logger;
            _accessor = accessor;
            _alias = tenantManager.GetAlias();
        }

        public Task<List<Models.Banner>> GetBannersAsync(int ModuleId)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, ModuleId, PermissionNames.View))
            {
                return Task.FromResult(_BannerRepository.GetBanners(ModuleId).ToList());
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Banner Get Attempt {ModuleId}", ModuleId);
                return null;
            }
        }

        public Task<Models.Banner> GetBannerAsync(int BannerId, int ModuleId)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, ModuleId, PermissionNames.View))
            {
                return Task.FromResult(_BannerRepository.GetBanner(BannerId));
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Banner Get Attempt {BannerId} {ModuleId}", BannerId, ModuleId);
                return null;
            }
        }

        public Task<Models.Banner> AddBannerAsync(Models.Banner Banner)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, Banner.ModuleId, PermissionNames.Edit))
            {
                Banner = _BannerRepository.AddBanner(Banner);
                _logger.Log(LogLevel.Information, this, LogFunction.Create, "Banner Added {Banner}", Banner);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Banner Add Attempt {Banner}", Banner);
                Banner = null;
            }
            return Task.FromResult(Banner);
        }

        public Task<Models.Banner> UpdateBannerAsync(Models.Banner Banner)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, Banner.ModuleId, PermissionNames.Edit))
            {
                Banner = _BannerRepository.UpdateBanner(Banner);
                _logger.Log(LogLevel.Information, this, LogFunction.Update, "Banner Updated {Banner}", Banner);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Banner Update Attempt {Banner}", Banner);
                Banner = null;
            }
            return Task.FromResult(Banner);
        }

        public Task DeleteBannerAsync(int BannerId, int ModuleId)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, ModuleId, PermissionNames.Edit))
            {
                _BannerRepository.DeleteBanner(BannerId);
                _logger.Log(LogLevel.Information, this, LogFunction.Delete, "Banner Deleted {BannerId}", BannerId);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Banner Delete Attempt {BannerId} {ModuleId}", BannerId, ModuleId);
            }
            return Task.CompletedTask;
        }

        public Task<Models.Banner> GetByModuleAsync(int ModuleId)
        {
            if (_userPermissions.IsAuthorized(_accessor.HttpContext.User, _alias.SiteId, EntityNames.Module, ModuleId, PermissionNames.View))
            {
                return Task.FromResult(_BannerRepository.GetByModule(ModuleId));
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Banner Get Attempt {ModuleId}", ModuleId);
                return null;
            }
        }
    }
}
