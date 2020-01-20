using GalaSoft.MvvmLight;
using System.ComponentModel;
using System.Windows;
using TriviaGame.ViewModels;

namespace TriviaGame
{
    /// <summary>
    /// Base View Model that implements <see cref="INotifyPropertyChanged"/> for proper Data Binding.
    /// </summary>
    public class BaseViewModel : ViewModelBase, INotifyPropertyChanged
    {
        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged = (sender, e) => { };

        /// <summary>
        /// Invoked on a property changed in the View Models and Pages. Allows two way data binding.
        /// </summary>
        /// <param name="propertyName"></param>
        public void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion PropertyChanged

        #region Protected Methods

        /// <summary>
        /// Changes the current page of the main window
        /// </summary>
        /// <param name="page">Redirect application to this page.</param>
        protected void ChangePage(ApplicationPage page)
        {
            ((WindowViewModel)((MainWindow)Application.Current.MainWindow).DataContext).CurrentPage = page;
        }

        #endregion Protected Methods
    }
}