using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTools.GeometryTools.Exceptions
{
    public class ImpossibleDataException : Exception
    {
        private string _message;
        public ImpossibleDataException(string message)
        {
            _message = message;
        }
        public override string Message => _message;
    }
}
