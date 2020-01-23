using System;
using System.IO;
using System.Windows;

namespace TriviaGame
{
    /// <summary>
    /// Base class for the file handlers with functionality to serialize/deserialize and remove file.
    /// </summary>
    public abstract class BaseFileHandler
    {
        #region Private Fields

        /// <summary>
        /// File destination with a name.
        /// </summary>
        private readonly string fileName;

        #endregion Private Fields

        #region Constructors

        /// <summary>
        /// Set the file name.
        /// </summary>
        /// <param name="fileName">File destination with name.</param>
        public BaseFileHandler(string fileName)
        {
            this.fileName = fileName;
        }

        #endregion Constructors

        #region Protected Methods

        /// <summary>
        /// Removes the file with serialized questions.
        /// </summary>
        protected void RemoveFile()
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

        #region Public Methods

        /// <summary>
        /// Checks if the file exists.
        /// </summary>
        /// <returns>Boolean value of current file existance.</returns>
        public bool DoesFileExist()
        {
            return File.Exists($"{Environment.CurrentDirectory}\\{fileName}");
        }

        #endregion Public Methods
    }
}