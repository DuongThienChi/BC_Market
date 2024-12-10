using System;
using System.Globalization;
using BC_Market.Models; // Assuming you have a Customer model and a method to get username by ID
using BC_Market.BUS;
using BC_Market.Factory;
using System.Collections.Generic;
using Microsoft.UI.Xaml.Data;

namespace BC_Market.Converter
{
    public class CustomerIdToUsernameConverter : IValueConverter
    {
        private IFactory<USER> _userFactory = new UserFactory();
        private IBUS<USER> _userBUS;
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            if (value == null)
                return string.Empty;

            string customerId = value.ToString();
            // Assuming you have a method to get the username by customer ID
            string username = GetUsernameByCustomerId(customerId);
            return username;
        }
        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
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
