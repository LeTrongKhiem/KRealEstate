using KRealEstate.ViewModels.Common.API;
using KRealEstate.ViewModels.System.Addresss;

namespace KRealEstate.Application.System.Addresss
{
    public interface IAddressService
    {
        public Task<ResultApi<List<ProvinceViewModel>>> GetProvinces();
        public Task<ResultApi<List<ProvinceViewModel>>> GetProvinceByUnitRegionId(int unitId, int regionId);
        public Task<ResultApi<List<DistrictViewModel>>> GetDistrictByProvinceId(string provinceId);
        public Task<ResultApi<List<WardViewModel>>> GetWardByDistrictId(string districtId);
    }
}
