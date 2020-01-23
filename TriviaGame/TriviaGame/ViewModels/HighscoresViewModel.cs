using GalaSoft.MvvmLight.Messaging;
using System.Collections.Generic;
using System.Windows.Input;
using TriviaGame.Models;
using TriviaGame.ViewModels.Base;

namespace TriviaGame
{
    /// <summary>
    /// View Model for <see cref="HighscoresPage"/>, which displayes score summary of all the other games.
    /// </summary>
    public class HighscoresViewModel : BaseViewModel
    {
        #region Private Fields

        /// <summary>
        /// All questions answered by the user.
        /// </summary>
        private List<Answer> answers;

        /// <summary>
        /// File handler for a <see cref="Scoreboard"/>, which saves user information with AES encryption.
        /// </summary>
        private readonly HighscoresFileHandler highscoresFileHandler = new HighscoresFileHandler();

        #endregion Private Fields

        #region Public Properties

        /// <summary>
        ///Users that have played the game before.
        /// </summary>
        public List<User> Users { get; set; }

        #endregion Public Properties

        #region Commands

        /// <summary>
        /// Moves the user to <see cref="ApplicationPage.FinalScore"/> page.
        /// </summary>
        public ICommand ReturnCommand { get; set; }

        #endregion Commands

        #region Constructor

        /// <summary>
        /// View Model for <see cref="HighscoresPage"/>, which displayes score summary of all the other games.
        /// </summary>
        public HighscoresViewModel()
        {
            ReturnCommand = new RelayCommand(() => ReturnToPreviousPage());
            Users = highscoresFileHandler.DoesFileExist() ? GetUsers() : new List<User>();

            // Register the MVVM light message to get the answers from FinalViewModel.
            MessengerInstance.Register<NotificationMessage<List<Answer>>>(this, GetAnswers);
        }

        #endregion Constructor

        #region Private Methods

        /// <summary>
        /// Deserializes the .xml file and returns users that have played the game before.
        /// </summary>
        /// <returns>Users that have played the game before.</returns>
        private List<User> GetUsers()
        {
            Scoreboard scoreboard = highscoresFileHandler.DeserializeScoreboard();
            return scoreboard.Users;
        }

        /// <summary>
        /// Return to the previous page, which could be either FinalScore or First.
        /// </summary>
        private void ReturnToPreviousPage()
        {
            if (GetPreviousPage() == ApplicationPage.FinalScore)
            {
                ChangePage(ApplicationPage.FinalScore);

                MessengerInstance.Send(new NotificationMessage<List<Answer>>(answers, "FinalView"));
            }
            else
            {
                ChangePage(ApplicationPage.First);
            }
        }

        /// <summary>
        /// Get all the answers from the <see cref="FinalViewModel"/>
        /// </summary>
        /// <param name="message">MVVM Light message sent from the <see cref="FinalViewModel"/>/></param>
        private void GetAnswers(NotificationMessage<List<Answer>> message)
        {
            // Continue if the message notification matches
            if (message.Notification == "Results")
            {
                answers = message.Content;
            }
        }

        #endregion Private Methods
    }
}