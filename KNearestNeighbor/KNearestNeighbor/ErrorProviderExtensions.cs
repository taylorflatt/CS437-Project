using System.Windows.Forms;

namespace KNearestNeighbor
{
    /// <summary>
    /// Provides a way to handle custom validation using a count system. If the count is greater than 0, there
    /// is an error. If it is zero, there isn't an error.
    /// </summary>
    public static class ErrorProviderExtensions
    {
        private static int count; //Number of errors.

        /// <summary>
        /// Set an error count that holds the total number of errors for a particular error provider.
        /// </summary>
        /// <param name="ep">The error provider handling the error.</param>
        /// <param name="c">The current control.</param>
        /// <param name="message">The message that the error will display.</param>
        public static void SetErrorWithCount(this ErrorProvider ep, Control c, string message)
        {
            if (message == "")
            {
                if (ep.GetError(c) != "")
                    count--;
            }
            else
                count++;

            ep.SetError(c, message);
        }

        /// <summary>
        /// Returns a true/false as to whether a particular error provider has an error.
        /// </summary>
        /// <param name="ep">The error provider handling the error.</param>
        /// <returns></returns>
        public static bool HasErrors(this ErrorProvider ep)
        {
            return count != 0;
        }

        /// <summary>
        /// Returns the number of errors of a particular error provider.
        /// </summary>
        /// <param name="ep">The error provider handling the error.</param>
        /// <returns></returns>
        public static int GetErrorCount(this ErrorProvider ep)
        {
            return count;
        }

        /// <summary>
        /// Removes the errors from a particular error provider.
        /// </summary>
        /// <param name="ep">The error provider handling the error.</param>
        /// <returns></returns>
        public static int RemoveErrors(this ErrorProvider ep)
        {
            return count = 0;
        }
    }
}