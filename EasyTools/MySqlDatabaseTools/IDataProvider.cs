using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTools.MySqlDatabaseTools
{
    /// <summary>
    /// This class can provide data and specify column names for them.
    /// </summary>
    public interface IDataProvider
    {
        IParameterValueCollection GetParameterArgs();
    }
}
