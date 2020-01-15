using System.Windows.Input;
using TriviaGame.ViewModels.Base;

namespace TriviaGame
{
    internal class HighscoresViewModel : BaseViewModel
    {
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
        }

        #endregion Constructor
    }
}