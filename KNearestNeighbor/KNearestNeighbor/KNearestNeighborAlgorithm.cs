using System;
using System.Collections.Generic;
using System.Linq;

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
            : base(k, trainingData, outputs, MathFunctions.SquareEuclidean)
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

        //This array will have the correct distance from the input to the training data points.
        private List<double> distances;

        //This array will only have the distances between two attribute points not all of them.
        private List<double> closestCompetitorDistances;

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
            this.closestCompetitorDistances = new List<double>(trainingData.Count);

            //Add the requisite number of elements to the distance list.
            for (int index = 0; index < trainingData.Count; index++)
            {
                this.distances.Add(0);
                this.closestCompetitorDistances.Add(0);
            }
        }

        /// <summary>
        /// Empty constructor.
        /// </summary>
        public KNearestNeighbor() { }

        /// <summary>
        /// Sets the value of k given a new value.
        /// </summary>
        /// <param name="newK">The new k value.</param>
        /// <returns></returns>
        public int setKValue(int newK)
        {
            k = newK;
            return k;
        }

        /// <summary>
        /// Returns the list of distances.
        /// </summary>
        /// <returns></returns>
        public List<double> GetDistances()
        {
            return this.distances;
        }

        /// <summary>
        /// Returns the list of closest competitor distances (within a k range).
        /// </summary>
        /// <returns></returns>
        public List<double> GetClosestCompetitorDistances()
        {
            return this.closestCompetitorDistances;
        }

        /// <summary>
        /// Gets the k-closest distances.
        /// </summary>
        /// <param name="k">Chosen k value.</param>
        /// <param name="weightedVote">True if you want the distance returned to be weighted. False otherwise.</param>
        /// <returns></returns>
        public List<double> GetKDistances(int k, bool weightedVote = false)
        {
            List<double> kClosestDistances = new List<double>();
            List<double> sortedDistancesList = new List<double>(this.distances);

            try
            {
                sortedDistancesList.Sort();

                if (weightedVote == true)
                    for (int index = 0; index < k; index++)
                        kClosestDistances.Add(1.0 / sortedDistancesList[index]);

                else
                    for (int index = 0; index < k; index++)
                        kClosestDistances.Add(this.distances[index]);
            }

            catch (IndexOutOfRangeException error)
            {
                Console.WriteLine("The k value cannot exceed the size of the training set. ");
                Console.WriteLine("Packed Message: " + error.Message);
                Console.WriteLine("Call Stack: " + error.StackTrace);
            }
            return kClosestDistances;
        }

        /// <summary>
        /// Gets the index of the training values that actually voted for the class of the input.
        /// </summary>
        /// <returns>The index of the element (relative to the training set index) of the training element that voted for the input class.</returns>
        public List<double> IndexOfKNearestDistances()
        {
            if (distances == null)
                throw new ArgumentNullException("self");

            if (distances.Count == 0)
                throw new ArgumentException("List is empty.", "self");

            List<double> tempList = new List<double>(this.distances);
            List<double> kMinDistances = new List<double>();

            double min = tempList[0];
            int minIndex = 0;

            //Allows us to make sure that an element already chosen isn't picked again. It is skipped over.
            List<double> storedMin = new List<double>(k);

            for(int count = 0; count < k; count++)
            {
                for (int index = 0; index < tempList.Count; index++)
                {
                    if(count > 0)
                    {
                        if (tempList[index] < min && storedMin.Contains(tempList[index]) == false)
                        {
                            min = tempList[index];
                            minIndex = index;
                        }
                    }

                    else if (tempList[index] < min)
                    {
                        min = tempList[index];
                        minIndex = index; 
                    }
                }

                storedMin.Add(min);

                kMinDistances.Add(minIndex); //Due to the output file having an additional 4 rows at the beginning.
                //tempList.RemoveAt(minIndex);

                min = tempList[0];
                minIndex = 0;
            }

            return kMinDistances;
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
                double distance = MathFunctions.Euclidean(normalizedInput, normalizedTrainingSet[i]); //compute the distance
                distances[i] = distance; //store distance in this array.
            }

            int[] nearestIndices = MathFunctions.Indices(0, normalizedTrainingSet.Count);

            List<double> sortedDistances = new List<double>(this.distances);

            Array.Sort(sortedDistances.ToArray(), nearestIndices);

            double[] scores = new double[classCount];

            for (int i = 0; i < k; i++)
            {
                int j = nearestIndices[i];

                int label = outputs[j];
                double d = sortedDistances[j];

                scores[label] += 1.0 / (d * d); //weights the score. 1/d^2
            }

            // Get the maximum weighted score
            int result;

            scores.Max(out result);

            return result;
        }

        /// <summary>
        /// The output is a List of List objects whose first row is always the closest point to the input data. The rest of the rows
        /// (if any)  are the next k closest points to the input point.
        ///
        /// Returns a List of List objects that whose first element will always be the index of the closest competitor (trainingData), the index
        /// of the class (outputClass), and the distanace it was from our point. The boolean flag returnKDistances will also add further elements
        /// to the list. It will add the k-closest competitors in terms of the index of the closest competitor (trainingData), the index of the
        /// class (outputClass), and the distance it was from the input point.
        /// </summary>
        /// <param name="normalizedInput">The normalized list of inputs.</param>
        /// <param name="normalizedTrainingSet">The normalized list of training poitns.</param>
        /// <param name="attribute1">The first attribute to consider.</param>
        /// <param name="attribute2">The second attribute to consider.</param>
        /// <param name="returnKDistances">A boolean to determine if you want to display the closest competitor (k = 1, given attribute 1
        /// and attribute 2) or the k-closest competitors (k = dynamic, given attribute 1 and attribute 2) </param>
        /// <param name="numDistancesToShow">The number of k closest points to show.</param>
        /// <returns></returns>
        public List<List<Object>> FindNearestCompetitor(List<double> normalizedInput, List<List<double>> normalizedTrainingSet, int attribute1, int attribute2, bool returnKDistances, int numDistancesToShow)
        {
            List<double> tempInputList = new List<double>(); //Temporary list to hold our normalized inputs.

            tempInputList.Add(normalizedInput[attribute1]); //Add normalized input value for the first given attribute.
            tempInputList.Add(normalizedInput[attribute2]); //Add normalized input value for the second given attribute.

            //Iterate through the normalized training data and compute the distance from the data points and out given input points.
            for (int index = 0; index < normalizedTrainingSet.Count; index++)
            {
                List<double> tempTrainingList = new List<double>();
                tempTrainingList.Add(normalizedTrainingSet[index][attribute1]);
                tempTrainingList.Add(normalizedTrainingSet[index][attribute2]);

                double distance = MathFunctions.Euclidean(tempInputList, tempTrainingList); //Compute the distance between the two points.

                closestCompetitorDistances[index] = distance; //Add the computed distance to the distances array.

                tempTrainingList.Remove(0); //Remove the first normalized training data point since we are finished with it.
                tempTrainingList.Remove(1); //Remove the second normalized training data point since we are finished with it.
            }

            //Corresponds to the ROW location in the training set.
            int[] nearestIndices = MathFunctions.Indices(0, normalizedTrainingSet.Count); //Instantiate the nearestIndices list to the right size.

            //Sort the distances stored in the distances array and rearrange the elements in the nearestIndices array to match.
            Array.Sort(closestCompetitorDistances.ToArray(), nearestIndices);

            List<List<Object>> value = new List<List<Object>>();

            int closestCompetitor = nearestIndices[0]; //grabs the first element of the array (corresponds to a row in the training set).
            int closestCompetitorClass = outputs[closestCompetitor]; //grabs the class that the element j belongs to.
            double distanceFromInput = closestCompetitorDistances[closestCompetitor]; //grabs the distance computed (distance the training point is from the input data point)

            value.Add
                    (
                        new List<Object> { closestCompetitor, closestCompetitorClass, distanceFromInput }
                    );

            //If we want to add more than the closest competitor:
            if (returnKDistances == true)
            {
                //Add the closest competitor distances.
                for (int index = 0; index < numDistancesToShow; index++)
                {
                    //The indexing for this will START at 1 (so element 1 will be the distance for element 0 in the normalizedTrainingSet).
                    //But we eventually remove the first element in our main class restoring the correct indexing.
                    value.Add
                        (
                            new List<Object> { closestCompetitorDistances[index] }
                        );
                }
            }

            return value;
        }
    }
}