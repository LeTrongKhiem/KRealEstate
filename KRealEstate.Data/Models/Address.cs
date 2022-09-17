namespace KRealEstate.Data.Models
{
    public partial class Address
    {
        public Address()
        {
            //AspNetUsers = new HashSet<AppUser>();
            Products = new HashSet<Product>();
        }

        public string Id { get; set; } = null!;
        public string ProviceCode { get; set; } = null!;
        public string DistrictCode { get; set; } = null!;
        public string WardCode { get; set; } = null!;
        public int RegionId { get; set; }
        public int UnitId { get; set; }

        public virtual District DistrictCodeNavigation { get; set; } = null!;
        public virtual Province ProviceCodeNavigation { get; set; } = null!;
        public virtual AdministrativeRegion Region { get; set; } = null!;
        public virtual AdministrativeUnit Unit { get; set; } = null!;
        public virtual Ward WardCodeNavigation { get; set; } = null!;
        //public virtual ICollection<AppUser> AspNetUsers { get; set; }
        public virtual ICollection<Product> Products { get; set; }
    }
}
