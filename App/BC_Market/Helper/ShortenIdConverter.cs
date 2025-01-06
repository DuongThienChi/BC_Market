using System;
using Microsoft.UI.Xaml.Data;

namespace BC_Market.Converter
{
    /// <summary>
    /// Converts a long ID to a shortened version.
    /// </summary>
    public class ShortenIdConverter : IValueConverter
    {
        /// <summary>
        /// Converts a long ID to a shortened version.
        /// </summary>
        /// <param name="value">The ID value to convert.</param>
        /// <param name="targetType">The type of the target property.</param>
        /// <param name="parameter">Optional parameter to be used in the converter logic.</param>
        /// <param name="language">The language of the conversion.</param>
        /// <returns>A shortened version of the ID if it is longer than 8 characters; otherwise, the original value.</returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is string id && id.Length > 8)
            {
                return id.Substring(0, 8) + ".....";
            }
            return value;
        }

        /// <summary>
        /// Converts back the shortened ID to its original form.
        /// </summary>
        /// <param name="value">The value to convert back.</param>
        /// <param name="targetType">The type of the target property.</param>
        /// <param name="parameter">Optional parameter to be used in the converter logic.</param>
        /// <param name="language">The language of the conversion.</param>
        /// <returns>Throws NotImplementedException.</returns>
        /// <exception cref="NotImplementedException">Thrown always as this method is not implemented.</exception>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
