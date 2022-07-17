using System;
using System.Collections.Generic;

namespace KRealEstate.Data.Models
{
    public partial class AspNetUserToken
    {
        public Guid UserId { get; set; }
        public string LoginProvider { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? Value { get; set; }

        public virtual AspNetUser User { get; set; } = null!;
    }
}
