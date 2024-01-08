using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyTools.ExtensionMethods;
using EasyTools.ObjectManagingTools;

namespace EasyTools.Algorithms
{
    public static class Sort
    {
        public static void Bubble<T>(IList<T> comparables) where T : IComparable<T>
        {
            bool changesMade = true;

            while (changesMade)
            {
                changesMade = false;
                for (int i = 0; i < comparables.Count - 1; i++)
                {
                    if (comparables[i].CompareTo(comparables[i + 1]) > 0)
                    {
                        changesMade = true;
                        T temp = comparables[i];
                        comparables[i] = comparables[i + 1];
                        comparables[i + 1] = temp;
                    }
                }
            } 

           
        }

        
    }
}
