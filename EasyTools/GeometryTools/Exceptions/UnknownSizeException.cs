using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTools.GeometryTools.Exceptions
{
    public class UnknownSizeException : Exception
    {
        private const string defaultErrorMessage = "Attempted calculation with unknown or nonexistant data.";
        private readonly string customErrorMessage;
        private bool useCustomMessage;

        public UnknownSizeException()
        {
            useCustomMessage = true;
        }

        public UnknownSizeException(string message)
        {
            customErrorMessage = message;
            useCustomMessage = false;
        }

        public override string Message
        {
            get
            {
                if (useCustomMessage)
                {
                    return customErrorMessage;
                }
                else
                {
                    return defaultErrorMessage;
                }
            }
        }
    }
}
