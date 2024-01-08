using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTools.MySqlDatabaseTools
{
    public class ParameterValueCollection : IParameterValueCollection, IReadOnlyParameterValueCollection
    {
        private readonly List<ParameterValuePair> paramValues = new List<ParameterValuePair>();


        public int Count => paramValues.Count;

        public bool IsReadOnly
        {
            get
            {
                if(this is IReadOnlyCollection<ParameterValuePair>)
                {
                    return true;
                }
                return false;
            }
        }

        public ParameterValuePair this[int index]
        {
            get
            {
                return paramValues[index];
            }
            set
            {
                paramValues[index] = value;
            }
        }

        public object this[string name]
        {
            get
            {
                if (name is not null)
                {
                    foreach(var pair in paramValues)
                    {
                        if(pair.ParameterName == name)
                        {
                            return pair.Value;
                        }
                    }
                }
                return null;
            }
        }

        public void Add(string parameterName, object value)
        {
            paramValues.Add(new ParameterValuePair(parameterName, value));
        }

        public void Add(IList<ParameterValuePair> paramValues)
        {
            foreach (ParameterValuePair paramValuePair in paramValues)
            {
                this.paramValues.Add(paramValuePair);
            }
        }

        public void Add(ParameterValuePair paramValue)
        {
            paramValues.Add(paramValue);
        }

        public void Clear()
        {
            paramValues.Clear();
        }

        public bool Contains(ParameterValuePair item)
        {
            return paramValues.Contains(item);
        }

        public void CopyTo(ParameterValuePair[] array, int arrayIndex)
        {
            paramValues.CopyTo(array, arrayIndex);
        }

        public IEnumerator<ParameterValuePair> GetEnumerator()
        {
            return paramValues.GetEnumerator();
        }

        public bool Remove(ParameterValuePair item)
        {
            return paramValues.Remove(item);
        }

        public void RemoveAt(int index)
        {
            paramValues.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public TCast GetValueAs<TCast>(int index)
        {
            return (TCast)this[index].Value;
        }
        public TCast GetValueAs<TCast>(string parameterName)
        {
            return (TCast)this[parameterName];
        }
    }
}
