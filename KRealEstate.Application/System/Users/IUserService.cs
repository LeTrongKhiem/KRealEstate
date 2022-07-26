using KRealEstate.ViewModels.Common;
using KRealEstate.ViewModels.Common.API;
using KRealEstate.ViewModels.System.Users;

namespace KRealEstate.Application.System.Users
{
    public interface IUserService
    {
        Task<ResultApi<string>> Authenticate(LoginRequest request);
        Task<ResultApi<bool>> CreateUser(UserCreateRequest request);
        Task<ResultApi<bool>> EditUser(UserEditRequest request);
        Task<ResultApi<PageResult<UserViewModel>>> GetAllUser();
        Task<ResultApi<bool>> DeleteUser(UserDeleteRequest request);
        Task<ResultApi<UserViewModel>> GetById(string id);
        Task<ResultApi<UserViewModel>> GetByUsername(string username);
        Task<ResultApi<bool>> ConfirmEmails(string username, string code);
        public Task<ResultApi<bool>> ForgotPassword(ForgotPasswordRequest request);
        public Task<ResultApi<bool>> ResetPassword(ResetPasswordRequest request);
        public Task<ResultApi<bool>> UpdatePassword(string id, EditPasswordRequest request);
    }
}
