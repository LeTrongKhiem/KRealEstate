using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KRealEstate.ViewModels.Catalog.Product
{
    public class ProductViewModel
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal Area { get; set; }
        public string Address { get; set; }
        public string AddressDisplay { get; set; }
        public string Description { get; set; }
        public string ThumbnailImage { get; set; }
    }
}
