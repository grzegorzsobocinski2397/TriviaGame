using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Input;
using TriviaGame.Models;
using TriviaGame.ViewModels.Base;

namespace TriviaGame
{
    internal class FirstPageViewModel : BaseViewModel
    {
        #region Private Fields

        /// <summary>
        /// Name of the file that will contain serialized questions.
        /// </summary>
        private const string FILE_NAME = "QUESTIONS.XML";

        #endregion Private Fields

        #region Public Properties

        /// <summary>
        /// Text that informs user about serialized questions.
        /// </summary>
        public string InformationText { get; set; }

        #endregion Public Properties

        #region Commands

        /// <summary>
        /// Opens up the System Browser to select new files.
        /// </summary>
        public ICommand LoadCommand { get; set; }

        /// <summary>
        /// Moves the user to <see cref="ApplicationPage.Highscores"/> page.
        /// </summary>
        public ICommand HighscoresCommand { get; set; }

        /// <summary>
        /// Moves the user to <see cref="ApplicationPage.User"/> page.
        /// </summary>
        public ICommand UserPageCommand { get; set; }

        #endregion Commands

        #region Constructor

        public FirstPageViewModel()
        {
            HighscoresCommand = new RelayCommand(() => ChangePage(ApplicationPage.Highscores));
            UserPageCommand = new RelayCommand(() => ChangePage(ApplicationPage.User));
            LoadCommand = new RelayCommand(() => OpenFileDialog());
            CheckForFile();
        }

        #endregion Constructor

        #region Private Methods

        private void CheckForFile()
        {
            if (File.Exists($"{Environment.CurrentDirectory}\\{FILE_NAME}"))
            {
                Save save = DeserializeQuestions();
                InformationText = $"We've found {save.Questions.Count} previously loaded questions!";
            }
        }
        /// <summary>
        /// Open up a File Dialog window and let the user choose an .xls file.
        /// </summary>
        private void OpenFileDialog()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "File (*.csv)|*.csv"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                ReadCSV(openFileDialog.FileName);
            }
        }

        /// <summary>
        /// Read .csv file and parse the content into questions.
        /// </summary>
        /// <param name="path">Path of the file selected by the user.</param>
        private void ReadCSV(string path)
        {
            using (var reader = new StreamReader(path, Encoding.UTF8))
            {
                List<Question> questions = new List<Question>();
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    string[] values = line.Split(';');
                    bool answer = values[1] == "1";

                    Question question = new Question(values[0], answer);
                    questions.Add(question);
                }

                SerializeQuestions(questions);
            }
        }

        /// <summary>
        /// Serialize the loaded questions.
        /// </summary>
        /// <param name="questions">Questions loaded by the user.</param>
        private void SerializeQuestions(List<Question> questions)
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
                            MessageBox.Show("A handled exception just occurred: " + exception.Message, "Exception Sample", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        catch (ArgumentNullException exception)
                        {
                            MessageBox.Show("A handled exception just occurred: " + exception.Message, "Exception Sample", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        catch (ArgumentException exception)
                        {
                            MessageBox.Show("A handled exception just occurred: " + exception.Message, "Exception Sample", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show("A handled exception just occurred: " + exception.Message, "Exception Sample", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        /// <summary>
        /// Deserialize previously loaded questions.
        /// </summary>
        /// <returns>Save file that contains previously loaded questions.</returns>
        private Save DeserializeQuestions()
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
                MessageBox.Show("A handled exception just occurred: " + exception.Message, "Exception Sample", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (UnauthorizedAccessException exception)
            {
                MessageBox.Show("A handled exception just occurred: " + exception.Message, "Exception Sample", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (ArgumentNullException exception)
            {
                MessageBox.Show("A handled exception just occurred: " + exception.Message, "Exception Sample", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (ArgumentException exception)
            {
                MessageBox.Show("A handled exception just occurred: " + exception.Message, "Exception Sample", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception exception)
            {
                MessageBox.Show("A handled exception just occurred: " + exception.Message, "Exception Sample", MessageBoxButton.OK, MessageBoxImage.Warning);
                RemoveFile();
            }

            return null;
        }

        /// <summary>
        /// Removes the file with serialized questions.
        /// </summary>
        private void RemoveFile()
        {
            try
            {
                File.Delete($"{Environment.CurrentDirectory}\\{FILE_NAME}");
            }
            catch (UnauthorizedAccessException exception)
            {
                MessageBox.Show("A handled exception just occurred: " + exception.Message, "Exception Sample", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception exception)
            {
                MessageBox.Show("A handled exception just occurred: " + exception.Message, "Exception Sample", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        #endregion Private Methods
    }
}