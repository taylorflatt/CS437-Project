using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.IO;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Formula.Functions;

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

        public Wizard()
        {
            InitializeComponent();

            //Don't let them manipulate the training data until it has been entered.
            kValueTB.Enabled = false;
            dontNormalizeInputDataCheckBox.Enabled = false;
            dontNormalizeTrainingDataCheckBox.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = openFileDialog1.ShowDialog();

                if (result == DialogResult.OK)
                {
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
                            outputClassNames.Add(Convert.ToString(sheet.GetRow(row).GetCell(0).StringCellValue));

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
                    wizardControl2.NextButtonEnabled = true;
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

                //dataGridView1.AutoGenerateColumns = false; //Remove all of the junk columns.

                //DataSet testDataSet = new DataSet("Test");
                //DataTable table = testDataSet.Tables.Add("TrainingData");

                //DataColumn makeColumn = new DataColumn("Make");
                //DataColumn modelColumn = new DataColumn("Model");
                //table.Columns.Add(makeColumn);
                //table.Columns.Add(modelColumn);

                ////dataGridView1.Columns.Add("Make", "Make"); //class
                ////dataGridView1.Columns.Add("Model", "Model"); //attribute label

                //foreach (var element in attributeNames)
                //{
                //    DataColumn attributeColumn = new DataColumn(element.ToString());
                //    table.Columns.Add(attributeColumn);                
                //    //dataGridView1.Columns.Add(element.ToString(), element.ToString());
                //}

                ////make sure there are rows there.
                //for(int index = 0; index < trainingSet.Count; index++)
                //{
                //    table.Rows.Add();
                //}

                //for (int row = 0; row < trainingSet.Count; row++)
                //{
                //    DataRow temp = table.NewRow();

                //    temp[makeColumn] = outputClassNames[row].ToString(); //Make
                //    temp[modelColumn] = trainingSetLabel[row].ToString(); //Model

                //    int column = 2; //set it to position 2 (past make/model)
                //    while (column < attributeNames.Count + 2)
                //    {

                //        temp[column] = trainingSet[row][column - 2].ToString(); //Attributes
                //        column++;
                //    }

                //    column = 0; //reset to zero.

                //    //table.Rows.Add(temp.ItemArray);

                //    table.Rows[row].BeginEdit();

                //    //maybe try modifying the existing rows?
                //    //Didn't work. 
                //    for(int count = 0; count < temp.ItemArray.Count(); count++)
                //        table.Rows[row].SetField(count, temp.ItemArray[count]);

                //    table.Rows[row].EndEdit();

                //    //testDataSet.Tables[0].ImportRow(temp);
                //}

                ////dataGridView1.DataSource = table; //Set the DGV to be our custom data set.
                //dataGridView1.DataSource = dataSet1;
            }

        private void populateAttributeList()
        {
            tableLayoutPanel1.Controls.Clear(); //If they keep changing the value then we should remove the current rows.

            int numAttributes = attributeNames.Count;

            try
            {
                //Add values starting from zero to value - 1 to the outputClass.
                int column = 2;
                for (int count = 1; count <= numAttributes; count++)
                {
                    int row = count;
                    outputClass.Add(count - 1);

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
            int numAttributes = attributeNames.Count;

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

            if(errorProviderK.HasErrors())
            {
                //Stop from being able to select "next".
                e.Cancel = true;
            }

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
                        e.Cancel = true;
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
                        e.Cancel = true;
                }
            }

            //If there are errors, stop from proceeding.
            if (errorProviderAttributes.HasErrors() == true)
            {
                //Stop from being able to select "next".
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
            {
                normalizedInputSet = inputSet;
            }
            

            if(dontNormalizeTrainingDataCheckBox.Checked == false)
            {
                normalizedTrainingSet = NormalizeData.Normalize(trainingSet, numAttributes);
            }

            else
            {
                normalizedTrainingSet = trainingSet;
            }

            

            //initialize our KNN object.
            knn = new KNearestNeighborAlgorithm(k, inputs: trainingSet, outputs: outputClass); //initialize our algorithm with inputs

            int answer = knn.Compute(inputSet);

            label14.Text = Convert.ToString(answer);

            
        }

        private void wizardControl2_CurrentStepIndexChanged(object sender, EventArgs e)
        {
            if (wizardControl2.CurrentStepIndex == 2)
            {
                wizardControl2.BackButtonEnabled = false;
            }

            if (wizardControl2.CurrentStepIndex == 1)
            {
                wizardControl2.NextButtonEnabled = false;
            }
        }
    }
}