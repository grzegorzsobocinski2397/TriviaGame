using System;
using System.IO;
using System.Windows;

namespace TriviaGame
{
    /// <summary>
    /// Base class for the file handlers with functionality to serialize/deserialize and remove file.
    /// </summary>
    internal abstract class BaseFileHandler
    {
        #region Protected Methods

        /// <summary>
        /// Removes the file with serialized questions.
        /// </summary>
        /// <param name="fileName">File destination with name.</param>
        protected void RemoveFile(string fileName)
        {
            try
            {
                File.Delete($"{Environment.CurrentDirectory}\\{fileName}");
            }
            catch (UnauthorizedAccessException exception)
            {
                MessageBox.Show("A handled exception just occurred: " + exception.Message, "Unauthorized Access Exception", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception exception)
            {
                MessageBox.Show("A handled exception just occurred: " + exception.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        #endregion Protected Methods
    }
}