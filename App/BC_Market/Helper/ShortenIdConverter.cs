using System;
using Microsoft.UI.Xaml.Data;

namespace BC_Market.Converter
{
    public class ShortenIdConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is string id && id.Length > 8)
            {
                return id.Substring(0, 8) + ".....";
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
