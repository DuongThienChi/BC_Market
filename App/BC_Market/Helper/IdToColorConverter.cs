using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC_Market.Converter
{
    public class IdToColorConverter : IValueConverter // Convert the id of item to color
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            //if (value == null)
            //{
            //    return null;
            //}
            //if (value is string id)
            //{
            //    return "#0f0f0f";
            //    int idInt = int.Parse(id);

            //    if (idInt % 2 == 0)
            //    {
            //        return "#292C2D";
            //    }
            //    else
            //    {
            //        return "#3D4142";
            //    }
            //}
            return "#888888";
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
