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
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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
        #region Authenticaion
        public async Task<ResultApi<string>> Authenticate(LoginRequest request)
        {
            var user = await _userManager.FindByNameAsync(request.Username);
            if (user == null)
            {
                return new ResultApiError<string>("Tài khoản không tồn tại. Vui lòng kiểm tra lại");
            }
            var email = await _userManager.FindByEmailAsync(user.Email);
            var resultLogin = await _signInManager.PasswordSignInAsync(user, request.Password, request.RememberPassword, true);
            if (!resultLogin.Succeeded)
            {
                return new ResultApiError<string>("Đăng nhập thất bại. Vui lòng kiểm tra lại mật khẩu");
            }
            if (!email.EmailConfirmed)
            {
                return new ResultApiError<string>("Đăng nhập thất bại. Vui lòng kiểm tra Email và xác nhận");
            }
            var roles = await _userManager.GetRolesAsync(user);
            var claims = new[] {
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.GivenName, user.FirstName),
                new Claim(ClaimTypes.Role, string.Join(";", roles)),
                new Claim(ClaimTypes.Name, request.Username)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Token:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(_config["Token:Issuer"],
                        _config["Token:Issuer"],
                        claims,
                        expires: DateTime.Now.AddHours(3),
                        signingCredentials: creds);
            return new ResultApiSuccess<string>(new JwtSecurityTokenHandler().WriteToken(token));
        }
        #endregion
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
            var query = from p in _context.Provinces
                        join d in _context.Districts on p.Code equals d.ProvinceCode
                        into pd
                        from d in pd.DefaultIfEmpty()
                        join w in _context.Wards on d.Code equals w.DistrictCode
                        into dw
                        from w in dw.DefaultIfEmpty()
                        where p.Code.Equals(request.ProviceCode) && d.Code.Equals(request.DistrictCode) && w.Code.Equals(request.WardCode)
                        select new { p, d, w, pd, dw };
            var unitId = await query.Select(x => x.p.AdministrativeUnitId).FirstOrDefaultAsync();
            var regionId = await query.Select(x => x.p.AdministrativeRegionId).FirstOrDefaultAsync();
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
                    //RegionId = request.RegionId,
                    RegionId = (int)regionId,
                    UnitId = (int)unitId,
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
        #region Delete User
        public async Task<ResultApi<bool>> DeleteUser(UserDeleteRequest request)
        {
            var user = await _userManager.FindByIdAsync(request.Id);
            if (user == null)
            {
                return new ResultApiError<bool>("Tài khoản không tồn tại");
            }
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                return new ResultApiSuccess<bool>();
            }
            return new ResultApiError<bool>("Xóa thất bại");
        }
        #endregion
        #region EditUser
        public async Task<ResultApi<bool>> EditUser(string id, UserEditRequest request)
        {

            if (await _userManager.Users.AnyAsync(x => x.Email == request.Email && x.Id.ToString() != id))
            {
                return new ResultApiError<bool>("Email đã tồn tại");
            }
            var user = await _userManager.FindByIdAsync(request.Id);
            if (user == null)
            {
                return new ResultApiError<bool>("Tài khoản không tồn tại");
            }
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Dob = request.Dob;
            user.Email = request.Email;
            user.PhoneNumber = request.PhoneNumber;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return new ResultApiSuccess<bool>();
            }
            return new ResultApiError<bool>("Cập nhật không thành công");
        }
        #endregion
        #region ForgotPassword
        public async Task<ResultApi<bool>> ForgotPassword(ForgotPasswordRequest request)
        {
            if (request.Email == null)
            {
                return new ResultApiError<bool>("Vui lòng nhập Email của bạn !");
            }
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null || !(await _userManager.IsEmailConfirmedAsync(user)))
            {
                return new ResultApiError<bool>("Email không tồn tại vui lòng kiểm tra lại.");
            }
            // Phát sinh Token để reset password
            // Token sẽ được kèm vào link trong email,
            // link dẫn đến trang /Account/ResetPassword để kiểm tra và đặt lại mật khẩu

            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            //code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(code));
            string urlWebApp = SystemConstants.BaseUrl.urlApp;
            var url = urlWebApp + $"/Account/ResetPassword?code={code}";
            await _emailSender.SendEmailAsync(request.Email, "Xác nhận thay đổi mật khẩu",
                $"Bạn đã yêu cầu thay đổi mật khẩu. Vui lòng <a href='{url}'>bấm vào đây</a>. Nếu không bạn có thể bỏ qua email này.");
            return new ResultApiSuccess<bool>();
        }
        #endregion
        #region GetAllUser
        public async Task<ResultApi<PageResult<UserViewModel>>> GetAllUser(PagingWithKeyword request)
        {
            var users = _userManager.Users;
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                users = users.Where(x => x.LastName.Contains(request.Keyword) || x.PhoneNumber.Contains(request.Keyword));
            }
            if (request.Active == true)
            {
                users = users.Where(x => x.EmailConfirmed == true);
            }
            else
            {
                users = users.Where(x => x.EmailConfirmed == false);
            }
            var items = await users.Skip((request.PageIndex - 1) * request.PageSize)
                            .Take(request.PageSize)
                            .Select(x => new UserViewModel()
                            {
                                Id = x.Id.ToString(),
                                AddressId = x.AddressId,
                                Dob = x.Dob,
                                Email = x.Email,
                                FirstName = x.FirstName,
                                LastName = x.LastName,
                                PhoneNumber = x.PhoneNumber,
                                UserName = x.UserName,
                            }).ToListAsync();
            var totalRow = await users.CountAsync();
            var result = new PageResult<UserViewModel>()
            {
                Items = items,
                TotalRecord = totalRow,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize
            };
            return new ResultApiSuccess<PageResult<UserViewModel>>(result);
        }
        #endregion
        #region GetById
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
        #endregion
        #region GetByUserName
        public async Task<ResultApi<UserViewModel>> GetByUsername(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return new ResultApiError<UserViewModel>("Tài khoản không tồn tại");
            }
            var roles = await _userManager.GetRolesAsync(user);
            var userViewModel = new UserViewModel()
            {
                Id = user.Id.ToString(),
                Dob = user.Dob,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                UserName = user.UserName,
                Roles = roles
            };
            return new ResultApiSuccess<UserViewModel>(userViewModel);
        }
        #endregion
        #region ResetPassword
        public async Task<ResultApi<bool>> ResetPassword(ResetPasswordRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return new ResultApiError<bool>("Email không tồn tại, Vui lòng kiểm tra lại");
            }
            var result = await _userManager.ResetPasswordAsync(user, request.Code, request.Password);

            if (!result.Succeeded)
            {
                return new ResultApiError<bool>("Reset Password Failed. Please try again!!!");
            }
            return new ResultApiSuccess<bool>();
        }
        #endregion
        #region Update Password
        public async Task<ResultApi<bool>> UpdatePassword(string id, EditPasswordRequest request)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return new ResultApiError<bool>("Không tìm thấy người dùng");
            }
            if (request.NewPassword != request.NewPasswordConfirm)
            {
                return new ResultApiError<bool>("Mật khẩu xác nhận không đúng");
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var result = await _userManager.ResetPasswordAsync(user, token, request.NewPassword);
            if (!result.Succeeded)
            {
                return new ResultApiError<bool>("Password phải chứa kí tự in hoa, chữ số và kí tự đặc biệt");
            }
            return new ResultApiSuccess<bool>();
        }
        #endregion
    }
}
