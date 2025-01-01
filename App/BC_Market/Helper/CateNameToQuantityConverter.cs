using BC_Market.ViewModels;
using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC_Market.Converter
{
    /// <summary>
    /// Converts the name of a category to the quantity of products in that category.
    /// </summary>
    internal class CateNameToQuantityConverter : IValueConverter
    {
        /// <summary>
        /// Converts the category ID to the quantity of products in that category.
        /// </summary>
        /// <param name="value">The category ID.</param>
        /// <param name="targetType">The type of the target property.</param>
        /// <param name="parameter">The ViewModel containing the list of products.</param>
        /// <param name="language">The language of the conversion.</param>
        /// <returns>The quantity of products in the category.</returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
            {
                return null;
            }
            if (value is int id)
            {
                if (parameter is ManageProductViewModel viewModel)
                {
                    int sum = 0;
                    foreach (var item in viewModel.ListProduct)
                    {
                        if (item.Id == id)
                        {
                            sum++;
                        }
                    }
                    return sum;
                }
            }
            return 0;
        }

        /// <summary>
        /// Converts back the quantity of products to the category ID.
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
