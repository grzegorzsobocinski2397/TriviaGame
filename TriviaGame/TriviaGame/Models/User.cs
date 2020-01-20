using System;
using System.Runtime.Serialization;

namespace TriviaGame.Models
{
    /// <summary>
    /// Representation of the current player.
    /// </summary>
    [DataContract(Name = "User")]
    internal struct User
    {
        #region Public Properties

        /// <summary>
        /// User's name which will be also displayed in the highscores.
        /// </summary>
        [DataMember()]
        public string Name { get; }

        /// <summary>
        /// User's score which is calculated by number of correct questions.
        /// </summary>
        [DataMember()]
        public int Score { get; set; }

        /// <summary>
        /// Time of the game start.
        /// </summary>
        [DataMember()]
        public DateTime StartDate { get; set; }

        #endregion Public Properties

        #region Constructors

        /// <summary>
        /// Initialize the user instance.
        /// </summary>
        /// <param name="name">User's name which will be also displayed in the highscores.</param>
        public User(string name)
        {
            Name = name;
            Score = 0;
            StartDate = DateTime.Now;
        }

        #endregion Constructors
    }
}