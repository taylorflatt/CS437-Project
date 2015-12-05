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

        protected List<List<double>> trainingSet = new List<List<double>>(); //create jagged list.
        protected List<double> outputClass = new List<double>(); //Actual computational set.
        protected List<string> trainingSetLabel = new List<string>();
        protected List<string> outputClassNames = new List<string>(); //Name we read from the data. (Assume this is first).

        public Wizard()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = openFileDialog1.ShowDialog();

            if(result == DialogResult.OK)
            {
                try
                {
                    XSSFWorkbook xssfwb;
                    using (FileStream file = new FileStream(openFileDialog1.FileName, FileMode.Open, FileAccess.Read))
                    {
                        xssfwb = new XSSFWorkbook(file);
                    }

                    ISheet sheet = xssfwb.GetSheet("Sheet1");
                    for (int row = 0; row <= sheet.LastRowNum; row++)
                    {
                        if (sheet.GetRow(row) != null) //null is when the row only contains empty cells 
                        {
                            outputClass.Add(Convert.ToDouble(sheet.GetRow(row).GetCell(0).NumericCellValue));
                            trainingSetLabel.Add(Convert.ToString(sheet.GetRow(row).GetCell(1).StringCellValue));

                            int count = 2;
                            List<double> temp = new List<double>();

                            while (sheet.GetRow(row) != null)
                            {
                                temp.Add(Convert.ToDouble(sheet.GetRow(row).GetCell(count).NumericCellValue));
                            }

                            trainingSet.Add(temp);
                        }
                    }
                }

                catch(IOException)
                {

                }
            }
        }

        //Need to do some sort of validation before "Next" that the number of classes isn't greater than the number of inputs.
        //Or zero.
        private void numClassesTB_TextChanged(object sender, EventArgs e)
        {
            int value = Convert.ToInt32(numClassesTB.Text);

            try
            {
                //Add values starting from zero to value - 1 to the outputClass.
                for(int count = 0; count <= value; count++)
                {
                    outputClass.Add(count);
                }
            }

            catch{ }
        }
    }
}