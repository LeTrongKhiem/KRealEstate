using KRealEstate.APIIntegration.UserClient;
using KRealEstate.ViewModels.System.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

namespace KRealEstate.AdminWebApp.Controllers
{
    public class UserController : BaseController
    {
        private readonly IUserApiClient _userApiClient;
        private readonly IConfiguration _configuration;
        private readonly IAddressApiClient _addressApiClient;
        public UserController(IUserApiClient userApiClient, IConfiguration configuration, IAddressApiClient addressApiClient)
        {
            _userApiClient = userApiClient;
            _configuration = configuration;
            _addressApiClient = addressApiClient;
        }
        public IActionResult Index()
        {
            return View();
        }

        #region Logout Validate Token
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Remove("Token");
            return RedirectToAction("Index", "Home");
        }
        #endregion
        #region Confirm ForgotPassword
        [HttpGet]
        public IActionResult Confirm()
        {
            return View();
        }
        [HttpGet]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var result = await _userApiClient.ForgotPassword(request);
            if (result.IsSuccess)
            {
                return RedirectToAction("Confirm", "Account");
            }
            ModelState.AddModelError("", result.Message);
            return View(request);
        }
        #endregion
    }
}
