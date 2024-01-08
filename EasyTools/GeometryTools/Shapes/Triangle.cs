using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTools.GeometryTools.Shapes
{
    public class Triangle : Shape
    {
        protected override int OverlapCode()
        {
            throw new NotImplementedException();
        }
        #region Side Lines
        public Line[] Sides
        {
            get
            {
                return new Line[] { Side1, Side2, Side3 };
            }
        }

        public Line Side1 { get; }
        public Line Side2 { get; }
        public Line Side3 { get; }
        #endregion


        /// <summary>
        /// Create a triangle shape.
        /// </summary>
        /// <param name="base">Called by Width. The base size of the triangle.</param>
        /// <param name="height">Called by Length. The height of the triangle.</param>
        public Triangle(double @base, double height)
        {
            Width = @base;
            Length = height;
        }

        public override bool Contains(Point p)
        {
            throw new NotImplementedException();
        }

        public override double SurfaceArea
        {
            get
            {
                return 0.5 * (Length * Width);
            }
        }
    }
}
