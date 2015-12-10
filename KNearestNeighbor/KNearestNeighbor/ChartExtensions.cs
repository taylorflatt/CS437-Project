using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms.DataVisualization.Charting;

namespace KNearestNeighbor
{
    public class Plot
    {
        protected Chart chart;
        protected int xCoordChoice;
        protected int yCoordChoice;
        protected List<List<double>> normalizedTrainingSet;
        protected List<double> normalizedInputSet;
        protected List<int> outputClass;
        protected List<string> outputClassName;
        protected List<string> attributeNames;
        protected List<string> trainingSetLabels;
        protected bool returnKDistances;
        protected List<double> listOfDistances;
        protected int numOfDistances;
        protected bool listAllDistances;
        private static List<int> pickedColors = new List<int>();

        protected static List<Color> acceptableColorList = new List<Color>
                {
                    Color.Maroon, //This will be our input data point color.
                    Color.Orange,
                    Color.Fuchsia,
                    Color.Lime,
                    Color.Aqua,
                    Color.LightBlue,
                    Color.DarkBlue,
                    Color.SlateGray,
                    Color.DarkGreen,
                    Color.LightCoral,
                    Color.Red,
                    Color.BlueViolet
                };

        /// <summary>
        /// This will plot the points for the graph giving them appropriate labels. We assume a normalized dataset.
        /// </summary>
        /// <param name="chart">The chart that the graph will be plotted.</param>
        /// <param name="xCoordChoice">The x-coordinate which corresponds to the chosen attribute.</param>
        /// <param name="yCoordChoice">The y-coordinate which corresponds to the chosen attribute.</param>
        /// <param name="normalizedTrainingSet">The normalized training set.</param>
        /// <param name="normalizedInputSet">The normalized input set.</param>
        /// <param name="outputClass">The list of output classes for the training set. The rows of this list must match the rows in
        /// the training set.</param>
        /// <param name="outputClassName">The list of unique class names.</param>
        /// <param name="attributeNames">The total list of attribute names.</param>
        /// <param name="trainingSetLabels">The name of the given point whose index corresponds to the index of the training set.</param>
        /// <param name="returnKDistances">Determines whether or not we display a distance in the label.</param>
        /// <param name="listOfDistances">The list of distances corresponding to the index of their point.</param>
        /// <param name="numOfDistances">The number of points (k-value) that we should add the distance label to.</param>
        /// <param name="listAllDistances">A flag that denotes whether we want to display all distances outright or not.</param>
        public Plot(Chart chart, int xCoordChoice, int yCoordChoice, List<List<double>> normalizedTrainingSet,
            List<double> normalizedInputSet, List<int> outputClass, List<string> outputClassName, List<string> attributeNames, List<string> trainingSetLabels, bool returnKDistances, List<double> listOfDistances, int numOfDistances, bool listAllDistances = false)
        {
            this.chart = chart;
            this.xCoordChoice = xCoordChoice;
            this.yCoordChoice = yCoordChoice;
            this.normalizedTrainingSet = normalizedTrainingSet;
            this.normalizedInputSet = normalizedInputSet;
            this.outputClass = outputClass;
            this.outputClassName = outputClassName;
            this.attributeNames = attributeNames;
            this.trainingSetLabels = trainingSetLabels;
            this.returnKDistances = returnKDistances;
            this.listOfDistances = listOfDistances;
            this.numOfDistances = numOfDistances;
            this.listAllDistances = listAllDistances;
        }

        /// <summary>
        /// Sets the default properties of the chart. Sets the smart label properties, the x,y coordinate labels, the legend text for each class
        /// the chart type for each series, the marker style (circle for training, square for input), marker size (6 for training, 7 for input),
        /// the color for each point.
        /// </summary>
        public void SetChartProperties()
        {
            //Make sure the legend doesn't already exist.
            if (chart.Legends.Count < 1)
                chart.Legends.Add("Legend");

            //Remove all generated information from the chart if any exist.
            chart.Series.Clear();

            //Generate the general characteristics for the input point.
            //Label the axis with the appropriate attribute names.
            chart.ChartAreas["ChartArea1"].AxisX.Title = attributeNames[xCoordChoice];
            chart.ChartAreas["ChartArea1"].AxisY.Title = attributeNames[yCoordChoice];

            //Display the labels on the Y axis to be displayed at an angle to appear nicer.
            chart.ChartAreas["ChartArea1"].AxisX.LabelStyle.Angle = 45;

            var temp = outputClassName.Count;
            Random randomGen = new Random();

            for (int index = 0; index < outputClassName.Count; index++)
            {
                //Generate the Point General Characteristics
                chart.Series.Add("Class " + index);
                chart.Series[index].LegendText = outputClassName[index];
                chart.Series[index].ChartType = SeriesChartType.Point; //Point graph.
                chart.Series[index].MarkerStyle = MarkerStyle.Circle;
                chart.Series[index].MarkerSize = 6; //Might look into parameterizing this to give the user the option of increasing the size.

                //Set the colors only once.
                if (pickedColors.Count == 0)
                {
                    //Generate the Point color.
                    bool uniqueColor = false;
                    while (uniqueColor == false)
                    {
                        int colorPositionInList = randomGen.Next(1, acceptableColorList.Count - 1);

                        //If the color is unique, we will add it. Otherwise, we will pick a random color.
                        if (!pickedColors.Contains(colorPositionInList))
                        {
                            //Set the series color.
                            chart.Series[index].MarkerColor = acceptableColorList.ElementAt(colorPositionInList);

                            //Add the element location to our list so we don't pick the same color again.
                            pickedColors.Add(colorPositionInList);

                            //Drop from the while loop.
                            uniqueColor = true;
                        }

                        //If there are more than 11 attributes, then we will generate a random color.
                        if (pickedColors.Count >= (acceptableColorList.Count - 1))
                        {
                            chart.Series[index].MarkerColor = Color.FromArgb((byte)randomGen.Next(255), (byte)randomGen.Next(255), (byte)randomGen.Next(255));
                        }
                    }
                }

                //If the colors have already been set, just re-assign them to the same series.
                else
                {
                    //The list will already have the colors in order of series.
                    chart.Series[index].MarkerColor = acceptableColorList.ElementAt(index);
                }

                //Generate the Point Smart Label Characteristics.
                chart.Series[index].SmartLabelStyle.MinMovingDistance = 20;
                chart.Series[index].SmartLabelStyle.MaxMovingDistance = 40;
                chart.Series[index].SmartLabelStyle.Enabled = true;
            }

            //Properties for the input point.
            chart.Series.Add("My Input");
            chart.Series[outputClassName.Count].ChartType = SeriesChartType.Point; //Point graph.
            chart.Series[outputClassName.Count].MarkerColor = acceptableColorList.ElementAt(0); //Set the color to be the first in the list.
            chart.Series[outputClassName.Count].MarkerStyle = MarkerStyle.Square;
            chart.Series[outputClassName.Count].MarkerSize = 7; //Might look into parameterizing this to give the user the option of increasing the size.

            chart.Series[outputClassName.Count].SmartLabelStyle.MinMovingDistance = 5;
            chart.Series[outputClassName.Count].SmartLabelStyle.MaxMovingDistance = 10;
            chart.Series[outputClassName.Count].SmartLabelStyle.Enabled = true;
        }

        /// <summary>
        /// This will plot the points for the graph giving them appropriate labels. We assume a normalized dataset.
        /// </summary>
        public void PlotPoints()
        {
            //We create a key-value list which holds the ({x-coordinate, y-coordinate, point label, point distance from input}, (training class)).
            var points = new List<KeyValuePair<List<Object>, int>>();

            //Add the points to the list with the auxiliary information.
            for (int index = 0; index < normalizedTrainingSet.Count; index++)
            {
                points.Add(new KeyValuePair<List<Object>, int>(
                    new List<Object>
                    {
                        normalizedTrainingSet[index][xCoordChoice],
                        normalizedTrainingSet[index][yCoordChoice],
                        trainingSetLabels[index],
                        listOfDistances[index]
                    },

                    outputClass[index]));
            }

            //Now we want to sort this list using the class. So it should sort in increasing order.
            points.Sort(SortByClass);

            int pointIndexInSeries = 0; //A particular point's location within the series.
            int currentPosition = 0; //Our position in the distancesSortedList (show distance for k-value).

            /// Now we iterate through our list of points and add the points with the appropriate labels.
            foreach (var pair in points)
            {
                //If we have moved to the next series, we need to reset our point counter.
                if (chart.Series[pair.Value].Points.Count == 0)
                    pointIndexInSeries = 0;

                //Make an array out of the Key value so we can grab the individual coordinates.
                var pointSet = pair.Key.ToArray();

                double xCoord = (double)pointSet[0]; //Get the full x-coordinate.
                double yCoord = (double)pointSet[1]; //Get the full y-coordinate.
                string pointLabel = (string)pointSet[2]; //Get the point's label.
                string pointDistance = StringExtensions.Truncate(Convert.ToString(pointSet[3]), 5); //Get the point's distance from the input.
                string xCoordTruncated = StringExtensions.Truncate(Convert.ToString(pointSet[0]), 4); //Trim the number for a nicer display.
                string yCoordTruncated = StringExtensions.Truncate(Convert.ToString(pointSet[1]), 4); //Trim the number for a nicer display.

                string className = outputClassName[pair.Value]; //Get the class name of the particular point.

                int pairValue = pair.Value;

                chart.Series[pair.Value].Points.AddXY(xCoord, yCoord); //Add the point to the graph.

                //List all of the distances.
                if (listAllDistances == true)
                    DisplayLabelWithDistance(pairValue, pointIndexInSeries, xCoordTruncated, yCoordTruncated, pointLabel, className, pointDistance);

                //List only k-nearest distances.
                else if (returnKDistances == true && listAllDistances == false)
                    LabelKDistances(points, currentPosition, pointIndexInSeries, pairValue, pointIndexInSeries, xCoordTruncated, yCoordTruncated, pointLabel, className, pointDistance);

                //If neither option is chosen, display all tooltips without the distance.
                else
                    DisplayLabelWithoutDistance(pairValue, pointIndexInSeries, xCoordTruncated, yCoordTruncated, pointLabel, className);

                pointIndexInSeries++; //Move to the next point.
            }

            //Now I need to plot my data point.
            chart.Series[outputClassName.Count].Points.AddXY(normalizedInputSet[xCoordChoice], normalizedInputSet[yCoordChoice]);

            string xInputCoord = StringExtensions.Truncate(Convert.ToString(chart.Series[outputClassName.Count].Points[0].XValue), 4);
            string yInputCoord = StringExtensions.Truncate(Convert.ToString(chart.Series[outputClassName.Count].Points[0].YValues[0]), 4);

            //Label the input.
            chart.Series[outputClassName.Count].Points[0].ToolTip = string.Format("Coordinate: ({0},{1}) " + "\n"
                    + "My Data Point", xInputCoord, yInputCoord);
        }

        /// <summary>
        /// Labels the k nearest points from the input with their distance from the input.
        /// </summary>
        /// <param name="points">The list of points to plot.</param>
        /// <param name="currentPosition">The number of k values that have been labeled.</param>
        /// <param name="pointIndexInSeries">The current point (index) in a particular series.</param>
        /// <param name="pairValue">The class number.</param>
        /// <param name="currentPointIndex">The current data point.</param>
        /// <param name="xCoordTruncated">The reduced x-Coordinate.</param>
        /// <param name="yCoordTruncated">The reduced y-Coordinate.</param>
        /// <param name="pointLabel">The data point's name.</param>
        /// <param name="className">The class name of the data point.</param>
        /// <param name="pointDistance">The distance from this point to the input.</param>
        private void LabelKDistances(List<KeyValuePair<List<Object>, int>> points, int currentPosition, int pointIndexInSeries, int pairValue, int currentPointIndex, string xCoordTruncated, string yCoordTruncated, string pointLabel, string className, string pointDistance)
        {
            //Create a temporary list that is our sorted list.
            var tempDistanceSortedList = new List<KeyValuePair<List<Object>, int>>(points);

            //Now we need to take our temporary list and sort it by DISTANCE instead of class.
            tempDistanceSortedList.Sort(
                delegate (KeyValuePair<List<Object>, int> firstPair,
                          KeyValuePair<List<Object>, int> nextPair)
                {
                    var firstPairSet = firstPair.Key.ToArray();
                    var nextPairSet = nextPair.Key.ToArray();

                    double firstPairDistance = (double)firstPairSet[3]; //Distance is the third element in the set.
                    double nextPairDistance = (double)nextPairSet[3]; //Distance is the third element in the set.

                    return firstPairDistance.CompareTo(nextPairDistance); //Sort by shortest distance.
                }
            );

            //Now we need to check the value in our DISTANCE sorted list and see if that equals the element in our CLASS sorted list.
            //If it does, then we put its distance. We only need to do this so long as we are within the k-value (some or all).
            if (currentPosition <= numOfDistances - 1)
            {
                /// Essentially we iterate through out sorted list of distances up to the k-value looking to see if our current element
                /// is in that list. If it is, we set the exists flag to true which will put the distance into the label. If it isn't in
                /// the array in that range, we don't print the label.
                bool exists = false;
                for (int index = 0; index < numOfDistances; index++)
                {
                    var tempPair = tempDistanceSortedList[index];
                    var tempPointSet = tempPair.Key.ToArray();

                    string tempPointLabel = (string)tempPointSet[2];

                    if (tempPointLabel.Equals(pointLabel))
                        exists = true;
                }

                //They are the SAME element so we increment the current position we look at in our tempDistanceSortedList.
                if (exists == true)
                {
                    currentPosition++; //Go to the next closest point to our input.
                    DisplayLabelWithDistance(pairValue, pointIndexInSeries, xCoordTruncated, yCoordTruncated, pointLabel, className, pointDistance);
                }

                //The current point is NOT within the k closest elements, so print without the distance.
                else
                    DisplayLabelWithoutDistance(pairValue, pointIndexInSeries, xCoordTruncated, yCoordTruncated, pointLabel, className);
            }

            //There are no more unique points left in the k-closest points. In other words, we have already added all of the
            //k closest points.
            else
                DisplayLabelWithoutDistance(pairValue, pointIndexInSeries, xCoordTruncated, yCoordTruncated, pointLabel, className);
        }

        /// <summary>
        /// Will compare two key value pairs and return their order.
        /// </summary>
        /// <param name="a">First key value pair.</param>
        /// <param name="b">Next key value pair.</param>
        /// <returns></returns>
        private static int SortByClass(KeyValuePair<List<Object>, int> a, KeyValuePair<List<Object>, int> b)
        {
            return a.Value.CompareTo(b.Value);
        }

        #region Data Labeling

        /// <summary>
        /// Prints the label with a distance measurement included.
        /// </summary>
        /// <param name="pairValue">The class number.</param>
        /// <param name="currentPointIndex">The current data point.</param>
        /// <param name="xCoordTruncated">The reduced x-Coordinate.</param>
        /// <param name="yCoordTruncated">The reduced y-Coordinate.</param>
        /// <param name="pointLabel">The data point's name.</param>
        /// <param name="className">The class name of the data point.</param>
        /// <param name="pointDistance">The distance from this point to the input.</param>
        private void DisplayLabelWithDistance(int pairValue, int currentIndexInSeries, string xCoordTruncated, string yCoordTruncated, string pointLabel, string className, string pointDistance)
        {
            chart.Series[pairValue].Points[currentIndexInSeries].ToolTip = string.Format("Coordinate: ({0},{1}) " + "\n"
+ "Name: {2}" + "\n" + "Class: {3}" + "\n" + "Distance: {4}", xCoordTruncated, yCoordTruncated, pointLabel, className, pointDistance);
        }

        /// <summary>
        /// Prints the label without a distance measurement included.
        /// </summary>
        /// <param name="pairValue">The class number.</param>
        /// <param name="currentPointIndex">The current data point.</param>
        /// <param name="xCoordTruncated">The reduced x-Coordinate.</param>
        /// <param name="yCoordTruncated">The reduced y-Coordinate.</param>
        /// <param name="pointLabel">The data point's name.</param>
        /// <param name="className">The class name of the data point.</param>
        private void DisplayLabelWithoutDistance(int pairValue, int currentIndexInSeries, string xCoordTruncated, string yCoordTruncated, string pointLabel, string className)
        {
            chart.Series[pairValue].Points[currentIndexInSeries].ToolTip = string.Format("Coordinate: ({0},{1}) " + "\n"
+ "Name: {2}" + "\n" + "Class: {3}", xCoordTruncated, yCoordTruncated, pointLabel, className);
        }

        #endregion Data Labeling
    }
}