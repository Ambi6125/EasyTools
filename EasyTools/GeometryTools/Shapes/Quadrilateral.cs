using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTools.GeometryTools.Shapes
{
    
    public class Quadrilateral : Shape
    {
        #region Corners
        public Point TopLeft { get; protected set; }
        public Point TopRight { get; protected set; }
        public Point BottomLeft { get; protected set; }
        public Point BottomRight { get; protected set; }

        public Point[] CornerPoints
        {
            get
            {
                return new Point[] { TopLeft, TopRight, BottomLeft, BottomRight };
            }
        }
        #endregion
        //public  Point CenterPoint
        //{
        //    get
        //    {
        //        return new Point(TopLeft.DistanceTo(BottomRight), BottomLeft.DistanceTo(TopRight));
        //    }
        //}

        #region Sides

        public Line TLTR
        {
            get
            {
                return new Line(TopLeft, TopRight);
            }
        }
        public Line TRBR 
        { 
            get
            {
                return new Line(TopRight, BottomRight);
            } 
        }
        public Line TLBL
        {
            get
            {
                return new Line(TopLeft, BottomLeft);
            }
        }
        public Line BLBR
        {
            get
            {
                return new Line(BottomLeft, BottomRight);
            }
        }
        #endregion


        public Quadrilateral(double length, double width)
        {
            Length = length;
            Width = width;

            for(int i = 0; i < 4; i++)
            {
                CornerPoints[i] = Point.UnknownOrNotExistant;
            }
        }


        public Quadrilateral(Point cornerTopLeft, Point cornerTopRight, Point cornerBottomRight, Point cornerBottomLeft)
        {
            Point[] points = new Point[4] { cornerTopLeft, cornerTopRight, cornerBottomRight, cornerBottomLeft };
            //Check if points are correctly ordered.
            bool checkFailed = false;
            Point[] lowestXpts = Point.LowestX(points);
            Point lowestXPoint = lowestXpts[0];
            if (lowestXPoint == cornerBottomLeft || lowestXPoint == cornerTopLeft) //The lowest x should be one of the 2 left pts.
            {
                if(lowestXpts.Contains(cornerTopRight) || lowestXpts.Contains(cornerBottomRight))
                {
                    checkFailed = true;
                }
            }
            else
            {
                checkFailed = true;
            }

            if (checkFailed)
            {
                throw new Exceptions.ImpossibleDataException("Impossible data. Make sure points are provided in accordance to the correct parameter. (Did you enter a right-side point in a left-side parameter?)");
            }
            

            Length = cornerTopLeft.DistanceTo(cornerBottomLeft);
            Width = cornerTopLeft.DistanceTo(cornerTopRight);

            TopLeft = cornerTopLeft;
            TopRight = cornerTopRight;
            BottomLeft = cornerBottomLeft;
            BottomRight = cornerBottomRight;
        }

        [Obsolete("Functionality not ready yet. Avoid using this constructor for the time being.")]
        public Quadrilateral(Point bottomLeft, double length, double width)
        {
            //BottomLeft = bottomLeft;

            //Length = length;
            //Width = width;

            //TopLeft
            throw new NotImplementedException();
        }


        public Line DrawDiagonal(Geometry.Direction direction)
        {
            switch (direction)
            {
                case Geometry.Direction.TopLeftToBottomRight:
                    return new Line(TopLeft, BottomRight);
                case Geometry.Direction.BottomLeftToTopRight:
                    return new Line(BottomLeft, TopRight);
                default:
                    throw new ArgumentException("Impossible direction. Draw diagonal from 2 opposing cornerpoints.");
                
            }
        }

        public override bool Contains(Point p)
        {
            Point lowComparisonXaxis = Point.LowestX(CornerPoints)[0];
            Point highComparisonXaxis = Point.HighestX(CornerPoints)[0];
            Point lowComparisonYaxis = Point.LowestY(CornerPoints)[0];
            Point highComparisonYaxis = Point.HighestY(CornerPoints)[0];

            if(p.X < lowComparisonXaxis.X)
            {
                return false;
            }
            if(p.X > highComparisonXaxis.X)
            {
                return false;
            }
            if(p.Y < lowComparisonYaxis.Y)
            {
                return false;
            }
            if(p.Y > highComparisonYaxis.Y)
            {
                return false;
            }
            return true;
        }

        protected override int OverlapCode()
        {
            throw new NotImplementedException();
        }

        public override void MoveCenterPointTo(Point p)
        {
            
        }
    }
}