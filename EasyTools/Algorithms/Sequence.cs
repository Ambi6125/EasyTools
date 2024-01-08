using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTools.Algorithms
{
    /// <summary>
    /// Provides formatting method for IEnumerable types.
    /// </summary>
    public static class SeqExt
    {
        public static Sequence<SequenceType> ToSequence<SequenceType>(this IEnumerable<SequenceType> self)
        {
            return new Sequence<SequenceType>(self);
        }
    }
    public struct Sequence<T> : IEnumerable<T>, IReadOnlyCollection<T>
    {
        public IReadOnlyCollection<T> Content { get; }
        public bool IsUniform
        {
            get
            {
                T comparer = Content.ElementAt(0);
                for(int i = 0; i < Content.Count; i++)
                {
                    if(!comparer.Equals(Content.ElementAt(i)))
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        public int Count => Content.Count;

        public T this[int index]
        {
            get
            {
                return Content.ElementAt(index);
            }
        }

        public Sequence(IEnumerable<T> collection)
        {
            Content = collection.ToArray();
        }

        #region Funcs & Predicates

        public void Reorder(Func<IEnumerable<T>> func)
        {
            this = new Sequence<T>(func());
        }

        public void Reorder<TypeArg1>(Func<TypeArg1, IEnumerable<T>> func, TypeArg1 arg1)
        {
            this = new Sequence<T>(func(arg1));
        }

        public void Reorder<TypeArg1, TypeArg2>(Func<TypeArg1, TypeArg2, IEnumerable<T>> func, TypeArg1 arg1, TypeArg2 arg2)
        {
            this = new Sequence<T>(func(arg1, arg2));
        }

        public bool ForAll(Predicate<T> condition)
        {
            foreach(T t in Content)
            {
                if (!condition(t))
                {
                    return false;
                }
            }
            return true;
        }

        public T[] AllWhere(Predicate<T> condition)
        {
            List<T> result = new List<T>();
            foreach(T t in Content)
            {
                if (condition(t))
                {
                    result.Add(t); ;
                }
            }
            return result.ToArray();
        }

        public Sequence<T> First(int amount)
        {
            List<T> result = new List<T>();
            if(amount > Content.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(amount) + " exceeded size.");
            }
            for(int i = 0; i < amount; i++)
            {
                result.Add(Content.ElementAt(i));
            }
            return result.ToSequence();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("<");
            foreach(T t in Content)
            {
                sb.Append($"-{t}-");
            }
            return sb.ToString() + ">";
        }

        public IEnumerator<T> GetEnumerator()
        {
            return (IEnumerator<T>)Content.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion
    }
}
