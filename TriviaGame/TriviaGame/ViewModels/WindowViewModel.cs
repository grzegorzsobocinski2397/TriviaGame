using System.Windows;
using System.Windows.Input;
using TriviaGame.ViewModels.Base;

namespace TriviaGame.ViewModels
{
    /// <summary>
    /// View Model for the MainWindow contains basic information about the application such as round corners
    /// or currently active page.
    /// </summary>
    internal class WindowViewModel : BaseViewModel
    {
        #region Private Members

        /// <summary>
        /// The window for this view model
        /// </summary>
        private readonly Window window;

        #endregion Private Members

        #region Public Properties

        /// <summary>
        /// Current page being displayed in the application.
        /// </summary>
        public ApplicationPage CurrentPage { get; set; }

        #endregion Public Properties

        #region Commands

        /// <summary>
        /// Closes the application
        /// </summary>
        public ICommand CloseWindowCommand { get; set; }

        #endregion Commands

        #region Constructor

        public WindowViewModel(Window window)
        {
            // Binds the window to the view model
            this.window = window;

            // Creates commands
            CloseWindowCommand = new RelayCommand(() => window.Close());

            CurrentPage = ApplicationPage.First;
        }

        #endregion Constructor
    }
}
