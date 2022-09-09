using KRealEstate.ViewModels.Common.API;
using KRealEstate.ViewModels.System.Addresss;

namespace KRealEstate.APIIntegration.UserClient
{
    public interface IAddressApiClient
    {
        public Task<ResultApi<List<ProvinceViewModel>>> GetProvincesByUnitRegionId(int unitId, int regionId);
        public Task<ResultApi<List<DistrictViewModel>>> GetDistrictsByProvinceId(string provinceId);
        public Task<ResultApi<List<WardViewModel>>> GetWardsByDistrictId(string districtId);
        public Task<ResultApi<List<ProvinceViewModel>>> GetProvinces();
    }
}
