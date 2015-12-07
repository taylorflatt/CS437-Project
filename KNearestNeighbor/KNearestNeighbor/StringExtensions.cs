using System.Windows.Forms;
using System.Drawing;
using System.Text.RegularExpressions;

namespace KNearestNeighbor
{
    public static class StringExtensions
    {
        public static string Truncate(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }

        public static void ParseLine(RichTextBox richTB, string line, int orderCount, ref int newCount, int fontSize, string fontType)
        {
            Regex r = new Regex("(\\<strong\\>)|(\\<\\/strong\\>)|(\\<em\\>)|(\\<\\/em\\>)|(\\<li\\>)|(\\<\\/li\\>)|(\\<ul\\>)|(\\<\\/ul\\>)|(\\<ol\\>)|(\\<\\/ol\\>)|(\\<h2\\>)|(\\<\\/h2\\>)|(\\<u\\>)|(\\<\\/u\\>)");
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

            string listStart = "<li>";
            string listEnd = "</li>";

            string listItemUnordered = "<ul>";
            string listItemEndUnordered = "</ul>";

            string listItemOrdered = "<ol>";
            string listItemEndOrdered = "</ol>";

            

            foreach (string token in tokens)
            {
                bool isValid = true; //Allows for the first character in the line to be null and assists in handling the beeping problem.

                if (bold == token)
                    richTB.SelectionFont = new Font(fontType, fontSize, FontStyle.Bold);

                else if (endBold == token)
                    richTB.SelectionFont = defaultFont;

                else if (italics == token)
                    richTB.SelectionFont = new Font(fontType, fontSize, FontStyle.Italic);

                else if (endItalics == token)
                    richTB.SelectionFont = defaultFont;

                else if (underline == token)
                    richTB.SelectionFont = new Font(fontType, fontSize, FontStyle.Underline);

                else if (endUnderline == token)
                    richTB.SelectionFont = defaultFont;

                else if (h2Start == token)
                    richTB.SelectionFont = new Font(fontType, 24, FontStyle.Regular);

                else if (h2End == token)
                    richTB.SelectionFont = defaultFont;

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

            richTB.SelectedText = "\n";
        }
    }   
}