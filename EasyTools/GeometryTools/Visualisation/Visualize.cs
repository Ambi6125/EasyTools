using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace EasyTools.GeometryTools.Visualisation
{
    public static class Visualize
    {
        public static void Spawn(Shape s)
        {
            if(s.CenterPoint == Point.UnknownOrNotExistant)
            {
                throw new Exceptions.UnknownSizeException("s.CenterPoint == Point.UnknownOrNonExistant");
            }
        }

        public static string ReadDetails(Shape s)
        {
            return s.ToString();
        }


    }
}
