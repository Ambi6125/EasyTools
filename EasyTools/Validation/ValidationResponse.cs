using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTools.Validation
{
    public class ValidationResponse : IValidationResponse
    {
        public bool Success { get; init; } = false;

        public string Message { get; init; } = string.Empty;

        public ValidationResponse(bool success, string message)
        {
            Success = success;
            Message = message;
        }
    }
}
