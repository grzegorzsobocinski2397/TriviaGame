using System.Windows.Input;
using TriviaGame.ViewModels.Base;

namespace TriviaGame
{
    internal class UserViewModel : BaseViewModel
    {
        #region Public Properties

        /// <summary>
        /// Value of the textbox.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Text that informs user about serialized questions.
        /// </summary>
        public string InformationText { get; set; }

        #endregion Public Properties

        #region Commands

        /// <summary>
        /// Moves the user to <see cref="ApplicationPage.First"/> page.
        /// </summary>
        public ICommand ReturnCommand { get; set; }

        /// <summary>
        /// Moves the user to <see cref="ApplicationPage.Game"/> page.
        /// </summary>
        public ICommand StartCommand { get; set; }

        #endregion Commands

        #region Constructor

        public UserViewModel()
        {
            ReturnCommand = new RelayCommand(() => ChangePage(ApplicationPage.First));
            StartCommand = new RelayCommand(() => StartGame());
        }

        #endregion Constructor

        #region Private Methods

        /// <summary>
        /// Try starting the game if the <see cref="UserName"/> isn't empty.
        /// </summary>
        private void StartGame()
        {
            if (string.IsNullOrEmpty(UserName))
            {
                InformationText = "You must set your name!";
            }
            else
            {
                ChangePage(ApplicationPage.Game);
            }
        }

        #endregion Private Methods
    }
}