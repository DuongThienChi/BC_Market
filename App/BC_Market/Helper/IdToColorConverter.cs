using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC_Market.Converter
{
    /// <summary>
    /// Converts the ID of an item to a color.
    /// </summary>
    public class IdToColorConverter : IValueConverter
    {
        /// <summary>
        /// Converts an ID to a color.
        /// </summary>
        /// <param name="value">The ID value to convert.</param>
        /// <param name="targetType">The type of the target property.</param>
        /// <param name="parameter">Optional parameter to be used in the converter logic.</param>
        /// <param name="language">The language of the conversion.</param>
        /// <returns>A color corresponding to the ID.</returns>
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

        /// <summary>
        /// Converts back the color to an ID.
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
