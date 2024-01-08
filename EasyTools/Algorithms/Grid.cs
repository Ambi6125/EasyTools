using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyTools.ExtensionMethods;

namespace EasyTools.Algorithms
{
    public class Grid<T> : IEnumerable<T>
    {
        private T[][] backGrid;

        public int Rows
        {
            get
            {
                return backGrid.Length;
            }
        }
        public int Columns
        {
            get
            {
                return backGrid[0].Length;
            }
        }

        public Grid(int rows, int columns)
        {
            List<T[]> backList = new List<T[]>();
            for(int i = 0; i < rows; i++)
            {
                backList.Add(new T[columns]);
            }
            backGrid = backList.ToArray();
        }

        public T this[int row, int column]
        {
            get
            {
                try
                {
                    return backGrid[row][column];
                }
                catch (NullReferenceException)
                {
                    return default(T);
                }
            }
            set
            {
                backGrid[row][column] = value;
            }
        }

        public T[] ReadFullRow(int row)
        {
            List<T> result = new List<T>();
            foreach(T t in backGrid[row])
            {
                result.Add(t);
            }
            return result.ToArray();
        }
        public T[] ReadFullColumn(int column)
        {
            List<T> result = new List<T>();
            foreach(T[] array in backGrid)
            {
                try
                {
                    result.Add(array[column]);
                }
                catch (NullReferenceException)
                {
                    result.Add(default(T));
                }
                catch (IndexOutOfRangeException)
                {
                    return null;
                }
            }
            return result.ToArray();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(string.Empty);
            for(int outerLoop = 0; outerLoop < backGrid.Length; outerLoop++)
            {
                StringBuilder lineBuilder = new StringBuilder(string.Empty);
                for(int innerLoop = 0; innerLoop < backGrid[outerLoop].Length; innerLoop++)
                {
                    lineBuilder.Append(" " + backGrid[outerLoop][innerLoop].ToString());
                }
                sb.AppendLine($"[{lineBuilder.ToString()} ]");
            }
            return sb.ToString();
        }

        public Sequence<T> ReadSequence(int startRow, int startCol, int rowIncrease, int colIncrease)
        {
            int currentRow = startRow;
            int currentCol = startCol;
            List<T> result = new List<T>();
            while (currentRow < backGrid.Length && currentCol < backGrid[currentRow].Length)
            {
                result.Add(backGrid[currentRow][currentCol]);
                currentRow += rowIncrease;
                currentCol += colIncrease;
            }
            return result.ToSequence();
        }

        /// <summary>
        /// Set an entire row to the same value.
        /// </summary>
        public void SetFullRow(int row, T asValue)
        {
            for(int i = 0; i < backGrid[row].Length; i++)
            {
                this[row, i] = asValue;
            }
        }

        /// <summary>
        /// Sets an entire row by invoking the delegate. When using delegates for this method, it will be invoked on every
        /// individual tile.
        /// </summary>
        public void SetFullRow(int row, Func<T> func)
        {
            for (int i = 0; i < backGrid[row].Length; i++)
            {
                this[row, i] = func();
            }
        }
        /// <summary>
        /// Sets an entire row by invoking the delegate. When using delegates for this method, it will be invoked on every
        /// individual tile.
        /// </summary>
        public void SetFullRow<T1>(int row, Func<T1,T> func, T1 arg1)
        {
            for (int i = 0; i < backGrid[row].Length; i++)
            {
                this[row, i] = func(arg1);
            }
        }
        /// <summary>
        /// Sets an entire row by invoking the delegate. When using delegates for this method, it will be invoked on every
        /// individual tile.
        /// </summary>
        public void SetFullRow<T1, T2>(int row, Func<T1, T2, T> func, T1 arg1, T2 arg2)
        {
            for (int i = 0; i < backGrid[row].Length; i++)
            {
                this[row, i] = func(arg1, arg2);
            }
        }

        public void SetSequence(Sequence<T> sequence, int startRow, int startColumn, int rowIncrease, int columnIncrease)
        {
            int row = startRow;
            int column = startColumn;
            int iteration = 0;

            while(!(row > backGrid.Length) && !(column > backGrid[row].Length))
            {
                this[row, column] = sequence[iteration];
                row += rowIncrease;
                column += columnIncrease;
                iteration++;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return (IEnumerator<T>)backGrid.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        //public void RandomizeData()
        //{
        //    Func fillFunc;

        //    if()
        //}
    }
}
