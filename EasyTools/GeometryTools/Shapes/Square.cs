using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTools.GeometryTools.Shapes
{
    public sealed class Square : Rectangle
    {
        public Square(Point topLeft, Point topRight, Point bottomRight, Point bottomLeft) : base (topLeft, topRight, bottomRight, bottomLeft)
        {
            string retry = "Try creating a Rectangle instead?";
            if(TLTR.Length != TRBR.Length)
            {
                throw new Exceptions.ImpossibleDataException($"Top line length and right line length are not equal ({TLTR.Length}, {TRBR.Length})\n{retry}");
            }
            else if (TLBL.Length != TRBR.Length)
            {
                throw new Exceptions.ImpossibleDataException($"Left line length and right line length are not equal. ({TLBL.Length}, {TRBR.Length})\n{retry}");
            }
            else if(TLBL.Length != TLTR.Length)
            {
                throw new Exceptions.ImpossibleDataException($"Left line length and top line length are not equal. ({TLBL.Length}, {TLTR.Length})\n{retry}");
            }

            if (!TLTR.IsParallelTo(BLBR) || !TLBL.IsParallelTo(TRBR))
            {
                throw new Exceptions.ImpossibleDataException("Created shape is not a square with parallel lines. Try creating a Quadrilateral instead.");
            }
            
        }
    }
}
