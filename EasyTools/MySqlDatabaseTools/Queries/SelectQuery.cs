using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyTools.MySqlDatabaseTools.Tables;

namespace EasyTools.MySqlDatabaseTools.Queries
{
    /// <summary>
    /// Resembles a query that, when run, will retrieve data from a database.
    /// </summary>
    public class SelectQuery : MySqlQuery, IExecutable<MySqlCommand>
    {
        /// <summary>
        /// The part appended after SELECT. This specifies which columns are selected.
        /// </summary>
        private readonly string selectionPredicate;
        private readonly MySqlCondition condition = null;


        /// <summary>
        /// Builds WHERE clause.
        /// </summary>
        private string Condition
        {
            get
            {
                if(condition is null)
                {
                    return string.Empty;
                }
                else
                {
                    return "WHERE " + condition.ToString();
                }
            }
        }

        protected override string Prefix => $"SELECT {selectionPredicate} FROM";

        public MySqlCommand Execute()
        {
            MySqlCommand resultCommand =  new MySqlCommand(ToString());
            if(condition is not null)
            {
                resultCommand.Parameters.AddWithValue(condition.ParamName, condition.Value);
            }
            return resultCommand;
        }

        /// <summary>
        /// Creates a query that will retrieve every column from the table.
        /// </summary>
        /// <param name="table">The table to read from.</param>
        public SelectQuery(MySqlTable table) : base(table)
        {
            selectionPredicate = "*";
        }

        /// <summary>
        /// Creates a query that will retrieve the specified column(s) from the table.
        /// </summary>
        /// <param name="table">The table to read from</param>
        /// <param name="data"></param>
        public SelectQuery(MySqlTable table, string data) : base(table)
        {
            selectionPredicate = data;
        }

        public SelectQuery(MySqlTable table, string data, MySqlCondition condition) : base(table)
        {
            selectionPredicate = data;
            this.condition = condition;
        }

        public SelectQuery(MySqlTable table, in ICollection<string> data) : base(table)
        {
            StringBuilder sb = new StringBuilder();
            for(int i = 0; i < data.Count -1; i++)
            {
                sb.Append(data.ElementAt(i) + ", ");
            }
            sb.Append(data.Last() + ";");
        }

        public override string ToString()
        {
            return $"{Prefix} {table} {Condition}";
        }
    }
}
