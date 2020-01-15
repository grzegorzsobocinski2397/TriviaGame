using System;
using System.Windows.Input;

namespace TriviaGame.ViewModels.Base
{
    /// <summary>
    /// Base Command that implements <see cref="ICommand"/> for correct functionality.
    /// </summary>
    internal class RelayCommand : ICommand
    {
        /// <summary>
        /// Action to run
        /// </summary>
        private readonly Action action;

        public event EventHandler CanExecuteChanged = (sender, e) => { };

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="action"></param>
        public RelayCommand(Action action)
        {
            this.action = action;
        }

        /// <summary>
        /// Command can always execute
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            action();
        }
    }
}
