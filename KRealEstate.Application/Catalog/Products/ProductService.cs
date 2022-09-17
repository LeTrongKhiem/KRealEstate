using KRealEstate.Application.Common;
using KRealEstate.Data.DBContext;
using KRealEstate.Data.Models;
using KRealEstate.ViewModels.Catalog.Assigns;
using KRealEstate.ViewModels.Catalog.Images;
using KRealEstate.ViewModels.Catalog.Product;
using KRealEstate.ViewModels.Catalog.Products;
using KRealEstate.ViewModels.Common;
using KRealEstate.ViewModels.System.Addresss;
using Microsoft.EntityFrameworkCore;

namespace KRealEstate.Application.Catalog.Products
{
    public class ProductService : IProductService
    {
        #region Constructor
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
        #endregion
        #region ViewCount
        public async Task<int> AddViewCount(string id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return 0;
            }
            product.ViewCount += 1;
            return await _context.SaveChangesAsync();
        }

        #endregion
        #region DeletePost
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
        #endregion
        #region ShowProduct
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
            if (request.ProvinceCode != null)
            {
                query = query.Where(x => x.add.ProviceCode.Equals(request.ProvinceCode));
            }
            if (request.WardCode != null)
            {
                query = query.Where(x => x.add.WardCode.Equals(request.WardCode));
            }
            if (request.DistrictCode != null)
            {
                query = query.Where(x => x.add.DistrictCode.Equals(request.DistrictCode));
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
        #endregion
        #region GetById
        public async Task<ProductDetailViewModel> GetById(string id)
        {
            var product = await _context.Products.FindAsync(id);
            var productImages = await (from p in _context.Products
                                       join pi in _context.ProductImages
                                       on p.Id equals pi.ProductId
                                       where pi.ProductId == id
                                       select pi.Path).ToListAsync();
            var categories = await (from c in _context.Categories
                                    join pmc in _context.ProductMapCategories
                                    on c.Id equals pmc.CategoryId
                                    where pmc.ProductId == id
                                    select c.NameCategory).ToListAsync();
            var postDetail = await _context.PostDetails.FirstOrDefaultAsync(x => x.ProductId == id);
            var query = (from pro in _context.Products
                         join postdetail in _context.PostDetails
                         on pro.Id equals postdetail.ProductId into ppdt
                         from postdetail in ppdt.DefaultIfEmpty()
                         join post in _context.Posts on postdetail.Id equals post.PostId into pdtp
                         from post in pdtp.DefaultIfEmpty()
                         join posttype in _context.PostTypes on postdetail.PostTypeId equals posttype.Id
                         into ptpdt
                         from posttype in ptpdt.DefaultIfEmpty()
                         join address in _context.Addresses on pro.AddressId equals address.Id
                         into proa
                         from address in proa.DefaultIfEmpty()
                         join di in _context.Directions on pro.DirectionId equals di.Id
                         into prodi
                         from di in prodi.DefaultIfEmpty()

                         where pro.Id == id
                         select new { pro, postdetail, posttype, post, address, di });
            if (product == null)
            {
                return null;
            }
            var result = await query.Select(x => new ProductDetailViewModel()
            {
                Id = x.pro.Id,
                Name = x.pro.Name,
                AddressId = x.pro.AddressId,
                Price = x.pro.Price,
                Area = x.pro.Area,
                Bedroom = x.pro.Bedroom,
                Description = x.pro.Description,
                ToletRoom = x.pro.ToletRoom,
                ViewCount = x.pro.ViewCount,
                DirectionId = x.pro.DirectionId,
                IsShowWeb = x.pro.IsShowWeb,
                Floor = x.pro.Floor,
                Project = x.pro.Project,
                AddressDisplay = x.pro.AddressDisplay,
                Furniture = x.pro.Furniture,
                Slug = x.pro.Slug,
                ListCategory = categories,
                ListImages = productImages,
                AddressVm = new ViewModels.Catalog.Addresss.AddressViewModel()
                {
                    Id = x.address.Id,
                    DistrictCode = x.address.DistrictCode,
                    ProviceCode = x.address.ProviceCode,
                    RegionId = x.address.RegionId,
                    UnitId = x.address.UnitId,
                    WardCode = x.address.WardCode
                },
                DirectionVm = new ViewModels.Catalog.Addresss.DirectionViewModel()
                {
                    Id = x.di.Id,
                    Name = x.di.Name
                },
                PostDetailVm = new ViewModels.Catalog.Posts.PostDetailViewModel()
                {
                    Id = x.postdetail.Id,
                    ProductId = x.postdetail.ProductId,
                    DayLengthPost = x.postdetail.DayLengthPost,
                    DayPostEnd = x.postdetail.DayPostEnd,
                    DayPostStart = x.postdetail.DayPostStart,
                    PostTypeId = x.postdetail.PostTypeId,

                },
                PostTypeVm = new ViewModels.Catalog.Posts.PostTypeViewModel()
                {
                    Id = x.posttype.Id,
                    NamePostType = x.posttype.NamePostType
                },
                PostVm = new ViewModels.Catalog.Posts.PostViewModel()
                {
                    Id = x.post.Id,
                    DatePost = x.post.DatePost,
                    PostId = x.post.PostId,
                    Status = x.post.Status,
                    UserId = x.post.UserId
                }
            }).FirstOrDefaultAsync();
            return result;
        }
        #endregion
        #region GetBySlug
        public async Task<ProductDetailViewModel> GetBySlug(string slug)
        {
            var product = await _context.Products.FirstOrDefaultAsync(x => x.Slug == slug);
            var productImages = await (from p in _context.Products
                                       join pi in _context.ProductImages
                                       on p.Id equals pi.ProductId
                                       where pi.ProductId == product.Id
                                       select pi.Path).ToListAsync();
            var categories = await (from c in _context.Categories
                                    join pmc in _context.ProductMapCategories
                                    on c.Id equals pmc.CategoryId
                                    where pmc.ProductId == product.Id
                                    select c.NameCategory).ToListAsync();
            var postDetail = await _context.PostDetails.FirstOrDefaultAsync(x => x.ProductId == product.Id);
            var query = (from pro in _context.Products
                         join postdetail in _context.PostDetails
                         on pro.Id equals postdetail.ProductId into ppdt
                         from postdetail in ppdt.DefaultIfEmpty()
                         join post in _context.Posts on postdetail.Id equals post.PostId into pdtp
                         from post in pdtp.DefaultIfEmpty()
                         join posttype in _context.PostTypes on postdetail.PostTypeId equals posttype.Id
                         into ptpdt
                         from posttype in ptpdt.DefaultIfEmpty()
                         join address in _context.Addresses on pro.AddressId equals address.Id
                         into proa
                         from address in proa.DefaultIfEmpty()
                         join di in _context.Directions on pro.DirectionId equals di.Id
                         into prodi
                         from di in prodi.DefaultIfEmpty()

                         where pro.Id == product.Id
                         select new { pro, postdetail, posttype, post, address, di });
            if (product == null)
            {
                return null;
            }
            var result = await query.Select(x => new ProductDetailViewModel()
            {
                Id = x.pro.Id,
                Name = x.pro.Name,
                AddressId = x.pro.AddressId,
                Price = x.pro.Price,
                Area = x.pro.Area,
                Bedroom = x.pro.Bedroom,
                Description = x.pro.Description,
                ToletRoom = x.pro.ToletRoom,
                ViewCount = x.pro.ViewCount,
                DirectionId = x.pro.DirectionId,
                IsShowWeb = x.pro.IsShowWeb,
                Floor = x.pro.Floor,
                Project = x.pro.Project,
                AddressDisplay = x.pro.AddressDisplay,
                Furniture = x.pro.Furniture,
                Slug = x.pro.Slug,
                ListCategory = categories,
                ListImages = productImages,
                AddressVm = new ViewModels.Catalog.Addresss.AddressViewModel()
                {
                    Id = x.address.Id,
                    DistrictCode = x.address.DistrictCode,
                    ProviceCode = x.address.ProviceCode,
                    RegionId = x.address.RegionId,
                    UnitId = x.address.UnitId,
                    WardCode = x.address.WardCode
                },
                DirectionVm = new ViewModels.Catalog.Addresss.DirectionViewModel()
                {
                    Id = x.di.Id,
                    Name = x.di.Name
                },
                PostDetailVm = new ViewModels.Catalog.Posts.PostDetailViewModel()
                {
                    Id = x.postdetail.Id,
                    ProductId = x.postdetail.ProductId,
                    DayLengthPost = x.postdetail.DayLengthPost,
                    DayPostEnd = x.postdetail.DayPostEnd,
                    DayPostStart = x.postdetail.DayPostStart,
                    PostTypeId = x.postdetail.PostTypeId,

                },
                PostTypeVm = new ViewModels.Catalog.Posts.PostTypeViewModel()
                {
                    Id = x.posttype.Id,
                    NamePostType = x.posttype.NamePostType
                },
                PostVm = new ViewModels.Catalog.Posts.PostViewModel()
                {
                    Id = x.post.Id,
                    DatePost = x.post.DatePost,
                    PostId = x.post.PostId,
                    Status = x.post.Status,
                    UserId = x.post.UserId
                }
            }).FirstOrDefaultAsync();
            return result;
        }
        #endregion
        #region PostProduct
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
            var query = from p in _context.Provinces
                        join d in _context.Districts on p.Code equals d.ProvinceCode
                        into pd
                        from d in pd.DefaultIfEmpty()
                        join w in _context.Wards on d.Code equals w.DistrictCode
                        into dw
                        from w in dw.DefaultIfEmpty()
                        where w.Code.Equals(request.WardCode)
                        select new { p, d, w, pd, dw };
            //Address address = null;
            //Guid gAddress = Guid.NewGuid();
            //if (_context.Addresses.Any(x => x.UnitId == request.UnitId && x.ProviceCode.Equals(request.ProviceCode)
            //                    && x.DistrictCode.Equals(request.DistrictCode) && x.WardCode.Equals(request.WardCode)
            //                    && x.RegionId == request.RegionId))
            //{
            //    address = await (from add in _context.Addresses
            //                     where add.UnitId == request.UnitId && add.RegionId == request.RegionId
            //                     && add.ProviceCode.Equals(request.ProviceCode) && add.DistrictCode.Equals(request.DistrictCode)
            //                     && add.WardCode.Equals(request.WardCode)
            //                     select add).FirstOrDefaultAsync();
            //}
            //else
            //{
            //    address = new Address()
            //    {
            //        Id = gAddress.ToString(),
            //        DistrictCode = request.DistrictCode,
            //        ProviceCode = request.ProviceCode,
            //        WardCode = request.WardCode,
            //        RegionId = request.RegionId,
            //        UnitId = request.UnitId,
            //    };
            //    _context.Addresses.Add(address);
            //}
            var wardCode = await query.Select(x => new WardViewModel()
            {
                Code = x.w.Code
            }).FirstOrDefaultAsync();
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
                AddressId = wardCode.Code,
                IsShowWeb = request.IsShowWeb,
                ProductMapCategories = listCate,
                ViewCount = 0
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

        #endregion
        #region UpdateProduct
        public async Task<int> UpdateProduct(string id, ProductDetailViewModel request)
        {
            var product = await _context.Products.FindAsync(id);
            var postDetail = await _context.PostDetails.FirstOrDefaultAsync(x => x.ProductId == id);
            var post = await _context.Posts.FirstOrDefaultAsync(x => x.PostId == postDetail.Id);
            var images = await _context.ProductImages.FirstOrDefaultAsync(x => x.ProductId == id);
            if (product == null)
            {
                return 0;
            }
            product.Name = request.Name != null ? request.Name : product.Name;
            product.Price = request.Price != null ? request.Price : product.Price;
            product.Description = request.Description != null ? request.Description : product.Description;
            product.Area = request.Area != null ? request.Area : product.Area;
            product.Bedroom = request.Bedroom != null ? request.Bedroom : product.Bedroom;
            product.ToletRoom = request.ToletRoom != null ? request.ToletRoom : product.ToletRoom;
            product.IsShowWeb = request.IsShowWeb != null ? request.IsShowWeb : product.IsShowWeb;
            product.Floor = request.Floor != null ? request.Floor : product.Floor;
            product.Project = request.Project != null ? request.Project : product.Project;
            product.AddressDisplay = request.AddressDisplay != null ? request.AddressDisplay : product.AddressDisplay;
            product.Furniture = request.Furniture != null ? request.Furniture : product.Furniture;
            postDetail.DayLengthPost = request.PostDetailVm != null ? request.PostDetailVm.DayLengthPost : postDetail.DayLengthPost;
            postDetail.DayPostStart = request.PostDetailVm.DayPostStart != null ? request.PostDetailVm.DayPostStart : postDetail.DayPostStart;
            DateTime endDate = Convert.ToDateTime(postDetail.DayPostStart);
            Int64 addedDays = Convert.ToInt64(postDetail.DayLengthPost);
            endDate = endDate.AddDays(addedDays);
            postDetail.DayPostEnd = endDate;
            postDetail.PostTypeId = request.PostDetailVm.PostTypeId != null ? request.PostDetailVm.PostTypeId : postDetail.PostTypeId;
            if (request.ThumbnailImages == null)
            {
                images.Path = images.Path;
                return await _context.SaveChangesAsync();
            }
            else
            {
                string fullPath = "wwwroot" + images.Path;
                if (File.Exists(fullPath))
                {
                    await Task.Run(() =>
                    {
                        File.Delete(fullPath);
                    });
                }
                if (request.ThumbnailImages != null)
                {
                    var thumbnailImages = await _context.ProductImages.FirstOrDefaultAsync(x => x.ProductId == id);
                    if (thumbnailImages != null)
                    {
                        thumbnailImages.Path = await _storageService.SaveFile(request.ThumbnailImages, CHILD_PATH);
                        thumbnailImages.Alt = product.Name;
                        _context.ProductImages.Update(thumbnailImages);
                    }
                }
                return await _context.SaveChangesAsync();
            }
        }
        #endregion
        #region CategoryAssign
        public async Task<bool> CategoryAssign(string id, CategoryAssignRequest request)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return false;
            }
            foreach (var cate in request.Categories)
            {
                var productMapCate = await _context.ProductMapCategories.FirstOrDefaultAsync(x => x.ProductId == id && x.CategoryId == cate.Id);
                if (productMapCate != null && cate.Selected == false)
                {
                    _context.ProductMapCategories.Remove(productMapCate);
                }
                else if (productMapCate == null && cate.Selected == true)
                {
                    await _context.ProductMapCategories.AddAsync(new ProductMapCategory()
                    {
                        CategoryId = cate.Id,
                        ProductId = id,
                    });
                }
            }
            return await _context.SaveChangesAsync() > 0;
        }
        #endregion
        #region CreateImage
        public async Task<string> CreateImages(CreateImageRequest request)
        {
            var product = await _context.Products.FindAsync(request.ProductId);
            if (product == null)
            {
                return null;
            }
            Guid gImage = Guid.NewGuid();
            var productImage = new ProductImage()
            {
                Id = gImage.ToString(),
                Alt = request.ProductId.ToString(),
                IsThumbnail = request.IsThumbnail,
                ProductId = product.Id,
            };
            foreach (var image in request.Images)
            {
                if (request.Images != null)
                {
                    productImage.Path = await _storageService.SaveFile(image, CHILD_PATH);
                }
                _context.ProductImages.Add(productImage);
            }
            await _context.SaveChangesAsync();
            return productImage.Id;
        }
        #endregion
        #region GetListImage 
        public async Task<List<ImageViewModel>> GetListImage(string productID)
        {
            var images = await _context.ProductImages.Where(x => x.ProductId.Equals(productID)).Select(x => new ImageViewModel()
            {
                Alt = x.Alt,
                Id = x.Id,
                IsThumbnail = x.IsThumbnail,
                Path = x.Path
            }).ToListAsync();
            return images;
        }
        #endregion
        #region Update Image
        public async Task<int> UpdateImages(string imageId, UpdateImageRequest request)
        {
            var productImages = await _context.ProductImages.FindAsync(imageId);
            if (productImages == null)
            {
                throw new Exception("Not found");
            }
            if (request.Images != null)
            {
                string fullPath = "wwwroot" + productImages.Path;
                File.Delete(fullPath);
                productImages.Path = await _storageService.SaveFile(request.Images, CHILD_PATH);
                productImages.IsThumbnail = request.IsThumbnail;
                _context.ProductImages.Update(productImages);
                return await _context.SaveChangesAsync();
            }
            else
            {
                productImages.Path = productImages.Path;
                productImages.IsThumbnail = request.IsThumbnail;
                _context.ProductImages.Update(productImages);
                return await _context.SaveChangesAsync();
            }
        }
        #endregion
        #region Remove Image
        public async Task<int> RemoveImage(string imageId)
        {
            var productImages = await _context.ProductImages.FindAsync(imageId);
            if (productImages == null)
            {
                throw new Exception("Not found");
            }
            var fullPath = "wwwroot" + productImages.Path;
            if (File.Exists(fullPath))
            {
                await Task.Run(() =>
                {
                    File.Delete(fullPath);
                });
            }
            _context.ProductImages.Remove(productImages);
            return await _context.SaveChangesAsync();
        }
        #endregion
        #region Get Project Outstanding
        public async Task<List<ProductViewModel>> GetProjectOutStanding(int quantity, bool typeProject)
        {
            var query = (from p in _context.Products
                         join pi in _context.ProductImages
                         on p.Id equals pi.ProductId
                         into ppi
                         from pi in ppi.DefaultIfEmpty()
                         where p.IsShowWeb == true && p.Project == typeProject && pi.IsThumbnail == true
                         orderby p.ViewCount
                         select new { p, pi }).Take(quantity);
            var products = await query.Select(x => new ProductViewModel()
            {
                ThumbnailImage = x.pi.Path,
                AddressDisplay = x.p.AddressDisplay,
                Area = x.p.Area,
                Description = x.p.Description,
                Name = x.p.Name,
                Price = x.p.Price
            }).ToListAsync();
            return products;
        }
        #endregion
        #region Get Product By Province Id
        public async Task<PageResult<ProductViewModel>> GetProductByProvinceId(PagingProvince request)
        {
            var query = from p in _context.Products
                        join address in _context.Addresses on p.AddressId equals address.Id
                        into paddress
                        from address in paddress.DefaultIfEmpty()
                        join pi in _context.ProductImages
                        on p.Id equals pi.ProductId
                        into ppi
                        from pi
                        in ppi.DefaultIfEmpty()
                        where address.ProviceCode == request.ProvinceId && pi.IsThumbnail == true
                        select new { p, address, pi };
            var totalCount = await query.CountAsync();
            var items = await query.Skip((request.PageIndex - 1) * request.PageSize)
                                .Take(request.PageSize)
                                .Select(x => new ProductViewModel()
                                {
                                    ThumbnailImage = x.pi.Path,
                                    AddressDisplay = x.p.AddressDisplay,
                                    Area = x.p.Area,
                                    Description = x.p.Description,
                                    Name = x.p.Name,
                                    Price = x.p.Price
                                }).ToListAsync();
            var result = new PageResult<ProductViewModel>()
            {
                Items = items,
                TotalRecord = totalCount,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize
            };
            return result;
        }
        #endregion
        #region Get quantity product in province 
        public async Task<int> GetPostCountByProvinceId(string provinceId)
        {
            var query = from p in _context.Products
                        join address in _context.Addresses on p.AddressId equals address.Id
                        into paddress
                        from address in paddress.DefaultIfEmpty()
                        where address.ProviceCode == provinceId
                        select p;
            int count = await query.CountAsync();
            return count;
        }
        #endregion

    }
}

