using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTools.Algorithms
{
    public class Wheel<T> : IEnumerable<T>
    {
        private List<T> _items;
        public Wheel(IEnumerable<T> sequence)
        {
            _items = sequence.ToList();
        }

        public void Add(T item)
        {
            _items.Add(item);
        }
        public void Remove(T item)
        {
            _items.Remove(item);
        }

        public int Size => _items.Count;

        public T this[int index]
        {
            get
            {
                if (index < 0)
                {
                    return _items[index + _items.Count];
                }
                else if (index > _items.Count)
                {
                    return _items[index - _items.Count];
                }
                else
                {
                    return _items[index];
                }
            }
            
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public static class WheelExtensions
    {
        public static Wheel<T> ToWheel<T>(this IEnumerable<T> param)
        {
            return new Wheel<T>(param);
        }
    }
}

