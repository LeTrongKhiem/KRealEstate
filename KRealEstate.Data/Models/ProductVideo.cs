using System;
using System.Collections.Generic;

namespace KRealEstate.Data.Models
{
    public partial class ProductVideo
    {
        public string Id { get; set; } = null!;
        public string ProductId { get; set; } = null!;
        public string Path { get; set; } = null!;
    }
}
