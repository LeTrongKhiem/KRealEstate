using KRealEstate.Data.DBContext;
using KRealEstate.Data.Models;
using KRealEstate.Utilities.Constants;
using KRealEstate.ViewModels.Common;
using KRealEstate.ViewModels.Common.API;
using KRealEstate.ViewModels.System.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text;

namespace KRealEstate.Application.System.Users
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IConfiguration _config;
        private readonly RealEstateDBContext _context;
        private readonly ILogger<UserService> _logger;
        private readonly IEmailSender _emailSender;

        public UserService(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, SignInManager<AppUser> signInManager, IConfiguration config, RealEstateDBContext context, ILogger<UserService> logger, IEmailSender emailSender)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _config = config;
            _context = context;
            _logger = logger;
            _emailSender = emailSender;
        }

        public Task<ResultApi<string>> Authenticate(LoginRequest request)
        {
            throw new NotImplementedException();
        }
        #region ConfirmEmail
        public async Task<ResultApi<bool>> ConfirmEmails(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return new ResultApiError<bool>("Confirm Failed");
            }
            var user = await _userManager.FindByNameAsync(userId);
            code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            var result = await _userManager.ConfirmEmailAsync(user, code);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, isPersistent: false);
                return new ResultApiSuccess<bool>();
            }
            return new ResultApiError<bool>("Confirm Failed");
        }
        #endregion
        #region Register
        public async Task<ResultApi<bool>> CreateUser(UserCreateRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.UserName);
            var email = await _userManager.FindByEmailAsync(request.Email);
            if (user != null)
            {
                return new ResultApiError<bool>("Username đã tồn tại. Vui lòng thử lại!!");
            }
            if (email != null)
            {
                return new ResultApiError<bool>("Email đã tồn tại. Vui lòng thử Email khác.");
            }
            var gAddress = Guid.NewGuid();
            Address address = null;
            if (_context.Addresses.Any(x => x.UnitId == request.UnitId && x.ProviceCode.Equals(request.ProviceCode)
                                && x.DistrictCode.Equals(request.DistrictCode) && x.WardCode.Equals(request.WardCode)
                                && x.RegionId == request.RegionId))
            {
                address = await (from add in _context.Addresses
                                 where add.UnitId == request.UnitId && add.RegionId == request.RegionId
                                 && add.ProviceCode.Equals(request.ProviceCode) && add.DistrictCode.Equals(request.DistrictCode)
                                 && add.WardCode.Equals(request.WardCode)
                                 select add).FirstOrDefaultAsync();
            }
            else
            {
                address = new Address()
                {
                    Id = gAddress.ToString(),
                    DistrictCode = request.DistrictCode,
                    ProviceCode = request.ProviceCode,
                    WardCode = request.WardCode,
                    RegionId = request.RegionId,
                    UnitId = request.UnitId,
                };
                _context.Addresses.Add(address);
            }
            Guid gId = Guid.NewGuid();
            user = new AppUser()
            {
                Id = gId,
                Dob = request.Dob,
                UserName = request.UserName,
                LastName = request.LastName,
                FirstName = request.FirstName,
                Gender = request.Gender,
                TaxId = request.TaxId,
                PhoneNumber = request.Phone,
                Organization = request.Organization,
                AddressId = address.Id,
                Email = request.Email,
            };
            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                string urlWebapp = SystemConstants.BaseUrl.urlApp;
                var content = urlWebapp + $"/Account/ConfirmEmail?userId={user.Id}&code={code}";
                await _emailSender.SendEmailAsync(request.Email, "Email xác nhận đăng ký tài khoản", $"Để xác nhận tài khoản vui lòng " +
                    $"click vào <a href='{content}'>Bấm vào đây</a>");
                if (_userManager.Options.SignIn.RequireConfirmedEmail)
                {
                    return new ResultApiSuccess<bool>();
                }
                else
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return new ResultApiError<bool>("Vui lòng kiểm tra Email và xác thực tài khoản");
                }
                return new ResultApiError<bool>("Đăng kí không thành công");
            }
            return new ResultApiSuccess<bool>();
        }
        #endregion
        public Task<ResultApi<bool>> DeleteUser(UserDeleteRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<ResultApi<bool>> EditUser(UserEditRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<ResultApi<bool>> ForgotPassword(ForgotPasswordRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<ResultApi<PageResult<UserViewModel>>> GetAllUser()
        {
            throw new NotImplementedException();
        }

        public async Task<ResultApi<UserViewModel>> GetById(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            //var roles = await (from urole in _context.user join role in _context.AspNetRoles on urole.RoleId equals role.Id where urole.UserId.Equals(user.Id) select role.Name).ToListAsync();
            if (user == null)
            {
                return new ResultApiError<UserViewModel>("Not found user");
            }
            var userVm = new UserViewModel()
            {
                Id = user.Id.ToString(),
                AddressId = user.AddressId,
                Dob = user.Dob,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                UserName = user.UserName,
                //Roles = roles,
            };
            return new ResultApiSuccess<UserViewModel>(userVm);
        }

        public Task<ResultApi<UserViewModel>> GetByUsername(string username)
        {
            throw new NotImplementedException();
        }

        public Task<ResultApi<bool>> ResetPassword(ResetPasswordRequest request)
        {
            throw new NotImplementedException();
        }

        public Task<ResultApi<bool>> UpdatePassword(string id, EditPasswordRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
