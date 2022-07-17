using System;
using System.Collections.Generic;

namespace KRealEstate.Data.Models
{
    public partial class Direction
    {
        public Direction()
        {
            Products = new HashSet<Product>();
        }

        public string Id { get; set; } = null!;
        public string? Name { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}
