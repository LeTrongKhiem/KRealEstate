using KRealEstate.ViewModels.Common.API;
using KRealEstate.ViewModels.System.Address;

namespace KRealEstate.Application.System.Address
{
    public interface IAddressService
    {
        public Task<ResultApi<List<ProvinceViewModel>>> GetProvinceByUnitRegionId(int unitId, int regionId);
        public Task<ResultApi<List<DistrictViewModel>>> GetDistrictByProvinceId(string provinceId);
        public Task<ResultApi<List<WardViewModel>>> GetWardByDistrictId(string districtId);
    }
}
