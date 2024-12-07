using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Media.Imaging;
using System;

namespace BC_Market.Converter
{
    public class BooleanToImageConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is bool paymentStatus)
            {
                return paymentStatus ? new BitmapImage(new Uri("ms-appx:///Assets/Images/success.png")) : new BitmapImage(new Uri("ms-appx:///Assets/Images/fail.png"));
            }
            return new BitmapImage(new Uri("ms-appx:///Assets/Images/fail.png"));
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
