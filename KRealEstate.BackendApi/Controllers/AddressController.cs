using KRealEstate.Application.System.Address;
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
    }
}
