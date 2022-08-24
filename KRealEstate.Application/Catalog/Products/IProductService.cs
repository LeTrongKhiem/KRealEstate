using KRealEstate.ViewModels.Catalog.Assigns;
using KRealEstate.ViewModels.Catalog.Images;
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
        public Task<int> UpdateProduct(string id, ProductDetailViewModel request);
        public Task<List<ProductViewModel>> GetProjectOutStanding(int quantity, bool typeProject);
        public Task<PageResult<ProductViewModel>> GetProductByProvinceId(PagingProvince request);
        public Task<int> GetPostCountByProvinceId(string provinceId);
        //assign method
        public Task<bool> CategoryAssign(string id, CategoryAssignRequest request);

        //update images method
        public Task<List<ImageViewModel>> GetListImage(string productID);
        public Task<string> CreateImages(CreateImageRequest request);
        public Task<int> UpdateImages(string imageId, UpdateImageRequest request);
        public Task<int> RemoveImage(string imageId);

    }
}
