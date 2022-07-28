using KRealEstate.Application.Common;
using KRealEstate.Data.DBContext;
using KRealEstate.Data.Dtos;
using KRealEstate.Data.Models;
using KRealEstate.ViewModels.Catalog.Categories;
using KRealEstate.ViewModels.Common;
using Microsoft.EntityFrameworkCore;

namespace KRealEstate.Application.Catalog.Categories
{
    public class CategoryService : ICategoryService
    {
        private readonly RealEstateDBContext _context;
        private SEO _utilities;
        public CategoryService(RealEstateDBContext context)
        {
            _context = context;
            _utilities = new SEO();
        }
        public async Task<string> Create(CategoryCreateRequest request)
        {
            Guid g = Guid.NewGuid();
            var categoryDTO = new CategoryDTO()
            {
                Id = g.ToString(),
                NameCategory = request.NameCategory,
                ParentId = request.ParentId,
                Slug = _utilities.SEOUrl(request.NameCategory),
                SortOrder = request.SortOrder,
                IsShowWeb = request.IsShowWeb,
                Type = request.Type,
            };
            Category category = new Category();
            category.MappingCategory(categoryDTO);
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return categoryDTO.Id;
        }

        public async Task<bool> Delete(CategoryDeleteRequest request)
        {
            var cate = await _context.Categories.FindAsync(request.Id);
            if (cate == null)
            {
                return false;
            }
            _context.Categories.Remove(cate);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> Edit(string id, CategoryEditRequest request)
        {
            var cate = await _context.Categories.FindAsync(id);
            if (cate == null)
            {
                return false;
            }
            cate.NameCategory = request.NameCategory;
            cate.ParentId = request.ParentId;
            cate.IsShowWeb = request.IsShowWeb;
            cate.Type = request.Type;
            cate.SortOrder = request.SortOrder;
            cate.Slug = _utilities.SEOUrl(request.NameCategory);
            _context.Categories.Update(cate);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<PageResult<CategoryViewModel>> GetAll(PagingPageRequest request)
        {
            var query = from c in _context.Categories
                        select c;
            var count = await query.CountAsync();
            if (request.Keyword != null)
            {
                query = query.Where(x => x.NameCategory.Contains(request.Keyword));
            }
            var items = await query.Skip((request.PageSize - 1) * request.PageIndex)
                .Take(request.PageSize)
                .Select(x => new CategoryViewModel()
                {
                    Id = x.Id,
                    NameCategory = x.NameCategory,
                    ParentId = x.ParentId,
                    IsShowWeb = x.IsShowWeb,
                    Type = x.Type,
                    SortOrder = x.SortOrder,
                    Slug = x.Slug,
                }).ToListAsync();
            var pageResult = new PageResult<CategoryViewModel>()
            {
                Items = items,
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                TotalRecord = count
            };
            return pageResult;
        }

        public async Task<CategoryViewModel> GetById(string id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return null;
            }
            List<string> listChild = new List<string>();
            foreach (var item in _context.Categories)
            {
                if (item.ParentId.Equals(category.Id))
                {
                    listChild.Add(item.NameCategory);
                }
            }
            var categoryVm = new CategoryViewModel()
            {
                Slug = category.Slug,
                Id = id,
                IsShowWeb = category.IsShowWeb,
                NameCategory = category.NameCategory,
                ParentId = category.ParentId,
                SortOrder = category.SortOrder,
                Type = category.Type,
                ListChildCates = listChild
            };
            return categoryVm;
        }

        public async Task<CategoryViewModel> GetBySlug(string slug)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Slug.Equals(slug));
            if (category == null)
            {
                return null;
            }
            List<string> listChild = new List<string>();
            foreach (var item in _context.Categories)
            {
                if (item.ParentId.Equals(category.Id))
                {
                    listChild.Add(item.NameCategory);
                }
            }
            var categoryVm = new CategoryViewModel()
            {
                Slug = slug,
                Id = category.Id,
                IsShowWeb = category.IsShowWeb,
                NameCategory = category.NameCategory,
                ParentId = category.ParentId,
                SortOrder = category.SortOrder,
                Type = category.Type,
                ListChildCates = listChild
            };
            return categoryVm;
        }
    }
}
