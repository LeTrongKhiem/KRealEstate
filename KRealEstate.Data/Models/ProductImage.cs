using System;
using System.Collections.Generic;

namespace KRealEstate.Data.Models
{
    public partial class ProductImage
    {
        public string Id { get; set; } = null!;
        public string ProductId { get; set; } = null!;
        public string? Path { get; set; }
        public bool? IsThumbnail { get; set; }
        public string? Alt { get; set; }

        public virtual Product Product { get; set; } = null!;
    }
}
