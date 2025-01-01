using System;
using System.Globalization;
using BC_Market.Models;
using Microsoft.UI.Xaml.Data;

namespace BC_Market.Converters
{
    /// <summary>
    /// Converts a voucher to a descriptive string.
    /// </summary>
    public class VoucherDescriptionConverter : IValueConverter
    {
        /// <summary>
        /// Converts a voucher to a descriptive string.
        /// </summary>
        /// <param name="value">The voucher to convert.</param>
        /// <param name="targetType">The type of the target property.</param>
        /// <param name="parameter">Optional parameter to be used in the converter logic.</param>
        /// <param name="language">The language of the conversion.</param>
        /// <returns>A descriptive string based on the voucher's properties.</returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            var voucher = value as Voucher;
            if (voucher == null) return string.Empty;

            if (voucher.Percent > 0)
            {
                return $"With Order more than ${voucher.Condition}, We will decrease {voucher.Percent}%";
            }
            else if (voucher.Amount > 0)
            {
                return $"With Order more than ${voucher.Condition}, We will decrease ${voucher.Amount}";
            }
            return string.Empty;
        }

        /// <summary>
        /// Converts back the descriptive string to a voucher.
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
