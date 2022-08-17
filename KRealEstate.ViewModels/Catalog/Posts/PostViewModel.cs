namespace KRealEstate.ViewModels.Catalog.Posts
{
    public class PostViewModel
    {
        public string Id { get; set; } = null!;
        public string PostId { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public DateTime DatePost { get; set; }
        public bool Status { get; set; }
    }
}
