using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTools.Algorithms
{
    public struct FullModResult
    {
        public enum HandDown { Number, Remainder };
        public double FullNumber { get; }
        public double Remainder { get; }

        public FullModResult(double number, double divider)
        {
            FullNumber = number / divider;
            Remainder = number % divider;
        }

        public void FullRepeat()
        {
            this = new FullModResult(FullNumber, Remainder);
        }

        //public FullModResult Repeat(HandDown usingWhichValue, double otherValue)
        //{
        //    if (usingWhichValue == HandDown.Number)
        //    {
        //        this = new FullModResult(FullNumber, otherValue);
        //    }
        //    else if(usingWhichValue == HandDown.Remainder)
        //    {
        //        this = new FullModResult(otherValue, Remainder);
        //    }
        //}



        public override string ToString()
        {
            return $"{FullNumber}, {Remainder}";
        }
    }
}
