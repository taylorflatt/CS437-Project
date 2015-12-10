using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace KNearestNeighbor
{
    internal class DataReader
    {
        private OpenFileDialog fileName;
        private Label fileDisplayLabel;
        private List<string> trainingDataName;
        private List<string> attributeName;
        private List<string> outputClassName;
        private List<int> outputClass;
        private List<List<double>> trainingSet;
        private DataGridView dgv;
        private TableLayoutPanel tablePanel;

        public DataReader(OpenFileDialog fileName, Label fileDisplayLabel, List<string> trainingDataName, List<string> attributeName, List<string> outputClassName, List<int> outputClass, List<List<double>> trainingSet, DataGridView dataGridView, TableLayoutPanel tablePanel)
        {
            this.fileName = fileName;
            this.fileDisplayLabel = fileDisplayLabel;
            this.trainingDataName = trainingDataName;
            this.attributeName = attributeName;
            this.outputClassName = outputClassName;
            this.outputClass = outputClass;
            this.trainingSet = trainingSet;
            this.dgv = dataGridView;
            this.tablePanel = tablePanel;
        }

        /// <summary>
        /// Return the data from the *.XLSX file.
        /// </summary>
        public void GetXlsxData()
        {
            readXlsxFile();
        }

        /// <summary>
        /// Return the data from the *.XLS file.
        /// </summary>
        public void GetXlsData()
        {
            readXlsFile();
        }

        /// <summary>
        /// This method will read in the data from a file of type XLS. (Excel 2003).
        /// </summary>
        private void readXlsFile()
        {
            HSSFWorkbook hssfwb;

            //Read the file they have chosen.
            using (FileStream file = new FileStream(fileName.FileName, FileMode.Open, FileAccess.Read))
            {
                hssfwb = new HSSFWorkbook(file);
                fileDisplayLabel.Text = fileName.SafeFileName;
            }

            //We default to the first sheet always.
            ISheet sheet = hssfwb.GetSheet("Sheet1");

            //Add the rows to the sheet.
            AddRow(sheet);

            //Now actually display the DGV with our data.
            populateDataGrid(sheet);

            //Now display the list of attributes dynamically.
            populateAttributeList();
        }

        /// <summary>
        /// This method will read in the data from a file of type XLSX. (Excel 2007, 2013).
        /// </summary>
        private void readXlsxFile()
        {
            XSSFWorkbook xssfwb;

            //Read the file they have chosen.
            using (FileStream file = new FileStream(fileName.FileName, FileMode.Open, FileAccess.Read))
            {
                xssfwb = new XSSFWorkbook(file);
                fileDisplayLabel.Text = fileName.SafeFileName;
            }

            //We default to the first sheet always.
            ISheet sheet = xssfwb.GetSheet("Sheet1");

            //Add the rows to the sheet.
            AddRow(sheet);

            //Now actually display the DGV with our data.
            populateDataGrid(sheet);

            //Now display the list of attributes dynamically.
            populateAttributeList();
        }

        /// <summary>
        /// Populates the rows of the table.
        /// </summary>
        /// <param name="sheet">The particular data sheet that we are focused on.</param>
        private void AddRow(ISheet sheet)
        {
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
                        attributeName.Add(Convert.ToString(sheet.GetRow(row).GetCell(location).StringCellValue));
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
                    if (outputClassName.Contains(className) == false)
                        outputClassName.Add(className);

                    //Now I need to check the outputClassNames with the currentClassName and add the appropriate class number.
                    if (outputClassName.Contains(className) == true)
                        outputClass.Add(outputClassName.IndexOf(className));

                    /// Assumption 2: The training data label will always be the second value in a row.
                    trainingDataName.Add(Convert.ToString(sheet.GetRow(row).GetCell(1).StringCellValue));

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
        }

        /// <summary>
        /// This will generate the DGV information using an excel spreadsheet.
        /// </summary>
        /// <param name="xssfwb">The particular excel file you would like to load.</param>
        /// <param name="sheet">The sheet you wish to read into the program.</param>
        private void populateDataGrid(ISheet sheet)
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

            dgv.DataSource = table; //Set the source of the DGV to be the table we created.
        }

        /// <summary>
        /// This method will generate the attribute textboxes and their labels dynamically based on the training data.
        /// This is a vestigial method that won't be called unless I fix the text location problem.
        /// </summary>
        private void populateAttributeList()
        {
            tablePanel.Controls.Clear(); //If they keep changing the value then we should remove the current rows.

            int numAttributes = attributeName.Count;

            //Add values starting from zero to value - 1.
            int column = 2;
            for (int count = 1; count <= numAttributes; count++)
            {
                int row = count;

                System.Windows.Forms.Label temp = new System.Windows.Forms.Label();
                temp.Text = attributeName.ElementAt(count - 1).ToString() + ": ";

                tablePanel.Controls.Add(temp, 0, row);

                //I need to be explicit and set a name so I can reference the fields later.
                System.Windows.Forms.TextBox tempTB = new System.Windows.Forms.TextBox();
                tempTB.Name = "attribute" + count + "TB"; //attribute1TB, attribute2TB...

                tablePanel.Controls.Add(tempTB, column, row);
            }
        }
    }
}