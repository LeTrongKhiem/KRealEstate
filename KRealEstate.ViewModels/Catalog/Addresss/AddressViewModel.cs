namespace KRealEstate.ViewModels.Catalog.Addresss
{
    public class AddressViewModel
    {
        public string Id { get; set; } = null!;
        public string ProviceCode { get; set; } = null!;
        public string DistrictCode { get; set; } = null!;
        public string WardCode { get; set; } = null!;
        public int RegionId { get; set; }
        public int UnitId { get; set; }
    }
}
