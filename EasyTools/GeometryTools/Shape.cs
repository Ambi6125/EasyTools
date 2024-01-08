using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTools.GeometryTools
{
    public enum OriginSide { TopLeft }
    public abstract class Shape
    {
        public double Length { get; protected set; }
        public double Width { get; protected set; }

        public Point CenterPoint { get; protected set; }

        public virtual double Circumference
        {
            get
            {
                return (Length + Width) * 2; 
            }
        }

        public virtual double SurfaceArea
        {
            get
            {
                return Length * Width;
            }
        }

        public void MoveCenterPoint(double xOffset, double yOffset)
        {
            CenterPoint.Move(xOffset, yOffset);
        }

        public void MoveCenterPointTo(double x, double y)
        {
            CenterPoint = new Point(x, y);
        }

        public virtual void MoveCenterPointTo(Point p)
        {
            CenterPoint = new Point(p.X, p.Y);
        }

        public virtual void SetPointAsCenter(Point point)
        {
            CenterPoint = point;
        }

        public abstract bool Contains(Point p);

        public static bool Overlaps(Shape s1, Shape s2)
        {
            //Use s1.OverlapCode() and s2.OverlapCode() to determine if they overlap.
            throw new NotImplementedException();
        }

        protected abstract int OverlapCode();
    }
}
