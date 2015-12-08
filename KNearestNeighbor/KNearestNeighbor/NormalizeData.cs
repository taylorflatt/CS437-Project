using System;
using System.Collections.Generic;

namespace KNearestNeighbor
{
    /// <summary>
    /// Normalizes the data for the input set and training set.
    /// </summary>
    public static class NormalizeData
    {
        private static double max = -1;
        private static double min = -1;

        private static double[] jaggedMin;
        private static double[] jaggedMax;

        /// <summary>
        /// Normalizes the input set. Considers the input set + training set as a whole (positionally restricted).
        /// </summary>
        /// <param name="trainingSet">The non-normalized training set.</param>
        /// <param name="curVal">The current input value to be normalized.</param>
        /// <param name="colNum">The position of the curVal in the input row.</param>
        /// <returns>A normalized input value with respect to the entire training set and input set values for the particular column.</returns>
        public static double Normalize(List<List<double>> trainingSet, string curVal, int colNum)
        {
            double currentValue = Convert.ToDouble(curVal);
            double normalizedValue = 0;

            FindMinMax(trainingSet, currentValue, colNum);

            //Compute the normalized value of the 
            normalizedValue = (currentValue - min) / (max - min);

            //We need to reset the values for max and min so the next column isn't influenced by the previous column.
            min = -1;
            max = -1;

            return normalizedValue;
        }

        /// <summary>
        /// Normalizes the entire training set. Considers the input set + training set as a whole (posititonally restricted).
        /// </summary>
        /// <param name="trainingSet">The non-normalized training set.</param>
        /// <param name="inputSet">The non-normalized input set</param>
        /// <param name="numAttributes">The number of attributes per row.</param>
        /// <returns>A normalized list of training set data.</returns>
        public static List<List<double>> Normalize(List<List<double>> trainingSet, List<double> inputSet, int numAttributes)
        {
            //Find the min and max.
            FindMinMax(trainingSet, inputSet, numAttributes);

            //We want the same sized array as before.
            List<List<double>> normalizedTrainingSet = new List<List<double>>();

            List<double> tempRow = new List<double>();

            //Build the temp row array.
            for (int index = 0; index < numAttributes; index++)
                tempRow.Add(0);

            //Initialize the normalized training set to the proper size.
            for(int count = 0; count < trainingSet.Count; count++)
                normalizedTrainingSet.Add(new List<double>(numAttributes));

            //For each row's elements, this adds the normalized point to the position saving it in a tempRow array.
            for (int row = 0; row < trainingSet.Count; row++)
            {
                for (int column = 0; column < trainingSet[0].Count; column++)
                    tempRow[column] = (trainingSet[row][column] - jaggedMin[column]) / (jaggedMax[column] - jaggedMin[column]);

                //Add the normalized temp row data to the normalized training set.
                foreach(var element in tempRow)
                    normalizedTrainingSet[row].Add(element);
            }

            //Return the entire normalized training set.
            return normalizedTrainingSet;
        }

        /// <summary>
        /// Finds the min and max values for a given INPUT set.
        /// </summary>
        /// <param name="trainingSet">The non-normalized training set.</param>
        /// <param name="curVal">The current input value to be normalized.</param>
        /// <param name="colNum">The position of the curVal in the input row.</param>
        private static void FindMinMax(List<List<double>> trainingSet, double currentValue, int colNum)
        {
            //Find the maximum and minimum value relative to the training set and input set. All values must be included.
            for (int row = 0; row < trainingSet.Count; row++)
            {
                if (row == 0)
                    min = trainingSet[row][colNum];

                if (trainingSet[row][colNum] > max)
                    max = trainingSet[row][colNum];

                if (trainingSet[row][colNum] < min)
                    min = trainingSet[row][colNum];

                if (currentValue < min)
                    min = currentValue;

                if (currentValue > max)
                    max = currentValue;
            }
        }

        /// <summary>
        /// Finds the min and max for a given TRAINING set.
        /// </summary>
        /// <param name="trainingSet">The non-normalized training set.</param>
        /// <param name="inputSet">The non-normalized input set</param>
        /// <param name="numAttributes">The number of attributes per row.</param>
        private static void FindMinMax(List<List<double>> trainingSet, List<double> inputSet, int numAttributes)
        {
            jaggedMin = new double[numAttributes];
            jaggedMax = new double[numAttributes];

            for (int column = 0; column < trainingSet[0].Count; column++)
            {
                for (int row = 0; row < trainingSet.Count; row++)
                {
                    if (row == 0)
                        jaggedMin[column] = trainingSet[row][column];

                    if (trainingSet[row][column] > jaggedMax[column])
                        jaggedMax[column] = trainingSet[row][column];

                    if (trainingSet[row][column] < jaggedMin[column])
                        jaggedMin[column] = trainingSet[row][column];

                    if (inputSet[column] > jaggedMax[column])
                        jaggedMax[column] = inputSet[column];

                    if (inputSet[column] < jaggedMin[column])
                        jaggedMin[column] = inputSet[column];
                }
            }
        }
    }
}