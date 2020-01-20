using System;
using System.Globalization;
using System.Windows.Media;

namespace TriviaGame
{
    /// <summary>
    /// Converts boolean into <see cref="SolidColorBrush"/>.
    /// </summary>
    public class BooleanToForegroundValueConverter : BaseValueConverter<BooleanToForegroundValueConverter>
    {
        /// <summary>
        /// Converts from boolean to <see cref="SolidColorBrush"/>
        /// </summary>
        /// <param name="value">Boolean.</param>
        /// <returns><see cref="SolidColorBrush"/></returns>
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? new SolidColorBrush(Colors.Red) : new SolidColorBrush(Colors.Green);
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