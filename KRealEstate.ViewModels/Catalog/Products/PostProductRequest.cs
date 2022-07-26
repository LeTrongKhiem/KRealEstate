using Microsoft.AspNetCore.Http;

namespace KRealEstate.ViewModels.Catalog.Product
{
    public class PostProductRequest
    {
        public string Id { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string AddressId { get; set; } = null!;
        public decimal Price { get; set; }
        public int Area { get; set; }
        public int? Bedroom { get; set; }
        public string? Description { get; set; }
        public int? ToletRoom { get; set; }
        public int? ViewCount { get; set; }
        public string? DirectionId { get; set; }
        public bool? IsShowWeb { get; set; }
        public int? Floor { get; set; }
        public string? Project { get; set; }
        public string? AddressDisplay { get; set; }
        public string? Furniture { get; set; }
        public string? Slug { get; set; }
        //address
        public string ProviceCode { get; set; }
        public string DistrictCode { get; set; }
        public string WardCode { get; set; }
        public int RegionId { get; set; }
        public int UnitId { get; set; }
        //contact info
        public string NameContact { get; set; }
        public string PhoneContact { get; set; }
        public string AddressContact { get; set; }
        public string EmailContact { get; set; }
        //images
        public List<IFormFile> ThumbnailImages { get; set; }
        //type post
        public int DayLengthPost { get; set; }
        public DateTime? DayPostStart { get; set; }
        public DateTime? DayPostEnd { get; set; }
        public string? PostTypeId { get; set; }

        public List<string> CategoryId { get; set; } = new List<string>();
        public string UserId { get; set; } = null!;
        public DateTime DatePost { get; set; }
    }
}
