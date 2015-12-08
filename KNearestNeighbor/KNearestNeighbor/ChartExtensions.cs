using System;
using System.Windows.Forms.DataVisualization.Charting;
using System.Collections.Generic;
using System.Drawing;

namespace KNearestNeighbor
{
    public static class Plot
    {
        static int ValueCompare(KeyValuePair<List<Object>, int> a, KeyValuePair<List<Object>, int> b)
        {
            return a.Value.CompareTo(b.Value);
        }

        //data must be normalized prior to plotting (assumes normalized dataset).
        public static void PlotPoints(Chart chart, int xCoordChoice, int yCoordChoice, List<List<double>> normalizedTrainingSet, 
            List<double> normalizedInputSet, List<int> outputClass, List<string> outputClassName, List<string> attributeNames, List<string> trainingSetLabels)
        {
            //We create a list which holds the coordinate pair in the first position and the class of that coordinate pair in the second position.
            var list = new List<KeyValuePair<List<Object>, int>>();

            //Add the points to the list with their labels.
            for (int index = 0; index < normalizedTrainingSet.Count; index++)
            {
                list.Add(new KeyValuePair<List<Object>, int>(new List<Object> { normalizedTrainingSet[index][xCoordChoice], normalizedTrainingSet[index][yCoordChoice], trainingSetLabels[index] }, outputClass[index]));
            }

            //Now we want to sort this list using the class. So it should sort in increasing order.
            list.Sort(ValueCompare);

            int pointNum = 0; //A particular point's location within the series.

            foreach (var pair in list)
            {
                //If we have moved to the next series, we need to reset our point counter.
                if (chart.Series[pair.Value].Points.Count == 0)
                    pointNum = 0;

                //Make an array out of the Key value so we can grab the individual coordinates.
                var pointSet = pair.Key.ToArray();

                double xCoord = (double) pointSet[0]; //Get the full x-coordinate.
                double yCoord = (double) pointSet[1]; //Get the full y-coordinate.
                string pointLabel = (string) pointSet[2]; //Get the point's label.

                string xCoordTruncated = StringExtensions.Truncate(Convert.ToString(pointSet[0]), 4); //Trim the number for a nicer display.
                string yCoordTruncated = StringExtensions.Truncate(Convert.ToString(pointSet[1]), 4); //Trim the number for a nicer display.

                string className = outputClassName[pair.Value]; //Get the class name of the particular point.

                //Label tests.
                chart.Series[pair.Value].SmartLabelStyle.MinMovingDistance = 5;
                chart.Series[pair.Value].SmartLabelStyle.MaxMovingDistance = 10;
                chart.Series[pair.Value].SmartLabelStyle.Enabled = true;

                //Label the axis with the appropriate attribute names.
                chart.ChartAreas["ChartArea1"].AxisX.Title = attributeNames[xCoordChoice];
                chart.ChartAreas["ChartArea1"].AxisY.Title = attributeNames[yCoordChoice];

                chart.Series[pair.Value].Points.AddXY(xCoord, yCoord); //Add the point to the graph.

                //Give the point an appropriate label when hovered.
                chart.Series[pair.Value].Points[pointNum].ToolTip = string.Format("Coordinate: ({0},{1}) " + "\n"
                + "Name: {2}" + "\n" + "Class: {3}", xCoordTruncated, yCoordTruncated, pointLabel, className);

                pointNum++;
            }

            chart.Series[outputClassName.Count].SmartLabelStyle.MinMovingDistance = 5;
            chart.Series[outputClassName.Count].SmartLabelStyle.MaxMovingDistance = 10;
            chart.Series[outputClassName.Count].SmartLabelStyle.Enabled = true;

            //Now I need to plot my data point.
            chart.Series[outputClassName.Count].Points.AddXY(normalizedInputSet[xCoordChoice], normalizedInputSet[yCoordChoice]);

            string xInputCoord = StringExtensions.Truncate(Convert.ToString(chart.Series[outputClassName.Count].Points[0].XValue), 4);
            string yInputCoord = StringExtensions.Truncate(Convert.ToString(chart.Series[outputClassName.Count].Points[0].YValues[0]), 4);


            chart.Series[outputClassName.Count].Points[0].ToolTip = string.Format("Coordinate: ({0},{1}) " + "\n"
                    + "My Data Point", xInputCoord, yInputCoord);
        }
    }
}