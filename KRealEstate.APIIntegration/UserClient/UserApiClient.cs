using KRealEstate.Utilities.Constants;
using KRealEstate.ViewModels.Common;
using KRealEstate.ViewModels.Common.API;
using KRealEstate.ViewModels.System.Users;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace KRealEstate.APIIntegration.UserClient
{
    public class UserApiClient : BaseAPIClient, IUserApiClient
    {
        public UserApiClient(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor, IConfiguration configuration) : base(httpClientFactory, httpContextAccessor, configuration)
        {
        }

        public async Task<ResultApi<string>> Authenticate(LoginRequest request)
        {
            var session = _httpContextAccessor.HttpContext.Session.GetString(SystemConstants.Token);
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemConstants.BaseUrl.BaseAddress]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(SystemConstants.Authentication.RequestHeader, session);
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync($"/api/users/authenticate", httpContent);
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ResultApiSuccess<string>>(await response.Content.ReadAsStringAsync());
            }
            return JsonConvert.DeserializeObject<ResultApiError<string>>(await response.Content.ReadAsStringAsync());
        }

        public async Task<ResultApi<bool>> ConfirmEmail(string id, string code)
        {
            return await GetAsync<ResultApi<bool>>($"/api/users/confirm?userId={id}&code={code}");
        }

        public async Task<ResultApi<bool>> Delete(Guid id)
        {
            var session = _httpContextAccessor.HttpContext.Session.GetString(SystemConstants.Token);
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemConstants.BaseUrl.BaseAddress]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", session);
            var response = await client.DeleteAsync($"/api/users/delete?userId={id}");
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return new ResultApiSuccess<bool>();
            }
            return new ResultApiError<bool>("Error");
        }

        public async Task<ResultApi<bool>> Edit(Guid id, UserEditRequest request)
        {
            return await PutAsync<bool>($"/api/users/{id}", request);
        }

        public async Task<ResultApi<bool>> EditPassword(Guid id, EditPasswordRequest request)
        {
            return await PutAsync<bool>($"/api/users/newpassword/{id}", request);
        }

        public async Task<ResultApi<bool>> ForgotPassword(ForgotPasswordRequest request)
        {
            return await PostAsync<bool>($"/api/users/forgotpassword", request);
        }

        public async Task<ResultApi<UserViewModel>> GetById(Guid id)
        {
            return await GetAsync<ResultApi<UserViewModel>>($"/api/users/{id}");
        }

        public async Task<ResultApi<UserViewModel>> GetByUsername(string username)
        {
            return await GetAsync<ResultApi<UserViewModel>>($"/api/users/get/{username}");
        }

        public async Task<ResultApi<PageResult<UserViewModel>>> GetListUser(PagingWithKeyword request)
        {
            //return await GetlistAsync<UserViewModel>($"/api/users/paging?PageIndex={request.PageIndex}&PageSize={request.PageSize}&Keyword={request.Keyword}&Active={request.Active}");
            var session = _httpContextAccessor.HttpContext.Session.GetString(SystemConstants.Token);
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemConstants.BaseUrl.BaseAddress]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(SystemConstants.Authentication.RequestHeader, session);
            var response = await client.GetAsync($"/api/users/paging?PageIndex={request.PageIndex}&PageSize={request.PageSize}&Keyword={request.Keyword}&Active={request.Active}");
            var result = await response.Content.ReadAsStringAsync();
            var users = JsonConvert.DeserializeObject<ResultApiSuccess<PageResult<UserViewModel>>>(result);
            return users;
        }

        public async Task<ResultApi<bool>> Register(UserCreateRequest request)
        {
            //return await PostAsync<bool>($"/api/users", request);
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemConstants.BaseUrl.BaseAddress]);
            var response = await client.PostAsync($"/api/users", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ResultApiSuccess<bool>>(result);
            return JsonConvert.DeserializeObject<ResultApiError<bool>>(result);
        }

        public async Task<ResultApi<bool>> ResetPassword(ResetPasswordRequest request)
        {
            return await PostAsync<bool>($"/api/users/resetpassword", request);
        }
    }
}
