using Microsoft.Win32;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Input;
using TriviaGame.Helpers;
using TriviaGame.Models;
using TriviaGame.ViewModels.Base;

namespace TriviaGame
{
    /// <summary>
    /// View Model for <see cref="FirstPage"/>, which can start the game, display highscores or load new questions.
    /// </summary>
    public class FirstPageViewModel : BaseViewModel
    {
        #region Private Fields

        /// <summary>
        /// Link to the repository of this project.
        /// </summary>
        private const string REPOSITORY_URL = "https://github.com/grzegorzsobocinski2397/TriviaGame";

        /// <summary>
        /// Allow user to go to the next page. User has loaded question and is ready to play.
        /// </summary>
        private bool areQuestionsLoaded = false;

        /// <summary>
        /// File handler for a <see cref="Save"/>, which serializes questions with AES encryption.
        /// </summary>
        private readonly SaveFileHandler fileHandler = new SaveFileHandler();

        #endregion Private Fields

        #region Public Properties

        /// <summary>
        /// Text that informs user about serialized questions.
        /// </summary>
        public string InformationText { get; set; }

        #endregion Public Properties

        #region Commands

        /// <summary>
        /// Open up the System Browser to select new files.
        /// </summary>
        public ICommand LoadCommand { get; set; }

        /// <summary>
        /// Move the user to <see cref="ApplicationPage.Highscores"/> page.
        /// </summary>
        public ICommand HighscoresCommand { get; set; }

        /// <summary>
        /// Move the user to <see cref="ApplicationPage.User"/> page.
        /// </summary>
        public ICommand UserPageCommand { get; set; }

        /// <summary>
        /// Open the GitHub repository.
        /// </summary>
        public ICommand OpenGitHubCommand { get; set; }

        #endregion Commands

        #region Constructor

        /// <summary>
        /// View Model for <see cref="FirstPage"/>, which can start the game, display highscores or load new questions.
        /// </summary>
        public FirstPageViewModel()
        {
            HighscoresCommand = new RelayCommand(() => ChangePage(ApplicationPage.Highscores));
            UserPageCommand = new RelayCommand(() => StartGame());
            LoadCommand = new RelayCommand(() => OpenFileDialog());
            OpenGitHubCommand = new RelayCommand(() => OpenGitHubPage());
            CheckForFile();
        }

        #endregion Constructor

        #region Private Methods

        /// <summary>
        /// Try starting the new game if there are questions loaded.
        /// </summary>
        private void StartGame()
        {
            if (areQuestionsLoaded)
            {
                ChangePage(ApplicationPage.User);
            }
            else
            {
                InformationText = $"Please load some questions before starting the game.";
            }
        }

        /// <summary>
        /// Check if there were some questions serialized.
        /// </summary>
        private void CheckForFile()
        {
            if (fileHandler.DoesFileExist())
            {
                Save save = fileHandler.DeserializeQuestions();
                areQuestionsLoaded = true;
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
                areQuestionsLoaded = questions.Count > 0;

                fileHandler.SerializeFile(questions);

                InformationText = $"Succesfully loaded {questions.Count} questions. Click start and let the game begin!";
            }
        }

        /// <summary>
        /// Opens GitHub repository of this project in default browser.
        /// </summary>
        private void OpenGitHubPage()
        {
            System.Diagnostics.Process.Start(REPOSITORY_URL);
        }

        #endregion Private Methods
    }
}