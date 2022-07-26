using KRealEstate.Application.Common;
using KRealEstate.Data.DBContext;
using KRealEstate.Data.Models;
using KRealEstate.ViewModels.Catalog.Product;
using KRealEstate.ViewModels.Common;
using Microsoft.EntityFrameworkCore;

namespace KRealEstate.Application.Catalog.Products
{
    public class ProductService : IProductService
    {
        private readonly RealEstateDBContext _context;
        private readonly Utilities _utilities;
        private readonly IStorageService _storageService;
        private string CHILD_PATH = "image-product";
        public ProductService(RealEstateDBContext context, IStorageService storageService)
        {
            _context = context;
            _utilities = new Utilities();
            _storageService = storageService;
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
                        join dis in _context.Districts
                        on add.ProviceCode equals dis.Code
                        into adddis
                        from dis in adddis.DefaultIfEmpty()
                        where pi.IsThumbnail == true
                        select new { p, pmc, c, pi, add, pro, dis };
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.c.Slug.Equals(request.Slug) || x.c.ParentId == cateBySlug.Id || x.pro.Name.Contains(request.Keyword));
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

        public async Task<string> PostProduct(PostProductRequest request)
        {
            Guid g = Guid.NewGuid();
            List<ProductMapCategory> listCate = new List<ProductMapCategory>();
            foreach (var cate in request.CategoryId)
            {
                listCate.Add(new ProductMapCategory()
                {
                    CategoryId = cate.ToString(),
                    ProductId = g.ToString(),
                });
            }
            Guid gAddress = Guid.NewGuid();
            var address = new Address()
            {
                Id = gAddress.ToString(),
                ProviceCode = request.ProviceCode,
                DistrictCode = request.DistrictCode,
                WardCode = request.WardCode,
                RegionId = request.RegionId,
                UnitId = request.UnitId,
            };
            Guid gContact = Guid.NewGuid();
            var contact = new Contact()
            {
                Id = gContact.ToString(),
                Email = request.EmailContact,
                AddressContact = request.AddressContact,
                NameContact = request.NameContact,
                PhoneNumber = request.PhoneContact,
                ProductId = g.ToString()
            };
            var productCreate = new Product()
            {
                Id = g.ToString(),
                Name = request.Name,
                AddressDisplay = request.AddressDisplay,
                Area = request.Area,
                DirectionId = request.DirectionId,
                Floor = request.Floor,
                Description = request.Description,
                Furniture = request.Furniture,
                ToletRoom = request.ToletRoom,
                Price = request.Price,
                Bedroom = request.Bedroom,
                Project = request.Project,
                Slug = _utilities.SEOUrl(request.Name),
                AddressId = address.Id,
                ProductMapCategories = listCate
            };
            foreach (var image in request.ThumbnailImages)
            {
                Guid gImages = Guid.NewGuid();
                var productImage = new ProductImage()
                {
                    Id = gImages.ToString(),
                    ProductId = g.ToString(),
                    Alt = productCreate.Name,
                    IsThumbnail = true,
                };
                if (request.ThumbnailImages != null)
                {
                    productImage.Path = await _storageService.SaveFile(image, CHILD_PATH);
                }
                else
                {
                    productImage.Path = "no-image";
                }
                _context.ProductImages.Add(productImage);
            }
            _context.Addresses.Add(address);
            _context.Contacts.Add(contact);
            _context.Products.Add(productCreate);
            await _context.SaveChangesAsync();
            return productCreate.Id;
        }
    }
}
