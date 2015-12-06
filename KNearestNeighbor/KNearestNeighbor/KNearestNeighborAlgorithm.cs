using System;
using System.Linq;
using Accord.Math;
using System.Collections.Generic;

namespace KNearestNeighbor
{
    public class KNearestNeighborAlgorithm : KNearestNeighbor<double>
    {

        /// <summary>
        ///   Creates a new <see cref="KNearestNeighborAlgorithm"/>.
        /// </summary>
        /// 
        /// <param name="k">The number of nearest neighbors to be used in the decision.</param>
        /// 
        /// <param name="inputs">The input data points.</param>
        /// <param name="outputs">The associated labels for the input points.</param>
        /// 
        public KNearestNeighborAlgorithm(int k, List<List<double>> inputs, List<int> outputs)
            : base(k, inputs, outputs, EuclideanDistance.SquareEuclidean)
        { }
    }

    /// <summary>
    ///   K-Nearest Neighbor (k-NN) algorithm.
    /// </summary>
    /// 
    /// <typeparam name="T">The type of the input data.</typeparam>
    /// 
    public class KNearestNeighbor<T>
    {
        private int k;
        private List<List<double>> inputs;
        private List<int> outputs;
        private int classCount;
        private Func<List<double>, List<double>, double> distance;
        private List<double> distances;

        /// <summary>
        ///   Creates a new <see cref="KNearestNeighborAlgorithm"/>.
        /// </summary>
        /// 
        /// <param name="k">The number of nearest neighbors to be used in the decision.</param>
        /// 
        /// <param name="inputs">The input data points.</param>
        /// <param name="outputs">The associated labels for the input points.</param>
        /// <param name="distance">The distance measure to use in the decision.</param>
        /// 
        public KNearestNeighbor(int k, List<List<double>> inputs, List<int> outputs, Func<List<double>, List<double>, double> distance)
        {
            this.inputs = inputs;
            this.outputs = outputs;

            this.k = k;
            this.classCount = outputs.Distinct().Count();

            this.distance = distance;
            this.distances = new List<double>(inputs.Count);

            //Add the requisite number of elements to the distance list.
            for (int index = 0; index < inputs.Count; index++)
                this.distances.Add(0);
        }
        
        public KNearestNeighbor() { }

        public int setKValue(int newK)
        {
            k = newK;
            return k;
        }

        /// <summary>
        ///   Gets the set of points given
        ///   as input of the algorithm.
        /// </summary>
        /// 
        /// <value>The input points.</value>
        /// 
        public List<List<double>> Inputs
        {
            get { return inputs; }
        }

        /// <summary>
        ///   Gets the set of labels associated
        ///   with each <see cref="Inputs"/> point.
        /// </summary>
        /// 
        public List<int> Outputs
        {
            get { return outputs; }
        }

        /// <summary>
        ///   Gets or sets the number of nearest 
        ///   neighbors to be used in the decision.
        /// </summary>
        /// 
        /// <value>The number of neighbors.</value>
        /// 
        public int K
        {
            get { return k; }
            set
            {
                if (value <= 0 || value > inputs.Count)
                    throw new ArgumentOutOfRangeException("value",
                        "The value for k should be greater than zero and less than total number of input points.");

                k = value;
            }
        }

        /// <summary>
        ///   Computes the most likely label of a new given point.
        /// </summary>
        /// 
        /// <param name="input">A point to be classificated.</param>
        /// 
        /// <returns>The most likely label for the given point.</returns>
        /// 
        public int Compute(List<double> input)
        {
            var temp = inputs.Count; //10
            for (int i = 0; i < inputs.Count; i++)
            {
                double distance = EuclideanDistance.Euclidean(input, inputs[i]); //compute the distance
                distances[i] = distance; //store distance in this array.
            }
                //distances[i] = distance(input, inputs[i]);

            int[] nearestIndices = Matrix.Indices(0, inputs.Count);

            List<int> tempArray = new List<int>(nearestIndices);

            tempArray.Sort();

            double[] scores = new double[classCount];

            for (int i = 0; i < k; i++)
            {
                int j = nearestIndices[i];

                int label = outputs[j];
                double d = distances[j];

                scores[label] += 1.0 / d;
            }

            // Get the maximum weighted score
            int result; scores.Max(out result);

            return result;
        }
    }
}