using System;
using System.Windows.Input;

namespace TriviaGame
{
    /// <summary>
    /// Base Command that implements <see cref="ICommand"/> for correct functionality,
    /// it also takes a parameter that will be passed into the action.
    /// </summary>
    public class RelayParameterCommand : ICommand
    {
        #region Private Members

        /// <summary>
        /// Method on the View Model.
        /// </summary>
        private readonly Action<object> action;

        #endregion Private Members

        #region Public Events

        /// <summary>
        /// The event thats fired when the <see cref="CanExecute(object)"/> value has changed
        /// </summary>
        public event EventHandler CanExecuteChanged = (sender, e) => { };

        #endregion Public Events

        #region Constructor

        /// <summary>
        /// Base Command that implements <see cref="ICommand"/> for correct functionality,
        /// it also takes a parameter that will be passed into the action.
        /// </summary>
        /// <param name="action">Method on the View Model.</param>
        public RelayParameterCommand(Action<object> action)
        {
            this.action = action;
        }

        #endregion Constructor

        #region Command Methods

        /// <summary>
        /// Command can always execute.
        /// </summary>
        /// <param name="parameter">In this case it's always null.</param>
        /// <returns>Always true, because in our case it doesn't matter.</returns>
        public bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Passes the parameter to an invoked action.
        /// </summary>
        /// <param name="parameter">An any object.</param>
        public void Execute(object parameter)
        {
            action(parameter);
        }

        #endregion Command Methods
    }
}