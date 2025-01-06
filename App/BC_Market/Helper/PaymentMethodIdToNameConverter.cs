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
    /// <summary>
    /// Converts a payment method ID to a payment method name.
    /// </summary>
    public class PaymentMethodIdToNameConverter : IValueConverter
    {
        private IFactory<PaymentMethod> paymentMethodFactory = new PaymentMethodFactory();
        private IBUS<PaymentMethod> paymentMethodBus;

        /// <summary>
        /// Converts a payment method ID to a payment method name.
        /// </summary>
        /// <param name="value">The payment method ID to convert.</param>
        /// <param name="targetType">The type of the target property.</param>
        /// <param name="parameter">Optional parameter to be used in the converter logic.</param>
        /// <param name="language">The language of the conversion.</param>
        /// <returns>The payment method name corresponding to the payment method ID.</returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            int paymentMethodId = (int)value;
            string paymentMethodName = GetPaymentMethodNameById(paymentMethodId);
            return paymentMethodName;
        }

        /// <summary>
        /// Converts back the payment method name to a payment method ID.
        /// </summary>
        /// <param name="value">The value to convert back.</param>
        /// <param name="targetType">The type of the target property.</param>
        /// <param name="parameter">Optional parameter to be used in the converter logic.</param>
        /// <param name="culture">The culture of the conversion.</param>
        /// <returns>Throws NotImplementedException.</returns>
        /// <exception cref="NotImplementedException">Thrown always as this method is not implemented.</exception>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Converts back the payment method name to a payment method ID.
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
        /// Gets the payment method name by payment method ID.
        /// </summary>
        /// <param name="paymentMethodId">The payment method ID.</param>
        /// <returns>The payment method name corresponding to the payment method ID.</returns>
        private string GetPaymentMethodNameById(int paymentMethodId)
        {
            Dictionary<string, string> config = new Dictionary<string, string>
                {
                    { "paymentMethodId", paymentMethodId.ToString() }
                };
            paymentMethodBus = paymentMethodFactory.CreateBUS();
            var paymentMethod = paymentMethodBus.Get(config);
            return paymentMethod.Name;
        }
    }
}
