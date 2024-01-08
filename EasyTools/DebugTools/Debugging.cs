using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTools.DebugTools
{
    public static class Debugging
    {
        public static bool Throws<T>(this Delegate method) where T : Exception
        {
            try
            {
                method.DynamicInvoke();
            }
            catch (T)
            {
                return true;
            }
            return false;
        }
    }
}
