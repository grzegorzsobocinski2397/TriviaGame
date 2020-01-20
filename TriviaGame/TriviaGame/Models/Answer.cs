namespace TriviaGame.Models
{
    /// <summary>
    /// Contains information about question asked and user's input.
    /// </summary>
    internal class Answer
    {
        #region Public Properties

        /// <summary>
        /// One trivia question.
        /// </summary>
        public Question Question { get; set; }

        /// <summary>
        /// User's answer.
        /// </summary>
        public bool IsCorrect { get; set; }

        #endregion Public Properties

        #region Constructors

        /// <summary>
        /// Creates instance of Answer class.
        /// </summary>
        /// <param name="question">User's answer.</param>
        /// <param name="isCorrect">One trivia question.</param>
        public Answer(Question question, bool isCorrect)
        {
            Question = question;
            IsCorrect = isCorrect;
        }

        #endregion Constructors
    }
}