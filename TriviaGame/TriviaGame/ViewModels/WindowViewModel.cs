using System.Windows;
using System.Windows.Input;
using TriviaGame.Models;
using TriviaGame.ViewModels.Base;

namespace TriviaGame.ViewModels
{
    /// <summary>
    /// View Model for the MainWindow contains basic information about the application currently active page or user.
    /// </summary>
    public class WindowViewModel : BaseViewModel
    {
        #region Public Properties

        /// <summary>
        /// Representation of the current player.
        /// </summary>
        internal User User { get; set; }

        #endregion Public Properties

        #region Public Properties

        /// <summary>
        /// Current page being displayed in the application.
        /// </summary>
        public ApplicationPage CurrentPage { get; set; }

        /// <summary>
        /// Previous page displayed in the application.
        /// </summary>
        public ApplicationPage PreviousPage { get; set; }

        #endregion Public Properties

        #region Commands

        /// <summary>
        /// Closes the application
        /// </summary>
        public ICommand CloseWindowCommand { get; set; }

        #endregion Commands

        #region Constructor

        /// <summary>
        /// View Model for the MainWindow contains basic information about the application currently active page or user.
        /// </summary>
        /// <param name="window">Instance of the application's window.</param>
        public WindowViewModel(Window window)
        {
            CloseWindowCommand = new RelayCommand(() => window.Close());
            CurrentPage = ApplicationPage.First;
        }

        #endregion Constructor
    }
}