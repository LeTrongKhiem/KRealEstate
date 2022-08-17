using KRealEstate.ViewModels.Common;

namespace KRealEstate.ViewModels.Catalog.Assigns
{
    public class CategoryAssignRequest
    {
        public string Id { get; set; }
        public List<SelectItem> Categories { get; set; } = new List<SelectItem>();
    }
}
