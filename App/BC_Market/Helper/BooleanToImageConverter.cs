using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media.Imaging;
using System;

namespace BC_Market.Converter
{
    /// <summary>
    /// Converts a boolean value to an image source.
    /// </summary>
    public class BooleanToImageConverter : IValueConverter
    {
        /// <summary>
        /// Converts a boolean value to an image source.
        /// </summary>
        /// <param name="value">The boolean value to convert.</param>
        /// <param name="targetType">The type of the target property.</param>
        /// <param name="parameter">Optional parameter to be used in the converter logic.</param>
        /// <param name="language">The language of the conversion.</param>
        /// <returns>A BitmapImage representing the boolean value.</returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is bool paymentStatus)
            {
                return paymentStatus ? new BitmapImage(new Uri("ms-appx:///Assets/Images/success.png")) : new BitmapImage(new Uri("ms-appx:///Assets/Images/fail.png"));
            }
            return new BitmapImage(new Uri("ms-appx:///Assets/Images/fail.png"));
        }

        /// <summary>
        /// Converts back the image source to a boolean value.
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
