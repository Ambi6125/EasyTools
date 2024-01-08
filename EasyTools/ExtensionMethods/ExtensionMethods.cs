using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyTools.Algorithms;

/// <summary>
/// Provides tons of useful extension methods to improve quality of life.
/// </summary>
namespace EasyTools.ExtensionMethods
{
    //public enum SearchCommand { Matches, IsNot }
    //public enum SearchCommandModifier { Unless }
    
    public static class ExtensionMethods
    {
        #region Strings
        public static string ChangeFirstLetterCase(this string self)
        {
            if (self.Length < 0)
            {
                char[] charArray = self.ToCharArray();
                charArray[0] = char.IsUpper(charArray[0]) ? char.ToLower(charArray[0]) : char.ToUpper(charArray[0]);
                return new string(charArray);
            }
            return self;
        }

        public static string Scramble(this string self)
        {
            char[] vs = self.ToCharArray();
            Random rnd = new Random();
            return new string(vs.OrderBy(x => rnd.Next()).ToArray());
        }

        public static string Reverse(this string self)
        {
            StringBuilder sb = new StringBuilder(String.Empty);
            for (int i = self.Length - 1; i >= 0; i--)
            {
                sb.Append(self[i]);
            }
            return sb.ToString();
        }

        public static void Print(this string self)
        {
            Console.WriteLine(self);
        }

        #endregion

        #region Bools

        /// <summary>
        /// Toggles the boolean value automatically. Cannot take properties or indexers.
        /// </summary>
        /// <param name="currentBool"></param>
        /// <returns></returns>
        public static bool Toggle(this ref bool currentBool)
        {
            currentBool = !currentBool;
            return currentBool;  
        }

        #endregion

        #region Numeric structs
        public static long AsBinary(this int thisInt)
        {
            //List<int> resultBuild = new List<int>();
            //int newInt = thisInt;
            //FullModResult resMod;
            throw new NotImplementedException();
        }
        
        public static int Plus(this int self, int n)
        {
            return self + n;
        }

        public static int Minus(this int self, int n)
        {
            return self - 1;
        }

        public static int Times(this int self, int n)
        {
            return self * n;
        }

        public static int DividedBy(this int self, int n)
        {
            return self / n;
        }

        public static FullModResult FullMod(this int self, int n)
        {
            return new FullModResult(self, n);
        }

        #region Percentage
        /// <summary>
        /// Calculates a specified percentage of the integers value and returns the result.
        /// </summary>
        /// <param name="currentInt"></param>
        /// <param name="percent">A double value between 1 and 100</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if percentage parameter is not between 1 and 100.</exception>
        /// <returns></returns>
        public static int Percentage(this int currentInt, double percentage)
        {
            if(percentage > 0)
            {
                double percentConverted = percentage / 100;
                return (int)(currentInt * percentConverted);
            }
            else
            {
                throw new ArgumentOutOfRangeException($"Percentage was less than 0: {percentage}");
            }
        }

        public static double Percentage(this double currentInt, double percentage)
        {
            if (percentage > 0)
            {
                double percentConverted = percentage / 100;
                return currentInt * percentConverted;
            }
            else
            {
                throw new ArgumentOutOfRangeException($"Percentage was out of range 1-100: {percentage}");
            }
        }

        public static decimal Percentage(this decimal currentInt, decimal percentage)
        {
            if (percentage <= 100 && percentage > 0)
            {
                decimal percentConverted = percentage / 100;
                return (currentInt * percentConverted);
            }
            else
            {
                throw new ArgumentOutOfRangeException($"Percentage was out of range 1-100: {percentage}");
            }
        }

        public static float Percentage(this float currentInt, double percentage)
        {
            if (percentage <= 100 && percentage > 0)
            {
                double percentConverted = percentage / 100;
                return (int)(currentInt * percentConverted);
            }
            else
            {
                throw new ArgumentOutOfRangeException($"Percentage was out of range 1-100: {percentage}");
            }
        }

        public static double PercentOf(this int thisInt, double number)
        {
            if (thisInt <= 0)
            {
                throw new ArgumentException("Percentage must be larger than 0.");
            }

            double percentFactor = thisInt / 100.0;
            return percentFactor * number;
        }


        public static double PercentOf(this double thisDouble, double number)
        {
            if (thisDouble <= 0)
            {
                throw new ArgumentException("Percentage must be larger than 0.");
            }
            double percentFactor = thisDouble / 100.0;
            return percentFactor * number;
        }

        #endregion


        #endregion


        #region Parsing
        public static bool TryParse(this string currentString, out double outResult)
        {
            double tempResult;
            if(double.TryParse(currentString, out tempResult))
            {
                outResult = tempResult;
                return true;
            }
            else
            {
                outResult = 0;
                return false;
            }
        }

        public static bool TryParse(this string currentString, out int outResult)
        {
            int tempResult;
            if (int.TryParse(currentString, out tempResult))
            {
                outResult = tempResult;
                return true;
            }
            else
            {
                outResult = 0;
                return false;
            }
        }
        #endregion

        #region Collections

        #region General
        public static List<T> AsList<T>(this IList<T> self)
        {
            List<T> result = new List<T>();

            foreach (T t in self)
            {
                result.Add(t);
            }
            return result;
        }

        public static IList<T> Reverse<T> (this IList<T> self)
        {
            IList<T> result = new List<T>();
            for (int i = self.Count - 1; i >= 0; i--)
            {
                result.Add(self[i]);
            }
            return result;
        }

        public static IList<T> Between<T>(this IList<T> self, int floor, int cap)
        {
            List<T> result = new List<T>();
            try
            {
                for (int i = floor -1; i <= cap -1; i++)
                {
                    result.Add(self[i]);
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                return result;
            }
            return result;
        }

        public static void WriteAll<T> (this IEnumerable<T> self)
        {
            foreach(T t in self)
            {
                Console.WriteLine(t);
            }
        }

        public static void Write<T> (this IList<T> self, int index)
        {
            if(index >= self.Count)
            {
                throw new ArgumentOutOfRangeException("index parameter did not fit in collection size.");
            }
            for(int i = index; i < self.Count; i++)
            {
                Console.WriteLine(self[i]);
            }
        }

        public static void Write<T> (this IList<T> self, int index, int cap)
        {
            if(index >= self.Count)
            {
                throw new ArgumentOutOfRangeException("index parameter did not fit in collection size.");
            }
            if(cap >= self.Count)
            {
                throw new ArgumentOutOfRangeException("last index (cap) parameter did not fit in collection size.");
            }
            for(int i = index; i <= cap; i++)
            {
                Console.WriteLine(self[i]);
            }
        }

        public static string[] ReadAll<T> (this IList<T> self)
        {
            string[] result = new string[0];
            foreach(T t in self)
            {
                result = result.With(t.ToString());
            }
            return result;
        }

        public static string[] Read<T> (this IList<T> self, int index)
        {
            if(index >= self.Count)
            {
                throw new ArgumentOutOfRangeException();
            }
            List<string> result = new List<string>();
            for(int i = index; i < self.Count; i++)
            {
                result.Add(self[i].ToString());
            }
            return result.ToArray();
        }

        public static string[] Read<T> (this IList<T> self, int index, int cap)
        {
            if(index >= self.Count || cap >= self.Count)
            {
                throw new ArgumentOutOfRangeException();
            }
            if(cap <= index)
            {
                throw new ArgumentException("Cap must be larger than index.");
            }
            List<string> result = new List<string>();
            for(int i = index; i <= cap; i++)
            {
                result.Add(self[i].ToString());
            }
            return result.ToArray();
        }

        public static bool Populate<T> (this IList<T> self, IList<object> collection)
        {
            try
            {
                foreach (object ob in collection)
                {
                    self.Add((T)ob);
                }
            }
            catch(InvalidCastException)
            {
                return false;
            }
            return true;
        }

        public static bool IsNullOrEmpty<T>(this IList<T> self)
        {
            if(self != null)
            {
                if(self.Count == 0)
                {
                    return true;
                }
                return true;
            }
            return false;
        }
        #endregion

        #region Arrays
        public static T[] Extend<T>(this T[] currentArray, int amount)
        {
            int endAmount = currentArray.Length + amount;
            T[] array = new T[endAmount];
            currentArray.CopyTo(array, 0);
            return array;
        }

        public static T[] With<T>(this T[] self, T value)
        {
            List<T> list = self.AsList();
            list.Add(value);
            return list.ToArray();
        }

        public static T[] With<T>(this T[] self, IList<T> values)
        {
            if(self == values)
            {
                throw new ArgumentException("Cannot include self.");
            }
            List<T> result = self.AsList();
            foreach(T t in values)
            {
                result.Add(t);
            }
            return result.ToArray();
        }

        
        #endregion

        #endregion

        #region Nullables
        public static int AsInt(this int? currentNullableInt)
        {
            if(currentNullableInt == null)
            {
                throw new InvalidOperationException($"{currentNullableInt} was null.");
            }
            return (int)currentNullableInt;
        }

        public static double AsDouble(this double? currentNullableDouble)
        {
            if(currentNullableDouble == null)
            {
                throw new InvalidOperationException($"{currentNullableDouble} was null.");
            }
            return (double)currentNullableDouble;
        }

        public static int? MakeNullable(this int thisInt)
        {
            return thisInt as int?;
        }

        public static double? MakeNullable(this double thisDouble)
        {
            return thisDouble as double?;
        }
        #endregion
    }
    
}
