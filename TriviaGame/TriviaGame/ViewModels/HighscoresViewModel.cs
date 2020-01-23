using System.Collections.Generic;
using System.Windows.Input;
using TriviaGame.Models;
using TriviaGame.ViewModels.Base;

namespace TriviaGame
{
    internal class HighscoresViewModel : BaseViewModel
    {
        #region Interal Properties

        /// <summary>
        ///Users that have played the game before.
        /// </summary>
        public List<User> Users { get; set; }

        #endregion Interal Properties

        #region Private Fields

        /// <summary>
        ///
        /// </summary>
        private readonly HighscoresFileHandler highscoresFileHandler = new HighscoresFileHandler();

        #endregion Private Fields

        #region Commands

        /// <summary>
        /// Moves the user to <see cref="ApplicationPage.FinalScore"/> page.
        /// </summary>
        public ICommand ReturnCommand { get; set; }

        #endregion Commands

        #region Constructor

        public HighscoresViewModel()
        {
            ReturnCommand = new RelayCommand(() => ReturnToPreviousPage());
            Users = highscoresFileHandler.DoesFileExist() ? GetUsers() : new List<User>();
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
            }
            else
            {
                ChangePage(ApplicationPage.First);
            }
        }

        #endregion Private Methods
    }
}