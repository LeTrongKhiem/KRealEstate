using KRealEstate.Data.DBContext;
using KRealEstate.ViewModels.Catalog.Product;
using KRealEstate.ViewModels.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KRealEstate.Application.Catalog.Products
{
    public class ProductService : IProductService
    {
        private readonly RealEstateDBContext _context;
        public ProductService(RealEstateDBContext context)
        {
            _context = context;
        }
        public async Task<PageResult<ProductViewModel>> GetAllPaging(PagingProduct request)
        {
            var cateBySlug = await _context.Categories.FirstOrDefaultAsync(x => x.Slug.Equals(request.Slug));
            var query = from p in _context.Products
                        join pmc in _context.ProductMapCategories
                        on p.Id equals pmc.ProductId into ppmc
                        from pmc in ppmc.DefaultIfEmpty()
                        join c in _context.Categories
                        on pmc.CategoryId equals c.Id
                        into pmcc
                        from c in pmcc.DefaultIfEmpty()
                        join pi in _context.ProductImages
                        on p.Id equals pi.ProductId
                        into ppi
                        from pi in ppi.DefaultIfEmpty()
                        join add in _context.Addresses
                        on p.AddressId equals add.Id
                        into padd
                        from add in padd.DefaultIfEmpty()
                        join pro in _context.Provinces
                        on add.ProviceCode equals pro.Code
                        into addpro
                        from pro in addpro.DefaultIfEmpty()
                        where pi.IsThumbnail == true
                        select new { p, pmc, c, pi, add, pro };
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.c.Slug.Equals(request.Slug) || x.c.ParentId == cateBySlug.Id);
            }
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.p.Name.Contains(request.Keyword) || x.pro.Name.Contains(request.Keyword));
            }
            if (request.PriceTo != 0 && request.PriceFrom != 0)
            {
                query = query.Where(x => (x.p.Price >= request.PriceFrom && x.p.Price <= request.PriceTo));
            }
            if (request.AreaTo != 0 && request.AreaFrom != 0)
            {
                query = query.Where(x => (x.p.Area >= request.AreaFrom && x.p.Area <= request.AreaTo));
            }
            if (request.DirectionId != null)
            {
                query = query.Where(x => x.p.DirectionId.Equals(request.DirectionId));
            }
            if (request.BedRoom != 0)
            {
                query = query.Where(x => x.p.Bedroom == request.BedRoom);
            }
            //if (request.HaveImages)
            //{
            //    if (query.Where(x => x.pi.ProductId == x.p.Id).Any())
            //    {
            //        query = 
            //    }
            //}
            var countRecord = await _context.Products.CountAsync();
            var items = await query.Skip((request.PageSize - 1) * request.PageIndex)
                                .Take(request.PageSize)
                                .Select(x => new ProductViewModel()
                                {
                                    AddressDisplay = x.p.AddressDisplay,
                                    Area = x.p.Area,
                                    Description = x.p.Description,
                                    Name = x.p.Name,
                                    Price = x.p.Price,
                                    ThumbnailImage = x.pi.Path
                                }).ToListAsync();
            var pageResult = new PageResult<ProductViewModel>()
            {
                Items = items,
                TotalRecord = countRecord,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
            };
            return pageResult;
        }

        public Task<string> PostProduct(PostProductRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
