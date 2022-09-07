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

        public async Task<List<DistrictViewModel>> GetDistrictsByProvinceId(string provinceId)
        {
            return await GetlistAsync<DistrictViewModel>($"/api/address/districts/{provinceId}");
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

        public async Task<List<ProvinceViewModel>> GetProvincesByUnitRegionId(int unitId, int regionId)
        {
            return await GetlistAsync<ProvinceViewModel>($"/api/address/provinces?unitId={unitId}&regionId={regionId}");
        }

        public async Task<List<WardViewModel>> GetWardsByDistrictId(string districtId)
        {
            return await GetlistAsync<WardViewModel>($"/api/address/wards/{districtId}");
        }
    }
}
