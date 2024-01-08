using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTools.ConversionTools
{
    public static class Converter
    {
        public static bool IntToBool(int boolean)
        {
            switch (boolean)
            {
                case 0:
                    return false;
                case 1:
                    return true;
                default:
                    throw new ArgumentException("Integer was not 0 or 1.");
            }
        }
    }
}
