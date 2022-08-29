namespace KRealEstate.ViewModels.Common
{
    public class PagingProduct
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public string? Keyword { get; set; }
        public string? CategoryId { get; set; }
        public string? Slug { get; set; }
        public string? PriceOrder { get; set; }
        public int? PriceTo { get; set; }
        public int? PriceFrom { get; set; }
        public int? AreaTo { get; set; }
        public int? AreaFrom { get; set; }
        public string? DirectionId { get; set; }
        public int? BedRoom { get; set; }
        public bool? HaveImages { get; set; }
        public bool? HaveVideo { get; set; }
        public string? ProvinceCode { get; set; }
        public string? WardCode { get; set; }
        public string? DistrictCode { get; set; }
    }
}
