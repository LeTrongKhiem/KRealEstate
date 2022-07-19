using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KRealEstate.ViewModels.Common
{
    public class PageResult<T> : PageResultPage
    {
        public List<T> Items { get; set; }
    }
}
