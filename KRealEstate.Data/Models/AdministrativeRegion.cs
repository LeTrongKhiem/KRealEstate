using System;
using System.Collections.Generic;

namespace KRealEstate.Data.Models
{
    public partial class AdministrativeRegion
    {
        public AdministrativeRegion()
        {
            Addresses = new HashSet<Address>();
            Provinces = new HashSet<Province>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string NameEn { get; set; } = null!;
        public string? CodeName { get; set; }
        public string? CodeNameEn { get; set; }

        public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<Province> Provinces { get; set; }
    }
}
