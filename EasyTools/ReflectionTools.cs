using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTools
{
    public static class Software
    {
        public static string OwnFileLocation
        {
            get
            {
                return AppContext.BaseDirectory;
            }
        }
    }
}
