using System;
using System.Windows.Forms.DataVisualization.Charting;
using System.Collections.Generic;

namespace KNearestNeighbor
{
    public static class Plot
    {
        /// <summary>
        /// Will compare two key value pairs and return their order.
        /// </summary>
        /// <param name="a">First key value pair.</param>
        /// <param name="b">Next key value pair.</param>
        /// <returns></returns>
        static int ValueCompareClass(KeyValuePair<List<Object>, int> a, KeyValuePair<List<Object>, int> b)
        {
            return a.Value.CompareTo(b.Value);
        }

        //data must be normalized prior to plotting (assumes normalized dataset).
        /// <summary>
        /// This will plot the points for the graph giving them appropriate labels.
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
        public static void PlotPoints(Chart chart, int xCoordChoice, int yCoordChoice, List<List<double>> normalizedTrainingSet, 
            List<double> normalizedInputSet, List<int> outputClass, List<string> outputClassName, List<string> attributeNames, List<string> trainingSetLabels, bool returnKDistances, List<double> listOfDistances, int numOfDistances, bool listAllDistances = false)
        {
            //We create a key-value list which holds the ({x-coordinate, y-coordinate, point label, point distance from input}, (training class)).
            var list = new List<KeyValuePair<List<Object>, int>>();

            //Add the points to the list with the auxiliary information.
            for (int index = 0; index < normalizedTrainingSet.Count; index++)
            {
                list.Add(new KeyValuePair<List<Object>, int>(
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
            list.Sort(ValueCompareClass);

            int pointNum = 0; //A particular point's location within the series.
            int currentPosition = 0; //Our position in the distancesSortedList (show distance for k-value).

            /// Now we iterate through our list of points and add the points with the appropriate labels.
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
                string pointDistance = StringExtensions.Truncate(Convert.ToString(pointSet[3]), 5); //Get the point's distance from the input.
                string xCoordTruncated = StringExtensions.Truncate(Convert.ToString(pointSet[0]), 4); //Trim the number for a nicer display.
                string yCoordTruncated = StringExtensions.Truncate(Convert.ToString(pointSet[1]), 4); //Trim the number for a nicer display.

                string className = outputClassName[pair.Value]; //Get the class name of the particular point.

                //Smart Label.
                chart.Series[pair.Value].SmartLabelStyle.MinMovingDistance = 20;
                chart.Series[pair.Value].SmartLabelStyle.MaxMovingDistance = 40;
                chart.Series[pair.Value].SmartLabelStyle.Enabled = true;

                //Label the axis with the appropriate attribute names.
                chart.ChartAreas["ChartArea1"].AxisX.Title = attributeNames[xCoordChoice];
                chart.ChartAreas["ChartArea1"].AxisY.Title = attributeNames[yCoordChoice];

                //Display the labels on the Y axis to be displayed at an angle to appear nicer.
                chart.ChartAreas["ChartArea1"].AxisX.LabelStyle.Angle = 45;


                chart.Series[pair.Value].Points.AddXY(xCoord, yCoord); //Add the point to the graph.

                /// After adding the points, we now consider adding the labels. There are two cases we must consider:
                /// 
                /// Case 1: They wish to return the distances.
                ///     Case 1a: They want to return ALL of the distances.
                ///     Case 1b: They want to return only K distances.
                /// Case 2: They don't wish to return the distances.
                /// 
                /// Case 2 is trivial. We simply print the label without the distance.
                /// 
                /// Case 1a is fairly trivial. We simply print the label with the distance.
                /// 
                /// Case 1b is more difficult. The option is to plot the points closest to the input (k number of points). In other words, 
                /// we are plotting the closest k points. So we need to copy the list we created earlier into a temporary list that we must 
                /// sort by the distance instead of class. Once sorted, we simply consider the first k items of that list. Then we do a 
                /// comparison to check if the label of any of the points in the tempList match with our current point in the list. If they 
                /// do match, that means it is one of the k closest points to our input. So we should add the distance label to it. If the 
                /// points don't match then that means that our current point is not one of the k closest points so we print the label without 
                /// the distance label attached.
                /// 
                if(returnKDistances == true && listAllDistances == true)
                {
                    //Print label WITH distance.
                    chart.Series[pair.Value].Points[pointNum].ToolTip = string.Format("Coordinate: ({0},{1}) " + "\n"
                    + "Name: {2}" + "\n" + "Class: {3}" + "\n" + "Distance: {4}", xCoordTruncated, yCoordTruncated, pointLabel, className, pointDistance);
                }

                else if(returnKDistances == true && listAllDistances == false)
                {
                    //Create a temporary list that is our sorted list.
                    var tempDistanceSortedList = new List<KeyValuePair<List<Object>, int>>(list);

                    //Now we need to take our temporary list and sort it by DISTANCE instead of class.
                    tempDistanceSortedList.Sort(
                        delegate (KeyValuePair<List<Object>, int> firstPair,
                                  KeyValuePair<List<Object>, int> nextPair)
                        {
                            var firstPairSet = firstPair.Key.ToArray();
                            var nextPairSet = nextPair.Key.ToArray();

                            double firstPairDistance = (double) firstPairSet[3]; //Distance is the third element in the set.
                            double nextPairDistance = (double) nextPairSet[3]; //Distance is the third element in the set.

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
                        for(int index = 0; index < numOfDistances; index++)
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

                            //Print label WITH distance.
                            chart.Series[pair.Value].Points[pointNum].ToolTip = string.Format("Coordinate: ({0},{1}) " + "\n"
                            + "Name: {2}" + "\n" + "Class: {3}" + "\n" + "Distance: {4}", xCoordTruncated, yCoordTruncated, pointLabel, className, pointDistance);
                        }

                        //The current point is NOT within the k closest elements, so print without the distance.
                        else
                        {
                            //Print label WITHOUT distance.
                            chart.Series[pair.Value].Points[pointNum].ToolTip = string.Format("Coordinate: ({0},{1}) " + "\n"
                            + "Name: {2}" + "\n" + "Class: {3}", xCoordTruncated, yCoordTruncated, pointLabel, className);
                        }
                    }

                    //There are no more unique points left in the k-closest points. In other words, we have already added all of the 
                    //k closest points. 
                    else
                    {
                        //So print label WITHOUT distance.
                        chart.Series[pair.Value].Points[pointNum].ToolTip = string.Format("Coordinate: ({0},{1}) " + "\n"
                        + "Name: {2}" + "\n" + "Class: {3}", xCoordTruncated, yCoordTruncated, pointLabel, className);
                    }
                }

                pointNum++; //Move to the next point.
            }

            //Smart Label for the input series.
            chart.Series[outputClassName.Count].SmartLabelStyle.MinMovingDistance = 5;
            chart.Series[outputClassName.Count].SmartLabelStyle.MaxMovingDistance = 10;
            chart.Series[outputClassName.Count].SmartLabelStyle.Enabled = true;

            //Now I need to plot my data point.
            chart.Series[outputClassName.Count].Points.AddXY(normalizedInputSet[xCoordChoice], normalizedInputSet[yCoordChoice]);

            string xInputCoord = StringExtensions.Truncate(Convert.ToString(chart.Series[outputClassName.Count].Points[0].XValue), 4);
            string yInputCoord = StringExtensions.Truncate(Convert.ToString(chart.Series[outputClassName.Count].Points[0].YValues[0]), 4);

            //Label the input.
            chart.Series[outputClassName.Count].Points[0].ToolTip = string.Format("Coordinate: ({0},{1}) " + "\n"
                    + "My Data Point", xInputCoord, yInputCoord);
        }
    }
}