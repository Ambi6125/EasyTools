using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyTools.MySqlDatabaseTools.Tables;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace EasyTools.MySqlDatabaseTools.Queries
{
    public class InsertQuery : MySqlQuery, IExecutable<MySqlCommand>
    {
        private IParameterValueCollection paramArgs;
        protected override string Prefix => "INSERT INTO";
        
        public MySqlCommand Execute()
        {
            MySqlCommand mySqlCommand = new MySqlCommand(ToString());
            for(int i = 0; i < paramArgs.Count; i++)
            {
                ParameterValuePair currentPair = paramArgs[i];

                mySqlCommand.Parameters.AddWithValue(currentPair.ParameterName, currentPair.Value);
            }
            return mySqlCommand;
        }

        public InsertQuery(MySqlTable table, IDataProvider parameters) : base (table)
        {
            //if (table.IsJoined)
            //{
            //    throw new InvalidQueryException("Cannot run an insert query on a joined table.");
            //}
            this.table = table;
            paramArgs = parameters.GetParameterArgs();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(Prefix + " ");
            sb.Append($"{table.ToString()} VALUES (");
            for (int i = 0; i < paramArgs.Count - 1; i++)
            {
                sb.Append($" @{paramArgs[i].ParameterName},");
            }
            sb.Append($" @{paramArgs.Last().ParameterName});");
            return sb.ToString();
        }
    }
}
