using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace TriviaGame
{
    /// <summary>
    /// Base class for converters that convert values to specific XAML objects/properties.
    /// </summary>
    public abstract class BaseValueConverter<T> : MarkupExtension, IValueConverter
         where T : class, new()
    {
        #region Private Members

        /// <summary>
        /// A single static instance of this value converter.
        /// </summary>

        private static T converter = null;

        #endregion Private Members

        #region Public Properties

        /// <summary>
        /// Provides an instance of the value converter.
        /// </summary>
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return converter ?? (converter = new T());
        }

        #endregion Public Properties

        #region Value converter methods

        /// <summary>
        /// The method to convert one type to WPF specific one.
        /// </summary>
        public abstract object Convert(object value, Type targetType, object parameter, CultureInfo culture);

        /// <summary>
        /// The method to convert a value back from WPF specific one.
        /// </summary>
        public abstract object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture);

        #endregion Value converter methods
    }
}