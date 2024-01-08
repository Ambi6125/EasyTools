using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTools.GeometryTools.Shapes
{
    public class Rectangle : Quadrilateral
    {
        public Rectangle(Point topLeft, Point topRight, Point bottomRight, Point bottomLeft) : base (topLeft, topRight, bottomRight, bottomLeft)
        {
            if(TLTR.Length != BLBR.Length)
            {
                throw new Exceptions.ImpossibleDataException($"Shape is not a rectangle: Top line length ({TLTR.Length}) is not the same as bottom line length ({BLBR.Length}).\nTry creating a Quadrilateral instead?");
            }
            if(TLBL.Length != TRBR.Length)
            {
                throw new Exceptions.ImpossibleDataException($"Shape is not a rectangle: Left line length ({TLBL.Length}) is not the same as right line length ({TRBR.Length})");
            }
        }
    }
}
