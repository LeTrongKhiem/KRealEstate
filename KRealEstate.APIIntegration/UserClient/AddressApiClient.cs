using KRealEstate.Utilities.Constants;
using KRealEstate.ViewModels.Common.API;
using KRealEstate.ViewModels.System.Addresss;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace KRealEstate.APIIntegration.UserClient
{
    public class AddressApiClient : BaseAPIClient, IAddressApiClient
    {
        public AddressApiClient(IHttpClientFactory httpClientFactory, IHttpContextAccessor httpContextAccessor, IConfiguration configuration) : base(httpClientFactory, httpContextAccessor, configuration)
        {
        }

        public async Task<ResultApi<List<DistrictViewModel>>> GetDistrictsByProvinceId(string provinceId)
        {
            return await GetResultApi<DistrictViewModel>($"/api/address/districts/{provinceId}");
        }

        public async Task<ResultApi<List<ProvinceViewModel>>> GetProvinces()
        {
            //return await GetlistAsync<ProvinceViewModel>($"/api/address/");
            var session = _httpContextAccessor.HttpContext.Session.GetString(SystemConstants.Token);
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration[SystemConstants.BaseUrl.BaseAddress]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(SystemConstants.Authentication.RequestHeader, session);
            var response = await client.GetAsync($"/api/address/");
            var result = await response.Content.ReadAsStringAsync();
            var provinces = JsonConvert.DeserializeObject<ResultApiSuccess<List<ProvinceViewModel>>>(result);
            return provinces;
        }

        public async Task<ResultApi<List<ProvinceViewModel>>> GetProvincesByUnitRegionId(int unitId, int regionId)
        {
            return await GetResultApi<ProvinceViewModel>($"/api/address/provinces?unitId={unitId}&regionId={regionId}");
        }

        public async Task<ResultApi<List<WardViewModel>>> GetWardsByDistrictId(string districtId)
        {
            return await GetResultApi<WardViewModel>($"/api/address/wards/{districtId}");
        }
    }
}
