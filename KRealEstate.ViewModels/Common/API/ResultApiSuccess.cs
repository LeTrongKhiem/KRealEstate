using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KRealEstate.ViewModels.Common.API
{
    public class ResultApiSuccess<T> : ResultApi<T>
    {
        public ResultApiSuccess(T resultObject)
        {
            IsSuccess = true;
            ResultObject = resultObject;
        }
        public ResultApiSuccess()
        {
            IsSuccess = true;
        }
    }
}
