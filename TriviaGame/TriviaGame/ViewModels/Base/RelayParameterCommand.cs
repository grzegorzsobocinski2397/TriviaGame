using System;
using System.Windows.Input;

namespace TriviaGame
{
    internal class RelayParameterCommand : ICommand {
        #region Private Members

        /// <summary>
        /// The action to run
        /// </summary>
        private Action<object> action;

        #endregion Private Members

        #region Public Events

        /// <summary>
        /// The event thats fired when the <see cref="CanExecute(object)"/> value has changed
        /// </summary>
        public event EventHandler CanExecuteChanged = (sender, e) => { };

        #endregion Public Events

        #region Constructor

        /// <summary>
        /// Default constrcutor
        /// </summary>
        /// <param name="action"></param>
        public RelayParameterCommand(Action<object> action)
        {
            this.action = action;
        }

        #endregion Constructor

        #region Command Methods

        /// <summary>
        /// A relay command can always execute
        /// </summary>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Executes the commands action
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            action(parameter);
        }

        #endregion Command Methods
    }
}
}