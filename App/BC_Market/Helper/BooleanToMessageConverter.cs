using Microsoft.UI.Xaml.Data;
using System;

namespace BC_Market.Converter
{
    public class BooleanToMessageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is bool paymentStatus)
            {
                return paymentStatus ? "Your order will be delivered to you soon. Thank you for shopping with us!" : "Payment failed. Please try again.";
            }
            return "Payment failed. Please try again.";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
