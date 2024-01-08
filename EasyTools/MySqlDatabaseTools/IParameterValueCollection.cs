using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTools.MySqlDatabaseTools
{
    public interface IReadOnlyParameterValueCollection: IReadOnlyCollection<ParameterValuePair>
    {
        object this[string parameterName] { get; }
        ParameterValuePair this[int index] { get; }
        public TCast GetValueAs<TCast>(string parameterName);
        public TCast GetValueAs<TCast>(int index);
    }
    public interface IParameterValueCollection : ICollection<ParameterValuePair>
    {
        void Add(string parameterName, object value);

        ParameterValuePair this[int index] { get; }
        object this[string parameterName] { get; }

        public TCast GetValueAs<TCast>(string parameterName);
        public TCast GetValueAs<TCast>(int index);
    }
}
