using Microsoft.AspNetCore.Http;

namespace KRealEstate.ViewModels.Catalog.Images
{
    public class CreateImageRequest
    {
        public string ProductId { get; set; }
        public string ImageId { get; set; }
        public List<IFormFile> Images { get; set; }
        public bool IsThumbnail { get; set; }
        public string Path { get; set; }
    }
}
