using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace KNearestNeighbor
{
    public partial class Wizard : Form
    {
        #region Instance Variables

        protected int k;
        private KNearestNeighborAlgorithm knn;

        protected List<List<double>> trainingSet = new List<List<double>>(); //Training Set
        protected List<int> outputClass = new List<int>(); //Class (output) Set
        protected List<double> inputSet = new List<double>(); //User Input Set.
        protected int userOutputClass; //User's class after computing KNN.

        protected List<List<double>> normalizedTrainingSet = new List<List<double>>(); //Normalized Training Set
        protected List<double> normalizedInputSet = new List<double>(); //Normalized Input Set

        protected List<string> trainingDataName = new List<string>(); //The name of the data point.
        protected List<string> outputClassNames = new List<string>(); //Name we read from the data. (Assume this is first).
        protected List<string> attributeNames = new List<string>(); //without make/model

        protected int inputClass; //Classification of the input data.

        #endregion Instance Variables

        #region Main Class

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

            //Display the description on the first step using a document resource.
            displayProgramDescription(programDescriptionTB);

            displayDataStep3.Subtitle = "View the classification for your input on the left. You may plot the graph to view the training data and input data along with the input's closest competitor with the given two attributes. You may also opt to show the distance calculations on the point labels as well.";

            //Don't let them manipulate the training data until it has been entered.
            kValueTB.Enabled = false;
            dontNormalizeInputDataCheckBox.Enabled = false;
            dontNormalizeTrainingDataCheckBox.Enabled = false;
        }

        #endregion Main Class

        #region Helper Methods

        /// <summary>
        /// Resets the normalized input set, normalized training set, plot-x dropdown list items, plot-x dropdown selected text, plot-x
        /// selected value, plot-y dropdown list items, plot-y dropdown selected text, plot-y selected value, unchecks the k-distances radio
        /// button, unchecks the all distances radio button, closest competitior (with respect to the attributes chosen - not the overall
        /// (input class) labels.
        /// </summary>
        private void ResetPlottedData()
        {
            normalizedInputSet.Clear(); //Remove all members of the input set. (only normalized since that is what we graph)
            normalizedTrainingSet.Clear(); //Remove all members of the training set. (only normalized since that is what we graph)
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
        }

        /// <summary>
        /// Resets the input set, training set, normalized input set, normalized training set, output class, and output class names.
        /// </summary>
        private void ResetData()
        {
            inputSet.Clear();
            trainingSet.Clear();
            normalizedInputSet.Clear();
            normalizedTrainingSet.Clear();
            outputClassNames.Clear();
            outputClass.Clear();
        }

        #endregion Helper Methods

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
                chart1.Series.Clear();

                /// We are renaming the cancel button to NEW and its function is to restart the program.
                baseControl.CancelButtonEnabled = true;
                baseControl.CancelButtonText = "New";
                baseControl.CancelButtonVisible = true;
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
                string helpTopic = @"/html/KNN Algorithm/knnIndex.htm";
                int currentStep = this.baseControl.CurrentStepIndex;

                //Program Start Page.
                if (currentStep == 0)
                    helpTopic = @"/html/KNN Program/programoverview.htm";

                //Data Initialize Page.
                else if (currentStep == 1)
                    helpTopic = @"/html/KNN Program/datainputpage.htm";

                //Data Display Page.
                else if (currentStep == 2)
                    helpTopic = @"/html/KNN Program/dataoutputpage.htm";

                //System.Diagnostics.Process.Start("knnHelp.CHM");
                Help.ShowHelp(this, "knnHelp.CHM", HelpNavigator.Topic, helpTopic);
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

        #endregion Global Methods

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
        private void programDescriptionTB_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        #endregion Description Step

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
                    /// In the event that this method is called multiple times (they attempt to enter multiple documents at different times)
                    /// then we need to remove the previous data. We can just re-add the data from the new file chosen by the user.
                    attributeNames.Clear(); //Clear attribute names list
                    kValueTB.Clear(); //Clear the k value.

                    //Clear our data sets as well.
                    ResetData();

                    //TODO: Add support for *.ODS files (Libre Calc).

                    DataReader reader = new DataReader(openFileDialog1, fileLocationLabel, trainingDataName, attributeNames, outputClassNames, outputClass, trainingSet, dataGridView1, tableLayoutPanel1);

                    //If the file is an excel file with the *.xlsx format, then we need to read it using XSSF.
                    if (Path.GetExtension(openFileDialog1.FileName).Equals(".xlsx", StringComparison.InvariantCultureIgnoreCase))
                        reader.GetXlsxData();

                    //If the file is an excel file (2003) with the *.xls format, then we need to read it using HSSF.
                    else if (Path.GetExtension(openFileDialog1.FileName).Equals(".xls", StringComparison.InvariantCultureIgnoreCase))
                        reader.GetXlsData();

                    //We do not accept any other formats.
                    else
                        throw new ICSharpCode.SharpZipLib.Zip.ZipException();

                    //We give the user our count of the number of attributes.
                    predictAttributeNumLabel.Text = string.Format("We see {0} attributes.", attributeNames.Count);

                    //Now allow them to be able to manipulate the data and attempt to click next.
                    kValueTB.Enabled = true;
                    dontNormalizeInputDataCheckBox.Enabled = true;
                    dontNormalizeTrainingDataCheckBox.Enabled = true;
                    baseControl.NextButtonEnabled = true;
                }
            }

            //Wrong file type error. Or they put in a file with extension .xlsx that isn't an excel file.
            catch (ICSharpCode.SharpZipLib.Zip.ZipException error)
            {
                Console.WriteLine("Either the file extension wasn't acceptable or it was the correct file extension but not the right file type (if you change a *.docx file type to a *.xlsx file type by just changing the name. It won't work. ");
                Console.WriteLine("Packed Message: " + error.Message);
                Console.WriteLine("Call Stack: " + error.StackTrace);

                DialogResult errorMessage = MessageBox.Show("Your file must be a valid *.xlsx/*.xls/*.odf file type. ",
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

                    //Clear the data sets.
                    ResetData();
                    tableLayoutPanel1.Controls.Clear();

                    //Uncheck the checkboxes if any were checked.
                    dontNormalizeTrainingDataCheckBox.Checked = false;
                    dontNormalizeInputDataCheckBox.Checked = false;

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
        /// Allows the next button to be pressed by hitting the enter key.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void initialDataStep2_KeyPress(object sender, KeyPressEventArgs e)
        {
            //If the enter key is pressed on the data input page, attempt to go to the next step.
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                baseControl.SelectNextControl(displayDataStep3, true, true, true, true);
            }
        }

        /// <summary>
        /// Generates the attribute textboxes dynamically based on the number of attributes read in from the file. It also sets the labels as well.
        /// </summary>
        /// <param name="attributeNumber">The current attribute.</param>
        /// <param name="value">The value of the textbox for the current attribute.</param>
        /// <param name="textBox">The specific textbox name for reference.</param>
        private void GenerateAttributeTextboxes(int attributeNumber, out string value, out System.Windows.Forms.TextBox textBox)
        {
            string textBoxName = "attribute" + attributeNumber + "TB";
            textBox = (System.Windows.Forms.TextBox)this.tableLayoutPanel1.Controls[textBoxName];
            value = this.tableLayoutPanel1.Controls[textBoxName].Text;
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
                string value;
                TextBox textBox;
                GenerateAttributeTextboxes(count, out value, out textBox);

                //Validate the attribute boxes.
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
                    string value;
                    TextBox textBox;
                    GenerateAttributeTextboxes(count, out value, out textBox);

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
            ResetPlottedData();

            // The input data has been normalized by the user AND we validated that it *appeared* to be normalized.
            if (dontNormalizeInputDataCheckBox.Checked == true)
                normalizedInputSet = inputSet;

            //We need to normalize the input data.
            if (dontNormalizeInputDataCheckBox.Checked == false)
            {
                //Add the normalized inputs to the normalizedInputs list.
                for (int count = 1; count <= numAttributes; count++)
                {
                    string value;
                    TextBox textBox;
                    GenerateAttributeTextboxes(count, out value, out textBox);

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

        #endregion Initial Data Step

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
                else if (showAllDistancesRadioButton.Checked == true)
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

                closestCompetitorNameLabel.Text = trainingDataName[closestCompetitorNameIndex];
                closestCompetitorClassLabel.Text = Convert.ToString(outputClassNames[closestCompetitorClassIndex]);
                closestCompetitorDistanceLabel.Text = StringExtensions.Truncate(Convert.ToString(closestCompetitorDistance), 8);

                var listOfDistances = knn.getclosestCompetitorDistances();

                //Now remove that first input (we don't need it anymore).
                closestCompetitor.RemoveAt(0);

                Plot populatedGraph = new Plot(chart1, xCoord, yCoord, normalizedTrainingSet, normalizedInputSet, outputClass, outputClassNames, attributeNames, trainingDataName, returnKDistances, listOfDistances, showDistanceKValue, showAllDistances);

                //Set the chart's parameters.
                populatedGraph.SetChartProperties();

                //Plot the graph given our parameters.
                populatedGraph.PlotPoints();

                //Display the graph.
                chart1.Show();
            }
        }

        #endregion Data Display Step
    }
}