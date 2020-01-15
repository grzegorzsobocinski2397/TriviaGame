using System;
using System.Windows.Input;
using TriviaGame.ViewModels.Base;

namespace TriviaGame
{
    internal class FinalViewModel : BaseViewModel
    {
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
            ResultsCommand = new RelayCommand(() => ChangePage(ApplicationPage.Results));
        }

        #endregion Constructor
    }
}