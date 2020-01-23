using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TriviaGame.Models
{
    /// <summary>
    /// Save object that will contain all the questions loaded by the user.
    /// </summary>
    [DataContract(Name = "Save")]
    public class Save
    {
        #region Public Properties

        /// <summary>
        /// List of questions loaded by the user.
        /// </summary>
        [DataMember()]
        public List<Question> Questions { get; set; }

        #endregion Public Properties

        #region Constructor

        /// <summary>
        /// Save object that will contain all the questions loaded by the user.
        /// </summary>
        /// <param name="questions">List of all questions loaded by the user.</param>
        public Save(List<Question> questions)
        {
            Questions = questions;
        }

        #endregion Constructor
    }
}