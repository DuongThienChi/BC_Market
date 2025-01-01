using Microsoft.UI.Xaml.Data;
using System;
using System.Windows.Input;

namespace BC_Market.Converters
{
    /// <summary>
    /// Converts an ICommand to a boolean indicating whether the command can execute.
    /// </summary>
    public class CommandCanExecuteConverter : IValueConverter
    {
        /// <summary>
        /// Converts an ICommand to a boolean indicating whether the command can execute.
        /// </summary>
        /// <param name="value">The ICommand to check.</param>
        /// <param name="targetType">The type of the target property.</param>
        /// <param name="parameter">Optional parameter to be used in the command execution check.</param>
        /// <param name="language">The language of the conversion.</param>
        /// <returns>A boolean indicating whether the command can execute.</returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value is ICommand command)
            {
                return command.CanExecute(parameter);
            }
            return false;
        }

        /// <summary>
        /// Converts back the boolean to an ICommand.
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
