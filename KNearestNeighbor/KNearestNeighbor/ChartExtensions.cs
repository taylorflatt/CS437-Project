using System;
using System.Windows.Forms.DataVisualization.Charting;

namespace KNearestNeighbor
{
    public static class Plot
    {
        public static void PlotPoints(Chart chart, int xCoordChoice, int yCoordChoice, double[][] trainingSet, double[][] normalizedTrainingSet, int[] outputs, double[] inputData)
        {
            int point = 0;
            for (int count = 0; count < trainingSet.Length; count++)
            {
                string className = outputs[count].ToString();

                chart.Series[className].Points.AddXY(normalizedTrainingSet[count][xCoordChoice], normalizedTrainingSet[count][yCoordChoice]);

                try
                {
                    string xInputCoord = StringExtensions.Truncate(Convert.ToString(chart.Series[className].Points[point].XValue), 4);
                    string yInputCoord = StringExtensions.Truncate(Convert.ToString(chart.Series[className].Points[point].YValues[0]), 4);

                    chart.Series[className].Points[point].ToolTip = string.Format("Coordinate: ({0},{1}) " + "\n"
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
                    string xInputCoord = StringExtensions.Truncate(Convert.ToString(chart.Series[className].Points[point].XValue), 4);
                    string yInputCoord = StringExtensions.Truncate(Convert.ToString(chart.Series[className].Points[point].YValues[0]), 4);

                    chart.Series[className].Points[point].ToolTip = string.Format("Coordinate: ({0},{1}) " + "\n"
                    + "Class: {2}", xInputCoord, yInputCoord, className);
                }

                finally
                {
                    point++;
                }
            }

            chart.Series[5].Points.AddXY(inputData[xCoordChoice], inputData[yCoordChoice]); //add our input point
        }
    }
}
