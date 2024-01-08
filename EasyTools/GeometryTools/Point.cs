using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace EasyTools.GeometryTools
{
    public  class Point : IEquatable<Point>
    {
        public double X { get; private set; }

        public double Y { get; private set; }

        public Point(double xCoordinate, double yCoordinate)
        {
            X = xCoordinate;
            Y = yCoordinate;
        }

        public Point(Point state)
        {
            X = state.X;
            Y = state.Y;
        }

        public virtual double[] Coordinates
        {
            get
            {
                return new double[] { X, Y };
            }
        }

        public static Point UnknownOrNotExistant
        {
            get
            {
                return null;
            }
        }

        public virtual double DistanceTo(Point target)
        {
            double resultX = Math.Pow(this.X + target.X, 2);
            double resultY = Math.Pow(this.Y + target.Y, 2);

            return Math.Sqrt(resultX + resultY);
        }

        public void MoveX(double distance)
        {
            X += distance;
        }

        public void MoveY(double distance)
        {
            Y += distance;
        }

        public void Move(double distanceX, double distanceY)
        {
            MoveX(distanceX);
            MoveY(distanceY);
        }

        /// <summary>
        /// Find an imaginative point
        /// </summary>
        /// <param name="offset">How long to follow along the x axis</param>
        /// <returns>The distance from 0 that the point is at.</returns>
        public double DotX(double offset)
        {
            return X + offset;
        }

        public double DotY(double offset)
        {
            return Y + offset;
        }

        public Point DotXY(double offsetX, double offsetY)
        {
            return new Point(DotX(offsetX), DotY(offsetY));
        }

        #region Comparison methods
        public bool HasLowerX(Point comparison)
        {
            if (X < comparison.X && this != comparison)
            {
                return true;
            }
            return false;
        }

        public bool HasLowerOrEqualX(Point comparison)
        {
            if (X <= comparison.X && this != comparison)
            {
                return true;
            }
            return false;
        }

        public bool HasLowerY(Point comparison)
        {
            if (Y < comparison.Y && this != comparison)
            {
                return true;
            }
            return false;
        }

        public bool HasLowerOrEqualY(Point comparison)
        {
            if (Y <= comparison.Y && this != comparison)
            {
                return true;
            }
            return false;
        }

        public bool HasHigherX(Point comparison)
        {
            if(X  > comparison.X && this != comparison)
            {
                return true;
            }
            return false;
        }

        public bool HasHigherOrEqualX(Point comparison)
        {
            if (X >= comparison.X && this != comparison)
            {
                return true;
            }
            return false;
        }

        public bool HasHigherY(Point comparison)
        {
            return Y > comparison.Y;
        }

        public bool HasHigherOrEqualY(Point comparison)
        {
            return Y >= comparison.Y;
        }


        
        public static Point LowerX(Point sample, Point comparison)
        {
            if (sample.HasLowerX(comparison))
            {
                return sample;
            }
            return comparison;
        }

        public static Point LowerY(Point sample, Point comparison)
        {
            if (sample.HasLowerY(comparison))
            {
                return sample;
            }
            return comparison;
        }

        public static Point HigherX(Point sample, Point comparison)
        {
            if (sample.HasHigherX(comparison))
            {
                return sample;
            }
            return comparison;
        }

        public static Point HigherY(Point sample, Point comparison)
        {
            if (sample.HasHigherY(comparison))
            {
                return sample;
            }
            return comparison;
        }

        public static Point[] LowestX(IList<Point> sample)
        {
            List<Point> result = new List<Point>();
            double currentLowest = sample[0].X;
            for (int i = 1; i < sample.Count; i++)
            {
                if (sample[i].X < currentLowest)
                {
                    result.Clear();
                    currentLowest = sample[i].X;
                    result.Add(sample[i]);
                }
                else if (sample[i].X == currentLowest)
                {
                    result.Add(sample[i]);
                }
            }
            return result.ToArray();
        }

        public static Point[] LowestY(IList<Point> sample)
        {
            List<Point> result = new List<Point>();
            double currentLowest = sample[0].Y;
            for(int i = 1; i < sample.Count; i++)
            {
                Point currentPoint = sample[i];
                if(currentPoint.Y < currentLowest)
                {
                    sample.Clear();
                    currentLowest = currentPoint.Y;
                    result.Add(currentPoint);
                }
                else if(currentPoint.Y == currentLowest)
                {
                    result.Add(currentPoint);
                }
            }
            return result.ToArray();
        }

        public static Point[] HighestX(IList<Point> sample)
        {
            List<Point> result = new List<Point>();
            double currentHighest = sample[0].X;
            for(int i = 1; i < sample.Count; i++)
            {
                Point currentPoint = sample[i];
                if (currentPoint.X > currentHighest)
                {
                    result.Clear();
                    currentHighest = currentPoint.X;
                    result.Add(currentPoint);
                }
                else if (currentPoint.X == currentHighest)
                {
                    result.Add(currentPoint);
                }
            }
            return result.ToArray();
        }

        public static Point[] HighestY(IList<Point> sample)
        {
            List<Point> result = new List<Point>();
            double currentHighest = sample[0].Y;
            for (int i = 1; i < sample.Count; i++)
            {
                Point currentPoint = sample[i];
                if (currentPoint.Y > currentHighest)
                {
                    result.Clear();
                    currentHighest = currentPoint.Y;
                    result.Add(currentPoint);
                }
                else if (currentPoint.Y == currentHighest)
                {
                    result.Add(currentPoint);
                }
            }
            return result.ToArray();
        }

        #endregion

        /// <summary>
        /// Draw a line from this point to another.
        /// </summary>
        /// <param name="target">A point containing the coordinates that the line should go to.</param>
        /// <returns>A new Line where A = this and B is target.</returns>
        public Line DrawTo(Point target)
        {
            return new Line(this, target);
        }

        public Line DrawFor(double xOffset, double yOffset)
        {
            Point endPoint = new Point(X + xOffset, Y + yOffset);

            return new Line(this, endPoint);
        }

        public bool Equals(Point other)
        {
            return X.Equals(other.X) && Y.Equals(other.Y);
        }
    }
}
