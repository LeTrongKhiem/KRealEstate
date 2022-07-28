using KRealEstate.Application.System.Users;
using KRealEstate.ViewModels.System.Users;
using Microsoft.AspNetCore.Mvc;

namespace KRealEstate.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromBody] UserCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _userService.CreateUser(request);
            if (result.IsSuccess)
            {
                return Ok(result.Message);
            }
            return BadRequest(request);

        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _userService.GetById(id);
            if (result != null)
            {
                return Ok(result.ResultObject);
            }
            return BadRequest(result);
        }
    }
}
