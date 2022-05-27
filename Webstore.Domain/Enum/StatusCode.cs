using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Webstore.Domain.Enum
{
    public enum StatusCode
    {
        OK = 200,
        InternalServerError = 500,
        ProductNotFound = 10
    }
}
