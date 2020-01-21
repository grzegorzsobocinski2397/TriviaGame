using GalaSoft.MvvmLight.Messaging;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using TriviaGame.Helpers;
using TriviaGame.Models;
using TriviaGame.ViewModels;

namespace TriviaGame
{
    internal class GameViewModel : BaseViewModel
    {
        #region Private Fields

        /// <summary>
        /// Contains all currently played questions.
        /// </summary>
        private readonly List<Question> questions;

        /// <summary>
        /// User's answers and reference to the question.
        /// </summary>
        private readonly List<Answer> answers = new List<Answer>();

        /// <summary>
        /// Currently active question, which is displayed on the page.
        /// </summary>
        private Question activeQuestion;

        /// <summary>
        /// Current index of the question.
        /// </summary>
        private int questionCounter = 0;

        /// <summary>
        /// Multiply questions answered by this value to get the score.
        /// </summary>
        private const int SCORE_MULTIPLIER = 10;

        /// <summary>
        /// File handler for a <see cref="Save"/>, which saves questions.
        /// </summary>
        private readonly SaveFileHandler saveFileHandler = new SaveFileHandler();

        /// <summary>
        /// File handler for a <see cref="Scoreboard"/>, which saves user information with AES encryption.
        /// </summary>
        private readonly HighscoresFileHandler scoresFileHandler = new HighscoresFileHandler();

        #endregion Private Fields

        #region Public Properties

        /// <summary>
        /// Text displayed for the user on the GamePage with content of the question.
        /// </summary>
        public string QuestionText { get; set; }

        /// <summary>
        /// Text displayed for the user on the GamePage with number of the question.
        /// </summary>
        public string MainText { get; set; }

        #endregion Public Properties

        #region Commands

        /// <summary>
        /// User's way of selecting an answer.
        /// </summary>
        public ICommand OptionCommand { get; set; }

        #endregion Commands

        #region Constructor

        public GameViewModel()
        {
            OptionCommand = new RelayParameterCommand((parameter) => AnswerQuestion((bool)parameter));
            questions = (saveFileHandler.DeserializeQuestions()).Questions;
            activeQuestion = questions[0];
            GenerateText();
        }

        #endregion Constructor

        #region Private Methods

        /// <summary>
        /// User answered the question, add it to <see cref="answers"/> and set another one.
        /// </summary>
        /// <param name="userInput">User's answer.</param>
        private void AnswerQuestion(bool userInput)
        {
            bool isCorrect = userInput == activeQuestion.Answer;
            Answer answer = new Answer(activeQuestion, isCorrect);
            answers.Add(answer);

            questionCounter++;

            if (questionCounter == questions.Count)
            {
                EndGame();
            }
            else
            {
                activeQuestion = questions[questionCounter];

                GenerateText();
            }
        }

        /// <summary>
        /// Generate text values for the XAML page.
        /// </summary>
        private void GenerateText()
        {
            QuestionText = activeQuestion.Content;
            MainText = $"Question #{questionCounter + 1}";
        }

        /// <summary>
        /// End this game and move the player to Results page.
        /// </summary>
        private void EndGame()
        {
            ChangePage(ApplicationPage.FinalScore);

            SaveScoreboard();

            MessengerInstance.Send(new NotificationMessage<List<Answer>>(answers, "FinalView"));
        }

        /// <summary>
        /// Update or create a new scoreboard.
        /// </summary>
        private void SaveScoreboard()
        {
            User user = ((WindowViewModel)((MainWindow)Application.Current.MainWindow).DataContext).User;
            user.Score = answers.Where(a => a.IsCorrect).ToList().Count * SCORE_MULTIPLIER;

            Scoreboard scoreboard = scoresFileHandler.DoesFileExist() ? scoresFileHandler.DeserializeScoreboard() : new Scoreboard();

            scoreboard.Users.Add(user);

            scoresFileHandler.SerializeScores(scoreboard);
        }

        #endregion Private Methods
    }
}