using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTools.ObjectManagingTools
{
    public class DuplicateConflictException : Exception
    {
        public override string Message => "Objects may not have identical identifiers in this Manager.";
    }

    public class UnidentifiableException : Exception
    {
        public override string Message => "Cannot request by ID because the manager is not set to unique only.";
    }
}
