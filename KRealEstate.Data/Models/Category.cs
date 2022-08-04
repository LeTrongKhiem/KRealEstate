namespace KRealEstate.Data.Models
{
    public partial class Category
    {
        public Category()
        {
            ProductMapCategories = new HashSet<ProductMapCategory>();
        }
        public string Id { get; set; } = null!;
        public string NameCategory { get; set; } = null!;
        public string ParentId { get; set; } = null!;
        public bool? IsShowWeb { get; set; }
        public int? SortOrder { get; set; }
        public string? Slug { get; set; }
        public bool? Type { get; set; }
        public virtual ICollection<ProductMapCategory> ProductMapCategories { get; set; }
    }
}
