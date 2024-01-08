using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTools.Algorithms
{
    public static class Generate
    {
        public static int NewInt()
        {
            Random rng = new Random();
            return rng.Next();
        }
        public static int NewInt(int min, int max)
        {
            Random rng = new Random();
            return rng.Next(min, max);
        }

        public static void ExistingInt(out int reference)
        {
            reference = new Random().Next();
        }
        public static void ExisingInt(out int reference, int min, int max)
        {
            reference = new Random().Next(min, max);
        }

        public static string NewString(int length)
        {
            StringBuilder sb = new StringBuilder(String.Empty);
            Random rng = new Random();
            for(int i = 0; i < length; i++)
            {
                sb.Append(Convert.ToChar(rng.Next(0, 25) + 65));
            }
            return sb.ToString();

        }

        public static string ComplexString(int length)
        {
            char[] chars = "ABCDEFGHIJKLMNOPQRSTUVXYZabcdefghijklmnopqrstuvwxys0123456789!@#$%^&*()-=".ToCharArray();
            StringBuilder sb = new StringBuilder();
            Random rng = new Random();
            for(int i = 0; i < length; i++)
            {
                int index = rng.Next(0, chars.Length);
                sb.Append(chars[index]);
            }
            return sb.ToString();
        }
    }
}
