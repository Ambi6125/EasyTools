using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTools.ObjectManagingTools
{
    public sealed class Manager<T, IDType> : Manager, IReadOnlyList<T>, IEnumerable<T> where T : IManagable<IDType>
    {
        private readonly List<T> items;
        private readonly DuplicateMode duplicateSetting;


        #region Events
        /// <summary>
        /// Triggered whenever anything changes about this manager.
        /// </summary>
        public event EventHandler<ManagerEventArgs<T>> OnAny;
        /// <summary>
        /// Triggered when a single item is added.
        /// </summary>
        public event EventHandler<ManagerEventArgs<T>> OnItemAdded;
        /// <summary>
        /// Triggered when multiple items are added.
        /// Args contain the amount added and any object skipped due to duplicate conflicts.
        /// </summary>
        public event EventHandler<ManagerFloodEventArgs<T>> OnMultipleAdded;
        /// <summary>
        /// Triggered when an item is removed.
        /// </summary>
        public event EventHandler<ManagerEventArgs<T>> OnItemRemoved;
        /// <summary>
        /// Triggered when the order of items is changed.
        /// </summary>
        public event EventHandler<ManagerReorganizeEventArgs<T>> OnEntriesReordered;
        /// <summary>
        /// Triggered when a query to a database is executed.
        /// </summary>
        public event EventHandler<ManagerEventArgs<T>> OnDatabaseQueried;
        #endregion

        #region Properties
        public T this[int index]
        {
            get
            {
                return items[index];
            }
        }

        public T[] this[int startIndex, int finalIndex]
        {
            get
            {
                if (startIndex >= finalIndex)
                {
                    throw new ArgumentException("startIndex was smaller than finalIndex.");
                }
                List<T> result = new List<T>();
                for (int i = startIndex; i <= finalIndex; i++)
                {
                    result.Add(items[i]);
                }
                return result.ToArray();
            }
        }

        /// <summary>
        /// Gets the object in front of the collection.
        /// </summary>
        public T FirstItem
        {
            get
            {
                return items[0];
            }
        }

        /// <summary>
        /// The median item of the Manager. If Count property is even, return the lower one.
        /// </summary>
        public T CenterItem
        {
            get
            {
                int halfWayThreshold = Count / 2;
                return items[halfWayThreshold];
            }
        }

        /// <summary>
        /// Gets the object at the back of the collection.
        /// </summary>
        public T LastItem
        {
            get
            {
                return items[items.Count - 1];
            }
        }

        /// <summary>
        /// Get the amount of objects being managed as an integer.
        /// </summary>
        public int Count
        {
            get
            {
                return items.Count;
            }
        }

        #endregion
        #region Constructors
        public Manager()
        {
            duplicateSetting = DuplicateMode.UniqueOnly;
            items = new List<T>();
        }

        public Manager(IList<T> collection)
        {
            duplicateSetting = DuplicateMode.UniqueOnly;
            items = (List<T>)collection;
        }

        public Manager(DuplicateMode duplicateMode)
        {
            duplicateSetting = duplicateMode;
            items = new List<T>();
        }

        public Manager(DuplicateMode duplicateMode, IList<T> collection)
        {
            duplicateSetting = duplicateMode;
            items = collection.ToList();
        }
        #endregion


        #region Methods
        public void AddItem(T item)
        {
            if(duplicateSetting == DuplicateMode.UniqueOnly)
            {
                if (Contains(item.GetIdentifier()))
                {
                    throw new DuplicateConflictException();
                }
            }
            items.Add(item);
            OnItemAdded?.Invoke(this, new ManagerEventArgs<T>(1));
            OnAny?.Invoke(this, new ManagerEventArgs<T>(1));
        }


        /// <summary>
        /// Used only for flood calls.
        /// Do not call this. Only checks for GetIdentifier() result.
        /// </summary>
        /// <param name="item">The item to addm</param>
        private void AddItemEventless(T item)
        {
            if (duplicateSetting == DuplicateMode.UniqueOnly)
            {
                if (Contains(item.GetIdentifier()))
                {
                    throw new DuplicateConflictException();
                }
            }
            items.Add(item);
        }

        public bool AddUniqueItem(T item)
        {
            if (!Contains(item.GetIdentifier()))
            {
                items.Add(item);
                OnItemAdded?.Invoke(this, new ManagerEventArgs<T>(1));
                OnAny?.Invoke(this, new ManagerEventArgs<T>(1));
                return true;
            }
            return false;
        }

        public void Remove(T item)
        {
            if (Contains(item))
            {
                items.Remove(item);
                OnItemRemoved?.Invoke(this, new ManagerEventArgs<T>(1));
                OnAny?.Invoke(this, new ManagerEventArgs<T>(1));
            }
            else
            {
                throw new ObjectNotFoundException();
            }
        }

        public void Remove(IDType identifier)
        {
            if (Contains(identifier))
            {
                Remove(FindItem(identifier));
                OnItemRemoved?.Invoke(this, new ManagerEventArgs<T>(1));
                OnAny?.Invoke(this, new ManagerEventArgs<T>(1));
            }
            else
            {
                throw new ObjectNotFoundException();
            }
        }

        public bool Contains(T item)
        {
            return items.Contains(item);
        }

        public bool Contains(IDType id)
        {
            foreach(T t in items)
            {
                if (t.GetIdentifier().Equals(id))
                {
                    return true;
                }
            }
            return false;
        }
        public T FindItem(IDType identifier)
        {
            foreach(T t in items)
            {
                if(t.GetIdentifier().Equals(identifier))
                {
                    return t;
                }
            }
            throw new ObjectNotFoundException();
        }

        public void Clear()
        {
            int amount = Count;
            items.Clear();
            OnItemRemoved?.Invoke(this, new ManagerEventArgs<T>(amount));
            OnAny?.Invoke(this, new ManagerEventArgs<T>(amount));
        }

        public void Flood(IEnumerable<T> collection)
        {
            List<T> skipped = new List<T>();
            int resultAffected = 0;
            foreach(T t in collection)
            {
                try
                {
                    AddItemEventless(t);
                    resultAffected++;
                    OnItemAdded?.Invoke(this, ManagerEventArgs<T>.Empty);
                }
                catch(DuplicateConflictException)
                {
                    skipped.Add(t);
                }
            }
            OnMultipleAdded?.Invoke(this, new ManagerFloodEventArgs<T>(resultAffected, skipped.ToArray()));
            OnAny?.Invoke(this, new ManagerEventArgs<T>(resultAffected));
        }

        public void Flood(IEnumerable<T> collection, ReplaceExisting replace)
        {
            if(replace == ReplaceExisting.Yes)
            {
                Clear();
            }
            Flood(collection);
        }
        public void Flood<CollectionType>(Func<CollectionType> func, ReplaceExisting replace) where CollectionType : IEnumerable<T>
        {
            List<T> skipped = new List<T>();
            int amount = 0;
            if (replace == ReplaceExisting.Yes)
            {
                Clear();
            }
            foreach( T t in func())
            {
                try
                {
                    AddItemEventless(t);
                    amount++;
                }
                catch (DuplicateConflictException)
                {
                    skipped.Add(t);
                }
            }
            OnMultipleAdded?.Invoke(this, new ManagerFloodEventArgs<T>(amount, skipped.ToArray()));
            OnAny?.Invoke(this, new ManagerEventArgs<T>(amount));
        }
        public void Flood<TypeArg1, CollectionType>(Func<TypeArg1, CollectionType> func, TypeArg1 arg1, ReplaceExisting replace) where CollectionType : IEnumerable<T>
        {
            int amount = 0;
            List<T> skipped = new List<T>();
            if (replace == ReplaceExisting.Yes)
            {
                Clear();
            }
            foreach (T t in func(arg1))
            {
                try
                {
                    AddItemEventless(t);
                    amount++;
                }
                catch (DuplicateConflictException)
                {
                    skipped.Add(t);
                }
            }
            OnMultipleAdded?.Invoke(this, new ManagerFloodEventArgs<T>(amount, skipped));
            OnAny?.Invoke(this, new ManagerEventArgs<T>(amount));
        }
        public void Flood<TypeArg1, TypeArg2, CollectionType>(Func<TypeArg1, TypeArg2, CollectionType> func, TypeArg1 arg1, TypeArg2 arg2, ReplaceExisting replace) where CollectionType : IEnumerable<T>
        {
            int amount = 0;
            List<T> skipped = new List<T>();
            if (replace == ReplaceExisting.Yes)
            {
                OnItemRemoved?.Invoke(this, new ManagerEventArgs<T>(Count));
                Clear();
            }
            foreach (T t in func(arg1, arg2))
            {
                try
                {
                    AddItemEventless(t);
                    amount++;
                }
                catch (DuplicateConflictException)
                {
                    skipped.Add(t);
                }
            }
            OnMultipleAdded?.Invoke(this, new ManagerFloodEventArgs<T>(amount, skipped));
        }

        public void Flood<TypeArg1, TypeArg2, TypeArg3, CollectionType>(Func<TypeArg1, TypeArg2, CollectionType> func, TypeArg1 arg1, TypeArg2 arg2, TypeArg3 arg3, ReplaceExisting replace) where CollectionType : IEnumerable<T>
        {
            if (replace == ReplaceExisting.Yes)
            {
                OnItemRemoved?.Invoke(this, new ManagerEventArgs<T>(Count));
                Clear();
            }
            foreach (T t in func(arg1, arg2))
            {
                AddItem(t);
            }
        }

            /// <summary>
            /// Searches for an item with specified identifier, starting at index k.
            /// </summary>
            /// <param name="identifier">the identifier to filter to.</param>
            /// <param name="result">Will contain the result if found. If not, will be set to null.</param>
            /// <returns>True</returns>
            public bool FindItemOffset(IDType identifier, int k, out T result)
        {
            if (k > items.Count)
            {
                throw new IndexOutOfRangeException();
            }
            for (int i = k; i <= items.Count; i++)
            {
                if (items[i].GetIdentifier().Equals(identifier))
                {
                    result = items[i];
                    return true;
                }
            }
            result = default(T);
            return false;
        }

        public bool FindAll(IDType identifier, out IEnumerable<T> resultArray)
        {
            bool hasResult = false;
            List<T> result = new List<T>();
            foreach (T t in items)
            {
                if (t.GetIdentifier().Equals(identifier))
                {
                    if (!hasResult)
                    {
                        hasResult = true;
                    }
                    result.Add(t);
                }
            }
            if (hasResult)
            {
                resultArray = result.ToArray();
            }
            else
            {
                resultArray = null;
            }
            return hasResult;
        }

        public int FindIndexOf(T item)
        {
            if (item == null)
            {
                throw new NullReferenceException(item + " was null.");
            }

            for (int i = 0; i < Count; i++)
            {
                if (ReferenceEquals(items[i], item))
                {
                    return i;
                }
            }
            throw new ObjectNotFoundException();
        }

        public int FindIndexOf(IDType identifier)
        {
            if(identifier == null)
            {
                throw new ArgumentNullException();
            }
            if(duplicateSetting == DuplicateMode.AllowDuplicates)
            {
                throw new UnidentifiableException();
            }

            for (int i = 0; i < Count; i++)
            {
                if (items[i].GetIdentifier().Equals(identifier))
                {
                    return i;
                }
            }
            return -1;
        }

        public void Replace(T old, T update)
        {
            items[FindIndexOf(old)] = update;
            OnAny?.Invoke(this, new ManagerEventArgs<T>(1));
        }

        /// <summary>
        /// Remove all objects with a certain identifier.
        /// </summary>
        /// <param name="identifier"></param>
        /// <returns>True if 1 or more objects were removed. Otherwise, false.</returns>
        public bool RemoveAll(IDType identifier)
        {
            int affected = 0;
            bool result = false;

            foreach (T t in items)
            {
                if (t.GetIdentifier().Equals(identifier))
                {
                    affected++;
                    Remove(t);
                    result = true;
                }
            }
            OnItemRemoved?.Invoke(this, new ManagerEventArgs<T>(affected));
            OnAny?.Invoke(this, new ManagerEventArgs<T>(affected));
            return result;
        }

        /// <summary>
        /// Remove an item.
        /// </summary>
        /// <param name="index">The item at this index will be removed.</param>
        public void RemoveAt(int index)
        {
            items.RemoveAt(index);
            OnItemRemoved?.Invoke(this, new ManagerEventArgs<T>(1));
        }
        public override string ToString()
        {
            string result = String.Empty;
            StringBuilder sb = new StringBuilder(result);

            sb.Append(FirstItem.ToString());

            for (int i = 1; i < items.Count; i++)
            {
                sb.Append(", " + items[i].ToString());
            }

            return sb.ToString();
        }

        #region Manipulation

        /// <summary>
        /// Switch the order in which 2 entries are stored.
        /// </summary>
        /// <param name="index1">An index in Collection.</param>
        /// <param name="index2">The index you wish to move it to. The object currently there will be swapped to that of the first.</param>
        public void Swap(int index1, int index2)
        {
            List<KeyValuePair<int, T>> eventArgData = new List<KeyValuePair<int, T>>();
            T temporaryStorage = items[index1];
            items[index1] = items[index2];
            items[index2] = temporaryStorage;
            eventArgData.Add(new KeyValuePair<int, T>(index1, items[index1]));
            eventArgData.Add(new KeyValuePair<int, T>(index2, items[index2]));
            OnEntriesReordered?.Invoke(this, new ManagerReorganizeEventArgs<T>(2, eventArgData));
            OnAny?.Invoke(this, new ManagerEventArgs<T>(2));
        }

        /// <summary>
        /// Randomizes the order of the entire Collection.
        /// </summary>
        public void Shuffle()
        {
            Random rng = new Random();
            int n = items.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = items[k];
                items[k] = items[n];
                items[n] = value;
            }
            OnEntriesReordered?.Invoke(this, ManagerReorganizeEventArgs<T>.Empty);
            OnAny?.Invoke(this, ManagerEventArgs<T>.Empty);
        }

        /// <summary>
        /// Split the Manager Collection in 2, returning both new Managers
        /// </summary>
        /// <returns></returns>
        public Manager<T, IDType>[] Split()
        {
            List<T> firstHalf = new List<T>();
            int halfWayPoint = Count / 2;
            for (int i = 0; i <= halfWayPoint; i++)
            {
                firstHalf.Add(items[i]);
            }

            List<T> secondHalf = new List<T>();
            for (int i = halfWayPoint + 1; i <= Count; i++)
            {
                secondHalf.Add(items[i]);
            }
            Manager<T, IDType> mOne = new Manager<T, IDType>(duplicateSetting, firstHalf);
            Manager<T, IDType> mTwo = new Manager<T, IDType>(duplicateSetting, secondHalf);
            List<Manager<T, IDType>> result = new List<Manager<T, IDType>>();
            result.Add(mOne);
            result.Add(mTwo);
            return result.ToArray();
        }

        /// <summary>
        /// Split the Manager in 2, with the split at a specified index. Return both Managers.
        /// </summary>
        /// <param name="splitIndex">The index point to split at.</param>
        /// <returns></returns>
        public Manager<T, IDType>[] Split(int splitIndex)
        {
            List<T> firstHalf = new List<T>();
            int halfWayPoint = splitIndex;
            for (int i = 0; i <= splitIndex; i++)
            {
                firstHalf.Add(items[i]);
            }

            List<T> secondHalf = new List<T>();
            for (int i = splitIndex + 1; i <= Count; i++)
            {
                secondHalf.Add(items[i]);
            }
            Manager<T, IDType> mOne = new Manager<T, IDType>(duplicateSetting, firstHalf);
            Manager<T, IDType> mTwo = new Manager<T, IDType>(duplicateSetting, secondHalf);
            return new Manager<T, IDType>[] { mOne, mTwo };
        }
        #endregion
        #region Sorting
        public void Sort()
        {
            items.Sort();
            OnEntriesReordered?.Invoke(this, ManagerReorganizeEventArgs<T>.Empty);
            OnAny?.Invoke(this, ManagerEventArgs<T>.Empty);
        }

        public void Sort(ManagerOrder orderCriterium)
        {
            items.Sort();
            if (orderCriterium == ManagerOrder.Descending)
                items.Reverse();
            OnEntriesReordered?.Invoke(this, ManagerReorganizeEventArgs<T>.Empty);
            OnAny?.Invoke(this, ManagerEventArgs<T>.Empty);
        }

        public void Sort(Comparison<T> comparison)
        {
            items.Sort(comparison);
            OnEntriesReordered?.Invoke(this, ManagerReorganizeEventArgs<T>.Empty);
            OnAny?.Invoke(this, ManagerEventArgs<T>.Empty);
        }

        public void Sort(Comparison<T> comparison, ManagerOrder orderCriterium)
        {
            items.Sort(comparison);
            items.Reverse();
            OnEntriesReordered?.Invoke(this, ManagerReorganizeEventArgs<T>.Empty);
            OnAny?.Invoke(this, ManagerEventArgs<T>.Empty);
        }

        public void Sort(IComparer<T> comparer)
        {
            items.Sort(comparer);
            OnEntriesReordered?.Invoke(this, ManagerReorganizeEventArgs<T>.Empty);
            OnAny?.Invoke(this, ManagerEventArgs<T>.Empty);
        }


        #endregion
        #endregion

        /// <summary>
        /// Removes all entries and then re-adds them. Mostly used for refreshes.
        /// Does not trigger any event besides OnAnyUpdate.
        /// </summary>
        public void Refill()
        {
            List<T> items = this.items;
            this.items.Clear();
            this.items.AddRange(items);
            OnAny?.Invoke(this, new ManagerEventArgs<T>(Count));
        }

        

        #region Code to make this enumerable
        public IEnumerator<T> GetEnumerator()
        {
            //return new ManagerReader<T, IDType>(this);
            return items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion

        #region Conversions
        public T[] ToArray()
        {
            return items.ToArray();
        }


        #endregion

        #region DataBaseInteraction

        public object FreeQuery<TDelegate>(TDelegate query) where TDelegate : Delegate
        {
            OnDatabaseQueried?.Invoke(this, ManagerEventArgs<T>.Empty);
            return query.DynamicInvoke();
        }

        public object FreeQueryCallback<TDelegateResult, TDelegateCallback>(TDelegateResult resultQuery, TDelegateCallback callback) where TDelegateCallback : Delegate where TDelegateResult : Delegate
        {
            object result = resultQuery.DynamicInvoke();
            callback.DynamicInvoke();
            OnDatabaseQueried?.Invoke(this, ManagerEventArgs<T>.Empty);
            return result;
        }

        public void NonQuery(Action<string> query, string arg)
        {
            query.Invoke(arg);
            OnDatabaseQueried?.Invoke(this, ManagerEventArgs<T>.Empty);
        }

        public void DBReadDirect<TRead>(Func<string, TRead[]> query, string arg, ReplaceExisting replace) where TRead : T
        {
            int amount = 0;
            List<T> skipped = new List<T>();
            TRead[] queryReadResults = query.Invoke(arg);
            if(replace == ReplaceExisting.Yes)
            {
                Clear();
            }
            foreach(T t in queryReadResults)
            {
                try
                {
                    AddItemEventless(t);
                    amount++;
                }
                catch (DuplicateConflictException)
                {
                    skipped.Add(t);
                }
            }
            OnDatabaseQueried?.Invoke(this, new ManagerFloodEventArgs<T>(amount, skipped));
            OnMultipleAdded?.Invoke(this, new ManagerFloodEventArgs<T>(amount, skipped));
        } 

        #endregion
    }
}
