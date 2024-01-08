using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTools.MySqlDatabaseTools.Tables
{
    public class MySqlColumn
    {
        private string name;
        public int ColumnIndex { get; }

        public MySqlColumn(MySqlTable parentTable, string name, int index)
        {
            this.name = $"{parentTable.ToString()}.{name}";
            ColumnIndex = index;
        }

        public override string ToString() => name;
    }
}
