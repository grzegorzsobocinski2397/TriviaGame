using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using TriviaGame.Models;
using TriviaGame.ViewModels.Base;

namespace TriviaGame
{
    internal class FinalViewModel : BaseViewModel
    {
        #region Public Properties

        /// <summary>
        /// Informs about the total amount of questions answered by the user and total amount of questions answered correctly by the user.
        /// </summary>
        public string MainText { get; set; }

        #endregion Public Properties

        #region Private Fields

        /// <summary>
        /// User's answers and reference to the question.
        /// </summary>
        private List<Answer> answers;

        #endregion Private Fields

        #region Commands

        /// <summary>
        /// Closes the application.
        /// </summary>
        public ICommand ExitCommand { get; set; }

        /// <summary>
        /// Moves the user to <see cref="ApplicationPage.Highscores"/> page.
        /// </summary>
        public ICommand HighscoresCommand { get; set; }

        /// <summary>
        /// Moves the user to <see cref="ApplicationPage.Results"/> page.
        /// </summary>
        public ICommand ResultsCommand { get; set; }

        #endregion Commands

        #region Constructor

        public FinalViewModel()
        {
            ExitCommand = new RelayCommand(() => Environment.Exit(0));
            HighscoresCommand = new RelayCommand(() => ChangePage(ApplicationPage.Highscores));
            ResultsCommand = new RelayCommand(() => GoToResults());

            // Register the MVVM light message to get the answers from GameViewModel.
            MessengerInstance.Register<NotificationMessage<List<Answer>>>(this, SummarizeGame);
        }

        #endregion Constructor

        #region Private Methods

        private void GoToResults()
        {
            ChangePage(ApplicationPage.Results);

            MessengerInstance.Send(new NotificationMessage<List<Answer>>(answers, "Results"));
        }

        /// <summary>
        /// Count the amount of correct answers and display the information to the user
        /// </summary>
        /// <param name="message">MVVM Light message sent from the <see cref="GameViewModel"/>/></param>
        private void SummarizeGame(NotificationMessage<List<Answer>> message)
        {
            // Continue if the message notification matches
            if (message.Notification == "FinalView")
            {
                answers = message.Content;
                int totalCorrectAnswers = answers.Where(a => a.IsCorrect).ToList().Count;
                MainText = $"You've answered {totalCorrectAnswers} questions correctly out of {answers.Count}";
            }
        }

        #endregion Private Methods
    }
}