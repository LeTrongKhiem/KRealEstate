using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KRealEstate.ViewModels.Catalog.Categories
{
    public class CategoryEditRequest
    {
        public string NameCategory { get; set; } = null!;
        public string ParentId { get; set; } = null!;
        public bool? IsShowWeb { get; set; }
        public int? SortOrder { get; set; }
        public string? Slug { get; set; }
        public bool? Type { get; set; }
    }
}
