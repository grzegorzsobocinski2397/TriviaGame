namespace TriviaGame
{
    /// <summary>
    /// Contains possible pages in the application.
    /// </summary>
    public enum ApplicationPage
    {
        None,
        /// <summary>
        /// First page that loads. Contains starting option.
        /// </summary>
        First,
        /// <summary>
        /// Page with user's name input element.
        /// </summary>
        User,
        /// <summary>
        /// Page with currently active question.
        /// </summary>
        Game,
        /// <summary>
        /// Page with user's final output.
        /// </summary>
        FinalScore,
        /// <summary>
        /// Page with summary of user's results.
        /// </summary>
        Results,
        /// <summary>
        /// Page with current scores. 
        /// </summary>
        Highscores
    }
}
