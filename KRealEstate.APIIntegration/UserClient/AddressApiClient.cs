using KRealEstate.ViewModels.System.Addresss;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

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

        public async Task<List<ProvinceViewModel>> GetProvinces()
        {
            return await GetlistAsync<ProvinceViewModel>($"/api/address");
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
