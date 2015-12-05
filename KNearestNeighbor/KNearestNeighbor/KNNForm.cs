using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;
using System.Windows.Forms.DataVisualization.Charting;

namespace KNearestNeighbor
{

    public partial class KNNForm : Form
    {
        public int k; //k-value to be set.
    //public double[][] inputs = 
    //    {
    //        new double[] { .5, .2, .1, .2, .9},
    //        new double[] { .5, .5, .2, .5, .2},
    //        new double[] {  .2, .1, .7, .8, .2},
    //        new double[] {  .1, .1, .8, .1, .2},
    //        new double[] {  .1, .2, .2, .4, .1},
    //        new double[] {  .3, .1, .2, 0, .1},
    //        new double[] { .11, .5, .1, .2, .1},
    //        new double[] { .15, .5, .2, .5, .9},
    //        new double[] { .10, .5, .2, .1, 0},
    //        new double[] { .10, .5, .2, .1, .1},
    //    };

        //Will need to read in all these values via excel sheet so: http://csharp.net-informations.com/excel/csharp-read-excel.htm
        //Training set attribute values.
        public double[][] inputs =
            {
                new double[] { 5, 2, 1, 2, 9},
                new double[] { 5, 5, 2, 5, 2},
                new double[] {  2, 1, 7, 8, 2},
                new double[] {  1, 1, 8, 1, 2},
                new double[] {  1, 2, 2, 4, 1},
                new double[] {  3, 1, 2, 0, 1},
                new double[] { 11, 5, 1, 2, 1},
                new double[] { 15, 5, 2, 5, 9},
                new double[] { 10, 5, 2, 1, 0},
                new double[] { 10, 5, 2, 1, 1},
            };

        //training set model values
        public string[] labels =
            {
                "make1",
                "make2",
                "make3",
            };


        //training set make values (classes)
        public int[] outputs =
            {
                0,0,0,0,0,
                1,1,1,
                2,2
            };

        public string inputModel; //label for our input point
        public double attribute1, attribute2, attribute3, attribute4, attribute5;
        protected bool trainingDataNormalized = false; //assume training data isn't normalized.

        protected string xCoord = "-1";
        protected string yCoord = "-1";

        public int outputClass; //need a default value

        KNearestNeighbor knn;

        public KNNForm()
        {
            InitializeComponent();

            //k = Convert.ToInt32(Math.Ceiling(Math.Sqrt(inputs.Count()))); //set the k value to default be sqrt(num_inputs)

            knn = new KNearestNeighbor(k, inputs: inputs, outputs: outputs); //initialize our algorithm with inputs
        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {
            TextBox objTextBox = (TextBox)sender;
            int value = Convert.ToInt32(objTextBox.Text);

            k = knn.setKValue(value);
            label4.Text = String.Format(Convert.ToString(k));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int temp3 = errorProviderK.GetErrorCount();
                int temp2 = errorProviderCalculate.GetErrorCount();
                int temp4 = errorProviderModel.GetErrorCount();

                inputModel = modelTB.Text;

                if (kValueTB.Text.Count() == 0)
                    errorProviderK.SetErrorWithCount(kValueTB, "The k-value must have at least 1 (non-zero) character.");

                if (inputModel.Count() == 0)
                    errorProviderModel.SetErrorWithCount(modelTB, "The model must have at least 1 character.");

                if (attribute1TB.Text.Count() == 0)
                    errorProviderAttribute1.SetErrorWithCount(attribute1TB, "This attribute must have at least 1 character.");

                if (attribute2TB.Text.Count() == 0)
                    errorProviderAttribute2.SetErrorWithCount(attribute2TB, "This attribute must have at least 1 character.");

                if (attribute3TB.Text.Count() == 0)
                    errorProviderAttribute3.SetErrorWithCount(attribute3TB, "This attribute must have at least 1 character.");

                if (attribute4TB.Text.Count() == 0)
                    errorProviderAttribute4.SetErrorWithCount(attribute4TB, "This attribute must have at least 1 character.");

                if (attribute5TB.Text.Count() == 0)
                    errorProviderAttribute5.SetErrorWithCount(attribute5TB, "This attribute must have at least 1 character.");

                if (errorProviderK.HasErrors() == true ||
                    errorProviderCalculate.HasErrors() == true ||
                    errorProviderModel.HasErrors() == true ||
                    errorProviderAttribute1.HasErrors() == true ||
                    errorProviderAttribute2.HasErrors() == true ||
                    errorProviderAttribute3.HasErrors() == true ||
                    errorProviderAttribute4.HasErrors() == true ||
                    errorProviderAttribute5.HasErrors() == true)
                {
                    Exception error = new Exception();

                    errorProviderCalculate.SetError(calculateButton, "Please fix all errors before submitting");

                    throw error;
                }

                //Get rid of the error if they fixed everything.
                else
                {
                    errorProviderCalculate.RemoveErrors();
                    errorProviderCalculate.Clear();
                }

                //Actual computations.
                int temp = k;

                //Normalize input data.
                if (checkBox1.Checked)
                {
                    //Normalize my input
                    double[][] temp5 = NormalizeData.Normalize(inputs);
                }

                //double[] data = { attribute1, attribute2, attribute3, attribute4, attribute5 };
                double[] data = { NormalizeData.Normalize(inputs, attribute1TB, 0),
                    NormalizeData.Normalize(inputs, attribute2TB, 1),
                    NormalizeData.Normalize(inputs, attribute3TB, 2),
                    NormalizeData.Normalize(inputs, attribute4TB, 3),
                    NormalizeData.Normalize(inputs, attribute5TB, 4) };

                double[][] temp6 = NormalizeData.Normalize(inputs);
                    

                //Compute input data's class.
                outputClass = knn.Compute(data);

                //debug: show it to make sure it works correctly.
                System.Windows.Forms.MessageBox.Show(Convert.ToString(outputClass));
            }

            catch { }
        }

        private void kValueTB_TextChanged(object sender, EventArgs e)
        {
            string value = kValueTB.Text;
            label4.Text = kValueTB.Text;

            ErrorProviderExtensions.ValidateKValue(errorProviderK, value, kValueTB, inputs);

            //Display a valid k-value.
            if (errorProviderK.HasErrors() == false)
                label4.Text = kValueTB.Text;

            //Don't display a bad k-value.
            else
                label4.Text = "N/A";
        }

        private void modelTB_TextChanged(object sender, EventArgs e)
        {
            inputModel = modelTB.Text;

            ErrorProviderExtensions.ValidateModel(errorProviderModel, inputModel, modelTB, inputs);
        }

        private void attribute1TB_TextChanged(object sender, EventArgs e)
        {
            string value = attribute1TB.Text;

            try
            {
                attribute1 = Convert.ToDouble(value); //Will throw formatexception if it is a letter.
            }

            //If you enter a number and backspace very quickly then an error is thrown. Let's just overlook the error.
            catch (System.FormatException error)
            {
                Console.WriteLine("This attribute can only accept discrete values. ");
                Console.WriteLine("Packed Message: " + error.Message);
                Console.WriteLine("Call Stack: " + error.StackTrace);

            }

            //Unknown error, print it.
            catch (Exception error)
            {
                Console.WriteLine("An unknown error has occured. Please review the error outputs. ");
                Console.WriteLine("Packed Message: " + error.Message);
                Console.WriteLine("Call Stack: " + error.StackTrace);
            }

            //Always run the validation on the data.
            finally
            {
                ErrorProviderExtensions.ValidateAttributes(errorProviderAttribute1, value, attribute1TB, inputs, false);
            }
        }


        private void attribute2TB_TextChanged(object sender, EventArgs e)
        {
            string value = attribute2TB.Text;

            try
            {
                attribute2 = Convert.ToDouble(value); //Will throw formatexception if it is a letter.
            }

            //If you enter a number and backspace very quickly then an error is thrown. Let's just overlook the error.
            catch (System.FormatException error)
            {
                Console.WriteLine("This attribute can only accept discrete values. ");
                Console.WriteLine("Packed Message: " + error.Message);
                Console.WriteLine("Call Stack: " + error.StackTrace);

            }

            //Unknown error, print it.
            catch (Exception error)
            {
                Console.WriteLine("An unknown error has occured. Please review the error outputs. ");
                Console.WriteLine("Packed Message: " + error.Message);
                Console.WriteLine("Call Stack: " + error.StackTrace);
            }

            //Always run the validation on the data.
            finally
            {
                ErrorProviderExtensions.ValidateAttributes(errorProviderAttribute2, value, attribute2TB, inputs, false);
            }
        }

        private void attribute3TB_TextChanged(object sender, EventArgs e)
        {
            string value = attribute3TB.Text;

            try
            {
                attribute3 = Convert.ToDouble(value); //Will throw formatexception if it is a letter.
            }

            //If you enter a number and backspace very quickly then an error is thrown. Let's just overlook the error.
            catch (System.FormatException error)
            {
                Console.WriteLine("This attribute can only accept discrete values. ");
                Console.WriteLine("Packed Message: " + error.Message);
                Console.WriteLine("Call Stack: " + error.StackTrace);

            }

            //Unknown error, print it.
            catch (Exception error)
            {
                Console.WriteLine("An unknown error has occured. Please review the error outputs. ");
                Console.WriteLine("Packed Message: " + error.Message);
                Console.WriteLine("Call Stack: " + error.StackTrace);
            }

            //Always run the validation on the data.
            finally
            {
                ErrorProviderExtensions.ValidateAttributes(errorProviderAttribute3, value, attribute3TB, inputs, false);
            }
        }

        private void attribute4TB_TextChanged(object sender, EventArgs e)
        {
            string value = attribute4TB.Text;

            try
            {
                attribute4 = Convert.ToDouble(value); //Will throw formatexception if it is a letter.
            }

            //If you enter a number and backspace very quickly then an error is thrown. Let's just overlook the error.
            catch (System.FormatException error)
            {
                Console.WriteLine("This attribute can only accept discrete values. ");
                Console.WriteLine("Packed Message: " + error.Message);
                Console.WriteLine("Call Stack: " + error.StackTrace);

            }

            //Unknown error, print it.
            catch (Exception error)
            {
                Console.WriteLine("An unknown error has occured. Please review the error outputs. ");
                Console.WriteLine("Packed Message: " + error.Message);
                Console.WriteLine("Call Stack: " + error.StackTrace);
            }

            //Always run the validation on the data.
            finally
            {
                ErrorProviderExtensions.ValidateAttributes(errorProviderAttribute4, value, attribute4TB, inputs, false);
            }
        }

        private void attribute5TB_TextChanged(object sender, EventArgs e)
        {
            string value = attribute5TB.Text;

            try
            {
                attribute5 = Convert.ToDouble(value); //Will throw formatexception if it is a letter.
            }

            //If you enter a number and backspace very quickly then an error is thrown. Let's just overlook the error.
            catch (System.FormatException error)
            {
                Console.WriteLine("This attribute can only accept discrete values. ");
                Console.WriteLine("Packed Message: " + error.Message);
                Console.WriteLine("Call Stack: " + error.StackTrace);

            }

            //Unknown error, print it.
            catch (Exception error)
            {
                Console.WriteLine("An unknown error has occured. Please review the error outputs. ");
                Console.WriteLine("Packed Message: " + error.Message);
                Console.WriteLine("Call Stack: " + error.StackTrace);
            }

            //Always run the validation on the data.
            finally
            {
                ErrorProviderExtensions.ValidateAttributes(errorProviderAttribute5, value, attribute5TB, inputs, false);
            }
        }

        private void dataTabControl_Selected(object sender, TabControlEventArgs e)
        {
            if(dataTabControl.SelectedIndex == 1)
            {
                dataGridDenormalizedData.AutoGenerateColumns = false;
                dataGridDenormalizedData.Columns.Add("Make", "Make"); //class
                dataGridDenormalizedData.Columns.Add("Model", "Model");
                dataGridDenormalizedData.Columns.Add("Attribute1", "Attribute1");
                dataGridDenormalizedData.Columns.Add("Attribute2", "Attribute2");
                dataGridDenormalizedData.Columns.Add("Attribute3", "Attribute3");
                dataGridDenormalizedData.Columns.Add("Attribute4", "Attribute4");
                dataGridDenormalizedData.Columns.Add("Attribute5", "Attribute5");

                dataGridDenormalizedData.DataSource = inputs.ToList();
            }
                
        }

        private void label6_TextChanged(object sender, EventArgs e)
        {
            Label objLabel = (Label)sender;
            string text = Convert.ToString(outputClass);

            objLabel.Text = text;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                xCoord = comboBox1.SelectedItem.ToString(); //Need this first so it will definitely commit if comboBox2 is null.
                yCoord = comboBox2.SelectedItem.ToString();
                
                //Only do validation IF both items have been selected.
                ErrorProviderExtensions.ValidateCoordinates(errorProviderCoordinateX, xCoord, yCoord, comboBox1, comboBox2, inputs);
            }

            catch (NullReferenceException error)
            {
                Console.WriteLine("ComboBox2 has not been initiated to a value and you are attempting to set yCoord to a null value. We will let this slide for right now because it should only happen initially when the first value is selected. ");
                Console.WriteLine("Packed Message: " + error.Message);
                Console.WriteLine("Call Stack: " + error.StackTrace);
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                yCoord = comboBox2.SelectedItem.ToString(); //Need this first so it will definitely commit if comboBox2 is null.
                xCoord = comboBox1.SelectedItem.ToString();

                //Only do validation IF both items have been selected.
                ErrorProviderExtensions.ValidateCoordinates(errorProviderCoordinateY, xCoord, yCoord, comboBox1, comboBox2, inputs);
            }

            catch (NullReferenceException error)
            {
                Console.WriteLine("ComboBox1 has not been initiated to a value and you are attempting to set yCoord to a null value. We will let this slide for right now because it should only happen initially when the first value is selected. ");
                Console.WriteLine("Packed Message: " + error.Message);
                Console.WriteLine("Call Stack: " + error.StackTrace);
            }
        }

        //TODO: Need to rework the error provider extension because it relies on a static count field so if there are multiple errors (like here) 
        // then you might run into errors displaying and checking values. 
        //Example: If you have a bad x AND y coordinate and ONLY change the x coordinate to a valid selection then it will still display the error 
        //since the error count is still 1. But once you fix both it will resolve.
        private void plotButton_Click(object sender, EventArgs e)
        {
            try
            {
                //If the x-coordinate hasn't been chosen yet.
                if (xCoord.Equals("-1"))
                    ErrorProviderExtensions.SetErrorWithCount(errorProviderCoordinateX, comboBox1, "Please choose an X-Value");

                //If the y-coordinate hasn't been chosen yet.
                if (yCoord.Equals("-1"))
                    ErrorProviderExtensions.SetErrorWithCount(errorProviderCoordinateY, comboBox2, "Please choose an Y-Value");

                //Both have errors.
                if (errorProviderCoordinateX.HasErrors() == true &&
                    errorProviderCoordinateY.HasErrors() == true)
                {
                    ErrorProviderExtensions.SetErrorWithCount(errorProviderPlot, plotButton, "Please make sure the X and Y coordinate choices are distinct.");
                    ErrorProviderExtensions.SetErrorWithCount(errorProviderCoordinateX, comboBox1, "Please validate the X value.");
                    ErrorProviderExtensions.SetErrorWithCount(errorProviderCoordinateY, comboBox2, "Please validate the Y value.");

                    throw new CoordinateException("Need valid x and y coordinates.");
                }

                //Just the x-coordinate has errors.
                else if (ErrorProviderExtensions.HasErrors(errorProviderCoordinateX) == true)
                {
                    ErrorProviderExtensions.SetErrorWithCount(errorProviderCoordinateX, comboBox1, "Please validate the X value.");

                    throw new XCoordinateException("Need valid X-coordinate");
                }

                //Just the y-coordinate has errors.
                else if (ErrorProviderExtensions.HasErrors(errorProviderCoordinateY) == true)
                {
                    ErrorProviderExtensions.SetErrorWithCount(errorProviderCoordinateY, comboBox2, "Please validate the Y value.");

                    throw new XCoordinateException("Need valid Y-coordinate");
                }

                else
                {
                    ErrorProviderExtensions.RemoveErrors(errorProviderCoordinateX);
                    ErrorProviderExtensions.RemoveErrors(errorProviderCoordinateY);
                    ErrorProviderExtensions.RemoveErrors(errorProviderPlot);

                    errorProviderPlot.Clear();
                }

                //Only normalize the data if it already hasn't been normalized.

                double[][] normalizedInputs = NormalizeData.Normalize(inputs);

                double[] data = { NormalizeData.Normalize(inputs, attribute1TB, 0),
                    NormalizeData.Normalize(inputs, attribute2TB, 1),
                    NormalizeData.Normalize(inputs, attribute3TB, 2),
                    NormalizeData.Normalize(inputs, attribute4TB, 3),
                    NormalizeData.Normalize(inputs, attribute5TB, 4) };

                //NEED TO ACCOUNT FOR THE CASE THE PERSON JUST HITS "PLOT" and doesn't choose two values. Maybe just set the values to 
                //two default "Attribute1 and Attribute2 values. No - we want them to choose them so it will look good and display right.

                //Just for the case in which x-coord: Attribute1 and y-coord: Attribute2

                Plot.PlotPoints(chart1, 0, 1, inputs, normalizedInputs, outputs, data);

                //int point = 0;
                //for (int count = 0; count < inputs.Length; count++)
                //{
                //    string className = outputs[count].ToString();

                //    chart1.Series[className].Points.AddXY(normalizedInputs[count][0], normalizedInputs[count][1]);

                //    try
                //    {
                //        string xInputCoord = StringExtensions.Truncate(Convert.ToString(chart1.Series[className].Points[point].XValue), 4);
                //        string yInputCoord = StringExtensions.Truncate(Convert.ToString(chart1.Series[className].Points[point].YValues[0]), 4);

                //        chart1.Series[className].Points[point].ToolTip = string.Format("Coordinate: ({0},{1}) " + "\n"
                //        + "Class: {2}", xInputCoord, chart1.Series[className].Points[point].YValues[0], className);
                //    }

                //    catch (ArgumentOutOfRangeException error)
                //    {
                //        Console.WriteLine("This error SHOULD be ok. How the tooltips are added makes it so that it will access a non-existing" 
                //            + " element due to switching the series (aka the class) and restarting at position 0. To correc this, we simply" 
                //            + " reset the counter back to zero and require an increment at the end no matter what.");
                //        Console.WriteLine("Packed Message: " + error.Message);
                //        Console.WriteLine("Call Stack: " + error.StackTrace);

                //        point = 0; //Set value back to zero.

                //        //Now re-add the tooltip to the point that failed.
                //        string xInputCoord = StringExtensions.Truncate(Convert.ToString(chart1.Series[className].Points[point].XValue), 4);
                //        string yInputCoord = StringExtensions.Truncate(Convert.ToString(chart1.Series[className].Points[point].YValues[0]), 4);

                //        chart1.Series[className].Points[point].ToolTip = string.Format("Coordinate: ({0},{1}) " + "\n"
                //        + "Class: {2}", xInputCoord, chart1.Series[className].Points[point].YValues[0], className);
                //    }

                //    finally
                //    {
                //        point++;
                //    }

                //    chart1.Series[5].Points.AddXY(data[0], data[1]); //add our input point
                //}

                chart1.Show();

                ////X = attribute 1 combination choices
                //if (xCoord.Equals("Attribute1") && yCoord.Equals("Attribute2"))
                //{
                //    chart1.Series[0].Points.AddXY(inputs[0][1], inputs[0][2]);
                //}

                //if (comboBox1.SelectedText.Equals("Attribute1") && comboBox2.SelectedText.Equals("Attribute3"))
                //    chart1.Series[0].Points.AddXY(inputs.ElementAt(0), inputs.ElementAt(2));

                //else if (comboBox1.SelectedText.Equals("Attribute1") && comboBox2.SelectedText.Equals("Attribute4"))
                //    chart1.Series[0].Points.AddXY(inputs.ElementAt(0), inputs.ElementAt(3));

                //else if (comboBox1.SelectedText.Equals("Attribute1") && comboBox2.SelectedText.Equals("Attribute5"))
                //    chart1.Series[0].Points.AddXY(inputs.ElementAt(0), inputs.ElementAt(4));

                ////X = attribute2 cominbation choices
                //else if (comboBox1.SelectedText.Equals("Attribute2") && comboBox2.SelectedText.Equals("Attribute1"))
                //    chart1.Series[0].Points.AddXY(inputs.ElementAt(1), inputs.ElementAt(0));

                //else if (comboBox1.SelectedText.Equals("Attribute2") && comboBox2.SelectedText.Equals("Attribute3"))
                //    chart1.Series[0].Points.AddXY(inputs.ElementAt(1), inputs.ElementAt(2));

                //else if (comboBox1.SelectedText.Equals("Attribute2") && comboBox2.SelectedText.Equals("Attribute4"))
                //    chart1.Series[0].Points.AddXY(inputs.ElementAt(1), inputs.ElementAt(3));

                //else if (comboBox1.SelectedText.Equals("Attribute2") && comboBox2.SelectedText.Equals("Attribute5"))
                //    chart1.Series[0].Points.AddXY(inputs.ElementAt(1), inputs.ElementAt(4));

                ////X = attribute3 combination choices
                //else if (comboBox1.SelectedText.Equals("Attribute3") && comboBox2.SelectedText.Equals("Attribute1"))
                //    chart1.Series[0].Points.AddXY(inputs.ElementAt(2), inputs.ElementAt(0));

                //else if (comboBox1.SelectedText.Equals("Attribute3") && comboBox2.SelectedText.Equals("Attribute2"))
                //    chart1.Series[0].Points.AddXY(inputs.ElementAt(2), inputs.ElementAt(1));

                //else if (comboBox1.SelectedText.Equals("Attribute3") && comboBox2.SelectedText.Equals("Attribute4"))
                //    chart1.Series[0].Points.AddXY(inputs.ElementAt(2), inputs.ElementAt(3));

                //else if (comboBox1.SelectedText.Equals("Attribute3") && comboBox2.SelectedText.Equals("Attribute5"))
                //    chart1.Series[0].Points.AddXY(inputs.ElementAt(2), inputs.ElementAt(4));

                ////X = attribute4 combination choices
                //else if (comboBox1.SelectedText.Equals("Attribute4") && comboBox2.SelectedText.Equals("Attribute1"))
                //    chart1.Series[0].Points.AddXY(inputs.ElementAt(3), inputs.ElementAt(0));

                //else if (comboBox1.SelectedText.Equals("Attribute4") && comboBox2.SelectedText.Equals("Attribute2"))
                //    chart1.Series[0].Points.AddXY(inputs.ElementAt(3), inputs.ElementAt(1));

                //else if (comboBox1.SelectedText.Equals("Attribute4") && comboBox2.SelectedText.Equals("Attribute3"))
                //    chart1.Series[0].Points.AddXY(inputs.ElementAt(3), inputs.ElementAt(2));

                //else if (comboBox1.SelectedText.Equals("Attribute4") && comboBox2.SelectedText.Equals("Attribute5"))
                //    chart1.Series[0].Points.AddXY(inputs.ElementAt(3), inputs.ElementAt(4));


                ////X= attribute5 combination choices
                //else if (comboBox1.SelectedText.Equals("Attribute5") && comboBox2.SelectedText.Equals("Attribute1"))
                //    chart1.Series[0].Points.AddXY(inputs.ElementAt(4), inputs.ElementAt(0));

                //else if (comboBox1.SelectedText.Equals("Attribute5") && comboBox2.SelectedText.Equals("Attribute2"))
                //    chart1.Series[0].Points.AddXY(inputs.ElementAt(4), inputs.ElementAt(1));

                //else if (comboBox1.SelectedText.Equals("Attribute5") && comboBox2.SelectedText.Equals("Attribute3"))
                //    chart1.Series[0].Points.AddXY(inputs.ElementAt(4), inputs.ElementAt(2));

                //else if (comboBox1.SelectedText.Equals("Attribute5") && comboBox2.SelectedText.Equals("Attribute4"))
                //    chart1.Series[0].Points.AddXY(inputs.ElementAt(4), inputs.ElementAt(3));
            }

            catch (XCoordinateException error)
            {
                //If x has an error (and we checked if both had errors and that didn't trigger) then x must be the only 
                //one with an error so we can remove the previous errors (if any) from y.
                ErrorProviderExtensions.RemoveErrors(errorProviderCoordinateY);
                errorProviderCoordinateY.Clear();
            }

            catch (YCoordinateException error)
            {
                //If y has an error (and we checked if both had errors and that didn't trigger) then y must be the only 
                //one with an error so we can remove the previous errors (if any) from x.
                ErrorProviderExtensions.RemoveErrors(errorProviderCoordinateX);
                errorProviderCoordinateX.Clear();
            }

            catch { }
        }
    }

    public static class NormalizeData
    {
        //For input
        public static double Normalize(double[][]inputs, TextBox attribute, int attributeNum)
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

        ////For input
        //public static double[] Normalize(double[] training, double[][] inputs, TextBox attribute)
        //{
        //    double max = 0;
        //    double min = 1;
        //    double currentValue = Convert.ToDouble(attribute.Text);
        //    double normalizedValue = 0;

        //    //find the max/min value for attribute 1
        //    for (int count = 0; count < inputs.Length; count++)
        //    {
        //        if (inputs[count][0] > max)
        //            max = inputs[count][0];

        //        if (inputs[count][0] < min)
        //            min = inputs[count][0];
        //    }

        //    normalizedValue = (currentValue - min) / (max - min);

        //    return normalizedValue;
        //}

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


    //Allows me to check if there are any errors in the form prior to submition by calling "Has Errors".
    public static class ErrorProviderExtensions
    {
        private static int count;

        public static void SetErrorWithCount(this ErrorProvider ep, Control c, string message)
        {
            if (message == "")
            {
                if (ep.GetError(c) != "")
                    count--;
            }

            else
                count++;

            ep.SetError(c, message);
        }

        public static void ValidateKValue(this ErrorProvider ep, string value, TextBox textbox, double[][] inputs)
        {
            //If there are any letters, we throw an error.
            if (Regex.Matches(value, @"[a-zA-Z]").Count > 0)
            {
                ep.SetErrorWithCount(textbox, "K-Value cannot contain letters.");
            }

            //If there is nothing typed at all OR if there are no numbers typed, we throw an error.
            else if (value.Count() == 0 || Regex.Matches(value, @"[0-9]").Count == 0)
            {
                ep.SetErrorWithCount(textbox, "K-Value must contain a number.");
            }

            //If there is a negative number OR there is only a zero typed, we throw an error.
            else if (value.Contains("-") || (value.Length == 1 && value.Contains("0")))
            {
                ep.SetErrorWithCount(textbox, "K-Value must contain a positive number greater than zero.");
            }

            //If the value typed is out of bounds, we throw an error.
            else if (Convert.ToInt32(value) > inputs.Length)
            {
                ep.SetErrorWithCount(textbox, "K-Value must be less than the number of inputs");
            }

            //Clear the error.
            else
            {
                ep.RemoveErrors();
                ep.Clear();
            }
        }

        public static void ValidateModel(this ErrorProvider ep, string value, TextBox textbox, double[][] inputs)
        {
            if (value.Count() == 0)
            {
                ep.SetErrorWithCount(textbox, "The model must have at least 1 character.");
            }

            else if (value.Count() > 15)
            {
                ep.SetErrorWithCount(textbox, "The model cannot be longer than 15 characters.");
            }

            else
            {
                ep.RemoveErrors();
                ep.Clear();
            }
        }

        public static void ValidateAttributes(this ErrorProvider ep, string value, TextBox textbox, double[][] inputs, bool discreteValue)
        {
            //If there are any letters (we have continuous data), we throw an error.
            if (discreteValue == false && Regex.Matches(value, @"[a-zA-Z]").Count > 0)
            {
                ep.SetErrorWithCount(textbox, "This attribute cannot contain letters.");
            }

            //If there is nothing typed at all OR if there are no numbers typed, we throw an error.
            else if (value.Count() == 0 || Regex.Matches(value, @"[0-9]").Count == 0)
            {
                ep.SetErrorWithCount(textbox, "This attribute must contain a number.");
            }

            //If there is a negative number, we throw an error.
            else if (value.Contains("-"))
            {
                ep.SetErrorWithCount(textbox, "This attribute must contain a positive number greater than zero.");
            }

            //Clear the error.
            else
            {
                ep.RemoveErrors();
                ep.Clear();
            }
        }

        public static void ValidateCoordinates(this ErrorProvider ep, string xCoord, string yCoord, ComboBox comboboxX, ComboBox comboboxY, double[][] inputs)
        {
            //If the X-coordinate and Y-coordinate are the same attributes, pick different ones.
            if (xCoord.Equals(yCoord) || yCoord.Equals(xCoord))
            {
                ep.SetErrorWithCount(comboboxX, "You need to pick two different attributes to plot.");
                ep.SetErrorWithCount(comboboxY, "You need to pick two different attributes to plot.");
            }

            //Clear the error.
            else
            {
                ep.RemoveErrors();
                ep.Clear();
            }
        }

        public static bool HasErrors(this ErrorProvider ep)
        {
            return count != 0;
        }

        public static int GetErrorCount(this ErrorProvider ep)
        {
            return count;
        }

        public static int RemoveErrors(this ErrorProvider ep)
        {
            return count = 0;
        }
    }

    public class XCoordinateException : Exception
    {

        public XCoordinateException() { }

        public XCoordinateException(string message)
            : base(message)
        { }

        public XCoordinateException(string message, Exception inner)
            : base(message, inner)
        { }
    }

    public class YCoordinateException : Exception
    {

        public YCoordinateException() { }

        public YCoordinateException(string message)
            : base(message)
        { }

        public YCoordinateException(string message, Exception inner)
            : base(message, inner)
        { }
    }

    public class CoordinateException : Exception
    {

        public CoordinateException() { }

        public CoordinateException(string message)
            : base(message)
        { }

        public CoordinateException(string message, Exception inner)
            : base(message, inner)
        { }
    }

    public static class StringExtensions
    {
        public static string Truncate(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }
    }

    public static class Plot
    {
        public static void PlotPoints(Chart chart, int xCoordChoice, int yCoordChoice, double[][]trainingSet, double[][]normalizedTrainingSet, int[]outputs, double[]inputData )
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