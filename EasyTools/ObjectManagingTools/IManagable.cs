using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTools.ObjectManagingTools
{

    public interface IManagable<IDType>
    {
        IDType GetIdentifier();
    }
}
