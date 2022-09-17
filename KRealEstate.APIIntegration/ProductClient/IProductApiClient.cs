using KRealEstate.ViewModels.Catalog.Product;
using KRealEstate.ViewModels.Common;

namespace KRealEstate.APIIntegration.ProductClient
{
    public interface IProductApiClient
    {
        Task<PageResult<ProductViewModel>> GetAll(PagingProduct request);
        Task<bool> Create(PostProductRequest request);
        Task<bool> Delete(string id);
        //Task<bool> Edit(string id, ProductEditRequest request);
        Task<ProductViewModel> GetById(string id);
        //Task<bool> CategoryAssign(string id, CategoryAssignRequest request);
        //Task<bool> PromotionAssign(string id, PromotionAssignRequest request);
    }
}
