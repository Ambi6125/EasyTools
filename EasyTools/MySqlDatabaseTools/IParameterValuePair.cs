using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTools.MySqlDatabaseTools
{
    public interface IParameterValuePair
    {
        string ParameterName { get; }
        object Value { get; }
    }
}
