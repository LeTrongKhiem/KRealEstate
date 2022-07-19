using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KRealEstate.ViewModels.Catalog.Products
{
    public class ProductDetailViewModel
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

        public string UserId { get; set; } = null!;
        public DateTime DatePost { get; set; }

    }
}
