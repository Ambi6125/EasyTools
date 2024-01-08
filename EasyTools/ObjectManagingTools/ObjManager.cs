using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTools.ObjectManagingTools
{
    /// <summary>
    /// Easily manage objects for your application.
    /// </summary>
    /// <typeparam name="T">The object type this manager should keep track of. Must inherit from ManagableObject.</typeparam>
    [Serializable]
    public sealed class ObjManager<T> : Manager, IEnumerable<T>, IReadOnlyList<T> where T : ManagableObject //<-- Refactor to interface
    {
        #region Fields
        private readonly List<T> items = new List<T>();
        private readonly DuplicateMode duplicateSetting;
        #endregion

        #region Interfaces
        #region IEnumerable
        public IEnumerator<T> GetEnumerator()
        {
            return new ObjManagerReader<T>(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        #endregion
        #endregion

        #region Properties
        /// <summary>
        /// All objects currently being managed in an array.
        /// </summary>
        [Obsolete]
        public T[] Collection
        {
            get
            {
                return items.ToArray();
            }
        }

        /// <summary>
        /// Get the object at the specified index.
        /// </summary>
        /// <param name="index">the index of the object you wish to get.</param>
        /// <returns></returns>
        public T this[int index]
        {
            get
            {
                return items[index];
            }
        }

        /// <summary>
        /// Get a range of objects.
        /// </summary>
        /// <param name="startIndex">The starting index.</param>
        /// <param name="upToAndIncludingIndex">The index of the last item you wish to get. Must be larger than startIndex.</param>
        /// <returns></returns>
        public T[] this[int startIndex, int finalIndex]
        {
            get
            {
                if(startIndex >= finalIndex)
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
        /// The median item of the Manager. If Size property is even, return the lower one.
        /// </summary>
        public T CenterItem
        {
            get
            {
                int halfWayThreshold = Count / 2 ;
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
                return items[items.Count -1];
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

        #region Constructor & overloads
        /// <summary>
        /// Create a manager to easily keep track of ManagableObjects. Does not allow entries with identical identifiers.
        /// </summary>
        public ObjManager()
        {
            duplicateSetting = DuplicateMode.UniqueOnly;
        }

        /// <summary>
        /// Create a manager to easily keep track of ManagableObjects.
        /// </summary>
        /// <param name="duplicateSetting">Determine if entries are allowed to have identical identifiers.</param>
        public ObjManager(DuplicateMode duplicateSetting)
        {
            this.duplicateSetting = duplicateSetting;
        }

        /// <summary>
        /// Create a manager to easilykeep track of ManagableObjects.
        /// </summary>
        /// <param name="duplicateSetting">Determine if entries are allowed to have identical identifiers.</param>
        /// <param name="premadeCollection">Copy the elements in the provided collection to the Manager's Collection.</param>
        public ObjManager(DuplicateMode duplicateSetting, IList<T> premadeCollection)
        {
            this.duplicateSetting = duplicateSetting;
            foreach(T t in premadeCollection)
            {
                items.Add(t);
            }
        }

        public ObjManager(DuplicateMode duplicateSetting, DataSource dataSource, string connectionString)
        {
            this.duplicateSetting = duplicateSetting;
            foreach(T receivedObj in dataSource(connectionString))
            {
                items.Add(receivedObj);
            }
        }

        public ObjManager(DuplicateMode duplicateSetting, GenericDataSource<T> dataSource, string sourceLocation)
        {
            this.duplicateSetting = duplicateSetting;
            foreach(T receivedObj in dataSource(sourceLocation))
            {
                items.Add(receivedObj);
            }
        }

        /// <summary>
        /// Create a manager to easilykeep track of ManagableObjects.
        /// </summary>
        /// <param name="premadeCollection">Copy the elements in the provided collection to the Manager's Collection.</param>
        public ObjManager(IList<T> premadeCollection)
        {
            foreach(T t in premadeCollection)
            {
                items.Add(t);
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Add an object to the Collection.
        /// </summary>
        /// <param name="item">The item you wish to add.</param>
        public void AddItem(T item)
        {
            if(duplicateSetting == DuplicateMode.UniqueOnly)
            {
                if (this.Contains(item))
                {
                    throw new DuplicateConflictException();
                }
            }
            items.Add(item);
        }

        /// <summary>
        /// Attempt to add an item to the list. Has a built-in check to ensure noo duplicate identifiers are added.
        /// </summary>
        /// <param name="item">The item to add</param>
        /// <returns>False if an object with the same identifier already exists. Otherwise, returns true and adds it.</returns>
        public bool AddUniqueItem(T item)
        {
            if (this.Contains(item))
            {
                return false;
            }
            items.Add(item);
            return true;
        }

        /// <summary>
        /// Check if this manager is managing an object with an identical identifier as a specified one.
        /// </summary>
        /// <param name="comparison">The object to compare.</param>
        /// <returns>Returns true if an object was found in Collection. Otherwise, false.</returns>
        public bool Contains(T comparison)
        {
            foreach(T t in items)
            {
                if(t.Identifier  == comparison.Identifier)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Searches for an item with specified identifier.
        /// </summary>
        /// <param name="identifier">The identifier value to filter by.</param>
        /// <param name="result">If the request was found, this variable will hold the result.</param>
        /// <returns>True if an item was found; otherwise, false.</returns>
        public bool FindItem(string identifier, out T result)
        {
            foreach(T t in items)
            {
                if(t.Identifier == identifier)
                {
                    result = t;
                    return true;
                }
            }
            result = default(T);
            return false;
        }

        /// <summary>
        /// Searches for an item with specified identifier, starting at index k.
        /// </summary>
        /// <param name="identifier">the identifier to filter to.</param>
        /// <param name="result">Will contain the result if found. If not, will be set to null.</param>
        /// <returns>True</returns>
        public bool FindItemOffset(string identifier, int k, out T result)
        {
            if(k > items.Count)
            {
                throw new IndexOutOfRangeException();
            }
            for(int i = k; i <= items.Count; i++)
            {
                if(items[i].Identifier == identifier)
                {
                    result = items[i];
                    return true;
                }
            }
            result = default(T);
            return false;
        }

        public bool FindAllItems(string identifier, out T[] resultArray)
        {
            bool hasResult = false;
            List<T> result = new List<T>();
            foreach(T t in items)
            {
                if(t.Identifier == identifier)
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

        /// <summary>
        /// Remove item an item.
        /// </summary>
        /// <param name="index">The item at this index will be removed.</param>
        public void RemoveAt(int index)
        {
            items.RemoveAt(index);
        }
        
        /// <summary>
        /// Remove an item.
        /// </summary>
        /// <exception cref="ObjectNotFoundException">Thrown if specified object is not contained in collection.</exception>
        /// <param name="item">The item to remove.</param>
        public void Remove(T item)
        {
            if (!this.Contains(item))
            {
                throw new ObjectNotFoundException();
            }
            items.Remove(item);
        }
        /// <summary>
        /// Remove an item by identifier match.
        /// </summary>
        /// <param name="identifier">The identifier whose object to remove.</param>
        /// <remarks>This method will remove the first instance only.</remarks>
        /// <returns>True if any object was removed. Otherwise, false.</returns>
        public bool Remove(string identifier)
        {
            foreach(T t in items)
            {
                if (t.Identifier == identifier)
                {
                    Remove(t);
                    return true;
                }
            }
            return false;
        }

        public int FindIndexOf(T item)
        {
            if (item == null)
            {
                throw new NullReferenceException(item + " was null.");
            }

            for(int i = 0; i < Count; i++)
            {
                if(ReferenceEquals(items[i], item))
                {
                    return i;
                }
            }
            throw new ObjectNotFoundException();
        }

        public void Replace(T old, T update)
        {
            items[FindIndexOf(old)] = update;
        }

        /// <summary>
        /// Remove all objects with a certain identifier.
        /// </summary>
        /// <param name="identifier"></param>
        /// <returns>True if 1 or more objects were removed. Otherwise, false.</returns>
        public bool RemoveAll(string identifier)
        {
            bool result = false;

            foreach(T t in items)
            {
                if(t.Identifier == identifier)
                {
                    Remove(t);
                    result = true;
                }
            }
            return result;
        }

        /// <summary>
        /// Switch the order in which 2 entries are stored.
        /// </summary>
        /// <param name="index1">An index in Collection.</param>
        /// <param name="index2">The index you wish to move it to. The object currently there will be swapped to that of the first.</param>
        public void Swap(int index1, int index2)
        {
            T temporaryStorage = items[index1];
            items[index1] = items[index2];
            items[index2] = temporaryStorage;
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
        }

        public void Fill(object[] source)
        {
            items.Clear();
            foreach(object obj in source)
            {
                items.Add((T)obj);
            }
        }

        public void Fill(DataSource connectionHandler, string connection)
        {
            items.Clear();
            foreach(object obj in connectionHandler(connection))
            {
                items.Add((T)obj);
            }
        }

        public void Fill<TResult>(Func<TResult> func) where TResult : IEnumerable<T>
        {
            foreach(T t in func())
            {
                AddItem(t);
            }
        }

        public void Fill<T1, TResult>(Func<T1,TResult> func, T1 arg1) where TResult : IEnumerable<T>
        {
            foreach(T t in func(arg1))
            {
                AddItem(t);
            }
        }

        public void Fill<T1, T2, TResult>(Func<T1, T2, TResult> func, T1 arg1, T2 arg2) where TResult : IEnumerable<T>
        {
            foreach(T t in func(arg1, arg2))
            {
                AddItem(t);
            }
        }

        /// <summary>
        /// Split the Manager Collection in 2, returning both new Managers
        /// </summary>
        /// <returns></returns>
        public ObjManager<T>[] Split()
        {
            List<T> firstHalf = new List<T>();
            int halfWayPoint = Count / 2;
            for(int i = 0; i <= halfWayPoint; i++)
            {
                firstHalf.Add(items[i]);
            }

            List<T> secondHalf = new List<T>();
            for(int i = halfWayPoint + 1; i <= Count; i++)
            {
                secondHalf.Add(items[i]);
            }
            ObjManager<T> mOne = new ObjManager<T>(duplicateSetting, firstHalf);
            ObjManager<T> mTwo = new ObjManager<T>(duplicateSetting, secondHalf);
            List<ObjManager<T>> result = new List<ObjManager<T>>();
            result.Add(mOne);
            result.Add(mTwo);
            return result.ToArray();
        }

        /// <summary>
        /// Split the Manager in 2, with the split at a specified index. Return both Managers.
        /// </summary>
        /// <param name="splitIndex">The index point to split at.</param>
        /// <returns></returns>
        public ObjManager<T>[] Split(int splitIndex)
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
            ObjManager<T> mOne = new ObjManager<T>(duplicateSetting, firstHalf);
            ObjManager<T> mTwo = new ObjManager<T>(duplicateSetting, secondHalf);
            return new ObjManager<T>[] { mOne, mTwo };
        }

        public int AmountOf<T2>() where T2 : T
        {
            int count = 0;
            foreach(T t in items)
            {
                if(t is T2)
                {
                    count++;
                }
            }
            return count;
        }

        public int AmountOf(string id)
        {
            int count = 0;
            foreach(T t in items)
            {
                if(t.Identifier == id)
                {
                    count++;
                }
            }
            return count;
        }

        public void Clear()
        {
            items.Clear();
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


        #endregion
    }






    /// <summary>
    /// Represent managers with any generic. Typeless.
    /// </summary>
    public abstract class Manager
    {
        public static ObjManager<T> Parse<T>(Manager manager) where T : ManagableObject
        {
            return (ObjManager<T>)manager;
        }

        public ObjManager<T> Parse<T>() where T : ManagableObject
        {
            return (ObjManager<T>)this;
        }
    }
}
