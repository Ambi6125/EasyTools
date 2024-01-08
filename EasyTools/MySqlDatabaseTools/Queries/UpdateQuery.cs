using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyTools.MySqlDatabaseTools.Tables;
using EasyTools.MySqlDatabaseTools.Queries;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace EasyTools.MySqlDatabaseTools.Queries
{
    public class UpdateQuery : MySqlQuery, IExecutable<MySqlCommand>
    {
        protected override string Prefix => "UPDATE";
        private readonly IParameterValueCollection parameters;
        private readonly string predicate;
        private readonly MySqlCondition condition;

        private string Condition => condition is null ? predicate : condition.ToString();

        public UpdateQuery(MySqlTable table, IDataProvider parameters, string predicate) : base(table)
        {
            if (table.IsJoined)
            {
                throw new InvalidQueryException("Update queries cannot execute on joined tables.");
            }
            
            this.parameters = parameters.GetParameterArgs();
            this.predicate = predicate;
        }

        public UpdateQuery(MySqlTable table, IDataProvider parameters, MySqlCondition condition) : base(table)
        {
            this.parameters = parameters.GetParameterArgs();
            this.condition = condition;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder($"{Prefix} {table} SET ");
            for(int i = 0; i < parameters.Count - 1; i++)
            {
                sb.Append($"{parameters[i].ParameterName} = @{parameters[i].ParameterName}, ");
            }
            sb.Append($"{parameters.Last().ParameterName} = @{parameters.Last().ParameterName}");
            sb.Append($" WHERE {Condition};");
            return sb.ToString();
        }

        public MySqlCommand Execute()
        {
            MySqlCommand comm = new MySqlCommand(ToString());
            for(int i = 0; i < parameters.Count; i++)
            {
                comm.Parameters.AddWithValue(parameters[i].ParameterName, parameters[i].Value);
            }
            return comm;
        }
    }
}
