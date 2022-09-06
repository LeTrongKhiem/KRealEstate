using KRealEstate.Application.System.Addresss;
using Microsoft.AspNetCore.Mvc;

namespace KRealEstate.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;
        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }
        [HttpGet]
        public async Task<IActionResult> GetProvinces()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _addressService.GetProvinces();
            if (result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpGet("provinces")]
        public async Task<IActionResult> GetProvincesByUnitRegionId([FromQuery] int unitId, [FromQuery] int regionId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _addressService.GetProvinceByUnitRegionId(unitId, regionId);
            if (result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpGet("districts/{provinceId}")]
        public async Task<IActionResult> GetDistrictByProvinceId(string provinceId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _addressService.GetDistrictByProvinceId(provinceId);
            if (result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpGet("wards/{districtId}")]
        public async Task<IActionResult> GetWardsByDistrictId(string districtId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _addressService.GetWardByDistrictId(districtId);
            if (result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
