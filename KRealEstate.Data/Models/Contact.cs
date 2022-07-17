using System;
using System.Collections.Generic;

namespace KRealEstate.Data.Models
{
    public partial class Contact
    {
        public string Id { get; set; } = null!;
        public string ProductId { get; set; } = null!;
        public string NameContact { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public string? AddressContact { get; set; }
        public string? Email { get; set; }

        public virtual Product Product { get; set; } = null!;
    }
}
