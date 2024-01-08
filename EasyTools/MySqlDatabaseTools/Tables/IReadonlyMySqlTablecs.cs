using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTools.MySqlDatabaseTools.Tables
{
    public interface IReadOnlyDatabaseTable : IReadOnlyCollection<MySqlColumn>
    {
        IReadOnlyCollection<MySqlColumn> Columns { get; }
    }
}
