using Microsoft.UI.Xaml.Data;
using System;
using System.Windows.Input;

namespace BC_Market.Converters
{
    public class CommandCanExecuteConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is ICommand command) // Change to ICommand
            {
                return command.CanExecute(parameter);
            }
            return false;
            //throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
