using System;
using System.Globalization;
using System.Windows;
using BC_Market.Models;
using BC_Market.BUS;
using BC_Market.Factory;
using Microsoft.UI.Xaml.Data;
using System.Collections.Generic;
using System.Linq;

namespace BC_Market.Converter
{
    
    /// <summary>
    /// Converts a delivery ID to a delivery name.
    /// </summary>
    public class DeliveryIdToNameConverter : IValueConverter
    {
        private IFactory<DeliveryUnit> deliveryFactory = new DeliveryFactory();
        private IBUS<DeliveryUnit> deliveryBus;

        /// <summary>
        /// Converts a delivery ID to a delivery name.
        /// </summary>
        /// <param name="value">The delivery ID to convert.</param>
        /// <param name="targetType">The type of the target property.</param>
        /// <param name="parameter">Optional parameter to be used in the converter logic.</param>
        /// <param name="language">The language of the conversion.</param>
        /// <returns>The delivery name corresponding to the delivery ID.</returns>
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            int deliveryId = (int)value;
            string deliveryName = GetDeliveryNameById(deliveryId);
            return deliveryName;
        }

        /// <summary>
        /// Converts back the delivery name to a delivery ID.
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
        /// Converts back the delivery name to a delivery ID.
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
        /// Gets the delivery name by delivery ID.
        /// </summary>
        /// <param name="deliveryId">The delivery ID.</param>
        /// <returns>The delivery name corresponding to the delivery ID.</returns>
        private string GetDeliveryNameById(int deliveryId)
        {
            Dictionary<string, string> config = new Dictionary<string, string>
                {
                    { "deliveryId", deliveryId.ToString() }
                };
            deliveryBus = deliveryFactory.CreateBUS();
            List<DeliveryUnit> deliveryUnits = deliveryBus.Get(config);
            return deliveryUnits.First().Name;
        }
    }
}
