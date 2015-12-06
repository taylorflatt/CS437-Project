using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace KNearestNeighbor
{
    public static class NormalizeData
    {
        //For input
        public static double Normalize(double[][] inputs, TextBox attribute, int attributeNum)
        {
            double max = 0;
            double min = 1;
            double currentValue = Convert.ToDouble(attribute.Text);
            double normalizedValue = 0;

            //find the max/min value for attribute 1
            for (int count = 0; count < inputs.Length; count++)
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
        public static double[][] Normalize(double[][] training)
        {
            List<double> max = new List<double> { 0, 0, 0, 0, 0 };
            List<double> min = new List<double> { 1, 1, 1, 1, 1 };

            //normalize my input data set (training set)
            for (int column = 0; column < training[0].Length; column++)
            {
                for (int row = 0; row < training.Length; row++)
                {
                    var temp5 = max[column];
                    var temp2 = training[row][column];

                    if (row == 0 && column == 0)
                        min[column] = training[row][column];

                    if (training[row][column] > max[column])
                        max[column] = training[row][column];

                    if (training[row][column] < min[column])
                        min[column] = training[row][column];
                }
            }

            //We want the same sized array as before.
            double[][] normalizedInputs = new double[training.Length][];

            List<double> temp = new List<double> { 0, 0, 0, 0, 0 };

            for (int row = 0; row < training.Length; row++)
            {
                for (int column = 0; column < training[0].Length; column++)
                {
                    temp[column] = (training[row][column] - min[column]) / (max[column] - min[column]);
                }

                normalizedInputs[row] = new double[] { temp[0], temp[1], temp[2], temp[3], temp[4] }; //Add our temporary array to the normalized inputs.
            }

            return normalizedInputs;
        }
    }
}
