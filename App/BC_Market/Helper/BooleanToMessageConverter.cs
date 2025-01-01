using Microsoft.UI.Xaml.Data;
using System;

namespace BC_Market.Converter
{
    /// <summary>
    /// Converts a boolean value to a message string.
    /// </summary>
    public class BooleanToMessageConverter : IValueConverter
    {
        /// <summary>
        /// Converts a boolean value to a message string.
        /// </summary>
        /// <param name="value">The boolean value to convert.</param>
        /// <param name="targetType">The type of the target property.</param>
        /// <param name="parameter">Optional parameter to be used in the converter logic.</param>
        /// <param name="language">The language of the conversion.</param>
        /// <returns>A message string representing the boolean value.</returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is bool paymentStatus)
            {
                return paymentStatus ? "Your order will be delivered to you soon. Thank you for shopping with us!" : "Payment failed. Please try again.";
            }
            return "Payment failed. Please try again.";
        }

        /// <summary>
        /// Converts back the message string to a boolean value.
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
