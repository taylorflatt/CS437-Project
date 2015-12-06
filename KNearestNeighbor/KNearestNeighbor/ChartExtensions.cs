using System;
using System.Windows.Forms.DataVisualization.Charting;
using System.Collections.Generic;

namespace KNearestNeighbor
{
    public static class Plot
    {
        //data must be normalized prior to plotting (assumes normalized dataset).
        public static void PlotPoints(Chart chart, int xCoordChoice, int yCoordChoice, List<List<double>> normalizedTrainingSet, 
            List<double> normalizedInputSet, int inputClass, List<int> outputClass, List<string> outputClassName)
        {
            //I want to go trainingRow by trainingRow and plot the appropriate set of coordinates grabbing the correct outputClass to 
            //categorize it correctly (aka add it to the correct series).
            int point = 0;
            for (int row = 0; row < normalizedTrainingSet.Count; row++)
            {
                //Each element in outputClass corresponds the particular output for that training data row. That number also corresponds to 
                //the location of its name in the outputClassName list.
                int trainingSetClass = outputClass[row];
                string className = outputClassName[trainingSetClass];

                chart.Series[trainingSetClass].Points.AddXY(normalizedTrainingSet[row][xCoordChoice], normalizedTrainingSet[row][yCoordChoice]);

                try
                {
                    string xInputCoord = StringExtensions.Truncate(Convert.ToString(chart.Series[trainingSetClass].Points[point].XValue), 4);
                    string yInputCoord = StringExtensions.Truncate(Convert.ToString(chart.Series[trainingSetClass].Points[point].YValues[0]), 4);

                    chart.Series[trainingSetClass].Points[point].ToolTip = string.Format("Coordinate: ({0},{1}) " + "\n"
                    + "Class: {2}", xInputCoord, yInputCoord, className);
                }

                catch (ArgumentOutOfRangeException error)
                {
                    Console.WriteLine("This error SHOULD be ok. How the tooltips are added makes it so that it will access a non-existing"
                        + " element due to switching the series (aka the class) and restarting at position 0. To correc this, we simply"
                        + " reset the counter back to zero and require an increment at the end no matter what.");
                    Console.WriteLine("Packed Message: " + error.Message);
                    Console.WriteLine("Call Stack: " + error.StackTrace);

                    point = 0; //Set value back to zero.

                    //Now re-add the tooltip to the point that failed.
                    string xInputCoord = StringExtensions.Truncate(Convert.ToString(chart.Series[trainingSetClass].Points[point].XValue), 4);
                    string yInputCoord = StringExtensions.Truncate(Convert.ToString(chart.Series[trainingSetClass].Points[point].YValues[0]), 4);

                    chart.Series[trainingSetClass].Points[point].ToolTip = string.Format("Coordinate: ({0},{1}) " + "\n"
                    + "Class: {2}", xInputCoord, yInputCoord, className);
                }

                finally
                {
                    point++;
                }
            }

            //Now I need to plot my data point.
            chart.Series[outputClass.Count - 1].Points.AddXY(normalizedInputSet[xCoordChoice], normalizedInputSet[yCoordChoice]);
        }
    }
}
