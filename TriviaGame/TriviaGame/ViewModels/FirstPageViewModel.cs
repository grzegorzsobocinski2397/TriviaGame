using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Input;
using TriviaGame.Helpers;
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

        /// <summary>
        /// Allow user to go to the next page. User has loaded question and is ready to play.
        /// </summary>
        private bool areQuestionsLoaded = false;

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
            UserPageCommand = new RelayCommand(() => StartGame());
            LoadCommand = new RelayCommand(() => OpenFileDialog());
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
            if (File.Exists($"{Environment.CurrentDirectory}\\{FILE_NAME}"))
            {
                Save save = SaveFileHandler.DeserializeQuestions();
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

                SaveFileHandler.SerializeQuestions(questions);

                InformationText = $"Succesfully loaded {questions.Count} questions. Click start and let the game begin!";
            }
        }

        #endregion Private Methods
    }
}