using KRealEstate.ViewModels.Catalog.Categories;
using KRealEstate.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KRealEstate.Application.Catalog.Categories
{
    public interface ICategoryService
    {
        public Task<string> Create(CategoryCreateRequest request);
        public Task<bool> Edit(string id, CategoryEditRequest request);
        public Task<bool> Delete(CategoryDeleteRequest request);
        public Task<PageResult<CategoryViewModel>>GetAll(PagingPageRequest request);
        public Task<CategoryViewModel> GetById(string id);
        public Task<CategoryViewModel> GetBySlug(string slug);
    }
}
