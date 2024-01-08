using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTools.ObjectManagingTools
{
    public class ManagerEventArgs<T> : EventArgs
    {
        new public static readonly ManagerEventArgs<T> Empty;
        public int QueryResultAmount { get; }

        

        //public IReadOnlyList<T> AffectedReferences { get; }

        public ManagerEventArgs(int queryResultAmount)
        {
            QueryResultAmount = queryResultAmount;
            //AffectedReferences = null;
        }
        
        //public ManagerEventArgs(int resultAmount, T added)
        //{
        //    QueryResultAmount = resultAmount;
        //    //AffectedReferences = new T[] { added };
        //}

        //public ManagerEventArgs(int resultAmount, IList<T> added)
        //{
        //    QueryResultAmount = resultAmount;
        //    //AffectedReferences = added as IReadOnlyList<T>;
        //}

    }

    public sealed class ManagerFloodEventArgs<T> : ManagerEventArgs<T>
    {
        public ManagerFloodEventArgs(int resultAmount, IList<T> skipped) : base(resultAmount)
        {
            Skipped = skipped as IReadOnlyCollection<T>;
        }

        public IReadOnlyCollection<T> Skipped { get; }
    }


    public sealed class ManagerReorganizeEventArgs<T> : ManagerEventArgs<T>
    {
        private List<KeyValuePair<int, T>> changedResults;

        new public static readonly ManagerReorganizeEventArgs<T> Empty;

        public ManagerReorganizeEventArgs(int affected) : base (affected)
        {
            changedResults = null;
        }
        public ManagerReorganizeEventArgs(int affected, IReadOnlyList<KeyValuePair<int, T>> data) : base(affected)
        {
            changedResults = data as List<KeyValuePair<int, T>>;
        }

        public IReadOnlyDictionary<int, T> ChangedResults
        {
            get
            {
                if (changedResults != null)
                {
                    Dictionary<int, T> pairs = new Dictionary<int, T>();
                    foreach(KeyValuePair<int, T> pair in changedResults)
                    {
                        pairs.Add(pair.Key, pair.Value);
                    }
                    return pairs;
                }
                else
                    throw new NullReferenceException($"Data not provided.");
            }
        }

    }
}
