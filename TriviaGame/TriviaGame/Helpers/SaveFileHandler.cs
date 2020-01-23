using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Windows;
using TriviaGame.Models;

namespace TriviaGame.Helpers
{
    /// <summary>
    /// File handler for a <see cref="Save"/>, which saves questions.
    /// </summary>
    public class SaveFileHandler : BaseFileHandler
    {
        #region Private Fields

        /// <summary>
        /// Name of the file that will contain serialized questions.
        /// </summary>
        private const string FILE_NAME = "QUESTIONS.XML";

        #endregion Private Fields

        #region Constructors

        /// <summary>
        /// File handler for a <see cref="Save"/>, which saves questions.
        /// </summary>
        public SaveFileHandler() : base(FILE_NAME)
        {
        }

        #endregion Constructors

        #region Public Methods

        /// <summary>
        /// Serialize the loaded questions.
        /// </summary>
        /// <param name="questions">Questions loaded by the user.</param>
        public void SerializeFile(List<Question> questions)
        {
            Save save = new Save(questions);

            try
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    // Serialize the SaveFile.
                    DataContractSerializer serializer = new DataContractSerializer(typeof(Save));
                    serializer.WriteObject(memoryStream, save);
                    memoryStream.Position = 0L;

                    using (StreamReader memoryStreamReader = new StreamReader(memoryStream))

                        // Save to .xml and encrypt with AES.
                        try
                        {
                            using (Stream fileStream = new FileStream(FILE_NAME, FileMode.Create, FileAccess.Write, FileShare.None))
                            {
                                AesCryptoServiceProvider aesCryptoService = new AesCryptoServiceProvider();

                                using (ICryptoTransform cryptoTransform = aesCryptoService.CreateEncryptor(
                                    EncryptionValues.EncryptionKey,
                                    EncryptionValues.EncryptionInitializationVector))

                                using (CryptoStream cryptoStream = new CryptoStream(fileStream, cryptoTransform, CryptoStreamMode.Write))

                                using (StreamWriter cryptoStreamWriter = new StreamWriter(cryptoStream))
                                {
                                    string dataToEncrypt = memoryStreamReader.ReadToEnd();
                                    cryptoStreamWriter.Write(dataToEncrypt);

                                    cryptoStreamWriter.Flush();
                                }

                                aesCryptoService.Dispose();
                            }
                        }
                        catch (FileNotFoundException exception)
                        {
                            MessageBox.Show("A handled exception just occurred: " + exception.Message, "File Not Found Exception", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        catch (ArgumentNullException exception)
                        {
                            MessageBox.Show("A handled exception just occurred: " + exception.Message, "Argument Null Exception", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        catch (ArgumentException exception)
                        {
                            MessageBox.Show("A handled exception just occurred: " + exception.Message, "Argument Exception", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("A handled exception just occurred: " + exception.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        /// <summary>
        /// Deserialize previously loaded questions.
        /// </summary>
        /// <returns>Save file that contains previously loaded questions.</returns>
        public Save DeserializeQuestions()
        {
            try
            {
                // Read .xml file.
                using (FileStream fileStream = File.Open(FILE_NAME, FileMode.Open))
                {
                    // First decrypt.
                    AesCryptoServiceProvider aesCryptoService = new AesCryptoServiceProvider();
                    using (ICryptoTransform cryptoTransform =
                           aesCryptoService.CreateDecryptor(EncryptionValues.EncryptionKey, EncryptionValues.EncryptionInitializationVector))
                    using (CryptoStream cryptoStream = new CryptoStream(fileStream, cryptoTransform, CryptoStreamMode.Read))
                    using (StreamReader cryptoStreamReader = new StreamReader(cryptoStream))
                    using (MemoryStream memoryStream = new MemoryStream())

                    // Create new SaveFile object based on the saved configuration.
                    using (StreamWriter memoryStreamWriter = new StreamWriter(memoryStream))
                    {
                        string decryptedSave = cryptoStreamReader.ReadToEnd();
                        memoryStreamWriter.Write(decryptedSave);
                        memoryStreamWriter.Flush();
                        memoryStream.Position = 0L;

                        DataContractSerializer serializer = new DataContractSerializer(typeof(Save));

                        Save save = (Save)serializer.ReadObject(memoryStream);
                        aesCryptoService.Dispose();
                        return save;
                    }
                }
            }
            catch (FileNotFoundException exception)
            {
                MessageBox.Show("A handled exception just occurred: " + exception.Message, "File Not Found Exception", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (UnauthorizedAccessException exception)
            {
                MessageBox.Show("A handled exception just occurred: " + exception.Message, "Unauthorized Access Exception", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (ArgumentNullException exception)
            {
                MessageBox.Show("A handled exception just occurred: " + exception.Message, "Argument Null Exception", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (ArgumentException exception)
            {
                MessageBox.Show("A handled exception just occurred: " + exception.Message, "Argument Exception", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception exception)
            {
                MessageBox.Show("A handled exception just occurred: " + exception.Message, "Exception", MessageBoxButton.OK, MessageBoxImage.Warning);
                RemoveFile();
            }

            return null;
        }

        #endregion Public Methods
    }
}