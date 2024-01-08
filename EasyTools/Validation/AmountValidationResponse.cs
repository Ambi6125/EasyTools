using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTools.Validation
{
    internal class AmountValidationResponse : IValidationResponse
    {
        public bool Success { get; init; }
        public string Message { get; init; }

        public int Amount { get; init; }

        public AmountValidationResponse(bool success, string message, int amount)
        {
            Success = success;
            Message = message;
            Amount = amount;
        }
    }
}
