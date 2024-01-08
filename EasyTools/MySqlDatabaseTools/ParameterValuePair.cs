using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTools.MySqlDatabaseTools
{
    public readonly struct ParameterValuePair : IParameterValuePair
    {
        public string ParameterName { get; }

        public object Value { get; }

        public ParameterValuePair(string name, object value)
        {
            ParameterName = name;
            Value = value;
        }

        public override string ToString()
        {
            return $"{ParameterName}|{Value}";
        }

        public TCast As<TCast>()
        {
            return (TCast)Value;
        }
    }
}
