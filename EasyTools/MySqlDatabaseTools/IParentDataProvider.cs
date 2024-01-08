using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTools.MySqlDatabaseTools
{
    /// <summary>
    /// Indicates a base class that passes parameters down to its children.
    /// </summary>
    public interface IParentDataProvider
    {
        IParameterValueCollection GetParentParameterArgs();
    }
}
