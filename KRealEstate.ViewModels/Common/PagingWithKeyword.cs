using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KRealEstate.ViewModels.Common
{
    public class PagingWithKeyword : PagingPageRequest
    {
        public string Keyword { get; set; }
    }
}
