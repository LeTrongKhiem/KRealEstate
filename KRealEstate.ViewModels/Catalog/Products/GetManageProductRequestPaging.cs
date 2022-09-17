using KRealEstate.ViewModels.Common;

namespace KRealEstate.ViewModels.Catalog.Products
{
    public class GetManageProductRequestPaging : PagingPageRequest
    {
        public string? Keyword { get; set; }
        public string? CategoryId { get; set; }
        public string? Slug { get; set; }
        public string? BrandId { get; set; }
        public string? Price { get; set; }
    }
}
