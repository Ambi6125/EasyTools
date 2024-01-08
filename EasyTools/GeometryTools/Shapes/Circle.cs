using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTools.GeometryTools.Shapes
{
    public class Circle : Shape
    {
        public double Radius { get; }

        

        public Circle(double diameter)
        {
            Radius = diameter * 0.5;
            Length = diameter;
            Width = diameter;
            CenterPoint = Point.UnknownOrNotExistant;
        }

        public Circle(double diameter, Point center)
        {
            Radius = diameter * 0.5;
            Length = diameter;
            Width = diameter;
            CenterPoint = center;
        }

        public double Diameter
        {
            get
            {
                return Length;
            }
        }

        public override double Circumference
        {
            get
            {
                return 2 * Diameter;
            }
        }

        public override double SurfaceArea
        {
            get
            {
                return Math.PI * (Math.Pow(Radius, 2));
            }
        }

        public override bool Contains(Point p)
        {
            throw new NotImplementedException();
        }

        protected override int OverlapCode()
        {
            throw new NotImplementedException();
        }
    }
}
