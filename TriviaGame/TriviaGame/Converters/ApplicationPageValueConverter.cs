using System;
using System.Diagnostics;
using System.Globalization;
using TriviaGame.Pages;

namespace TriviaGame
{
    /// <summary>
    /// Converts the ENUM values to XAML pages.
    /// </summary>
    public class ApplicationPageValueConverter : BaseValueConverter<ApplicationPageValueConverter>
    {
        /// <summary>
        /// Converts from <see cref="ApplicationPage"/> to WPF Page.
        /// </summary>
        /// <param name="value">This is the <see cref="ApplicationPage"/> enum.</param>
        /// <returns>WPF page.</returns>
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((ApplicationPage)value)
            {
                case ApplicationPage.First:
                    return new FirstPage();

                case ApplicationPage.Game:
                    return new GamePage();

                case ApplicationPage.FinalScore:
                    return new FinalScorePage();

                case ApplicationPage.Highscores:
                    return new HighscoresPage();

                case ApplicationPage.Results:
                    return new ResultsPage();

                case ApplicationPage.User:
                    return new UserPage();

                case ApplicationPage.None:
                default:
                    Debugger.Break();
                    return null;
            };
        }

        /// <summary>
        /// Backwards convertion which is not implemented.
        /// </summary>
        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}