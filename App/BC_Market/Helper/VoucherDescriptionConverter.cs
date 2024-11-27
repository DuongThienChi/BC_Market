using System;
using System.Globalization;
using BC_Market.Models;
using Microsoft.UI.Xaml.Data;

namespace BC_Market.Converters
{
    public class VoucherDescriptionConverter : IValueConverter
    {
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
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
