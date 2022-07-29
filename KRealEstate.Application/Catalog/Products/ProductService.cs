using KRealEstate.Application.Common;
using KRealEstate.Data.DBContext;
using KRealEstate.Data.Models;
using KRealEstate.ViewModels.Catalog.Product;
using KRealEstate.ViewModels.Catalog.Products;
using KRealEstate.ViewModels.Common;
using Microsoft.EntityFrameworkCore;

namespace KRealEstate.Application.Catalog.Products
{
    public class ProductService : IProductService
    {
        private readonly RealEstateDBContext _context;
        private readonly SEO _utilities;
        private readonly IStorageService _storageService;
        private string CHILD_PATH = "image-product";
        public ProductService(RealEstateDBContext context, IStorageService storageService)
        {
            _context = context;
            _utilities = new SEO();
            _storageService = storageService;
        }

        public async Task<bool> DeletePostProduct(DeletePostProductRequest request)
        {
            if (request.Id == null)
            {
                return false;
            }
            var product = await _context.Products.FindAsync(request.Id);
            var productMapCate = await _context.ProductMapCategories.Where(x => x.ProductId == request.Id).ToListAsync();
            var contactInfo = await _context.Contacts.FirstOrDefaultAsync(x => x.ProductId == request.Id);
            var postDetail = await _context.PostDetails.FirstOrDefaultAsync(x => x.ProductId == request.Id);
            var post = await _context.Posts.FirstOrDefaultAsync(x => x.PostId == postDetail.Id);
            var imageProducts = await _context.ProductImages.Where(x => x.ProductId == request.Id).ToListAsync();
            foreach (var imageProduct in imageProducts)
            {
                var pathImage = "wwwroot" + imageProduct.Path;
                if (File.Exists(pathImage))
                {
                    await Task.Run(() =>
                    {
                        File.Delete(pathImage);
                    });
                }
                _context.ProductImages.Remove(imageProduct);
            }
            foreach (var productImg in productMapCate)
            {
                _context.ProductMapCategories.Remove(productImg);
            }
            _context.Products.Remove(product);
            _context.Contacts.Remove(contactInfo);
            _context.PostDetails.Remove(postDetail);
            _context.Posts.Remove(post);
            return await _context.SaveChangesAsync() > 0;
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
                        select new { p, c, pi, add, pro, dis };
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.c.Slug.Equals(request.Slug) || x.c.ParentId == cateBySlug.Id || x.pro.Name.Contains(request.Keyword));
            }
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.p.Name.Contains(request.Keyword) || x.pro.Name.Contains(request.Keyword));
            }
            if (request.PriceTo != null && request.PriceFrom != null)
            {
                query = query.Where(x => (x.p.Price >= request.PriceFrom && x.p.Price <= request.PriceTo));
            }
            if (request.AreaTo != null && request.AreaFrom != null)
            {
                query = query.Where(x => (x.p.Area >= request.AreaFrom && x.p.Area <= request.AreaTo));
            }
            if (request.DirectionId != null)
            {
                query = query.Where(x => x.p.DirectionId.Equals(request.DirectionId));
            }
            if (request.BedRoom != null)
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
            var items = await query.Skip((request.PageIndex - 1) * request.PageSize)
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
            Address address = null;
            Guid gAddress = Guid.NewGuid();
            if (_context.Addresses.Any(x => x.UnitId == request.UnitId && x.ProviceCode.Equals(request.ProviceCode)
                                && x.DistrictCode.Equals(request.DistrictCode) && x.WardCode.Equals(request.WardCode)
                                && x.RegionId == request.RegionId))
            {
                address = await (from add in _context.Addresses
                                 where add.UnitId == request.UnitId && add.RegionId == request.RegionId
                                 && add.ProviceCode.Equals(request.ProviceCode) && add.DistrictCode.Equals(request.DistrictCode)
                                 && add.WardCode.Equals(request.WardCode)
                                 select add).FirstOrDefaultAsync();
            }
            else
            {
                address = new Address()
                {
                    Id = gAddress.ToString(),
                    DistrictCode = request.DistrictCode,
                    ProviceCode = request.ProviceCode,
                    WardCode = request.WardCode,
                    RegionId = request.RegionId,
                    UnitId = request.UnitId,
                };
                _context.Addresses.Add(address);
            }
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
                    ProductId = productCreate.Id,
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
            Guid gContact = Guid.NewGuid();
            var contact = new Contact()
            {
                Id = gContact.ToString(),
                Email = request.EmailContact,
                AddressContact = request.AddressContact,
                NameContact = request.NameContact,
                PhoneNumber = request.PhoneContact,
                ProductId = productCreate.Id
            };
            Guid gPostDetails = Guid.NewGuid();
            var postDetail = new PostDetail()
            {
                Id = gPostDetails.ToString(),
                ProductId = productCreate.Id,
                DayLengthPost = request.DayLengthPost,
                DayPostStart = request.DayPostStart,
                DayPostEnd = request.DayPostEnd,
                PostTypeId = request.PostTypeId,
            };
            Guid gPost = Guid.NewGuid();
            var post = new Post()
            {
                Id = gPost.ToString(),
                PostId = postDetail.Id,
                UserId = request.UserId,
                DatePost = request.DatePost,
                Status = false
            };
            _context.Products.Add(productCreate);
            _context.PostDetails.Add(postDetail);
            _context.Posts.Add(post);
            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();
            return productCreate.Id;
        }
    }
}
