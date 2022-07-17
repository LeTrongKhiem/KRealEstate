using System;
using System.Collections.Generic;

namespace KRealEstate.Data.Models
{
    public partial class Banner
    {
        public string Id { get; set; } = null!;
        public string NameBanner { get; set; } = null!;
        public string Image { get; set; } = null!;
        public string Alt { get; set; } = null!;
        public string? Text { get; set; }
    }
}
