using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTools.ObjectManagingTools
{
    internal class ObjManagerReader<T> : IEnumerator<T> where T : ManagableObject
    {
        private ObjManager<T> enumManager;
        private int index;

        public ObjManagerReader(ObjManager<T> manager)
        {
            enumManager = manager;
            index = EasyTools.EnumeratorDefaultValue;
        }

        public T Current
        {
            get
            {
                return enumManager[index];
            }
        }

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public void Dispose()
        {
            return;
        }

        public bool MoveNext()
        {
            return ++index < enumManager.Count;
        }

        public void Reset()
        {
            index = EasyTools.EnumeratorDefaultValue;
        }
    }

    /// <summary>
    /// Enumerates through Manager instance
    /// </summary>
    internal class ManagerReader<T, IDType> : IEnumerator<T>  where T : IManagable<IDType>
    {
        private Manager<T, IDType> enumManager;
        private int index;

        public ManagerReader(Manager<T, IDType> manager)
        {
            enumManager = manager;
            index = -1;
        }

        public void Reset()
        {
            index = -1;
        }

        public bool MoveNext()
        {
            return ++index < enumManager.Count();
        }

        public void Dispose()
        {
            return;
        }

        public T Current
        {
            get
            {
                return enumManager[index];
            }
        }

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }
    }

}
