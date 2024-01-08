using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using EasyTools.MySqlDatabaseTools.Tables;

namespace EasyTools.MySqlDatabaseTools.Queries
{
    public abstract class MySqlQuery
    {
        protected MySqlTable table;
        protected abstract string Prefix { get; }

        public MySqlQuery(MySqlTable table)
        {
            this.table = table;
        }

        public override string ToString()
        {
            return $"{Prefix} {table.ToString()}";
        }
    }
}
