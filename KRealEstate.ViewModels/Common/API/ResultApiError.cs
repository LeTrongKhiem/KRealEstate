using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KRealEstate.ViewModels.Common.API
{
    public class ResultApiError<T> : ResultApi<T>
    {
        public string[] ValidationErrors { get; set; }
        public ResultApiError(string message)
        {
            IsSuccess = false;
            Message = message;
        }
        public ResultApiError(string[] validationErrors)
        {
            IsSuccess = false;
            ValidationErrors = validationErrors;
        }
    }
}
