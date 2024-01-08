using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTools.LayerTransferTools
{
    public class Transferer<T> where T : DTO
    {
        private readonly List<T> objects;

        public Transferer(IList<T> objects)
        {
            this.objects = (List<T>)objects;
        }

        public Transferer(T item)
        {
            objects = new List<T>() { item };
        }
        public object Transfer()
        {
            return objects;
        }

        public T IntQuery(int query)
        {
            foreach(T t in objects)
            {
                if(t.Int1 == query)
                {
                    return t;
                }
            }
            return default(T);
        }

        public T StringQuery(string query)
        {
            foreach(T t in objects)
            {
                if(t.String1 == query)
                {
                    return t;
                }
            }
            return default(T);
        }
    }
}
