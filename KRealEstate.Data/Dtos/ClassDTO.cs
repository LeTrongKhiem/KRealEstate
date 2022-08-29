using KRealEstate.Data.Models;

namespace KRealEstate.Data.Dtos
{
    //public class ClassDTO
    //{
    #region CategoryDTO
    public class CategoryDTO
    {
        public string Id { get; set; } = null!;
        public string NameCategory { get; set; } = null!;
        public string ParentId { get; set; } = null!;
        public bool? IsShowWeb { get; set; }
        public int? SortOrder { get; set; }
        public string? Slug { get; set; }
        public bool? Type { get; set; }
    }
    #endregion
    #region BannerDTO
    public class BannerDTO
    {
        public string Id { get; set; } = null!;
        public string NameBanner { get; set; } = null!;
        public string Image { get; set; } = null!;
        public string Alt { get; set; } = null!;
        public string? Text { get; set; }
    }
    #endregion
    public static class Mapping
    {
        #region Category Mapping
        public static void MappingCategory(this Category category, CategoryDTO dto)
        {
            category.Id = dto.Id;
            category.NameCategory = dto.NameCategory;
            category.ParentId = dto.ParentId;
            category.IsShowWeb = dto.IsShowWeb;
            category.SortOrder = dto.SortOrder;
            category.Slug = dto.Slug;
            category.Type = dto.Type;
            category.SortOrder = dto.SortOrder;
        }
        #endregion
        #region Banner Mapping
        public static void BannerMapping(this Banner banner, BannerDTO dto)
        {
            banner.Id = dto.Id;
            banner.NameBanner = dto.NameBanner;
            banner.Text = dto.Text;
            banner.Alt = dto.Alt;
            banner.Image = dto.Image;
        }
        #endregion
    }
}
//}
