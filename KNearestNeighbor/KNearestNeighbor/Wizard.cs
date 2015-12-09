using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

//DON'T FORGET TO CHANGE THE PATHS TO DOCUMENTS AND THE HELP FILE.

namespace KNearestNeighbor
{
    [Serializable()]
    public partial class Wizard : Form
    {
        #region Main Initial Information
        protected int k;
        KNearestNeighborAlgorithm knn;

        protected List<List<double>> trainingSet = new List<List<double>>(); //Training Set
        protected List<int> outputClass = new List<int>(); //Class (output) Set
        protected List<double> inputSet = new List<double>(); //User Input Set.
        protected int userOutputClass; //User's class after computing KNN.

        protected List<List<double>> normalizedTrainingSet = new List<List<double>>(); //Normalized Training Set
        protected List<double> normalizedInputSet = new List<double>(); //Normalized Input Set

        protected List<string> trainingSetLabel = new List<string>(); //The name of the data point.
        protected List<string> outputClassNames = new List<string>(); //Name we read from the data. (Assume this is first).
        protected List<string> attributeNames = new List<string>(); //without make/model

        protected int inputClass; //Classification of the input data.

        /// <summary>
        /// Initialize the form as well as turn off validation. I also display the description of the program in this step.
        /// </summary>
        public Wizard()
        {
            InitializeComponent();

            //Don't let the form handle the validation. I have custom validation.
            this.AutoValidate = AutoValidate.Disable;

            //So the user can't enter their own data in the combobox.
            plotXComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            plotYComboBox.DropDownStyle = ComboBoxStyle.DropDownList;

            ///Display the description on the first step using a document resource.
            displayProgramDescription(programDescriptionTB);

            displayDataStep3.Subtitle = "View the classification for your input on the left. You may plot the graph to view the training data and input data along with the input's closest competitor with the given two attributes. You may also opt to show the distance calculations on the point labels as well.";
                
                //resources.GetString("initialDataStep2.Subtitle");

            //Don't let them manipulate the training data until it has been entered.
            kValueTB.Enabled = false;
            dontNormalizeInputDataCheckBox.Enabled = false;
            dontNormalizeTrainingDataCheckBox.Enabled = false;
        }
        #endregion

        #region Global Methods

        /// <summary>
        /// Will display the program description or return an error.
        /// </summary>
        /// <param name="textbox"> The ouput location of the text from the resource file.</param>
        private static void displayProgramDescription(RichTextBox textbox)
        {
            try
            {
                string descriptionDocument = global::KNearestNeighbor.Properties.Resources.description;
                string[] lines = descriptionDocument.Split("\r".ToCharArray());

                //orderCount will preserve the ordering.
                int orderCount = 0;

                //Parse line-by-line.
                for (int index = 0; index < lines.Count(); index++)
                {
                    StringExtensions.ParseLine(textbox, lines[index], orderCount, ref orderCount, 10, "Times New Roman");
                    orderCount++;
                }
            }

            //Maybe display some hard coded instructions so that the program may continue.
            catch (System.IO.FileNotFoundException error)
            {
                Console.WriteLine("We could not find the text file to display the instructions for step 1 (description step). ");
                Console.WriteLine("Packed Message: " + error.Message);
                Console.WriteLine("Call Stack: " + error.StackTrace);

                DialogResult errorMessage = MessageBox.Show(String.Format("Unfortunately, the description file cannot be found. Please report this error to the administrator. "),
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1);

                //Safely exit the program.
                if (errorMessage == DialogResult.OK)
                    Environment.Exit(1);
            }
        }

        /// <summary>
        /// Will display the initial data instructions to the user or return an error.
        /// </summary>
        /// <param name="textbox"> The ouput location of the text from the resource file.</param>
        private static void displayInitialDataInstructions(RichTextBox textbox)
        {
            try
            {
                //Need to clear or else it will loop the text in the box each time a failure occurs.
                textbox.Clear();
                string instructionsDocument = global::KNearestNeighbor.Properties.Resources.inputdatainstructions;
                string[] lines = instructionsDocument.Split("\r".ToCharArray());

                //orderCount will preserve the ordering.
                int orderCount = 0;

                //Parse line-by-line.
                for (int index = 0; index < lines.Count(); index++)
                    StringExtensions.ParseLine(textbox, lines[index], orderCount, ref orderCount, 10, "Times New Roman");
            }

            catch (System.IO.FileNotFoundException error)
            {
                Console.WriteLine("We could not find the text file to display the instructions for step 2 (initialize data step). ");
                Console.WriteLine("Packed Message: " + error.Message);
                Console.WriteLine("Call Stack: " + error.StackTrace);

                DialogResult errorMessage = MessageBox.Show(String.Format("Unfortunately, the instructions file cannot be found. Please report this error to the administrator. "),
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1);

                //Safely exit the program.
                if (errorMessage == DialogResult.OK)
                    Environment.Exit(1);
            }
        }

        //What about taking the data sheet itself as the input and then creating the view from the excel file directly?
        /// <summary>
        /// This will generate the DGV information using an excel spreadsheet.
        /// </summary>
        /// <param name="xssfwb">The particular excel file you would like to load.</param>
        /// <param name="sheet">The sheet you wish to read into the program.</param>
        private void populateDataGrid(XSSFWorkbook xssfwb, ISheet sheet)
        {
            DataTable table = new DataTable(); //Create a new table.
            IRow headerRow = sheet.GetRow(0); //The first row will always be considered the header.

            int cellCount = headerRow.LastCellNum; //Use the header to determine the length of the row.

            //Add the header row to the table.
            for (int i = headerRow.FirstCellNum; i < cellCount; i++)
            {
                DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue);
                table.Columns.Add(column);
            }

            int rowCount = sheet.LastRowNum; //Stores the number of rows in the sheet.

            /// We bypass the first header row by setting the starting value of i to be the first row + 1.
            /// Now we just add each row to the table.
            for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
            {
                IRow row = sheet.GetRow(i);
                DataRow dataRow = table.NewRow();
                for (int j = row.FirstCellNum; j < cellCount; j++)
                {
                    if (row.GetCell(j) != null)
                        dataRow[j] = row.GetCell(j).ToString();
                }

                table.Rows.Add(dataRow); //Add each row to the table.
            }

            dataGridView1.DataSource = table; //Set the source of the DGV to be the table we created.
        }

        /// <summary>
        /// This method will generate the attribute textboxes and their labels dynamically based on the training data. 
        /// This is a vestigial method that won't be called unless I fix the text location problem. 
        /// </summary>
        /// <param name="previousText"> The list that contains all of the previous values.</param>
        private void populateAttributeList(List<Object> previousText)
        {
            tableLayoutPanel1.Controls.Clear(); //If they keep changing the value then we should remove the current rows.

            int numAttributes = attributeNames.Count;

            //Add values starting from zero to value - 1.
            int column = 2;
            for (int count = 1; count <= numAttributes; count++)
            {
                int row = count;

                System.Windows.Forms.Label temp = new System.Windows.Forms.Label();
                temp.Text = attributeNames.ElementAt(count - 1).ToString() + ": ";

                tableLayoutPanel1.Controls.Add(temp, 0, row);

                //I need to be explicit and set a name so I can reference the fields later.
                System.Windows.Forms.TextBox tempTB = new System.Windows.Forms.TextBox();
                tempTB.Name = "attribute" + count + "TB"; //attribute1TB, attribute2TB...

                // If there is ANY text in the textbox from before, let's put it back.
                if (Convert.ToString(previousText[count - 1]) != "")
                    tempTB.Text = Convert.ToString(previousText[count - 1]);


                tableLayoutPanel1.Controls.Add(tempTB, column, row);
            }
        }

        /// <summary>
        /// This method will generate the attribute textboxes and their labels dynamically based on the training data.
        /// </summary>
        private void populateAttributeList()
        {
            tableLayoutPanel1.Controls.Clear(); //If they keep changing the value then we should remove the current rows.

            int numAttributes = attributeNames.Count;

            //Add values starting from zero to value - 1.
            int column = 2;
            for (int count = 1; count <= numAttributes; count++)
            {
                int row = count;

                System.Windows.Forms.Label temp = new System.Windows.Forms.Label();
                temp.Text = attributeNames.ElementAt(count - 1).ToString() + ": ";

                tableLayoutPanel1.Controls.Add(temp, 0, row);

                //I need to be explicit and set a name so I can reference the fields later.
                System.Windows.Forms.TextBox tempTB = new System.Windows.Forms.TextBox();
                tempTB.Name = "attribute" + count + "TB"; //attribute1TB, attribute2TB...

                tableLayoutPanel1.Controls.Add(tempTB, column, row);
            }
        }

        /// <summary>
        /// Allows for the program to close any any point by clicking the "X" button.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClosing(CancelEventArgs e)
        {
            e.Cancel = false;
            base.OnClosing(e);
        }

        /// <summary>
        /// Allows the program to close when the user selects the "Finish" button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void baseControl_FinishButtonClick(object sender, EventArgs e)
        {
            System.Windows.Forms.Application.Exit();
        }

        //Custom code for individual steps.
        /// <summary>
        /// This method checks whether the current step has changed and run custom code based on the next step.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void wizardControl2_CurrentStepIndexChanged(object sender, EventArgs e)
        {
            //Program description step.
            if (baseControl.CurrentStepIndex == 0)
                baseControl.NextButtonEnabled = true;

            //Data initialize step.
            if (baseControl.CurrentStepIndex == 1)
            {
                //If they haven't chosen a file yet (the label hasn't been changed), don't let them proceed to the next step.
                if (fileLocationLabel.Text.Equals("N/A"))
                    baseControl.NextButtonEnabled = false;

                /// Case: If they entered a file, then clicked "back" coming back to this step. 
                /// Solution: We let them go to the next step since they have entered a file.
                else
                    baseControl.NextButtonEnabled = true;

                //Display the instructions to the user.
                displayInitialDataInstructions(dataInitializationInstructionsTB);
            }

            //Data display step.
            else if (baseControl.CurrentStepIndex == 2)
            {
                /// We are renaming the cancel button to NEW and its function is to restart the program.
                baseControl.CancelButtonEnabled = true;
                baseControl.CancelButtonText = "New";
                baseControl.CancelButtonVisible = true;

                //Make sure the legend doesn't already exist.
                if (chart1.Legends.Count < 1)
                    chart1.Legends.Add("Legend");

                //Remove all generated information from the chart if any exist.
                chart1.Series.Clear();

                Random randomGen = new Random();

                /// List of colors for the first 11 attributes. The first element is the input data point's color.
                /// In the event that there are more than 11 attributes, we randomly generate a color below.
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
                    Color.LightCoral,
                    Color.Red,
                    Color.BlueViolet
                };

                //Colors that have already been chosen.
                List<int> pickedColors = new List<int>();

                int count = 0;

                //Generate the general characteristics for each series.
                while (count < outputClass.Distinct().Count())
                {
                    chart1.Series.Add("Class " + count);
                    chart1.Series[count].LegendText = outputClassNames[count];
                    chart1.Series[count].ChartType = SeriesChartType.Point; //Point graph.
                    chart1.Series[count].MarkerStyle = MarkerStyle.Circle;
                    chart1.Series[count].MarkerSize = 6; //Might look into parameterizing this to give the user the option of increasing the size.

                    bool uniqueColor = false;

                    while (uniqueColor == false)
                    {
                        int colorPositionInList = randomGen.Next(1, acceptableColorList.Count - 1);

                        //If the color is unique, we will add it. Otherwise, we will pick a random color.
                        if (!pickedColors.Contains(colorPositionInList))
                        {
                            //Set the series color.
                            chart1.Series[count].MarkerColor = acceptableColorList.ElementAt(colorPositionInList);

                            //Add the element location to our list so we don't pick the same color again.
                            pickedColors.Add(colorPositionInList);

                            //Drop from the while loop.
                            uniqueColor = true;
                        }

                        //If there are more than 11 attributes, then we will generate a random color.
                        if (pickedColors.Count >= (acceptableColorList.Count - 1))
                        {
                            chart1.Series[count].MarkerColor = Color.FromArgb((byte)randomGen.Next(255), (byte)randomGen.Next(255), (byte)randomGen.Next(255));
                        }
                    }

                    count++;
                }

                //Generate the general characteristics for the input point.
                chart1.Series.Add("My Input");
                chart1.Series[count].ChartType = SeriesChartType.Point; //Point graph.
                chart1.Series[count].MarkerColor = acceptableColorList.ElementAt(0); //Set the color to be the first in the list.
                chart1.Series[count].MarkerStyle = MarkerStyle.Square;
                chart1.Series[count].MarkerSize = 7; //Might look into parameterizing this to give the user the option of increasing the size.
            }
        }

        /// <summary>
        /// When the help button is clicked (at any point), a CHM file will be loaded and displayed to the user.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Wizard_HelpButtonClicked(object sender, EventArgs e)
        {
            //The CHM file will be immediately created upon compile and it will reference it in the created location.
            try
            {
                System.Diagnostics.Process.Start("knnHelp.CHM");
            }

            //If the file has been moved/deleted, notify the user that we cannot display the help file because it has been moved.
            catch (System.ComponentModel.Win32Exception error)
            {
                Console.WriteLine("We could not find the text file to display the instructions for step 2 (initialize data step). ");
                Console.WriteLine("Packed Message: " + error.Message);
                Console.WriteLine("Call Stack: " + error.StackTrace);

                DialogResult errorMessage = MessageBox.Show(String.Format("Unfortunately, the help file cannot be found. Please redownload the program or verify that the help file is in knnHelp.CHM"),
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1);
            }
        }

        /// <summary>
        /// We don't want any validation to occur when we select the "back" option. We don't care about validating 
        /// information moving backwards.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void baseControl_BackButtonClick(object sender, CancelEventArgs e)
        {
            descriptionStep1.CausesValidation = false;
            initialDataStep2.CausesValidation = false;
            displayDataStep3.CausesValidation = false;
        }

        /// <summary>
        /// We want to validate only select steps when we choose the "next" option. This validation is direction specific as well.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void baseControl_NextButtonClick(object sender, CancelEventArgs e)
        {
            // currentStep = 0: descriptionStep1
            // currentStep = 1: initialDataStep2
            // currentStep = 2: displayDataStep3
            // ...
            // This will get the current index. Since I know which step is at which index, I can just compare the numbers.
            var currentStep = this.baseControl.CurrentStepIndex;

            // Enable validation for the InitialDataStep2 if I am coming FROM descriptionStep1.
            if (currentStep == 0)
                initialDataStep2.CausesValidation = true;

            //Force validation to occur on the InitialDataStep2 if trying to go TO DataDisplayStep.
            //The case we are accounting for here is when I go from Step 1 to Step 2 (load a file), then back to Step 1 and then attempt 
            //to go onto Step 3 without adding anymore information. We need to validate the data and tell the user there is an error.
            if (currentStep == 1)
            {
                CancelEventArgs temp = new CancelEventArgs(); //Won't need/use.
                this.initialDataStep2_Validating(sender, temp);

                displayDataStep3.CausesValidation = true;
            }
        }

        /// <summary>
        /// If the user selects the "new" option in the data display step, then the program will restart at the first step.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void baseControl_CancelButtonClick(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(Application.ExecutablePath);
            Application.Exit();
        }

        #endregion

        #region Description Step

        /// <summary>
        /// Allows me to click and open a link in a browser from the description richtextbox.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void programDescriptionTB_LinkClicked(object sender, LinkClickedEventArgs e)
        {
            Process.Start(e.LinkText);
        }

        /// <summary>
        /// We don't want any action to occur when a key is pressed in the description richtextbox. This will
        /// remove the default beeps from occuring.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void programDescriptionTB_KeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
            e.SuppressKeyPress = true;
        }

        /// <summary>
        /// We don't want any action to occur when a key is pressed in the description richtextbox. This will
        /// remove the default beeps from occuring.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void programDescriptionTB_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        /// <summary>
        /// We don't want any action to occur when a key is pressed in the description richtextbox. This will
        /// remove the default beeps from occuring.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void programDescriptionTB_KeyUp(object sender, KeyEventArgs e)
        {
            e.Handled = true;
            e.SuppressKeyPress = true;
        }
        #endregion

        #region Initial Data Step

        /// <summary>
        /// When we choose the "browse" button, we need to handle what will happen. There are several cases to manage.
        /// 
        /// Case 1: They cancel from the browse dialog box.
        /// Case 2: They select a file from the browse dialog box.
        /// Case 2a: The selected file isn't the proper file type.
        /// Case 2b: The selected file is changed to a DIFFERENT file.
        /// Case 2c: The selected file is change to the SAME file (same result as 2b).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void browseButton_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = openFileDialog1.ShowDialog();

                /// Case 1: If they close the dialog box, don't validate the step yet.
                if (result == DialogResult.Cancel || result == DialogResult.Abort)
                    initialDataStep2.CausesValidation = false;

                /// Case 2: Add the file that they have selected.
                if (result == DialogResult.OK)
                {
                    XSSFWorkbook xssfwb;

                    //Read the file they have chosen.
                    using (FileStream file = new FileStream(openFileDialog1.FileName, FileMode.Open, FileAccess.Read))
                    {
                        xssfwb = new XSSFWorkbook(file);
                        fileLocationLabel.Text = openFileDialog1.SafeFileName;
                    }

                    List<Object> tempInputArray = new List<Object>();

                    /// In the event that this method is called multiple times (they attempt to enter multiple documents at different times)
                    /// then we need to remove the previous data. We can just re-add the data from the new file chosen by the user.
                    attributeNames.Clear(); //Clear attribute names list
                    kValueTB.Clear(); //Clear the k value.

                    //Clear our data sets as well.
                    inputSet.Clear();
                    trainingSet.Clear();
                    normalizedInputSet.Clear();
                    normalizedTrainingSet.Clear();
                    outputClassNames.Clear();
                    outputClass.Clear();

                    //We default to the first sheet always.
                    ISheet sheet = xssfwb.GetSheet("Sheet1");

                    int location = 2; //We skip the class position (0), and the label name (1).

                    //We add the training data to the list.
                    for (int row = 0; row <= sheet.LastRowNum; row++)
                    {
                        //First row and it isn't empty, these are our attribute names.
                        if (row.Equals(0) && sheet.GetRow(row) != null)
                        {
                            //We generate the list of attributes (we exlude the first two columns).
                            while (sheet.GetRow(0).GetCell(location) != null)
                            {
                                attributeNames.Add(Convert.ToString(sheet.GetRow(row).GetCell(location).StringCellValue));
                                location++;
                            }
                        }

                        //Now we add our training data to our training data list. This is null only when the row contains all empty cells.
                        else if (sheet.GetRow(row) != null)
                        {
                            /// Assumpetion 1: The class name will always be the first value in a row. 
                            /// We only add the class name if it is a new value (distinct).
                            string className = Convert.ToString(sheet.GetRow(row).GetCell(0).StringCellValue);

                            //If the class name is already in the class name list, then we don't add it.
                            if (outputClassNames.Contains(className) == false)
                                outputClassNames.Add(className);

                            //Now I need to check the outputClassNames with the currentClassName and add the appropriate class number.
                            if (outputClassNames.Contains(className) == true)
                                outputClass.Add(outputClassNames.IndexOf(className));

                            /// Assumption 2: The training data label will always be the second value in a row.
                            trainingSetLabel.Add(Convert.ToString(sheet.GetRow(row).GetCell(1).StringCellValue));

                            //Reset the location to 2 so we start with the first attribute and not the class.
                            location = 2;

                            //temp list to store the current training set attributes.
                            List<double> currentRowValues = new List<double>();

                            //While there are still values in the row we add them to our training set data.
                            while (sheet.GetRow(row).GetCell(location) != null)
                            {
                                currentRowValues.Add(Convert.ToDouble(sheet.GetRow(row).GetCell(location).NumericCellValue));
                                location++;
                            }

                            //Add the list of temp values to the training set.
                            trainingSet.Add(currentRowValues);
                        }
                    }

                    //We give the user our count of the number of attributes.
                    predictAttributeNumLabel.Text = string.Format("We see {0} attributes.", attributeNames.Count);

                    //Now actually display the DGV with our data.
                    populateDataGrid(xssfwb, sheet);

                    //Now display the list of attributes dynamically.
                    populateAttributeList();

                    //Now allow them to be able to manipulate the data and attempt to click next.
                    kValueTB.Enabled = true;
                    dontNormalizeInputDataCheckBox.Enabled = true;
                    dontNormalizeTrainingDataCheckBox.Enabled = true;
                    baseControl.NextButtonEnabled = true;
                }
            }

            //Wrong file type error.
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

                if (errorMessage == DialogResult.OK)
                    browseButton_Click(sender, e); //Restart the dialog process.
            }

            //The file is being used by another process.
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
                    browseButton_Click(sender, e); //Restart the dialog process.
            }

            //The file has incompatible data.
            catch (System.InvalidOperationException error)
            {
                Console.WriteLine("The file you are trying to add has incompatible data. This program doesn't allow for discrete values outside of the first row and first two columns of each row. The attribute values must be continuous (non-negative) numbers. ");
                Console.WriteLine("Packed Message: " + error.Message);
                Console.WriteLine("Call Stack: " + error.StackTrace);

                DialogResult errorMessage = MessageBox.Show("The file you are trying to add has incompatible data. Please view the help file to see the proper format. The attributes values must have continuous (non-negative) numbers. ",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1);

                if (errorMessage == DialogResult.OK)
                {
                    fileLocationLabel.Text = "N/A"; //Set the file name back to N/A.

                    //Open the knnHelp.CHM file to the place where we show how the data ought to be structured.
                    try
                    {
                        Help.ShowHelp(this, "knnHelp.CHM", HelpNavigator.Topic, @"/html/KNN Program/programoverview.htm#correctDataLayout");
                    }

                    //If the file has been moved/deleted, notify the user that we cannot display the help file because it has been moved.
                    catch (System.ComponentModel.Win32Exception error2)
                    {
                        Console.WriteLine("We could not find the text file to display the instructions for step 2 (initialize data step). ");
                        Console.WriteLine("Packed Message: " + error2.Message);
                        Console.WriteLine("Call Stack: " + error2.StackTrace);

                        DialogResult errorMessage2 = MessageBox.Show(String.Format("Unfortunately, the help file cannot be found. Please redownload the program or verify that the help file is in knnHelp.CHM"),
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error,
                            MessageBoxDefaultButton.Button1);
                    }
                }
            }

        }

        /// <summary>
        /// Set the k value enetered into the textbox to the value entered. Then perform validation on that value.
        /// Display any errors if the value is not permitted.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void kValueTB_TextChanged(object sender, EventArgs e)
        {
            string kValue = kValueTB.Text;

            DataValidation.ValidateKValue(errorProviderK, kValue, kValueTB, trainingSet);
        }

        /// <summary>
        /// This method contians the validation rules for the inital data step.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void initialDataStep2_Validating(object sender, CancelEventArgs e)
        {
            //THESE TWO LINES ARE REQUIRED OR THE VALIDATION WON'T WORK PROPERLY.
            DataValidation.RemoveProviderErrors(errorProviderAttributes); //Zero out the previous errors (if any).
            DataValidation.RemoveProviderErrors(errorProviderK); //Zero out the previous errors (if any).

            int numAttributes = attributeNames.Count;
            bool valid = true; //Assume the data is valid unless we find something invalid.

            //Run validation on each attribute entered.
            for (int count = 1; count <= numAttributes; count++)
            {
                string textBoxName = "attribute" + count + "TB";
                System.Windows.Forms.TextBox textBox = (System.Windows.Forms.TextBox)this.tableLayoutPanel1.Controls[textBoxName];
                string value = this.tableLayoutPanel1.Controls[textBoxName].Text;

                DataValidation.ValidateAttributes(errorProviderAttributes, value, textBox, false);
            }

            //Run validation on the K-Value
            string kValue = kValueTB.Text;
            DataValidation.ValidateKValue(errorProviderK, kValue, kValueTB, trainingSet);

            //Check if the data has been normalized properly (or at all). We are HOPING they normalized it properly.
            if (dontNormalizeInputDataCheckBox.Checked)
            {
                int count = 0; //So we can display an error message.
                for (int index = 1; index <= numAttributes; index++)
                {
                    string textBoxName = "attribute" + index + "TB";
                    System.Windows.Forms.TextBox textBox = (System.Windows.Forms.TextBox)this.tableLayoutPanel1.Controls[textBoxName];
                    string value = this.tableLayoutPanel1.Controls[textBoxName].Text;

                    //If there is a single attribute value that isn't in the range [0,1], then the data cannot possibly be normalized.
                    //We already exclude negative numbers so we just check that it is less than 1.
                    if (Convert.ToDouble(value) > 1)
                    {
                        count++;
                        break; //Exit the loop to save time.
                    }
                }

                //If even 1 value isn't less than 1, the data isn't normalized. Prompt the user.
                if (count > 0)
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
                int count = 0; //So we can display an error message.
                foreach (var listElement in trainingSet)
                {
                    foreach (var element in listElement)
                    {
                        //If there is a single attribute value that isn't in the range [0,1], then the data cannot possibly be normalized.
                        //We already exclude negative numbers so we just check that it is less than 1.
                        if (element > 1)
                        {
                            count++;
                            break; //Exit the loop to save time.
                        }
                    }

                    //No need to keep going if we found a single un-normalized value.
                    if (count > 0)
                        break;
                }

                //If even 1 value isn't less than 1, the data isn't normalized. Prompt the user.
                if (count > 0)
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

            //Invalidate this step if there are any errors in the attributes or k value.
            if (errorProviderAttributes.HasErrors() || errorProviderK.HasErrors())
                valid = false;

            //There are errors, so we DO NOT move onto the initialDataStep2_Validated method. Instead we tell the user to fix their issues.
            if (valid == false)
            {
                //Setting it to currentStep - 1 because after this method call the step will be incremented. (Out of my control).
                baseControl.CurrentStepIndex = 0;
                e.Cancel = true;
            }

            //If there are no errors, then we move onto the initialDataStep2_Validated method.
            else if (valid == true)
                initialDataStep2_Validated(sender, e);
        }

        /// <summary>
        /// This method runs only after the data in step 2 (initial data step) has been successfully validated. This is where we actually 
        /// call the compute method for the K-NN algorithm. This method also serves as a sort of initializer for the display data step.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void initialDataStep2_Validated(object sender, EventArgs e)
        {
            int numAttributes = attributeNames.Count;

            /// Case: In the event we compute the KNN (procede successfully to view the data) and then click the "back" option and decide 
            /// to change the information.
            /// Solution: Reset ALL of the values prior to entering new ones.
            normalizedInputSet.Clear(); //Remove all members of the input set.
            normalizedTrainingSet.Clear(); //Remove all members of the training set.
            plotXComboBox.Items.Clear(); //Remove all of the elements in the dropdown list for the x-coordinate
            plotYComboBox.Items.Clear(); //Remove all of the elements in the dropdown list for the y-coordinates.
            plotXComboBox.ResetText(); //Set the text back to blank.
            plotYComboBox.ResetText(); //Set the text back to blank.
            plotXComboBox.SelectedIndex = -1; //Set the selected index to the default index.
            plotYComboBox.SelectedIndex = -1; //Set the selected index to the default index.
            showKDistancesRadioButton.Checked = false; //Reset the k distances radio button.
            showAllDistancesRadioButton.Checked = false; //Reset the ALL distances radio button.

            closestCompetitorNameLabel.Text = "N/A"; //Set the name label back to default.
            closestCompetitorClassLabel.Text = "N/A"; //Set the class name label back to default.
            closestCompetitorDistanceLabel.Text = "N/A"; //Set the distance label back to default.

            // The input data has been normalized by the user AND we validated that it *appeared* to be normalized.
            if(dontNormalizeInputDataCheckBox.Checked == true)
                normalizedInputSet = inputSet;

            //We need to normalize the input data.
            if (dontNormalizeInputDataCheckBox.Checked == false)
            {
                //Add the normalized inputs to the normalizedInputs list.
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

            //We don't need to normalize the training data.
            if (dontNormalizeTrainingDataCheckBox.Checked == true)
                normalizedTrainingSet = trainingSet;

            //We need to normalize the training data.
            else if (dontNormalizeTrainingDataCheckBox.Checked == false)
                normalizedTrainingSet = NormalizeData.Normalize(trainingSet, inputSet, numAttributes);

            //Set the validated k-value in the textbox to be our k.
            k = Convert.ToInt32(kValueTB.Text);

            //Initialize our KNN object with the chosen k-value and our training set with its classes.
            knn = new KNearestNeighborAlgorithm(Convert.ToInt32(kValueTB.Text), trainingData: trainingSet, outputs: outputClass);

            //We classify our input with relative to the normalized input and training set and the k-value.
            inputClass = knn.Compute(normalizedInputSet, normalizedTrainingSet);

            //Show the closest competitor
            closestCompetitor.Text = Convert.ToString(outputClassNames.ElementAt(inputClass));

            //Add the dropdown choices for plotting attributes.
            foreach (var element in attributeNames)
            {
                plotXComboBox.Items.Add(element);
                plotYComboBox.Items.Add(element);
            }
        }

        #endregion

        #region Data Display Step

        /// <summary>
        /// Runs validation and actually plots the points on the graph when the plot button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void plotButton_Click(object sender, EventArgs e)
        {
            //Remove errors (if any) so we can revalidate during the next iteration.
            DataValidation.RemoveProviderErrors(errorProviderPlot);

            //We assume there aren't any errors in the plot.
            bool valid = true;

            //Clear previous data (if any) from the canvas.
            foreach (var series in chart1.Series)
                series.Points.Clear();

            int xCoord = plotXComboBox.SelectedIndex;
            int yCoord = plotYComboBox.SelectedIndex;

            DataValidation.ValidateCoordinates(errorProviderPlot, xCoord, yCoord, plotXComboBox, plotYComboBox);

            //If the user selected incorrect values in the PlotX and PlotY dropdowns (or any error involving the selection).
            if (errorProviderPlot.HasErrors())
                valid = false;

            //Go ahead and plot everything, there are no errors.
            if (valid == true)
            {
                bool returnKDistances = false; //Flag to check if the user wants to show the distances.
                bool showAllDistances = false; //Flag to check if the user wants to show ALL distances.
                int showDistanceKValue = 0; //Value we pass into the plot to plot k values (could be some or all).

                //User wants to display the k distances
                if (showKDistancesRadioButton.Checked == true)
                {
                    returnKDistances = true;
                    showDistanceKValue = k; //Will only send k points.
                }

                //User wants to display all the distances.
                else if(showAllDistancesRadioButton.Checked == true)
                {
                    returnKDistances = true;
                    showAllDistances = true;
                    showDistanceKValue = trainingSet.Count; //Will send every point.
                }

                //Find the closest competitor given the two chosen attributes.
                List<List<Object>> closestCompetitor = knn.FindNearestCompetitor(normalizedInputSet, normalizedTrainingSet, xCoord, yCoord, returnKDistances, showDistanceKValue);

                //Set the outputs for the closest competitor given the two attributes.
                var closestCompetitorNameIndex = (int)closestCompetitor[0][0];
                var closestCompetitorClassIndex = (int)closestCompetitor[0][1];
                var closestCompetitorDistance = closestCompetitor[0][2];

                closestCompetitorNameLabel.Text = trainingSetLabel[closestCompetitorNameIndex];
                closestCompetitorClassLabel.Text = Convert.ToString(outputClassNames[closestCompetitorClassIndex]);
                closestCompetitorDistanceLabel.Text = StringExtensions.Truncate(Convert.ToString(closestCompetitorDistance), 8);

                var listOfDistances = knn.getclosestCompetitorDistances();

                //Now remove that first input (we don't need it anymore).
                closestCompetitor.RemoveAt(0);

                //Plot the graph given our parameters.
                Plot.PlotPoints(chart1, xCoord, yCoord, normalizedTrainingSet, normalizedInputSet, outputClass, outputClassNames, attributeNames, trainingSetLabel, returnKDistances, listOfDistances, showDistanceKValue, showAllDistances);

                //Display the graph.
                chart1.Show();
            }
        }
        #endregion
    }
}