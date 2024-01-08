using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTools.MySqlDatabaseTools.Queries
{
    public enum Strictness { MustMatchExactly = 0, MustBeSimilar = 1 }


    public class MySqlCondition
    {
        private readonly static string[] strictnessAsString = { "=", "LIKE" };
        private readonly string paramName;
        private readonly string condition;
        private readonly object value;
        public string ParamName => $"@{paramName}";
        
        public object Value => value;

        private Strictness strictness;
        public MySqlCondition(IParameterValuePair parameters, Strictness strictness)
        {
            paramName = parameters.ParameterName;
            this.strictness = strictness;
            condition = strictnessAsString[(int)strictness];
            value = parameters.Value;
        }

        public MySqlCondition(string parameterName, object parameterValue, Strictness strictness)
        {
            paramName = parameterName;
            condition = strictnessAsString[(int)strictness];
            value = parameterValue;
            this.strictness = strictness;
        }

        public override string ToString()
        {
            return $"{paramName} {condition} {ParamName}";
        }
    }
}
