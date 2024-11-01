using BC_Market.ViewModels;
using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC_Market.Converter
{
    internal class CateNameToQuantityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
            {
                return null;
            }
            if (value is string id)
            {
                if (parameter is ManageProductViewModel)
                {
                    var ViewModel = parameter as ManageProductViewModel;
                    int sum = 0;
                    foreach (var item in ViewModel.ListProduct)
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

    public object ConvertBack(object value, Type targetType, object parameter, string language)
    {
        throw new NotImplementedException();
    }
}
}
