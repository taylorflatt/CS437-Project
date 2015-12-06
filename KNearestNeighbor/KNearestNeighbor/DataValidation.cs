using System;
using System.Linq;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace KNearestNeighbor
{
    public static class DataValidation
    {
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

        public static void ValidateKValue(this ErrorProvider ep, string value, TextBox textbox, List<List<double>> trainingData)
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

            //If the value typed is out of bounds (k-value is larger than our training set size), we throw an error.
            else if (Convert.ToInt32(value) > trainingData.Count)
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

        public static void ValidateAttributes(this ErrorProvider ep, string value, TextBox textbox, bool discreteValue)
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

    }

}
