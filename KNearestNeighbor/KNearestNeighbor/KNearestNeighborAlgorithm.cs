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
        public KNearestNeighborAlgorithm(int k, List<List<double>> trainingData, List<int> outputs)
            : base(k, trainingData, outputs, EuclideanDistance.SquareEuclidean)
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
        private List<List<double>> trainingData;
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
        /// <param name="trainingData">The input data points.</param>
        /// <param name="outputs">The associated labels for the input points.</param>
        /// <param name="distance">The distance measure to use in the decision.</param>
        /// 
        public KNearestNeighbor(int k, List<List<double>> trainingData, List<int> outputs, Func<List<double>, List<double>, double> distance)
        {
            this.trainingData = trainingData;
            this.outputs = outputs;

            this.k = k;
            this.classCount = outputs.Distinct().Count();

            this.distance = distance;
            this.distances = new List<double>(trainingData.Count);

            //Add the requisite number of elements to the distance list.
            for (int index = 0; index < trainingData.Count; index++)
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
            get { return trainingData; }
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
                if (value <= 0 || value > trainingData.Count)
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
        public int Compute(List<double> normalizedInput, List<List<double>> normalizedTrainingSet)
        {
            for (int i = 0; i < normalizedTrainingSet.Count; i++)
            {
                double distance = EuclideanDistance.Euclidean(normalizedInput, normalizedTrainingSet[i]); //compute the distance
                distances[i] = distance; //store distance in this array.
            }

            int[] nearestIndices = Matrix.Indices(0, normalizedTrainingSet.Count);

            Array.Sort(distances.ToArray(), nearestIndices);

            double[] scores = new double[classCount];

            for (int i = 0; i < k; i++)
            {
                int j = nearestIndices[i];

                int label = outputs[j];
                double d = distances[j];

                scores[label] += 1.0 / d;
            }

            // Get the maximum weighted score
            int result;

            scores.Max(out result);

            return result;
        }

        //Returns the index of the nearest competitor.
        public int FindNearestCompetitor(List<double> normalizedInput, List<List<double>> normalizedTrainingSet, int attribute1, int attribute2)
        {
            List<double> tempInputList = new List<double>();
            

            tempInputList.Add(normalizedInput[attribute1]);
            tempInputList.Add(normalizedInput[attribute2]);

            for (int index = 0; index < trainingData.Count; index++)
            {
                List<double> tempTrainingList = new List<double>();
                tempTrainingList.Add(normalizedTrainingSet[index][attribute1]);
                tempTrainingList.Add(normalizedTrainingSet[index][attribute2]);

                double distance = EuclideanDistance.Euclidean(tempInputList, tempTrainingList);

                distances[index] = distance;

                tempTrainingList.Remove(0);
                tempTrainingList.Remove(1);
            }

            int[] nearestIndices = Matrix.Indices(0, normalizedTrainingSet.Count);

            Array.Sort(distances.ToArray(), nearestIndices);

            double[] scores = new double[classCount];

            for (int i = 0; i < k; i++)
            {
                int j = nearestIndices[i];

                int label = outputs[j];
                double d = distances[j];

                scores[label] += 1.0 / d;
            }

            // Get the maximum weighted score
            int result;

            scores.Max(out result);

            int closestCompetitorIndex = scores.IndexOf(scores.Max());

            return closestCompetitorIndex; 
        }
    }
}