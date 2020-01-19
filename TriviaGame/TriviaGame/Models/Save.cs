using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TriviaGame.Models
{
    /// <summary>
    /// Save object that will contain all the questions loaded by the user.
    /// </summary>
    [DataContract(Name = "Save")]
    internal class Save
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
        /// Initalize the insance of Save.
        /// </summary>
        /// <param name="question"></param>
        public Save(List<Question> questions)
        {
            Questions = questions;
        }

        #endregion Constructor
    }
}