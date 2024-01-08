using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTools.ObjectManagingTools
{
    public class ObjectNotFoundException : Exception
    {
        public override string Message => "The object specified is not existant within this Manager.";
    }
}
