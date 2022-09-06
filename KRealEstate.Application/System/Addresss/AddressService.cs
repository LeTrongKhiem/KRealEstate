using KRealEstate.Data.DBContext;
using KRealEstate.ViewModels.Common.API;
using KRealEstate.ViewModels.System.Addresss;
using Microsoft.EntityFrameworkCore;

namespace KRealEstate.Application.System.Addresss
{
    public class AddressService : IAddressService
    {
        private readonly RealEstateDBContext _context;
        public AddressService(RealEstateDBContext context)
        {
            _context = context;
        }
        #region Get Districts By ProvinceId
        public async Task<ResultApi<List<DistrictViewModel>>> GetDistrictByProvinceId(string provinceId)
        {
            var districts = await _context.Districts.Where(x => x.ProvinceCode.Equals(provinceId)).Select(x => new DistrictViewModel()
            {
                ProvinceCode = x.ProvinceCode,
                AdministrativeUnitId = x.AdministrativeUnitId,
                Code = x.Code,
                CodeName = x.CodeName,
                FullName = x.FullName,
                FullNameEn = x.FullNameEn,
                Name = x.Name,
                NameEn = x.NameEn
            }).ToListAsync();
            if (districts == null)
            {
                return new ResultApiError<List<DistrictViewModel>>("Not found");
            }
            return new ResultApiSuccess<List<DistrictViewModel>>(districts);
        }
        #endregion
        #region Get Province By UnitId and RegionId
        public async Task<ResultApi<List<ProvinceViewModel>>> GetProvinceByUnitRegionId(int unitId, int regionId)
        {
            var query = from province in _context.Provinces where province.AdministrativeUnitId == unitId && province.AdministrativeRegionId == regionId select province;
            var provinces = await query.Select(x => new ProvinceViewModel()
            {
                Code = x.Code,
                CodeName = x.CodeName,
                FullName = x.FullName,
                Name = x.Name,
                Name_en = x.NameEn,
                RegionId = regionId,
                UnitId = unitId
            }).ToListAsync();
            if (provinces == null)
            {
                return new ResultApiError<List<ProvinceViewModel>>("Not found please again!");
            }
            return new ResultApiSuccess<List<ProvinceViewModel>>(provinces);
        }
        #endregion
        #region Get Ward by DistrictId
        public async Task<ResultApi<List<WardViewModel>>> GetWardByDistrictId(string districtId)
        {
            var wards = await _context.Wards.Where(x => x.DistrictCode.Equals(districtId)).Select(x => new WardViewModel()
            {
                AdministrativeUnitId = x.AdministrativeUnitId,
                Code = x.Code,
                CodeName = x.CodeName,
                DistrictCode = districtId,
                FullName = x.FullName,
                FullNameEn = x.FullNameEn,
                Name = x.Name,
                NameEn = x.NameEn
            }).ToListAsync();
            if (wards == null)
            {
                return new ResultApiError<List<WardViewModel>>("Not found");
            }
            return new ResultApiSuccess<List<WardViewModel>>(wards);
        }
        #endregion
        #region Get All Province
        public async Task<ResultApi<List<ProvinceViewModel>>> GetProvinces()
        {
            var provinces = await _context.Provinces.Select(x => new ProvinceViewModel()
            {
                Code = x.Code,
                CodeName = x.CodeName,
                FullName = x.FullName,
                Name = x.Name,
                Name_en = x.NameEn,
                RegionId = x.AdministrativeRegionId,
                UnitId = x.AdministrativeUnitId
            }).ToListAsync();
            if (provinces == null)
            {
                return new ResultApiError<List<ProvinceViewModel>>("Not found");
            }
            return new ResultApiSuccess<List<ProvinceViewModel>>(provinces);
        }
        #endregion
    }
}
