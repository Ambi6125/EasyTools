using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTools.MySqlDatabaseTools.Tables
{
    public interface IDataBaseTable : IReadOnlyDatabaseTable
    {
        public void AddColumn(string name);
        public void AddColumnRange(ICollection<string> columns);
    }
}
