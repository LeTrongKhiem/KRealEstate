using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KRealEstate.ViewModels.Common.API
{
    public class ResultApi<T>
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public T ResultObject { get; set; }
    }
}
