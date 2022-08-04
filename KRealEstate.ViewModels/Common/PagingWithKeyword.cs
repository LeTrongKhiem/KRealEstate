namespace KRealEstate.ViewModels.Common
{
    public class PagingWithKeyword : PagingPageRequest
    {
        public string Keyword { get; set; }
        public bool Active { get; set; }
    }
}
