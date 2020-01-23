using System.Collections.Generic;
using System.Runtime.Serialization;
using TriviaGame.Models;

namespace TriviaGame
{
    /// <summary>
    /// Scoreboard that contains list of all the users that have played the game.
    /// </summary>
    [DataContract(Name = "Scoreboard")]
    public class Scoreboard
    {
        #region Public Properties

        /// <summary>
        /// List of all users that have played the game.
        /// </summary>
        [DataMember()]
        public List<User> Users { get; set; }

        #endregion Public Properties

        #region Constructor

        /// <summary>
        /// Scoreboard that contains list of all the users that have played the game.
        /// </summary>
        public Scoreboard()
        {
            Users = new List<User>();
        }

        #endregion Constructor
    }
}