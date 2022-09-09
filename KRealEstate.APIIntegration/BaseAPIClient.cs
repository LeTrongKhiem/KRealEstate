using KRealEstate.Utilities.Constants;
using KRealEstate.ViewModels.Common.API;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace KRealEstate.APIIntegration
{
    public class BaseAPIClient
    {
        public readonly IHttpClientFactory _httpClientFactory;
        public readonly IHttpContextAccessor _httpContextAccessor;
        public readonly IConfiguration _configuration;
        protected BaseAPIClient(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration;
        }
        #region GetAsync
        protected async Task<TResponse> GetAsync<TResponse>(string uri)
        {
            var session = _httpContextAccessor.HttpContext.Session.GetString(SystemConstants.Token);
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemConstants.BaseUrl.BaseAddress]);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(SystemConstants.Authentication.RequestHeader, session);
            var response = await client.GetAsync(uri);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                TResponse myDeserializable = (TResponse)JsonConvert.DeserializeObject(result, typeof(TResponse));
                return myDeserializable;
            }
            return JsonConvert.DeserializeObject<TResponse>(result);
        }
        #endregion
        #region GetListAsync
        public async Task<List<T>> GetlistAsync<T>(string uri)
        {
            var session = _httpContextAccessor.HttpContext.Session.GetString(SystemConstants.Token);
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemConstants.BaseUrl.BaseAddress]);
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue(SystemConstants.Authentication.RequestHeader, session);
            var response = await client.GetAsync(uri);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                List<T> data = (List<T>)JsonConvert.DeserializeObject(result, typeof(List<T>));
                return data;
            }
            //return JsonConvert.DeserializeObject<List<T>>(result);
            throw new Exception(result);
        }
        #endregion
        #region Delete
        public async Task<ResultApi<bool>> DeleteAsync(string url)
        {
            var session = _httpContextAccessor.HttpContext.Session.GetString(SystemConstants.Token);
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemConstants.BaseUrl.BaseAddress]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", session);
            var response = await client.DeleteAsync($"{url}");
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
            {
                return new ResultApiSuccess<bool>();
            }
            return new ResultApiError<bool>("Error");
        }
        #endregion
        #region PostAsync
        public async Task<ResultApi<TResponse>> PostAsync<TResponse>(string url, Object request)
        {
            var session = _httpContextAccessor.HttpContext.Session.GetString(SystemConstants.Token);
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemConstants.BaseUrl.BaseAddress]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(SystemConstants.Authentication.RequestHeader, session);
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PostAsync(url, httpContent);
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ResultApiSuccess<TResponse>>(await response.Content.ReadAsStringAsync());
            }
            return JsonConvert.DeserializeObject<ResultApiError<TResponse>>(await response.Content.ReadAsStringAsync());
        }
        #endregion
        #region PutAsync
        public async Task<ResultApi<TResponse>> PutAsync<TResponse>(string url, Object request)
        {
            var session = _httpContextAccessor.HttpContext.Session.GetString(SystemConstants.Token);
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemConstants.BaseUrl.BaseAddress]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(SystemConstants.Authentication.RequestHeader, session);
            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await client.PutAsync(url, httpContent);
            if (response.IsSuccessStatusCode)
            {
                return JsonConvert.DeserializeObject<ResultApiSuccess<TResponse>>(await response.Content.ReadAsStringAsync());
            }
            return JsonConvert.DeserializeObject<ResultApiError<TResponse>>(await response.Content.ReadAsStringAsync());
        }
        #endregion
        #region GetListResultApi
        public async Task<ResultApi<List<T>>> GetResultApi<T>(string url)
        {
            var session = _httpContextAccessor.HttpContext.Session.GetString(SystemConstants.Token);
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemConstants.BaseUrl.BaseAddress]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(SystemConstants.Authentication.RequestHeader, session);
            var response = await client.GetAsync(url);
            var result = await response.Content.ReadAsStringAsync();
            var provinces = JsonConvert.DeserializeObject<ResultApiSuccess<List<T>>>(result);
            return provinces;
        }
        #endregion
    }
}
