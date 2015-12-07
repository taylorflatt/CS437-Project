﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Formula.Functions;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace KNearestNeighbor
{
    public partial class Wizard : Form
    {
        protected int k;
        KNearestNeighborAlgorithm knn;

        protected List<List<double>> trainingSet = new List<List<double>>(); //Training Set
        protected List<int> outputClass = new List<int>(); //Class (output) Set
        protected List<double> inputSet = new List<double>(); //User Input Set.
        protected int userOutputClass; //User's class after computing KNN.

        protected List<List<double>> normalizedTrainingSet = new List<List<double>>(); //Normalized Training Set
        protected List<double> normalizedInputSet = new List<double>(); //Normalized Input Set

        protected List<string> trainingSetLabel = new List<string>();
        protected List<string> outputClassNames = new List<string>(); //Name we read from the data. (Assume this is first).
        protected List<string> attributeNames = new List<string>(); //without make/model

        int inputClass;

        public Wizard()
        {
            InitializeComponent();

            //Load the description text into the description step.
            //var fileName = Path.Combine(Directory.GetCurrentDirectory(), "description.txt");
            string fileName = "description.txt";

            try
            {
                using (FileStream file = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    //programDescriptionTB.LoadFile(fileName, RichTextBoxStreamType.PlainText); //don't need

                    int orderCount = 0; //Set to zero because in the Regex it adds a whitespace line so it increments falsely by 1.
                    foreach (string line in File.ReadLines(fileName))
                    {
                        StringExtensions.ParseLine(programDescriptionTB, line, orderCount, ref orderCount);
                        orderCount++;
                    }
                }
            }
            
            catch(System.IO.FileNotFoundException error)
            {
                Console.WriteLine("We could not find the text file to display the instructions for step 1 (description step). ");
                Console.WriteLine("Packed Message: " + error.Message);
                Console.WriteLine("Call Stack: " + error.StackTrace);

                DialogResult errorMessage = MessageBox.Show(String.Format("Unfortunately, the {0} file cannot be found. Please check that the file exists and is in the directory of the program. ", fileName),
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1);

                if (errorMessage == DialogResult.OK)
                    Environment.Exit(1);
            }

            //Don't let them manipulate the training data until it has been entered.
            kValueTB.Enabled = false;
            dontNormalizeInputDataCheckBox.Enabled = false;
            dontNormalizeTrainingDataCheckBox.Enabled = false;

        }

        //After initial validation, it may not be re-validating the form to catch new errors.

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = openFileDialog1.ShowDialog();

                //If they cancel out of the dialog (not selecting a file).
                //if (result == DialogResult.Cancel || result == DialogResult.Abort)
                //    initialDataStep2.CausesValidation = false;

                //They have chosen a file.
                if (result == DialogResult.OK)
                {
                    //initialDataStep2.CausesValidation = true;
                    XSSFWorkbook xssfwb;
                    using (FileStream file = new FileStream(openFileDialog1.FileName, FileMode.Open, FileAccess.Read))
                    {
                        xssfwb = new XSSFWorkbook(file);
                        fileLocationLabel.Text = openFileDialog1.SafeFileName;
                    }

                    ISheet sheet = xssfwb.GetSheet("Sheet1");

                    int location = 2; //so we skip make/model
                    for (int row = 0; row <= sheet.LastRowNum; row++)
                    {
                        //First row and it isn't empty, these are our attribute names.
                        if (row.Equals(0) && sheet.GetRow(row) != null)
                        {
                            while (sheet.GetRow(0).GetCell(location) != null)
                            {
                                attributeNames.Add(Convert.ToString(sheet.GetRow(row).GetCell(location).StringCellValue));
                                location++;
                            }
                        }

                        else if (sheet.GetRow(row) != null) //null is when the row only contains empty cells 
                        {
                            //Assumption 1: The class will always be the first value in the row.
                            //Only add the outputClassName IF it is distinct so we can 1-to-1 match with outputClass.
                            string className = Convert.ToString(sheet.GetRow(row).GetCell(0).StringCellValue);

                            //if the outputClassNames list ISN'T already contained the current class name, then we don't add it to the list.
                            if (outputClassNames.Contains(className) == false)
                                outputClassNames.Add(className);

                            //Now I need to check the outputClassNames with the currentClassName and add the appropriate class number.
                            if (outputClassNames.Contains(className) == true)
                                outputClass.Add(outputClassNames.IndexOf(className));

                            //Assumption 2: The data label will always be the second value in the row.
                            trainingSetLabel.Add(Convert.ToString(sheet.GetRow(row).GetCell(1).StringCellValue));

                            //Always going to start adding the training attributes at location 2 in excel.
                            location = 2;

                            //temp list to store the current training set attributes.
                            List<double> temp = new List<double>();

                            //While there are still values in the row we add them to our training set data.
                            while (sheet.GetRow(row).GetCell(location) != null)
                            {
                                temp.Add(Convert.ToDouble(sheet.GetRow(row).GetCell(location).NumericCellValue));
                                location++;
                            }

                            trainingSet.Add(temp); //Add the list of temp values to the training set.
                        }
                    }

                    predictAttributeNumLabel.Text = string.Format("We see {0} attributes.", attributeNames.Count);
                    populateDataGrid(xssfwb, sheet);
                    populateAttributeList();

                    //Now allow them to be able to manipulate the data.
                    kValueTB.Enabled = true;
                    dontNormalizeInputDataCheckBox.Enabled = true;
                    dontNormalizeTrainingDataCheckBox.Enabled = true;
                    baseControl.NextButtonEnabled = true;
                }
            }

            catch (ICSharpCode.SharpZipLib.Zip.ZipException error)
            {
                Console.WriteLine("You need to select an acceptable file type. ");
                Console.WriteLine("Packed Message: " + error.Message);
                Console.WriteLine("Call Stack: " + error.StackTrace);

                DialogResult errorMessage = MessageBox.Show("You must use a file with .xlsx extension. ",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1);

                if(errorMessage == DialogResult.OK)
                    button1_Click(sender, e); //Restart the dialog process.
            }

            catch (System.IO.IOException error)
            {
                Console.WriteLine("The file is currently being used by another process. Close the file and try to reopen it. ");
                Console.WriteLine("Packed Message: " + error.Message);
                Console.WriteLine("Call Stack: " + error.StackTrace);

                DialogResult errorMessage = MessageBox.Show("The file you have selected is currently being used by another process. Please "
                    + "close it and click \"Ok\" when you have done so. Then try to open it. ",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1);

                if (errorMessage == DialogResult.OK)
                    button1_Click(sender, e); //Restart the dialog process.
            }
        }

        //What about taking the data sheet itself as the input and then creating the view from the excel file directly?
        private void populateDataGrid(XSSFWorkbook xssfwb, ISheet sheet)
        {

            DataTable table = new DataTable();
            IRow headerRow = sheet.GetRow(0);

            int cellCount = headerRow.LastCellNum;

            for (int i = headerRow.FirstCellNum; i < cellCount; i++)
            {
                DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue);
                table.Columns.Add(column);
            }

            int rowCount = sheet.LastRowNum;
            //set i to sheet.FirstRowNum + 1 so it bypasses the first (header) row.
            for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
            {
                IRow row = sheet.GetRow(i);
                DataRow dataRow = table.NewRow();
                for (int j = row.FirstCellNum; j < cellCount; j++)
                {
                    if (row.GetCell(j) != null)
                    {
                        dataRow[j] = row.GetCell(j).ToString();
                    }
                }

                table.Rows.Add(dataRow);
            }

                dataGridView1.DataSource = table;
            }

        private void populateAttributeList()
        {
            tableLayoutPanel1.Controls.Clear(); //If they keep changing the value then we should remove the current rows.

            int numAttributes = attributeNames.Count;

            try
            {
                //Add values starting from zero to value - 1.
                int column = 2;
                for (int count = 1; count <= numAttributes; count++)
                {
                    int row = count;
                    //inputSet.Add(count - 1);

                    System.Windows.Forms.Label temp = new System.Windows.Forms.Label();
                    temp.Text = attributeNames.ElementAt(count - 1).ToString() + ": ";

                    tableLayoutPanel1.Controls.Add(temp, 0, row);

                    //I need to be explicit and set a name so I can reference the fields later.
                    System.Windows.Forms.TextBox tempTB = new System.Windows.Forms.TextBox();
                    tempTB.Name = "attribute" + count + "TB"; //attribute1TB, attribute2TB...

                    tableLayoutPanel1.Controls.Add(tempTB, column, row);
                }
            }

            catch { }
        }

        private void kValueTB_TextChanged(object sender, EventArgs e)
        {
            string kValue = kValueTB.Text;

            DataValidation.ValidateKValue(errorProviderK, kValue, kValueTB, trainingSet);
        }

        private void groupBox1_TextChanged(object sender, EventArgs e)
        {
            string value = tableLayoutPanel1.GetControlFromPosition(2, 1).Text;
        }

        private void initialDataStep2_Validating(object sender, CancelEventArgs e)
        {
            //THESE TWO LINES ARE REQUIRED OR THE VALIDATION WON'T WORK PROPERLY.
            DataValidation.removeErrors(errorProviderAttributes); //Zero out the errors.
            DataValidation.removeErrors(errorProviderK); //Zero out the errors.

            int numAttributes = attributeNames.Count;
            bool valid = true; //Assume the data is valid unless we find something invalid.

            for (int count = 1; count <= numAttributes; count++)
            {
                string textBoxName = "attribute" + count + "TB";
                System.Windows.Forms.TextBox textBox = (System.Windows.Forms.TextBox)this.tableLayoutPanel1.Controls[textBoxName];
                string value = this.tableLayoutPanel1.Controls[textBoxName].Text;

                DataValidation.ValidateAttributes(errorProviderAttributes, value, textBox, false);
            }

            //Validate the K-Value
            string kValue = kValueTB.Text;
            DataValidation.ValidateKValue(errorProviderK, kValue, kValueTB, trainingSet);

            //Check if the data has been normalized properly (or at all). We are HOPING they normalized it properly.
            if (dontNormalizeInputDataCheckBox.Checked)
            {
                int count = 0;
                foreach (var element in inputSet)
                {
                    //If the elements are smaller than 1, our data is already normalized.
                    if (element < 1)
                        count++;
                }

                //If even 1 value isn't less than 1, the data isn't normalized. Prompt the user.
                if (count < inputSet.Count)
                {
                    DialogResult warning = MessageBox.Show("We noticed at least one input was greater than 1. Your inputs haven't been normalized properly. Please review your data. Or uncheck the \"Don't Normalize Input Data\" option. ",
                    "Warning",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button1);

                    //They are re-checking information.
                    if (warning == DialogResult.OK)
                        valid = false;
                }
            }

            //Check if the training data has been normalized properly (or at all).
            if (dontNormalizeTrainingDataCheckBox.Checked)
            {
                int count = 0;
                foreach (var listElement in inputSet)
                {
                    foreach(var element in inputSet)
                    {
                        if (element > 1)
                            count++;
                    }
                }

                //If even 1 value isn't less than 1, the data isn't normalized. Prompt the user.
                if (count != 0)
                {
                    DialogResult warning = MessageBox.Show("We noticed at least one training data point was greater than 1. The training data hasn't been normalized properly. Please review your data. Or uncheck the \"Don't Normalize Training Data\" option. ",
                    "Warning",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button1);

                    //They are re-checking information.
                    if (warning == DialogResult.OK)
                        valid = false;
                }
            }

            if(errorProviderAttributes.HasErrors() || errorProviderK.HasErrors())
                valid = false;
            
            if(valid == false)
            {
                e.Cancel = true;
            }
        }

        //Allow me to close the program without having to first fix the errors.
        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = false;
            base.OnClosing(e);
        }

        //If all the information is correct, compute the KNN.

        //If the user checked "don't normalize input data" AND it validated properly, then we are assuming the data is normalized. We just need to 
        //handle the case in which we have to normalize the data. (Most likely the majority of situations).

        //If the user checked "don't normalize training data" AND it validated properly, then we are assuming the data is normalized. We just need
        //to handle the case in which we have to normalize the data. (Most likely the majority of situations).
        private void initialDataStep2_Validated(object sender, EventArgs e)
        {
            int numAttributes = attributeNames.Count;

            if (dontNormalizeInputDataCheckBox.Checked == false)
            {
                for (int count = 1; count <= numAttributes; count++)
                {
                    string textBoxName = "attribute" + count + "TB"; //create textbox name
                    System.Windows.Forms.TextBox textBox = (System.Windows.Forms.TextBox)this.tableLayoutPanel1.Controls[textBoxName];
                    string value = this.tableLayoutPanel1.Controls[textBoxName].Text; //get the value in that textbox
                    double normalizedInput = NormalizeData.Normalize(trainingSet, value, count - 1); //normalize the value

                    inputSet.Add(Convert.ToDouble(value)); //store un-normalized value to the un-normalized list.
                    normalizedInputSet.Add(normalizedInput); //Add the normalized value to the normalized list.
                }
            }

            //Then it has already been normalized AND confirmed by the user, just set the normalized input list to be the input list.
            else
                normalizedInputSet = inputSet;
            

            if(dontNormalizeTrainingDataCheckBox.Checked == false)
                normalizedTrainingSet = NormalizeData.Normalize(trainingSet, numAttributes);

            else
                normalizedTrainingSet = trainingSet;

            //initialize our KNN object.
            knn = new KNearestNeighborAlgorithm(k, inputs: trainingSet, outputs: outputClass); //initialize our algorithm with inputs

            inputClass = knn.Compute(inputSet);

            label14.Text = Convert.ToString(inputClass);
        }

        //Custom code for individual steps.
        private void wizardControl2_CurrentStepIndexChanged(object sender, EventArgs e)
        {
            //Data initialize step.
            if (baseControl.CurrentStepIndex == 0)
                baseControl.NextButtonEnabled = true;

            //Data initialize step.
            if (baseControl.CurrentStepIndex == 1)
                baseControl.NextButtonEnabled = false;

            //Data display step.
            else if (baseControl.CurrentStepIndex == 2)
            {
                baseControl.BackButtonEnabled = false;

                chart1.Legends.Add("Legend");

                Random randomGen = new Random();

                List<Color> acceptableColorList = new List<Color>
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
                    Color.Gold,
                    Color.LightCoral,
                    Color.Red,
                    Color.BlueViolet

                    //In the case that there are more than 12 classes, just generate a random color.
                };

                List<int> pickedColors = new List<int>();

                int count = 0;
                int temp312 = outputClass.Distinct().Count();
                while (count < outputClass.Distinct().Count())
                {
                    chart1.Series.Add("Class " + count);
                    chart1.Series[count].ChartType = SeriesChartType.Point; //Point graph.
                    chart1.Series[count].MarkerStyle = MarkerStyle.Circle;
                    chart1.Series[count].MarkerSize = 6; //Might look into parameterizing this to give the user the option of increasing the size.

                    bool uniqueColor = false;

                    while(uniqueColor == false)
                    {
                        int colorPositionInList = randomGen.Next(1, acceptableColorList.Count - 1);

                        //If the color is unique, we will add it. Otherwise, we will pick another color from the list.
                        if(!pickedColors.Contains(colorPositionInList))
                        {
                            //Set the series color.
                            chart1.Series[count].MarkerColor = acceptableColorList.ElementAt(colorPositionInList);

                            //Add the element location to our list so we don't pick the same color again.
                            pickedColors.Add(colorPositionInList); 

                            //Drop from the while loop.
                            uniqueColor = true;
                        }

                        //If we have assigned all of the static colors, then we need to grab a random color.
                        if(pickedColors.Count >= (acceptableColorList.Count - 1))
                        {
                            chart1.Series[count].MarkerColor = Color.FromArgb((byte)randomGen.Next(255), (byte)randomGen.Next(255), (byte)randomGen.Next(255));
                        }
                    }

                    count++;
                }

                //Create out input data point.
                chart1.Series.Add("My Input");
                chart1.Series[count].ChartType = SeriesChartType.Point; //Point graph.
                chart1.Series[count].MarkerColor = acceptableColorList.ElementAt(0); //Set the color to be the first in the list.
                chart1.Series[count].MarkerStyle = MarkerStyle.Square;
                chart1.Series[count].MarkerSize = 7; //Might look into parameterizing this to give the user the option of increasing the size.



            }
        }

        private void plotButton_Click(object sender, EventArgs e)
        {
            try
            {
                int xCoord = plotXComboBox.SelectedIndex;
                int yCoord = plotYComboBox.SelectedIndex;

                //NEED TO ACCOUNT FOR THE CASE THE PERSON JUST HITS "PLOT" and doesn't choose two values. Maybe just set the values to 
                //two default "Attribute1 and Attribute2 values. No - we want them to choose them so it will look good and display right.

                //Just for the case in which x-coord: Attribute1 and y-coord: Attribute2

                Plot.PlotPoints(chart1, xCoord, yCoord, normalizedTrainingSet, normalizedInputSet, inputClass, outputClass, outputClassNames);

                chart1.Show();
            }
            catch { }
        }

        private void programDescriptionTB_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start(e.LinkText);
        }

        private void Wizard_HelpButtonClicked(object sender, EventArgs e)
        {
            string helpfile = "knnhelp.CHM";
            System.Windows.Forms.Help.ShowHelpIndex(this, helpfile);

            Help.ShowHelp(this, "helpfile.chm", HelpNavigator.TopicId, "1234");
        }

        private void wizardControl2_BackButtonClick(object sender, CancelEventArgs e)
        {

        }
    }
}