using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTools.Validation
{
    public interface IValidationResponse
    {
        bool Success { get; init; }
        string Message { get; init; }
    }
}
