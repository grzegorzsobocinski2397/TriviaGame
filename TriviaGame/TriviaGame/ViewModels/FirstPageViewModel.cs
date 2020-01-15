using System.Windows.Input;
using TriviaGame.ViewModels.Base;

namespace TriviaGame
{
    internal class FirstPageViewModel : BaseViewModel
    {
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
            UserPageCommand = new RelayCommand(() => ChangePage(ApplicationPage.User));
        }

        #endregion Constructor
    }
}