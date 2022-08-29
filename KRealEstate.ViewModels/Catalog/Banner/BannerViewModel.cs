namespace KRealEstate.ViewModels.Catalog.Banner
{
    public class BannerViewModel
    {
        public string Id { get; set; } = null!;
        public string NameBanner { get; set; } = null!;
        public string Image { get; set; } = null!;
        public string Alt { get; set; } = null!;
        public string? Text { get; set; }
    }
}
