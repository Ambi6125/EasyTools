using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTools.GeometryTools
{
    public class Line : Shape
    {
        private bool isVertical = false;

        public Point A { get; private set; }
        public Point B { get; private set; }


        /// <summary>
        /// The amount Y changes per increment of X.
        /// </summary>
        public double Slope { get; }

        public Line(Point a, Point b)
        {
            if(a.X == b.X)
            {
                isVertical = true;
            }
            Width = 0;
            Length = a.DistanceTo(b);
            A = a;
            B = b;

            Slope = CalculateSlope(a, b);
        }

        private double CalculateSlope(Point p1,  Point p2)
        {
            if(p2.X - p1.X == 0)
            {
                return 0;
            }

            double resultY = p2.Y - p1.Y;
            double resultX = p2.X - p1.X;

            return resultY / resultX;
        }

        public override double Circumference => Length;

        /// <summary>
        /// Y at X = 0;
        /// </summary>
        public double BConstant
        {
            get
            {
                return A.Y - Slope * A.X;
            }
        }

        public string Formula()
        {
            if (isVertical)
            {
                return $"x = {A.X}";
            }
            if(BConstant < 0)
            {
                return $"y = {Slope}x - {BConstant * -1}";
            }
             return $"y = {Slope}x + {BConstant}";
        }

        public string Formula(int roundToDecimals)
        {
            if (isVertical)
            {
                return $"x = {A.X}";
            }
            if (BConstant < 0)
            {
                return $"y = {Math.Round(Slope, roundToDecimals)}x - {Math.Round(BConstant, roundToDecimals) * -1}";
            }
            return $"y = {Math.Round(Slope, roundToDecimals)}x + {Math.Round(BConstant, roundToDecimals)}";
        }

        public bool IsSteeperThan(Line comparison)
        {
            if (isVertical)
            {
                return true;
            }
            return Slope > comparison.Slope;
        }

        public override string ToString()
        {
            return $"Line of {Formula(2)}";
        }

        /// <summary>
        /// Check if this line is parallel to another.
        /// </summary>
        /// <param name="comparisonLine">The second line to compare this to</param>
        /// <returns>True if both lines have identical slopes, otherwise, false.</returns>
        public bool IsParallelTo(Line comparisonLine)
        {
            if (this.Slope == comparisonLine.Slope)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Check if this line is perpendicular to another.
        /// </summary>
        /// <param name="line">The second line to compare this to.</param>
        /// <returns>True if the slope of the first line is inverse of the second, otherwise, false.</returns>
        public bool IsPerpendicularTo(Line line)
        {
            if(Math.Round(line.Slope, 2) == Math.Round(-(1 / this.Slope), 2))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Check if a line would cross through a certain point.
        /// </summary>
        /// <param name="p">The point to check for.</param>
        /// <returns>True if the Y coordinate is on the lineA at any given X.</returns>
        public virtual bool DrawsThrough(Point p)
        {
            if (isVertical)
            {
                return A.X == p.X;
            }
            return p.Y == p.X * Slope + BConstant;
        }

        public override bool Contains(Point p)
        {
            return DrawsThrough(p);
        }

        protected override int OverlapCode()
        {
            throw new NotImplementedException();
        }

        public Point Intersects(Line l)
        {
            throw new NotImplementedException();
        }
    }
}
