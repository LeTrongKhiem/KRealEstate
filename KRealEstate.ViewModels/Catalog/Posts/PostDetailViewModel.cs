namespace KRealEstate.ViewModels.Catalog.Posts
{
    public class PostDetailViewModel
    {
        public string? Id { get; set; }
        public string ProductId { get; set; }
        public int DayLengthPost { get; set; }
        public DateTime? DayPostStart { get; set; }
        public DateTime? DayPostEnd { get; set; }
        public string PostTypeId { get; set; }
    }
}
