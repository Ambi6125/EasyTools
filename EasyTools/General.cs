using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTools
{
    public enum Method { Succes, PartialSuccess, Failure, Exception, Refusal, Canceled, Invalid, Duplicate, Yes, No, Unclear }
    
    public static class EasyTools
    {
        public const int EnumeratorDefaultValue = -1;
        public const string NewLine = "\n";
        public const string Amogus =
            "         ..........    " + NewLine +
            "       .............   " + NewLine + 
            "   .......         .." +   NewLine +
            "   .......         .." + NewLine +
            "   .................." + NewLine +
            "       .............." + NewLine +
            "       .............." + NewLine +
            "       .....      ..." + NewLine +
            "       .....      ...";

        public static void WriteLineCallback(object obj, Action action)
        {
            Console.WriteLine(obj);
            action();
            Console.WriteLine(obj);
        }
    }
}
