using KRealEstate.ViewModels.Common;
using KRealEstate.ViewModels.Common.API;
using KRealEstate.ViewModels.System.Users;

namespace KRealEstate.APIIntegration.UserClient
{
    public interface IUserApiClient
    {
        Task<ResultApi<string>> Authenticate(LoginRequest request);
        Task<ResultApi<PageResult<UserViewModel>>> GetListUser(PagingWithKeyword request);
        Task<ResultApi<bool>> Register(UserCreateRequest request);
        Task<ResultApi<bool>> Edit(Guid id, UserEditRequest request);
        Task<ResultApi<UserViewModel>> GetById(Guid id);
        Task<ResultApi<UserViewModel>> GetByUsername(string username);
        Task<ResultApi<bool>> Delete(Guid id);
        //Task<ResultApi<bool>> RolesAssign(Guid id, RoleAssignRequest request);
        Task<ResultApi<bool>> ConfirmEmail(string id, string code);

        Task<ResultApi<bool>> ForgotPassword(ForgotPasswordRequest request);
        Task<ResultApi<bool>> ResetPassword(ResetPasswordRequest request);
        Task<ResultApi<bool>> EditPassword(Guid id, EditPasswordRequest request);
    }
}
