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

        /// <summary>
        /// Allows for the export of the output for computing the k-NN.
        /// </summary>
        /// <param name="kNearestDistances">The list of elements that voted on the class for the inpt.</param>
        /// <param name="allDistances">All of the distances for the training set.</param>
        /// <param name="saveFileDialog">The save dialog object.</param>
        /// <param name="trainingDataFilePath">The open file dialog object for the training data.</param>
        /// <param name="kValue">The chosen k-value.</param>
        /// <param name="trainingSet">The entire training set.</param>
        /// <param name="trainingDataName">The name of the particular data point.</param>
        /// <param name="outputClass">The list of output classes for each data point.</param>
        /// <param name="outputClassNames">The output class names (distinct).</param>
        /// <param name="attributeName">The names of the attributes.</param>
        /// <param name="inputSet">The user input.</param>
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

        /// <summary>
        /// This class will create the *.xls output file with the training data and the output data combined.
        /// </summary>
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

                //Create the file.
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
    }
}