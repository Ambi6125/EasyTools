using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTools.GeometryTools
{
    public abstract class Shape3D : Shape
    {
        public double Depth { get; private set; }

        public virtual double Volume
        {
            get
            {
                return Length * Width * Depth;
            }
        }
    }
}
