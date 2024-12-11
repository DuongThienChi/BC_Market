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
    
    public class DeliveryIdToNameConverter : IValueConverter
    {
        private IFactory<DeliveryUnit> deliveryFactory = new DeliveryFactory();
        private IBUS<DeliveryUnit> deliveryBus;
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            int deliveryId = (int)value;
            string deliveryName = GetDeliveryNameById(deliveryId);
            return deliveryName;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }

        private string GetDeliveryNameById(int deliveryId)
        {
            Dictionary<string, string> config = new Dictionary<string, string>();
            config.Add("deliveryId", deliveryId.ToString());
            deliveryBus = deliveryFactory.CreateBUS();
            List<DeliveryUnit> DeliveryUnit = deliveryBus.Get(config);
            return DeliveryUnit.First().Name;
        }
    }
}
