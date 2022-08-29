using KRealEstate.Application.Common;
using KRealEstate.Data.DBContext;
using KRealEstate.Data.Dtos;
using KRealEstate.Data.Models;
using KRealEstate.ViewModels.Catalog.Banner;
using KRealEstate.ViewModels.Common;
using KRealEstate.ViewModels.Common.API;
using Microsoft.EntityFrameworkCore;

namespace KRealEstate.Application.Banners
{
    public class BannerService : IBannerService
    {
        #region Constructor
        private readonly RealEstateDBContext _context;
        private readonly SEO _extension;
        private readonly IStorageService _storageService;
        private const string ChildPath = "banner";
        public BannerService(RealEstateDBContext context, IStorageService storageService)
        {
            _context = context;
            _extension = new SEO();
            _storageService = storageService;
        }
        #endregion
        #region Get All
        public async Task<PageResult<BannerViewModel>> GetAll(int pageSize, int pageIndex, string? keyWord)
        {
            var query = await _context.Banners.Skip((pageIndex - 1) * pageSize)
                .Take(pageSize).Select(x => new BannerViewModel()
                {
                    NameBanner = x.NameBanner,
                    Alt = x.Alt,
                    Id = x.Id,
                    Image = x.Image,
                    Text = x.Text
                }).ToListAsync();
            if (!string.IsNullOrEmpty(keyWord))
            {
                query = query.Where(x => x.NameBanner.Contains(keyWord)).ToList();
            }
            var totalRecord = await _context.Banners.CountAsync();
            var result = new PageResult<BannerViewModel>()
            {
                TotalRecord = totalRecord,
                Items = query,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            return result;
        }
        #endregion
        #region Create
        public async Task<ResultApi<string>> CreateBanner(CreateBannerRequest request)
        {
            foreach (var image in request.Images)
            {
                Guid id = Guid.NewGuid();
                var bannerDto = new BannerDTO()
                {
                    Id = id.ToString(),
                    Alt = _extension.SEOUrl(request.NameBanner),
                    NameBanner = request.NameBanner,
                    Text = request.Text,
                };
                if (request.Images != null)
                {
                    bannerDto.Image = await _storageService.SaveFile(image, ChildPath);
                }
                else
                {
                    bannerDto.Image = "no-image-display";
                }
                Banner banner = new Banner();
                banner.BannerMapping(bannerDto);
                _context.Banners.Add(banner);
            }
            await _context.SaveChangesAsync();
            return new ResultApiSuccess<string>("Create Banner Successful!");
        }
        #endregion
        #region Edit
        public async Task<ResultApi<bool>> EditBanner(string id, EditBannerRequest request)
        {
            var banner = await _context.Banners.FindAsync(id);
            if (banner == null)
            {
                return new ResultApiError<bool>("Not found banner");
            }
            banner.NameBanner = request.NameBanner != null ? request.NameBanner : banner.NameBanner;
            banner.Alt = _extension.SEOUrl(request.NameBanner);
            if (request.Image == null)
            {
                banner.Image = banner.Image;
                await _context.SaveChangesAsync();
            }
            else
            {
                string fullPath = "wwwroot" + banner.Image;
                if (File.Exists(fullPath))
                {
                    await Task.Run(() =>
                    {
                        File.Delete(fullPath);
                    });
                }
                if (request.Image != null)
                {
                    banner.Image = await _storageService.SaveFile(request.Image, ChildPath);
                    _context.Banners.Update(banner);
                    await _context.SaveChangesAsync();
                    return new ResultApiSuccess<bool>();
                }
            }
            return new ResultApiError<bool>("Error");
        }
        #endregion
        #region Delete
        public async Task<ResultApi<bool>> DeleteBanner(string id)
        {
            var banner = await _context.Banners.FindAsync(id);
            if (banner == null)
            {
                return new ResultApiError<bool>("Not found banner");
            }
            string fullPath = "wwwroot" + banner.Image;
            if (File.Exists(fullPath))
            {
                await Task.Run(() =>
                {
                    File.Delete(fullPath);
                });
            }
            _context.Banners.Remove(banner);
            await _context.SaveChangesAsync();
            return new ResultApiSuccess<bool>();
        }
        #endregion
    }
}
