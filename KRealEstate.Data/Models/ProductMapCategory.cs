using System;
using System.Collections.Generic;

namespace KRealEstate.Data.Models
{
    public partial class ProductMapCategory
    {
        public string ProductId { get; set; } = null!;
        public string CategoryId { get; set; } = null!;

        public virtual Category Category { get; set; } = null!;
        public virtual Product Product { get; set; } = null!;
    }
}
