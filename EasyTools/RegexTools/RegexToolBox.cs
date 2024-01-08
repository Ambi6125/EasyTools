using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace EasyTools.RegexTools
{
    public static class RegexToolBox
    {
        public static bool IsMatch(string input, string pattern)
        {
            return Regex.IsMatch(input, pattern);
        }

        public static string FileEnd
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public static bool IsEmail(string sample)
        {
            Regex validateEmailRegex = new Regex("^\\S+@\\S+\\.\\S+$");
            return validateEmailRegex.IsMatch(sample);
        }


        public static string EmailAddress => "^\\S+@\\S+\\.\\S+$";
    }
}
