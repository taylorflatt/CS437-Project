using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace KNearestNeighbor
{
    public static class DataValidation
    {
        /// <summary>
        /// This method validates the k-value that was entered.
        ///
        /// Input criteria:
        ///     -Must be a natural number.
        ///     -Cannot be larger than the number of data points in the training set.
        /// </summary>
        /// <param name="ep">The error provider handling the error.</param>
        /// <param name="value">The value that was entered into the textbox.</param>
        /// <param name="textbox">The textbox that is being validated and where the error will display.</param>
        /// <param name="trainingData">The set of training data.</param>
        //public static void ValidateKValue(this ErrorProvider ep, CancelEventArgs e, string value, TextBox textbox, List<List<double>> trainingData)
        //{
        //    //The input contained something other than a number.
        //    if (Regex.Matches(value, @"[a-zA-Z\D]").Count > 0)
        //    {
        //        ep.SetError(textbox, "K-Value cannot contain letters.");
        //        e.Cancel = true;
        //    }


        //    //There was nothing typed.
        //    else if (value.Count() == 0 || Regex.Matches(value, @"[0-9]").Count == 0)
        //    {
        //        ep.SetError(textbox, "K-Value must contain a number.");
        //        e.Cancel = true;
        //    }

        //    //A negative number was entered or just a zero.
        //    else if (value.Contains("-") || (value.Length == 1 && value.Contains("0")))
        //    {
        //        ep.SetError(textbox, "K-Value must contain a positive number greater than zero.");
        //        e.Cancel = true;
        //    }

        //    //The value entered was valid in type but not in size. It was larger than the total number of training inputs.
        //    else if (Convert.ToInt32(value) > trainingData.Count)
        //    {
        //        ep.SetError(textbox, "K-Value must be less than the number of training set inputs");
        //        e.Cancel = true;
        //    }
        //}

        /// <summary>
        /// This method validates the attribute that was entered.
        ///
        /// Input criteria:
        ///     -Must be a positive real number.
        ///
        /// If discreteValue flag is true, then we should allow letter inputs. But that functionality hasn't be added yet. We
        /// would have to populate boxes to determine how to quantify (and normalize) the discrete values.
        /// </summary>
        /// <param name="ep">The error provider handling the error.</param>
        /// <param name="value">The value that was entered into the textbox.</param>
        /// <param name="textbox">The textbox that is being validated and where the error will display.</param>
        /// <param name="discreteValue">Flag to check if we allow discrete data. This flag has no function yet.</param>
        //public static void ValidateAttributes(this ErrorProvider ep, CancelEventArgs e, string value, TextBox textbox, bool discreteValue)
        //{
        //    //A symbol (apart from the decimal) or letter was entered.
        //    if (discreteValue == false && Regex.Matches(value, "[-a-zA-Z/!$%^&*()_+|~=`{}\\[\\]:\"; '<>?,\\/]").Count > 0)
        //    {
        //        ep.SetError(textbox, "This attribute cannot contain letters or symbols apart from a decimal point.");
        //        e.Cancel = true;
        //    }

        //    //There was nothing typed.
        //    else if (value.Count() == 0 || Regex.Matches(value, @"[0-9]").Count == 0)
        //    {
        //        ep.SetError(textbox, "This attribute must contain a number.");
        //        e.Cancel = true;
        //    }

        //    //A negative number was entered.
        //    else if (value.Contains("-"))
        //    {
        //        ep.SetError(textbox, "This attribute must contain a positive number greater than zero.");
        //        e.Cancel = true;
        //    }
        //}

        /// <summary>
        /// Validates the dropdown boxes for plotting coordinates on the graph.
        ///
        /// Input criteria:
        ///     -There must be an x-coordinate and y-coordinate chosen.
        ///     -The x-coordinate and y-coordinate must not be the same.
        /// </summary>
        /// <param name="ep">The error provider handling the error.</param>
        /// <param name="xCoord">The index of the chosen x-coordinate. It corresponds to a specific attribute index.</param>
        /// <param name="yCoord">The index of the chosen y-coordinate. It corresponds to a specific attribute index.</param>
        /// <param name="comboboxX">The combo box that is being validated and where the error will display.</param>
        /// <param name="comboboxY">The combo box that is being validated and where the error will display.</param>
        //public static void ValidateCoordinates(this ErrorProvider ep, int xCoord, int yCoord, ComboBox comboboxX, ComboBox comboboxY)
        //{
        //    //The x-coordinate and y-coordinate inputs are identical.
        //    if (xCoord.Equals(yCoord) || yCoord.Equals(xCoord))
        //    {
        //        ep.SetError(comboboxX, "You need to pick two different attributes to plot.");
        //        ep.SetError(comboboxY, "You need to pick two different attributes to plot.");
        //        return;
        //    }

        //    //No x-coordinate was chosen.
        //    else if (xCoord == -1)
        //    {
        //        ep.SetError(comboboxX, "You need to select an attribute to plot. ");
        //        return;
        //    }

        //    //No y-coordinate was chosen.
        //    else if (yCoord == -1)
        //    {
        //        ep.SetError(comboboxY, "You need to select an attribute to plot. ");
        //        return;
        //    }
        //}

        /// <summary>
        /// Removes the errors of a specific error provider so that validation may occur as many times as needed.
        /// </summary>
        /// <param name="ep">The error provider handling the error.</param>
        //public static void RemoveProviderErrors(this ErrorProvider ep)
        //{
        //    ep.RemoveErrors();
        //    ep.Clear();
        //}
    }
}