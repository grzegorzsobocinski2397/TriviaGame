namespace TriviaGame.Models
{
    /// <summary>
    /// Contains information about question asked and user's input.
    /// </summary>
    public class Answer
    {
        #region Public Properties

        /// <summary>
        /// One trivia question.
        /// </summary>
        public Question Question { get; }

        /// <summary>
        /// User's answer.
        /// </summary>
        public bool IsCorrect { get; }

        #endregion Public Properties

        #region Constructors

        /// <summary>
        /// Contains information about question asked and user's input.
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