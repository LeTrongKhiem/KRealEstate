using KRealEstate.Data.DBContext;
using KRealEstate.Data.Models;
using KRealEstate.ViewModels.Common;
using KRealEstate.ViewModels.Common.API;
using KRealEstate.ViewModels.System.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace KRealEstate.Application.System.Users
{
    public class UserService : IUserService
    {
        private readonly UserManager<AspNetUser> _userManager;
        private readonly RoleManager<AspNetRole> _roleManager;
        private readonly SignInManager<AspNetUser> _signInManager;
        private readonly IConfiguration _config;
        private readonly RealEstateDBContext _context;
        private readonly ILogger<UserService> _logger;
        private readonly IEmailSender _emailSender;

        public UserService(UserManager<AspNetUser> userManager, RoleManager<AspNetRole> roleManager, SignInManager<AspNetUser> signInManager, IConfiguration config, RealEstateDBContext context, ILogger<UserService> logger, IEmailSender emailSender)
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

        public Task<ResultApi<bool>> ConfirmEmails(string username, string code)
        {
            throw new NotImplementedException();
        }

        public async Task<ResultApi<bool>> CreateUser(UserCreateRequest request)
        {
            var username = await _userManager.FindByNameAsync(request.UserName);
            var email = await _userManager.FindByEmailAsync(request.Email);
            if (username != null)
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
            var user = new AspNetUser()
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
                AddressId = address.Id
            };
            var result = await _userManager.CreateAsync(user, request.Password);
            if (result.Succeeded)
            {
                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            }
            return new ResultApiSuccess<bool>();
        }

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

        public Task<ResultApi<UserViewModel>> GetById(string id)
        {
            throw new NotImplementedException();
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
