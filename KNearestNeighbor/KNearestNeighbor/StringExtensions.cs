using System.Windows.Forms;
using System.Drawing;
using System.Text.RegularExpressions;

namespace KNearestNeighbor
{
    public static class StringExtensions
    {
        /// <summary>
        /// Truncates a given string to a certain length.
        /// </summary>
        /// <param name="value">The string that is to be truncated.</param>
        /// <param name="maxLength">The maximum lenght of the string.</param>
        /// <returns>A truncated version of the input value string.</returns>
        public static string Truncate(this string value, int maxLength)
        {
            //If there is nothing there, just return the value. Nothing to truncate.
            if (string.IsNullOrEmpty(value))
                return value;

            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }

        /// <summary>
        /// Parses text based on several key phrases using regex.
        /// 
        /// All values are enclosed in <> </> brackets.
        /// 
        /// Phrases:
        ///     strong - Will bold text.
        ///     em - Will italicize text.
        ///     u - Will underline text.
        ///     h2 - Will increase the size of the font to 24 (default h2 font size).
        ///     h3 - Will increase the size of the font to 18 (default h3 font size).
        ///     li - Will start a list.
        ///     ul - Will create an un-ordered list with square bullets.
        ///     ol - Will create an ordered list with numbers starting at orderCount.
        /// </summary>
        /// <param name="richTB">The location of the text.</param>
        /// <param name="line">The particular line of text to be parsed.</param>
        /// <param name="orderCount">The starting order number for lists.</param>
        /// <param name="newCount">The resulting order number for the last list created.</param>
        /// <param name="fontSize">The font size for the text.</param>
        /// <param name="fontType">The font type for the text.</param>
        public static void ParseLine(RichTextBox richTB, string line, int orderCount, ref int newCount, int fontSize, string fontType)
        {
            Regex r = new Regex("(\\<strong\\>)|(\\<\\/strong\\>)|(\\<em\\>)|(\\<\\/em\\>)|(\\<li\\>)|(\\<\\/li\\>)|(\\<ul\\>)|(\\<\\/ul\\>)|(\\<ol\\>)|(\\<\\/ol\\>)|(\\<h2\\>)|(\\<\\/h2\\>)|(\\<u\\>)|(\\<\\/u\\>)|(\\<h3\\>)|(\\<\\/h3\\>)");
            string[] tokens = r.Split(line);

            // Set the tokens default color and font. 
            richTB.SelectionColor = Color.Black;

            Font defaultFont = new Font(fontType, fontSize, FontStyle.Regular);
            richTB.SelectionFont = defaultFont;

            bool constructingUnrderedList = false;
            bool constructingOrderedList = false;

            // Check whether the token is a keyword. 
            string bold = "<strong>";
            string endBold = "</strong>";

            string italics = "<em>";
            string endItalics = "</em>";

            string underline = "<u>";
            string endUnderline = "</u>";

            string h2Start = "<h2>";
            string h2End = "</h2>";

            string h3Start = "<h3>";
            string h3End = "</h3>";

            string listStart = "<li>";
            string listEnd = "</li>";

            string listItemUnordered = "<ul>";
            string listItemEndUnordered = "</ul>";

            string listItemOrdered = "<ol>";
            string listItemEndOrdered = "</ol>";

            foreach (string token in tokens)
            {
                bool isValid = true; //Allows for the first character in the line to be null and assists in handling the beeping problem.

                //<strong></strong>
                if (bold == token)
                    richTB.SelectionFont = new Font(fontType, fontSize, FontStyle.Bold);

                else if (endBold == token)
                    richTB.SelectionFont = defaultFont;

                //<em></em>
                else if (italics == token)
                    richTB.SelectionFont = new Font(fontType, fontSize, FontStyle.Italic);

                else if (endItalics == token)
                    richTB.SelectionFont = defaultFont;

                //<u></u>
                else if (underline == token)
                    richTB.SelectionFont = new Font(fontType, fontSize, FontStyle.Underline);

                else if (endUnderline == token)
                    richTB.SelectionFont = defaultFont;

                //<h2></h2>
                else if (h2Start == token)
                    richTB.SelectionFont = new Font(fontType, 18, FontStyle.Regular);

                else if (h2End == token)
                    richTB.SelectionFont = defaultFont;

                //<h3></h3>
                else if (h3Start == token)
                    richTB.SelectionFont = new Font(fontType, 18, FontStyle.Regular);

                else if (h3End == token)
                    richTB.SelectionFont = defaultFont;

                //Maintenance
                else if (listStart == token)
                    newCount = 0; //Reset numbering

                else if (listEnd == token)
                    newCount = 0; //Reset numbering

                //<ul></ul>
                else if (listItemUnordered == token)
                    constructingUnrderedList = true;

                else if (listItemEndUnordered == token)
                    constructingUnrderedList = false;

                else if (constructingUnrderedList == true && listItemUnordered != token)
                    richTB.SelectedText = "\u25A0 " + token;

                //<ol></ol>
                else if (listItemOrdered == token)
                    constructingOrderedList = true;

                else if (listItemEndOrdered == token)
                    constructingOrderedList = false;

                else if (constructingOrderedList == true && listItemOrdered != token)
                    richTB.SelectedText = orderCount + ") " + token;

                //This statement is required else there will be a beep sound heard each time an empty value is added to the textbox.
                else if (token.Equals(""))
                    isValid = false;

                else if(isValid == true)
                    richTB.SelectedText = token;
            }

            //richTB.SelectedText = "\n"; //Generous spacing between elements. (Recommend to remain commented out).
        }
    }   
}