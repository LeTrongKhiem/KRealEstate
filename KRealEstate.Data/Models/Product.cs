using System;
using System.Collections.Generic;

namespace KRealEstate.Data.Models
{
    public partial class Product
    {
        public Product()
        {
            Contacts = new HashSet<Contact>();
            PostDetails = new HashSet<PostDetail>();
            ProductImages = new HashSet<ProductImage>();
        }

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

        public virtual Address Address { get; set; } = null!;
        public virtual Direction? Direction { get; set; }
        public virtual ICollection<Contact> Contacts { get; set; }
        public virtual ICollection<PostDetail> PostDetails { get; set; }
        public virtual ICollection<ProductImage> ProductImages { get; set; }
    }
}
