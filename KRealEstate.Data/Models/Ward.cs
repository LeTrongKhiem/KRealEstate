namespace KRealEstate.Data.Models
{
    public partial class Ward
    {
        public Ward()
        {
            //Addresses = new HashSet<Address>();
            Products = new HashSet<Product>();
            AspNetUsers = new HashSet<AppUser>();
        }

        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public string? NameEn { get; set; }
        public string? FullName { get; set; }
        public string? FullNameEn { get; set; }
        public string? CodeName { get; set; }
        public string? DistrictCode { get; set; }
        public int? AdministrativeUnitId { get; set; }

        public virtual AdministrativeUnit? AdministrativeUnit { get; set; }
        public virtual District? DistrictCodeNavigation { get; set; }
        //public virtual ICollection<Address> Addresses { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public virtual ICollection<AppUser> AspNetUsers { get; set; }
    }
}
