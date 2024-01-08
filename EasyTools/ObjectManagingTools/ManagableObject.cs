using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTools.ObjectManagingTools
{
    [Serializable]
    public abstract class ManagableObject : IEquatable<ManagableObject>, IManagable<string>
    {
        private string _id;

        public string Identifier
        {
            get
            {
                return _id;
            }
            protected set
            {
                _id = value;
            }
        }

        /// <summary>
        /// Create an object that can be managed by a Manager object.
        /// </summary>
        /// <param name="id">Provide something to recognize the object by. This can be a name or a numeric value.</param>
        public ManagableObject(string id)
        {
            _id = id;
        }

        public bool HasNumericID
        {
            get
            {
                foreach(char c in _id)
                {
                    if (!char.IsNumber(c))
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        public bool Equals(ManagableObject other)
        {
            return Identifier == other.Identifier;
        }

        public string GetIdentifier()
        {
            return Identifier;
        }
    }
}
