using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasyTools.GeometryTools
{
    public class Point3D : Point
    {
        public double Z { get; private set; }

        public Point3D(double x, double y, double z) : base (x, y)
        {
            Z = z;
        }

        public override double[] Coordinates
        {
            get
            {
                List<double> result = base.Coordinates.ToList();

                result.Add(Z);

                return result.ToArray();
            }
        }

        public void MoveZ(double distance)
        {
            Z += distance;
        }

        public void Move(double distanceX, double distanceY, double distanceZ)
        {
            Move(distanceX, distanceY);
            MoveZ(distanceZ);
        }

        public double DistanceTo(Point3D target)
        {
            double resultX = Math.Pow(X + target.X, 2);
            double resultY = Math.Pow(Y + target.Y, 2);
            double resultZ = Math.Pow(Z + target.Z, 2);

            return Math.Sqrt(resultX + resultY + resultZ);

        }
    }
}
