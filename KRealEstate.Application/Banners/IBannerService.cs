﻿using KRealEstate.ViewModels.Catalog.Banner;
using KRealEstate.ViewModels.Common;
using KRealEstate.ViewModels.Common.API;

namespace KRealEstate.Application.Banners
{
    public interface IBannerService
    {
        public Task<ResultApi<string>> CreateBanner(CreateBannerRequest request);
        public Task<PageResult<BannerViewModel>> GetAll(int pageSize, int pageIndex, string keyWord);
        public Task<ResultApi<bool>> EditBanner(string id, EditBannerRequest request);
        public Task<ResultApi<bool>> DeleteBanner(string id);
    }
}
