using System.Runtime.Serialization;

namespace TriviaGame.Models
{
    /// <summary>
    /// One trivia question.
    /// </summary>
    [DataContract(Name = "Question")]
    internal struct Question
    {
        #region Public Properties

        /// <summary>
        /// Text of the question.
        /// </summary>
        [DataMember()]
        public string Content { get; set; }

        /// <summary>
        /// Correct answer for the question.
        /// </summary>
        [DataMember()]
        public bool Answer { get; set; }

        #endregion Public Properties

        #region Constructors

        /// <summary>
        ///
        /// </summary>
        /// <param name="content">Text of the question.</param>
        /// <param name="answer">Correct answer for the question.</param>
        public Question(string content, bool answer)
        {
            Content = content;
            Answer = answer;
        }

        #endregion Constructors
    }
}