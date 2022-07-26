using KRealEstate.ViewModels.Catalog.Product;
using KRealEstate.ViewModels.Common;

namespace KRealEstate.Application.Catalog.Products
{
    public interface IProductService
    {
        public Task<PageResult<ProductViewModel>> GetAllPaging(PagingProduct request);
        public Task<string> PostProduct(PostProductRequest request);
    }
}
