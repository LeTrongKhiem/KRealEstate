using KRealEstate.ViewModels.Catalog.Product;
using KRealEstate.ViewModels.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KRealEstate.Application.Catalog.Products
{
    public interface IProductService
    {
        public Task<PageResult<ProductViewModel>> GetAllPaging(PagingProduct request);
        public Task<string> PostProduct(PostProductRequest request);
    }
}
