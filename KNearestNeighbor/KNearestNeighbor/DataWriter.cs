using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using NPOI.HSSF.Model;

/// <summary>
/// What I want to do here is take the original data input file that they used to generate the k-NN training set and copy it.
/// Then add my additions to it such as the input data, k-value, etc. Then save that for the user on their computer. I will create 
/// a button that says "Generate Report" and the user can save it anywhere that they wish.
/// </summary>

namespace KNearestNeighbor
{
    class DataWriter
    {
        protected OpenFileDialog trainingDataFilePath;
        protected SaveFileDialog saveFileDialog;
        protected List<List<double>> trainingSet;
        protected List<string> trainingDataName;
        protected List<string> attributeName;
        protected List<string> outputClassName;
        protected List<int> outputClass;
        protected List<double> inputSet;
        protected int kValue;

        protected List<double> kNearestDistances;
        protected List<double> allDistances;

        protected string inputValue;

        private string outputFileName = "knnOutputFile.xls";

        public DataWriter(List<double> kNearestDistances, List<double> allDistances, SaveFileDialog saveFileDialog, OpenFileDialog trainingDataFilePath, int kValue, List<List<double>> trainingSet, List<string> trainingDataName, List<int> outputClass, List<string> outputClassNames, List<string> attributeName, List<double> inputSet)
        {
            this.trainingDataFilePath = trainingDataFilePath;
            this.saveFileDialog = saveFileDialog;
            this.trainingSet = trainingSet;
            this.attributeName = attributeName;
            this.outputClass = outputClass;
            this.outputClassName = outputClassNames;
            this.inputSet = inputSet;
            this.kValue = kValue;
            this.kNearestDistances = kNearestDistances;
            this.allDistances = allDistances;
            this.trainingDataName = trainingDataName;

            inputValue = "{ ";
            bool firstVal = true;

            //Get a one-line input set.
            foreach (var element in inputSet)
            {
                if(firstVal == true)
                {
                    inputValue = inputValue + element.ToString();
                    firstVal = false;
                }

                else
                    inputValue = inputValue + ", " + element.ToString();
            }

            inputValue = inputValue + "}";
        }

        public void CreateOutputXlsFile()
        {
            HSSFWorkbook workbook;
            HSSFSheet sheet;

            // create xls if not exists
            if (!File.Exists(outputFileName))
            {
                workbook = HSSFWorkbook.Create(InternalWorkbook.CreateWorkbook());

                int topRowOffset = 4;

                //provide offset.
                for (int index = 0; index < kNearestDistances.Count; index++)
                {
                    kNearestDistances[index] = kNearestDistances[index] + 4;
                }

                // create sheet
                sheet = (HSSFSheet)workbook.CreateSheet("Sheet1");
                // 3 rows, 2 columns

                //Training set + 4 (because we add the k value in a row, blank row, header row, and the input. So we need to increment by 4.
                for (int row = 0; row < trainingSet.Count + topRowOffset; row++)
                {
                    var r = sheet.CreateRow(row);

                    //Print k value
                    if(row == 0)
                    {
                        r.CreateCell(0);
                        sheet.GetRow(row).GetCell(0).SetCellValue(string.Format("For k = {0}", kValue));
                    }

                    //Set a blank row.
                    else if(row == 1)
                    {
                        r.CreateCell(0);
                        sheet.GetRow(row).GetCell(0).SetCellValue("");
                    }

                    //Add header row
                    else if(row == 2)
                    {
                        //Create columns (0-9)
                        for (int column = 0; column < 10; column++)
                        {
                            r.CreateCell(column);

                            //Class Name
                            if (column == 0)
                                sheet.GetRow(row).GetCell(column).SetCellValue("Class");

                            //Data label
                            else if (column == 1)
                                sheet.GetRow(row).GetCell(column).SetCellValue("Name");

                            //Attribute Names (column - 2 since index of list starts at zero and column will be 2 at this point).
                            else if(column > 1 && column < 8)
                                sheet.GetRow(row).GetCell(column).SetCellValue(attributeName[column - 2]);

                            //Output label 1 (Vote weight)
                            else if (column == 8)
                                sheet.GetRow(row).GetCell(column).SetCellValue("Vote Weight");

                            //Output label 2 (Voted For)
                            else if (column == 9)
                                sheet.GetRow(row).GetCell(column).SetCellValue("Voted For");

                            //Output label 3 (Distance from input)
                            else if (column == 10)
                                sheet.GetRow(row).GetCell(column).SetCellValue("Distance From Input");
                        }
                    }

                    //Add input data
                    else if (row == 3)
                    {
                        //Create columns (0-7)
                        for (int column = 0; column < 8; column++)
                        {
                            r.CreateCell(column);

                            //Class Name
                            if (column == 0)
                                sheet.GetRow(row).GetCell(column).SetCellValue("Input");

                            //Data label
                            else if (column == 1)
                                sheet.GetRow(row).GetCell(column).SetCellValue("N/A");

                            //Input data values (column - 2 since index of list starts at zero and column will be 2 at this point).
                            else if (column > 1 && column < 8)
                                sheet.GetRow(row).GetCell(column).SetCellValue(inputSet[column - 2]);

                            //Output label 1 (Vote weight)
                            else if (column == 8)
                                sheet.GetRow(row).GetCell(column).SetCellValue("N/A");

                            //Output label 2 (Who they voted for)
                            else if (column == 9)
                                sheet.GetRow(row).GetCell(column).SetCellValue("N/A");

                            //Output label 3 (Distance from input)
                            else if (column == 10)
                                sheet.GetRow(row).GetCell(column).SetCellValue("N/A");
                        }
                    }

                    //Data rows.
                    else if(row > 3)
                    {
                        //Create columns (0-9)
                        for (int column = 0; column < 11; column++)
                        {
                            r.CreateCell(column);

                            //Class Name
                            if (column == 0)
                                sheet.GetRow(row).GetCell(column).SetCellValue(outputClassName[outputClass[row - topRowOffset]]);

                            //Data label (column - 1 since index of the list starts at zero and column will be 1 at this point).
                            else if (column == 1)
                                sheet.GetRow(row).GetCell(column).SetCellValue(Convert.ToString(trainingDataName[row - topRowOffset]));

                            //Training data values (column - 2 since index of list starts at zero and column will be 2 at this point).
                            else if (column > 1 && column < 8)
                                sheet.GetRow(row).GetCell(column).SetCellValue(Convert.ToString(trainingSet[row - topRowOffset][column - 2]));

                            //Training data k-nearest distance output (weighted value/how much it mattered).
                            else if (column == 8)
                            {
                                //If there are no more elements that voted.
                                if (kNearestDistances.Count == 0)
                                    sheet.GetRow(row).GetCell(column).SetCellValue(Convert.ToString("Didn't Vote."));

                                else
                                {
                                    for (int index = 0; index < kNearestDistances.Count; index++)
                                    {
                                        int currentTrainingRow = row - 4; //index of the training element

                                        //If the current row is a row that voted, list the voting power.
                                        if (currentTrainingRow == kNearestDistances[index])
                                        {
                                            double scoreSquared = (allDistances[row - topRowOffset]) * (allDistances[row - topRowOffset]);
                                            sheet.GetRow(row).GetCell(column).SetCellValue(Convert.ToString(1 / scoreSquared));
                                            kNearestDistances.RemoveAt(index); //remove the element so we reduce searching.
                                        }
                                    }

                                    //No vote was added, so we need to say they didn't vote.
                                    if(sheet.GetRow(row).GetCell(column).StringCellValue.Equals(""))
                                        sheet.GetRow(row).GetCell(column).SetCellValue(Convert.ToString("Didn't Vote."));
                                }
                            }

                            //Who did the point vote for if they did vote?
                            else if(column == 9)
                            {
                                //If there was a vote cast, give the name of the class it voted for.
                                if(!sheet.GetRow(row).GetCell(8).StringCellValue.Equals("Didn't Vote."))
                                    sheet.GetRow(row).GetCell(column).SetCellValue(outputClassName[outputClass[row - topRowOffset]]);
                            }

                            //Training data distance from output (non-weighted value).
                            else if (column == 10)
                                sheet.GetRow(row).GetCell(column).SetCellValue(Convert.ToString(allDistances[row - topRowOffset]));
                        }
                    }
                }

                using (var fileStream = new FileStream("dataOutput.xls", FileMode.Create, FileAccess.Write))
                {
                    workbook.Write(fileStream);
                }
            }

            else
            {
                //Find a file name that exists.
                for(int index = 1; index < 100; index++)
                {
                    outputFileName = string.Format(outputFileName + "({0})", index);
                    CreateOutputXlsFile();
                }
            }
        }

























        ////Will probably need to pass in the file path/name of the previously selected file.
        //public void CreateOutputXlsFile()
        //{
        //    //Training Data Template.
        //    FileStream fileStream = new FileStream(trainingDataFilePath.FileName, FileMode.Open, FileAccess.Read);

        //    // Getting the complete workbook...
        //    //HSSFWorkbook trainingDataFile = new HSSFWorkbook(fileStream, true);
        //    XSSFWorkbook trainingDataFile = new XSSFWorkbook(fileStream);

        //    // Getting the worksheet by its name...
        //    ISheet sheet = trainingDataFile.GetSheet("Sheet1");

        //    //Here is where you get the rows.

        //    //Rows and columns start at index 0.

        //    var temp = sheet.LastRowNum;
        //    var temp2 = sheet.PhysicalNumberOfRows;

        //    //Let's read all of the rows in the document into our new file.
        //    for (int index = 0; index < sheet.LastRowNum; index++)
        //    {
        //        //Get the row data from the trainingDataFile.
        //        IRow dataRow = sheet.GetRow(index);

        //        //Create the extra cells in the row for our new data.
        //        for (int column = 8; column <= 13; column++)
        //            dataRow.CreateCell(column);

        //        //If it is the first row, add the header text.
        //        if (index == 0)
        //        {
        //            dataRow.CreateCell(8);
        //            //Give a buffer column.
        //            dataRow.GetCell(8).SetCellValue("");
        //            dataRow.GetCell(9).SetCellValue("Input Data Set"); //Input data in a set. {a1, a2, a3, a4, a5}
        //            dataRow.GetCell(10).SetCellValue("k-Value"); //k-Value entered by the user.
        //            dataRow.GetCell(11).SetCellValue("Voted For"); //Class the particular data point voted for
        //            dataRow.GetCell(12).SetCellValue("Vote Weight"); //Blank for now.
        //            dataRow.GetCell(13).SetCellValue("Distance from input"); //The distance the data point is from the input.
        //        }

        //        //Add the input set and k-value (only need to put it once.
        //        else if(index == 1)
        //        {
        //            dataRow.GetCell(9).SetCellValue(inputValue); //Input data in a set. {a1, a2, a3, a4, a5}
        //            dataRow.GetCell(10).SetCellValue(Convert.ToString(kValue)); //k-Value entered by the user.
        //        }

        //        //Otherwise add the data.
        //        else
        //        {
        //            //Now for each row, we want to add onto the ends of them with our output.
        //            //Give a buffer column.
        //            dataRow.GetCell(8).SetCellValue("");
        //            dataRow.GetCell(11).SetCellValue(outputClass[index]); //Class the particular data point voted for

        //            var temp12381 = kClosestDistances;

        //            //Will print the k-closest (the values that voted) with their weights.
        //            for (int count = 0; count < kClosestDistances.Count; count++)
        //                dataRow.GetCell(12).SetCellValue(Convert.ToString(kClosestDistances[count])); //The distance the data point is from the input.

        //            //Will print the normalized distance from the input.
        //            dataRow.GetCell(13).SetCellValue(allDistances[index]);
        //        }
        //    }

        //    //Bug Fix
        //    if(sheet.PhysicalNumberOfRows < 100)
        //    {
        //        for (int index = sheet.PhysicalNumberOfRows + 1; index < 100; index++)
        //        {
        //            sheet.CreateRow(index);

        //            var dataRow = sheet.GetRow(index);

        //            dataRow.CreateCell(0);
        //            dataRow.GetCell(0).SetCellValue("");
        //        }
        //    }

        //    //We will be making the changes in memory and returning the file to the user.
        //    MemoryStream memoryStream = new MemoryStream();

        //    // Writing the workbook content to the FileStream...
        //    trainingDataFile.Write(memoryStream);

        //    //string newFileName = trainingDataFilePath.SafeFileName + "_output.xls";

        //    //string directory  = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().CodeBase);

        //    //string outputFileName = directory + newFileName;

        //    //var systemPath = System.Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);

        //    var systemPath = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

        //    //var outputFileName = Path.Combine(systemPath, "\\output_" + trainingDataFilePath.SafeFileName);

        //    var outputFileName = systemPath + "\\output_" + trainingDataFilePath.SafeFileName;


        //    FileStream outputFile = new FileStream(outputFileName, FileMode.Create);

        //    trainingDataFile.Write(outputFile);
        //}
    }
}