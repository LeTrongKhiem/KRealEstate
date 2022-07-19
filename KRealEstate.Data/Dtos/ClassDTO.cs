using KRealEstate.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KRealEstate.Data.Dtos
{
    //public class ClassDTO
    //{
    #region Category
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
    public static class Mapping
    {
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
    }
}
//}
