using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTools.GeometryTools
{
    class InboundLine : Line
    {
        public InboundLine(Point a, Point b) : base (a, b)
        { 
            //No additional ctor actions
        }

        

        public override bool DrawsThrough(Point p)
        {
            if (!base.DrawsThrough(p))
            {
                return false;
            }

            return
                p.X > Point.LowerX(A, B).X &&
                p.Y > Point.LowerY(A, B).Y &&
                p.X < Point.HigherX(A, B).X &&
                p.Y < Point.HigherY(A, B).Y;
        }
    }
}
