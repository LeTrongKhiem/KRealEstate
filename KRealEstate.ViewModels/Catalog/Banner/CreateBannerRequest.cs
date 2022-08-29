using Microsoft.AspNetCore.Http;

namespace KRealEstate.ViewModels.Catalog.Banner
{
    public class CreateBannerRequest
    {
        public string NameBanner { get; set; } = null!;
        public string Image { get; set; } = null!;
        public string Alt { get; set; } = null!;
        public string? Text { get; set; }
        public List<IFormFile> Images { get; set; }
    }
}
