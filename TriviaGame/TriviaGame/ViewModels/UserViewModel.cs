using System.Windows.Input;
using TriviaGame.ViewModels.Base;

namespace TriviaGame
{
    internal class UserViewModel : BaseViewModel
    {
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
            StartCommand = new RelayCommand(() => ChangePage(ApplicationPage.Game));
        }

        #endregion Constructor
    }
}
