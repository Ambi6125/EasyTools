using EasyTools.MySqlDatabaseTools.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTools.MySqlDatabaseTools.Tables
{
    public class MySqlJoinCondition
    {
        private Strictness _strictness;

        public string Parameter1 { get; }
        public string Parameter2 { get; }

        public MySqlJoinCondition()
        {

        }
    }
}
