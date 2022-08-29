using Microsoft.AspNetCore.Http;

namespace KRealEstate.ViewModels.Catalog.Banner
{
    public class EditBannerRequest
    {
        public string NameBanner { get; set; } = null!;
        public string? Text { get; set; }
        public IFormFile? Image { get; set; }
    }
}
