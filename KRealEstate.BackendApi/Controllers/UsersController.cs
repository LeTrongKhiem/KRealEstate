using KRealEstate.Application.System.Users;
using KRealEstate.ViewModels.Common;
using KRealEstate.ViewModels.System.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KRealEstate.BackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        [AllowAnonymous]
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
        [AllowAnonymous]
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
        [HttpPost("forgotpassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _userService.ForgotPassword(request);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPost("resetpassword")]
        [AllowAnonymous]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _userService.ResetPassword(request);
            if (!result.IsSuccess)
            {
                return BadRequest("Reset failed");
            }
            return Ok();
        }
        [HttpPost("authenticate")]
        [AllowAnonymous]
        public async Task<IActionResult> Authenticate([FromBody] LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var resultToken = await _userService.Authenticate(request);
            if (string.IsNullOrEmpty(resultToken.ResultObject))
            {
                return BadRequest(resultToken);
            }
            return Ok(resultToken);
        }
        [HttpGet("confirm")]
        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail([FromQuery] string userId, [FromQuery] string code)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _userService.ConfirmEmails(userId, code);
            if (!result.IsSuccess)
            {
                return BadRequest("Unconfimred");
            }
            return Ok();
        }
        [HttpGet("paging")]
        [AllowAnonymous]
        public async Task<IActionResult> GetListUser([FromQuery] PagingWithKeyword request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var users = await _userService.GetAllUser(request);
            if (users == null)
            {
                return BadRequest("No User find");
            }
            return Ok(users);
        }
        [HttpGet("get/{username}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetByUsername(string username)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var users = await _userService.GetByUsername(username);
            if (users == null)
            {
                return BadRequest("No User find");
            }
            return Ok(users);
        }
        [HttpPut("{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> Edit(string id, [FromBody] UserEditRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _userService.EditUser(id, request);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPut("newpassword/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdatePassword(string id, [FromBody] EditPasswordRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _userService.UpdatePassword(id, request);
            if (!result.IsSuccess)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
