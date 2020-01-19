namespace TriviaGame.Models
{
    /// <summary>
    /// Representation of the current player.
    /// </summary>
    internal struct User
    {
        #region Public Properties

        /// <summary>
        /// User's name which will be also displayed in the highscores.
        /// </summary>
        public string Name { get; }

        #endregion Public Properties

        #region Constructors

        /// <summary>
        /// Initialize the user instance.
        /// </summary>
        /// <param name="name">User's name which will be also displayed in the highscores.</param>
        public User(string name)
        {
            Name = name;
        }

        #endregion Constructors
    }
}