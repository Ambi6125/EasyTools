using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasyTools.ExtensionMethods;

namespace EasyTools.Algorithms
{
    public static class Algorithms
    {
        [Obsolete]
        public static long OldFibonacci(int n)
        {
            if ( n <= 1)
            {
                return n;
            }
            return OldFibonacci(n - 1) + OldFibonacci(n - 2);
        }

        public static ulong Fibonacci(int n)
        {
            ulong[] cache = new ulong[n + 1];

            if (n <= 1)
            {
                return (ulong)n;
            }

            if(cache[n] != 0)
            {
                return cache[n];
            }

            ulong nthNumber = Fibonacci(n - 1) + Fibonacci(n - 2);
            cache[n] = nthNumber;
            return nthNumber;
        }

        public static void Repeat(Action code, int amount)
        {
            for (int i = 1; i <= amount; i++)
            {
                code();
            }
        }

        public static void Repeat<T>(Action<T> code, T parameter, int amount)
        {
            for (int i = 1; i <= amount; i++)
            {
                code(parameter);
            }
        }

        public static void Repeat<T1, T2>(Action<T1, T2> code, T1 param1, T2 param2, int amount)
        {
            for (int i = 1; i <= amount; i++)
            {
                code(param1, param2);
            }
        }

        public static void Repeat<T1, T2, T3>(Action<T1, T2, T3> code, T1 param1, T2 param2, T3 param3, int amount)
        {
            for (int i = 1; i <= amount; i++)
            {
                code(param1, param2, param3);
            }
        }

        public static void Repeat<T>(T code, object[] args, int amount) where T : Delegate
        {
            for(int i = 1; i <= amount; i++)
            {
                code.DynamicInvoke(args);
            }
        }

        public static int Factorial(int n)
        {
            if (n < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(n), "Argument was below 1.");
            }
            else if(n == 1)
            {
                return 1;
            }
            else
            {
                return n * Factorial(n-1);
            }
        }
    }
}
