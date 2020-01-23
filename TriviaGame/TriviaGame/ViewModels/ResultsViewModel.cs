using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using TriviaGame.Models;

namespace TriviaGame
{
    internal class ResultsViewModel : BaseViewModel
    {
        #region Public Properties

        /// <summary>
        /// Informs about the total amount of questions answered by the user and total amount of questions answered correctly by the user.
        /// </summary>
        public string MainText { get; set; }

        /// <summary>
        /// All questions answered by the user.
        /// </summary>
        public List<Answer> Answers { get; set; }

        #endregion Public Properties

        #region Commands

        /// <summary>
        /// Return back to the Final Score window.
        /// </summary>
        public ICommand ReturnCommand { get; set; }

        #endregion Commands

        #region Constructors

        /// <summary>
        /// Register MVVM light message and initialize command.
        /// </summary>
        public ResultsViewModel()
        {
            ReturnCommand = new RelayCommand(() => ReturnToFinalView());

            // Register the MVVM light message to get the answers from FinalViewModel.
            MessengerInstance.Register<NotificationMessage<List<Answer>>>(this, GetAnswers);
        }

        #endregion Constructors

        #region Private Methods

        /// <summary>
        /// Get all the answers from the <see cref="FinalViewModel"/>
        /// </summary>
        /// <param name="message">MVVM Light message sent from the <see cref="FinalViewModel"/>/></param>
        private void GetAnswers(NotificationMessage<List<Answer>> message)
        {
            // Continue if the message notification matches
            if (message.Notification == "Results")
            {
                Answers = message.Content;
                int totalCorrectAnswers = Answers.Where(a => a.IsCorrect).ToList().Count;
                MainText = $"{totalCorrectAnswers}/{Answers.Count}";
            }
        }

        /// <summary>
        /// Return back to the Final Score window.
        /// </summary>
        private void ReturnToFinalView()
        {
            ChangePage(ApplicationPage.FinalScore);

            MessengerInstance.Send(new NotificationMessage<List<Answer>>(Answers, "FinalView"));
        }

        #endregion Private Methods
    }
}