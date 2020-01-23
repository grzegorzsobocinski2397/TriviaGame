using System;
using System.IO;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Windows;

namespace TriviaGame
{
    /// <summary>
    /// File handler for a <see cref="Scoreboard"/>, which saves user information with AES encryption.
    /// </summary>
    public class HighscoresFileHandler : BaseFileHandler
    {
        #region Private Fields

        /// <summary>
        /// Name of the file that will contain serialized questions.
        /// </summary>
        private const string FILE_NAME = "SCORES.XML";

        #endregion Private Fields

        #region Constructors

        /// <summary>
        /// File handler for a <see cref="Scoreboard"/>, which saves user information with AES encryption.
        /// </summary>
        public HighscoresFileHandler() : base(FILE_NAME)
        {
        }

        #endregion Constructors

        #region Public Methods

        /// <summary>
        /// Serialize user scores (current and previous).
        /// </summary>
        /// <param name="scoreboard">Scoreboard that contains all he users that have played this game.</param>
        public void SerializeScores(Scoreboard scoreboard)
        {
            try
            {
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    // Serialize the SaveFile.
                    DataContractSerializer serializer = new DataContractSerializer(typeof(Scoreboard));
                    serializer.WriteObject(memoryStream, scoreboard);
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
        /// Deserialize user scores.
        /// </summary>
        /// <returns>Scoreboard that contains all he users that have played this game.</returns>
        public Scoreboard DeserializeScoreboard()
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

                        DataContractSerializer serializer = new DataContractSerializer(typeof(Scoreboard));

                        Scoreboard scoreboard = (Scoreboard)serializer.ReadObject(memoryStream);
                        aesCryptoService.Dispose();
                        return scoreboard;
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