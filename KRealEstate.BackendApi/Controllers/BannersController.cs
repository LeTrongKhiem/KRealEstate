using KRealEstate.Application.Banners;
using KRealEstate.ViewModels.Catalog.Banner;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KRealEstate.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BannersController : ControllerBase
    {
        private readonly IBannerService _bannerService;
        public BannersController(IBannerService bannerService)
        {
            _bannerService = bannerService;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetAll([FromQuery] int pageSize, [FromQuery] int pageIndex, [FromQuery] string? keyWord)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _bannerService.GetAll(pageSize, pageIndex, keyWord);
            if (result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Create([FromForm] CreateBannerRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _bannerService.CreateBanner(request);
            if (result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Update(string id, [FromForm] EditBannerRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _bannerService.EditBanner(id, request);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpDelete("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Delete(string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _bannerService.DeleteBanner(id);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
