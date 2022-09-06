using KRealEstate.ViewModels.System.Addresss;

namespace KRealEstate.APIIntegration.UserClient
{
    public interface IAddressApiClient
    {
        public Task<List<ProvinceViewModel>> GetProvincesByUnitRegionId(int unitId, int regionId);
        public Task<List<DistrictViewModel>> GetDistrictsByProvinceId(string provinceId);
        public Task<List<WardViewModel>> GetWardsByDistrictId(string districtId);
        public Task<List<ProvinceViewModel>> GetProvinces();
    }
}
