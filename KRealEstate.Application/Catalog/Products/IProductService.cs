using KRealEstate.ViewModels.Catalog.Product;
using KRealEstate.ViewModels.Catalog.Products;
using KRealEstate.ViewModels.Common;

namespace KRealEstate.Application.Catalog.Products
{
    public interface IProductService
    {
        public Task<PageResult<ProductViewModel>> GetAllPaging(PagingProduct request);
        public Task<string> PostProduct(PostProductRequest request);
        public Task<bool> DeletePostProduct(DeletePostProductRequest request);
        public Task<int> AddViewCount(string id);
        public Task<ProductDetailViewModel> GetById(string id);
        public Task<ProductDetailViewModel> GetBySlug(string slug);
    }
}
