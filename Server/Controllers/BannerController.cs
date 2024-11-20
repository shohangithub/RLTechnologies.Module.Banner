using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Oqtane.Shared;
using Oqtane.Enums;
using Oqtane.Infrastructure;
using RLTechnologies.Module.Banner.Repository;
using Oqtane.Controllers;
using System.Net;

namespace RLTechnologies.Module.Banner.Controllers
{
    [Route(ControllerRoutes.ApiRoute)]
    public class BannerController : ModuleControllerBase
    {
        private readonly IBannerRepository _BannerRepository;

        public BannerController(IBannerRepository BannerRepository, ILogManager logger, IHttpContextAccessor accessor) : base(logger, accessor)
        {
            _BannerRepository = BannerRepository;
        }

        // GET: api/<controller>?moduleid=x
        [HttpGet]
        [Authorize(Policy = PolicyNames.ViewModule)]
        public IEnumerable<Models.Banner> Get(string moduleid)
        {
            int ModuleId;
            if (int.TryParse(moduleid, out ModuleId) && IsAuthorizedEntityId(EntityNames.Module, ModuleId))
            {
                return _BannerRepository.GetBanners(ModuleId);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Banner Get Attempt {ModuleId}", moduleid);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return null;
            }
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        [Authorize(Policy = PolicyNames.ViewModule)]
        public Models.Banner Get(int id)
        {
            Models.Banner Banner = _BannerRepository.GetBanner(id);
            if (Banner != null && IsAuthorizedEntityId(EntityNames.Module, Banner.ModuleId))
            {
                return Banner;
            }
            else
            { 
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Banner Get Attempt {BannerId}", id);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return null;
            }
        }

        // GET: api/<controller>?moduleid=x
        [HttpGet]
        [Authorize(Policy = PolicyNames.ViewModule)]
        public IEnumerable<Models.Banner> GetByModule(string moduleid)
        {
            int ModuleId;
            if (int.TryParse(moduleid, out ModuleId) && IsAuthorizedEntityId(EntityNames.Module, ModuleId))
            {
                return _BannerRepository.GetBanners(ModuleId);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Banner Get Attempt {ModuleId}", moduleid);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                return null;
            }
        }

        // POST api/<controller>
        [HttpPost]
        [Authorize(Policy = PolicyNames.EditModule)]
        public Models.Banner Post([FromBody] Models.Banner Banner)
        {
            if (ModelState.IsValid && IsAuthorizedEntityId(EntityNames.Module, Banner.ModuleId))
            {
                Banner = _BannerRepository.AddBanner(Banner);
                _logger.Log(LogLevel.Information, this, LogFunction.Create, "Banner Added {Banner}", Banner);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Banner Post Attempt {Banner}", Banner);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                Banner = null;
            }
            return Banner;
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        [Authorize(Policy = PolicyNames.EditModule)]
        public Models.Banner Put(int id, [FromBody] Models.Banner Banner)
        {
            if (ModelState.IsValid && Banner.BannerId == id && IsAuthorizedEntityId(EntityNames.Module, Banner.ModuleId) && _BannerRepository.GetBanner(Banner.BannerId, false) != null)
            {
                Banner = _BannerRepository.UpdateBanner(Banner);
                _logger.Log(LogLevel.Information, this, LogFunction.Update, "Banner Updated {Banner}", Banner);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Banner Put Attempt {Banner}", Banner);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                Banner = null;
            }
            return Banner;
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        [Authorize(Policy = PolicyNames.EditModule)]
        public void Delete(int id)
        {
            Models.Banner Banner = _BannerRepository.GetBanner(id);
            if (Banner != null && IsAuthorizedEntityId(EntityNames.Module, Banner.ModuleId))
            {
                _BannerRepository.DeleteBanner(id);
                _logger.Log(LogLevel.Information, this, LogFunction.Delete, "Banner Deleted {BannerId}", id);
            }
            else
            {
                _logger.Log(LogLevel.Error, this, LogFunction.Security, "Unauthorized Banner Delete Attempt {BannerId}", id);
                HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            }
        }
    }
}
