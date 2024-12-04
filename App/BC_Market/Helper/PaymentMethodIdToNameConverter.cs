using System;
using System.Globalization;
using System.Windows;
using BC_Market.Models;
using BC_Market.BUS;
using BC_Market.Factory;
using Microsoft.UI.Xaml.Data;
using System.Collections.Generic;

namespace BC_Market.Converter
{
    public class PaymentMethodIdToNameConverter : IValueConverter
    {
        private IFactory<PaymentMethod> paymentMethodFactory = new PaymentMethodFactory();
        private IBUS<PaymentMethod> paymentMethodBus;

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            int paymentMethodId = (int)value;
            string paymentMethodName = GetPaymentMethodNameById(paymentMethodId);
            return paymentMethodName;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }

        private string GetPaymentMethodNameById(int paymentMethodId)
        {
            Dictionary<string, string> config = new Dictionary<string, string>();
            config.Add("paymentMethodId", paymentMethodId.ToString());
            paymentMethodBus = paymentMethodFactory.CreateBUS();
            var paymentMethod = paymentMethodBus.Get(config);
            return paymentMethod.Name;
        }
    }
}
