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
        /// 
        /// </summary>
        public List<User> Users { get; set; }
        #endregion
        #region Private Fields
        /// <summary>
        /// 
        /// </summary>
        private readonly HighscoresFileHandler highscoresFileHandler = new HighscoresFileHandler();
        #endregion
        #region Commands

        /// <summary>
        /// Moves the user to <see cref="ApplicationPage.FinalScore"/> page.
        /// </summary>
        public ICommand ReturnCommand { get; set; }

        #endregion Commands

        #region Constructor

        public HighscoresViewModel()
        {
            ReturnCommand = new RelayCommand(() => ChangePage(ApplicationPage.FinalScore));
            Users = highscoresFileHandler.DoesFileExist() ? GetUsers() : new List<User>();
        }

        #endregion Constructor
        #region Private Methods

        private List<User> GetUsers()
        { 
            Scoreboard scoreboard = highscoresFileHandler.DeserializeScoreboard();
            return scoreboard.Users;
        }

        #endregion
    }
}