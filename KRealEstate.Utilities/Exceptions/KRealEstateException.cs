using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KRealEstate.Utilities.Exceptions
{
    public class KRealEstateException : Exception
    {
        public KRealEstateException()
        {
        }

        public KRealEstateException(string message)
            : base(message)
        {
        }

        public KRealEstateException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
