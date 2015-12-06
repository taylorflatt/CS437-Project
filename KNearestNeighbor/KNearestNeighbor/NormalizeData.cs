using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace KNearestNeighbor
{
    public static class NormalizeData
    {
        //For input
        public static double Normalize(List<List<double>> inputs, TextBox attribute, int attributeNum)
        {
            double max = 0;
            double min = 1;
            double currentValue = Convert.ToDouble(attribute.Text);
            double normalizedValue = 0;

            //find the max/min value for attribute 1
            for (int count = 0; count < inputs.Count; count++)
            {
                if (inputs[count][attributeNum] > max)
                    max = inputs[count][attributeNum];

                if (inputs[count][attributeNum] < min)
                    min = inputs[count][attributeNum];
            }

            normalizedValue = (currentValue - min) / (max - min);

            return normalizedValue;
        }

        //For training data
        public static List<List<double>> Normalize(List<List<double>> training, int numAttributes)
        {
            double[] max = new double[numAttributes];
            double[] min = new double[numAttributes];
            //List<double> max = new List<double>();
            //List<double> min = new List<double>();

            //max.Capacity = numAttributes;
            //min.Capacity = numAttributes;

            //for (int index = 0; index < numAttributes; index++)
            //{
            //    max.Add(0);
            //    min.Add(1);
            //}

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
                }
            }

            //We want the same sized array as before.
            List<List<double>> normalizedTrainingSet = new List<List<double>>();

            List<double> tempRow = new List<double>();

            for (int index = 1; index < numAttributes; index++)
            {
                tempRow.Add(0);
                normalizedTrainingSet.Add(new List<double> { 0 });
            }

            for (int row = 0; row < training.Count; row++)
            {
                for (int column = 0; column < training[0].Count; column++)
                    tempRow[column] = (training[row][column] - min[column]) / (max[column] - min[column]);

                normalizedTrainingSet[row].Add(tempRow[row]);
            }

            return normalizedTrainingSet;
        }
    }
}
