using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyTools.ExtensionMethods;

namespace EasyTools.WrapperClasses
{
    public static class WBool
    {
        public static bool Toggle(bool status)
        {
            if (status)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
