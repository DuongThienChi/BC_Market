using System;
using System.Globalization;
using BC_Market.Models; // Assuming you have a Customer model and a method to get username by ID
using BC_Market.BUS;
using BC_Market.Factory;
using System.Collections.Generic;
using Microsoft.UI.Xaml.Data;

namespace BC_Market.Converter
{
    /// <summary>
    /// Converts a customer ID to a username.
    /// </summary>
    public class CustomerIdToUsernameConverter : IValueConverter
    {
        private IFactory<USER> _userFactory = new UserFactory();
        private IBUS<USER> _userBUS;

        /// <summary>
        /// Converts a customer ID to a username.
        /// </summary>
        /// <param name="value">The customer ID to convert.</param>
        /// <param name="targetType">The type of the target property.</param>
        /// <param name="parameter">Optional parameter to be used in the converter logic.</param>
        /// <param name="language">The language of the conversion.</param>
        /// <returns>The username corresponding to the customer ID.</returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
                return string.Empty;

            string customerId = value.ToString();
            // Assuming you have a method to get the username by customer ID
            string username = GetUsernameByCustomerId(customerId);
            return username;
        }

        /// <summary>
        /// Converts back the username to a customer ID.
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

        /// <summary>
        /// Gets the username by customer ID.
        /// </summary>
        /// <param name="customerId">The customer ID.</param>
        /// <returns>The username corresponding to the customer ID.</returns>
        private string GetUsernameByCustomerId(string customerId)
        {
            _userBUS = _userFactory.CreateBUS();
            USER user = _userBUS.Get(new Dictionary<string, string> { { "customerId", customerId } });
            if (user != null)
            {
                return user.Username;
            }
            return "exampleUsername";
        }
    }
}
