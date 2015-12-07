using System;
using System.Collections.Generic;

namespace KNearestNeighbor
{
    public static class NormalizeData
    {
        //For input data
        public static double Normalize(List<List<double>> training, string curVal, int colNum)
        {
            double max = 0; //temp
            double min = 1; //temp
            double currentValue = Convert.ToDouble(curVal);
            double normalizedValue = 0;

            //Don't account for the case in which the INPUT point is the smallest value.
            for (int row = 0; row < training.Count; row++)
            {
                if (row == 0)
                    min = training[row][colNum];

                if (training[row][colNum] > max)
                    max = training[row][colNum];

                if (training[row][colNum] < min)
                    min = training[row][colNum];

                if (currentValue < min)
                    min = currentValue;

                if (currentValue > max)
                    max = currentValue;
            }

            normalizedValue = (currentValue - min) / (max - min);

            return normalizedValue;
        }

        //For training data
        public static List<List<double>> Normalize(List<List<double>> training, List<double> inputSet, int numAttributes)
        {
            double[] max = new double[numAttributes];
            double[] min = new double[numAttributes];

            //normalize my input data set (training set)
            for (int column = 0; column < training[0].Count; column++)
            {
                for (int row = 0; row < training.Count; row++)
                {
                    if (row == 0)
                        min[column] = training[row][column];

                    if (training[row][column] > max[column])
                        max[column] = training[row][column];

                    if (training[row][column] < min[column])
                        min[column] = training[row][column];

                    if (inputSet[column] > max[column])
                        max[column] = inputSet[column];

                    if (inputSet[column] < min[column])
                        min[column] = inputSet[column];
                }
            }

            //We want the same sized array as before.
            List<List<double>> normalizedTrainingSet = new List<List<double>>();

            List<double> tempRow = new List<double>();

            for (int index = 0; index < numAttributes; index++)
                tempRow.Add(0);

            for(int count = 0; count < training.Count; count++)
                normalizedTrainingSet.Add(new List<double>(numAttributes));

            for (int row = 0; row < training.Count; row++)
            {
                for (int column = 0; column < training[0].Count; column++)
                    tempRow[column] = (training[row][column] - min[column]) / (max[column] - min[column]);

                //Add the normalized row data to the normalized training set.
                foreach(var element in tempRow)
                    normalizedTrainingSet[row].Add(element);
            }

            return normalizedTrainingSet;
        }
    }
}