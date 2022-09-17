using KRealEstate.APIIntegration.UserClient;
using KRealEstate.ViewModels.System.Users;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace KRealEstate.AdminWebApp.Controllers
{
    public class RegisterController : Controller
    {
        private readonly IUserApiClient _userApiClient;
        private readonly IConfiguration _configuration;
        private readonly IAddressApiClient _addressApiClient;
        public RegisterController(IUserApiClient userApiClient, IConfiguration configuration, IAddressApiClient addressApiClient)
        {
            _userApiClient = userApiClient;
            _configuration = configuration;
            _addressApiClient = addressApiClient;
        }
        #region register
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var provinces = await _addressApiClient.GetProvinces();
            ViewBag.province = provinces.ResultObject.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Code,
                //Selected = categoryId == x.CategoryId && categoryId != null
            });
            ViewBag.ProvinceList = new SelectList(provinces.ResultObject, "Code", "Name");
            return View();
        }
        public async Task<IActionResult> GetDistrictByProvince(string provinceId)
        {
            var district = await _addressApiClient.GetDistrictsByProvinceId(provinceId);
            ViewBag.District = new SelectList(district.ResultObject, "Code", "Name");
            ViewBag.district = district.ResultObject.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Code,
                //Selected = categoryId == x.CategoryId && categoryId != null
            });
            return PartialView("DisplayDistrict");
        }
        public async Task<IActionResult> GetWardByDistrict(string districtId)
        {
            var wards = await _addressApiClient.GetWardsByDistrictId(districtId);
            ViewBag.Wards = wards.ResultObject.Select(x => new SelectListItem()
            {
                Text = x.Name,
                Value = x.Code,
            });
            return PartialView("DisplayWard");
        }
        [HttpPost]
        public async Task<IActionResult> Index(UserCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return View(ModelState);
            }
            var result = await _userApiClient.Register(request);

            var resultLogin = await _userApiClient.Authenticate(new LoginRequest()
            {
                Username = request.UserName,
                Password = request.Password,
                RememberPassword = true
            });
            ViewBag.Email = request.Email;
            TempData["Email"] = request.Email;
            if (!result.IsSuccess)
            {
                ModelState.AddModelError("", result.Message);
                return View(request);
            }
            var userPrincipal = this.ValidateToken(resultLogin.ResultObject);
            var authProperties = new AuthenticationProperties()
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30),
                IsPersistent = true,
            };

            HttpContext.Session.SetString("Token", resultLogin.ResultObject);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal, authProperties);
            return RedirectToAction("Confirm", "User");
        }
        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string code)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            var result = await _userApiClient.ConfirmEmail(userId, code);
            if (result.IsSuccess)
            {
                return RedirectToAction("Index", "Login");
            }
            return RedirectToAction("Error");
        }
        public ClaimsPrincipal ValidateToken(string jwtToken)
        {
            IdentityModelEventSource.ShowPII = true;

            SecurityToken validatedToken;
            TokenValidationParameters validationParameters = new TokenValidationParameters();

            validationParameters.ValidateLifetime = true;

            validationParameters.ValidAudience = _configuration["Token:Issuer"];
            validationParameters.ValidIssuer = _configuration["Token:Issuer"];
            validationParameters.IssuerSigningKey = new Microsoft.IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Token:Key"]));

            ClaimsPrincipal principal = new JwtSecurityTokenHandler().ValidateToken(jwtToken, validationParameters, out validatedToken);

            return principal;

        }
        #endregion
        #region Address
        #endregion
    }
}
