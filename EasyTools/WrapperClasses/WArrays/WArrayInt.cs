using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTools.WrapperClasses
{
    public static class WArrayInt
    {
        public static int[] TwoSumIndex(int[] collection, int target)
        {
            for(int i = 0; i < collection.Length; i++)
            {
                for(int j = 0; j < collection.Length; j++)
                {
                    if(i == j)
                    {
                        continue;
                    }
                    if(i + j == target)
                    {
                        return new int[] { i, j };
                    }
                }
            }
            return null;
        }

        public static int[] TwoSumValue(int[] collection, int target)
        {
            for(int i = 0; i < collection.Length; i++)
            {
                for(int j = 0; j < collection.Length; j++)
                {
                    int valueI = collection[i];
                    int valueJ = collection[j];
                    if(valueI == valueJ)
                    {
                        continue;
                    }
                    if(valueI + valueJ == target)
                    {
                        return new int[] { valueI, valueJ};
                    }
                }
            }
            return null;
        }
    }
}
