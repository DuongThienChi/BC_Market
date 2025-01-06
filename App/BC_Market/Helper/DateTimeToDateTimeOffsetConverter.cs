using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC_Market.Converter
{
    /// <summary>
    /// Converts between <see cref="DateTime"/> and <see cref="DateTimeOffset"/>.
    /// </summary>
    public class DateTimeToDateTimeOffsetConverter : IValueConverter
    {
        /// <summary>
        /// Converts a <see cref="DateTime"/> to a <see cref="DateTimeOffset"/>.
        /// </summary>
        /// <param name="value">The <see cref="DateTime"/> value to convert.</param>
        /// <param name="targetType">The type of the target property.</param>
        /// <param name="parameter">Optional parameter to be used in the converter logic.</param>
        /// <param name="language">The language of the conversion.</param>
        /// <returns>A <see cref="DateTimeOffset"/> representing the <see cref="DateTime"/> value.</returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is DateTime dateTime)
            {
                return new DateTimeOffset(dateTime);
            }
            return null;
        }

        /// <summary>
        /// Converts a <see cref="DateTimeOffset"/> back to a <see cref="DateTime"/>.
        /// </summary>
        /// <param name="value">The <see cref="DateTimeOffset"/> value to convert back.</param>
        /// <param name="targetType">The type of the target property.</param>
        /// <param name="parameter">Optional parameter to be used in the converter logic.</param>
        /// <param name="language">The language of the conversion.</param>
        /// <returns>A <see cref="DateTime"/> representing the <see cref="DateTimeOffset"/> value.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            if (value is DateTimeOffset dateTimeOffset)
            {
                return dateTimeOffset.DateTime;
            }
            return null;
        }
    }
}
