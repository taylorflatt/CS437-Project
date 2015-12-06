using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.OleDb;
using System.Reflection;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
using System.Diagnostics;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;

namespace KNearestNeighbor
{
    public partial class Wizard : Form
    {
        protected int k;
        KNearestNeighborAlgorithm knn;

        protected List<List<double>> trainingSet = new List<List<double>>(); //create jagged list.
        protected List<double> outputClass = new List<double>(); //Actual computational set.
        protected List<string> trainingSetLabel = new List<string>();
        protected List<string> outputClassNames = new List<string>(); //Name we read from the data. (Assume this is first).
        protected List<string> attributeNames = new List<string>(); //without make/model

        public Wizard()
        {
            InitializeComponent();

            //Don't let them manipulate the training data until it has been entered.
            kValueTB.Enabled = false;
            normalizeInputDataCheckBox.Enabled = false;
            normalizeTrainingDataCheckBox.Enabled = false;
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
                    populateDataGrid();
                    populateAttributeList();

                    //Now allow them to be able to manipulate the data.
                    kValueTB.Enabled = true;
                    normalizeInputDataCheckBox.Enabled = true;
                    normalizeTrainingDataCheckBox.Enabled = true;
                }
            }

            catch (ICSharpCode.SharpZipLib.Zip.ZipException)
            {
                DialogResult error = MessageBox.Show("You must use a file with .xlsx extension. ",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error,
                    MessageBoxDefaultButton.Button1);

                if(error == DialogResult.OK)
                    button1_Click(sender, e); //Restart the dialog process.
            }
        }

        private void populateDataGrid()
        {
            dataGridView1.AutoGenerateColumns = false; //Remove all of the junk columns.
            System.Data.DataTable table = new System.Data.DataTable("TrainingData");

            DataColumn makeColumn = new DataColumn("Make");
            DataColumn modelColumn = new DataColumn("Model");
            table.Columns.Add(makeColumn);
            table.Columns.Add(modelColumn);

            //dataGridView1.Columns.Add("Make", "Make"); //class
            //dataGridView1.Columns.Add("Model", "Model"); //attribute label

            foreach (var element in attributeNames)
            {
                DataColumn attributeColumn = new DataColumn(element.ToString());
                table.Columns.Add(attributeColumn);
                //dataGridView1.Columns.Add(element.ToString(), element.ToString());
            }

            for(int row = 0; row < trainingSet.Count; row++)
            {
                DataRow temp = table.NewRow();
                temp.BeginEdit();

                temp["Make"] = outputClassNames.ToString(); //Make
                temp["Model"] = trainingSetLabel.ToString(); //Model

                int column = 2; //set it to position 2 (past make/model)
                while (column < attributeNames.Count + 2)
                {
                    temp[column] = trainingSet[row][column - 2].ToString(); //Attributes
                    column++;
                }

                column = 0; //reset to zero.

                temp.EndEdit();
                table.Rows.Add(temp);
            }

            dataGridView1.DataSource = table; //Set the DGV to be our custom data set.
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
                    tableLayoutPanel1.Controls.Add(new System.Windows.Forms.TextBox(), column, row);
                }
            }

            catch { }
        }

        //Check whether the input data set has been normalized. Handle that situation.
        //Check whether the training data set has been normalized. Handle that situation.
        private void initialDataStep2_Click(object sender, EventArgs e)
        {
            //First check if the data appears normalized already or not. Confirm with the user prior to action.
            //If data IS normalized, then display a message indicating that the data appears normalized already.
            //If data IS NOT normalized, then normalize the data and proceed.
            if(normalizeInputDataCheckBox.Checked)
            {
                //foo
            }
        }

        private void kValueTB_TextChanged(object sender, EventArgs e)
        {
            string kValue = kValueTB.Text;

            DataValidation.ValidateKValue(errorProviderK, kValue, kValueTB, trainingSet);
        }
    }
}