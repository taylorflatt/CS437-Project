using System;
using System.Collections.Generic;

namespace KNearestNeighbor
{
    public static class MathFunctions
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

        /// <summary>
        ///   Returns a matrix with all elements set to a given value.
        /// </summary>
        public static T[,] Create<T>(int rows, int cols, T value)
        {
            if (rows < 0) throw new ArgumentOutOfRangeException("rows", rows, "Number of rows must be a positive integer.");
            if (cols < 0) throw new ArgumentOutOfRangeException("cols", cols, "Number of columns must be a positive integer.");

            T[,] matrix = new T[rows, cols];

            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    matrix[i, j] = value;

            return matrix;
        }

        /// <summary>
        ///   Returns a matrix with all elements set to a given value.
        /// </summary>
        public static T[,] Create<T>(int size, T value)
        {
            if (size < 0)
                throw new ArgumentOutOfRangeException("size", size,
      "Square matrix's size must be a positive integer.");

            return Create(size, size, value);
        }

        public static int[] Indices(int from, int to)
        {
            int[] vector = new int[to - from];
            for (int i = 0; i < vector.Length; i++)
                vector[i] = from++;
            return vector;
        }

        /// <summary>
        ///   Gets the maximum element in a vector.
        /// </summary>
        public static T Max<T>(this T[] values, out int imax) where T : IComparable
        {
            imax = 0;
            T max = values[0];
            for (int i = 1; i < values.Length; i++)
            {
                if (values[i].CompareTo(max) > 0)
                {
                    max = values[i];
                    imax = i;
                }
            }
            return max;
        }

        /// <summary>
        ///   Gets the minimum element in a vector.
        /// </summary>
        public static T Min<T>(this T[] values, out int imax) where T : IComparable
        {
            imax = 0;
            T max = values[0];
            for (int i = 1; i < values.Length; i++)
            {
                if (values[i].CompareTo(max) < 0)
                {
                    max = values[i];
                    imax = i;
                }
            }
            return max;
        }
    }
}