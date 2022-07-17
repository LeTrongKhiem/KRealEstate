using System;
using System.Collections.Generic;

namespace KRealEstate.Data.Models
{
    public partial class UnitPricePost
    {
        public string Id { get; set; } = null!;
        public decimal UnitPricePost1 { get; set; }
        public string? PostTypeId { get; set; }

        public virtual PostType? PostType { get; set; }
    }
}
