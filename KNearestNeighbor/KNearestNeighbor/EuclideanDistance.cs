using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Accord.Math;

namespace KNearestNeighbor
{
    public static class EuclideanDistance
    {
        /// <summary>
        ///   Gets the Square Euclidean distance between two points.
        /// </summary>
        /// 
        /// <param name="x">A point in space.</param>
        /// <param name="y">A point in space.</param>
        /// 
        /// <returns>The Square Euclidean distance between x and y.</returns>
        /// 
        public static double SquareEuclidean(this List<double> x, List<double> y)
        {
            double d = 0.0, u;

            for (int i = 0; i < x.Count; i++)
            {
                u = x[i] - y[i];
                d += u * u;
            }

            return d;
        }

        /// <summary>
        ///   Gets the Euclidean distance between two points.
        /// </summary>
        /// 
        /// <param name="x">A point in space.</param>
        /// <param name="y">A point in space.</param>
        /// 
        /// <returns>The Euclidean distance between x and y.</returns>
        /// 
        public static double Euclidean(this List<double> x, List<double> y)
        {
            return System.Math.Sqrt(SquareEuclidean(x, y));
        }
    }
}