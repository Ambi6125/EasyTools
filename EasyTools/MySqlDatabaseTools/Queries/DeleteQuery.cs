using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyTools.MySqlDatabaseTools.Tables;
using EasyTools.MySqlDatabaseTools;
using MySql.Data.MySqlClient;

namespace EasyTools.MySqlDatabaseTools.Queries
{
    public class DeleteQuery : MySqlQuery, IExecutable<MySqlCommand>
    {
        private ParameterValuePair predicate;

        protected override string Prefix => "DELETE FROM";

        public DeleteQuery(MySqlTable table, string predicateParameterName, IDataProvider dataProvider) : base(table)
        {
            if (table.IsJoined)
            {
                throw new InvalidQueryException("Cannot execute a delete query on a joint table.");
            }
            predicate = new ParameterValuePair(predicateParameterName, dataProvider.GetParameterArgs()[predicateParameterName]);
        }
        public MySqlCommand Execute()
        {
            MySqlCommand cmd = new MySqlCommand(ToString());
            cmd.Parameters.AddWithValue(predicate.ParameterName, predicate.Value);
            return cmd;
        }

        public override string ToString()
        {
            return $"{Prefix} {table.ToString()} WHERE {predicate.ParameterName} = @{predicate.ParameterName};";
        }
    }
}
