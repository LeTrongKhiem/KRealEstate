using KRealEstate.APIIntegration.UserClient;
using KRealEstate.ViewModels.Common;
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
        #region Show list user
        public async Task<IActionResult> Index(string? Keyword, bool? Active, int PageIndex = 1, int PageSize = 10)
        {
            var request = new PagingWithKeyword()
            {
                Keyword = Keyword,
                Active = Active,
                PageIndex = PageIndex,
                PageSize = PageSize
            };
            if (!ModelState.IsValid)
            {
                return View(ModelState);
            }
            ViewBag.Keyword = request.Keyword;
            var result = await _userApiClient.GetListUser(request);
            if (TempData["result"] != null)
            {
                ViewBag.SuccessMsg = TempData["result"];
            }
            return View(result.ResultObject);
        }
        #endregion
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
