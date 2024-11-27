using BC_Market.BUS;
using BC_Market.Factory;
using BC_Market.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC_Market.ViewModels
{
    class PaymentMethodViewModel : ObservableObject
    {
        private IFactory<PaymentMethod> _factory = new PaymentMethodFactory();
        private IBUS<PaymentMethod> _bus;
        public ObservableCollection<PaymentMethod> listMethod;

        public PaymentMethodViewModel()
        {
            LoadData();
        }

        public void LoadData() // Define the LoadData method
        {
            _bus = _factory.CreateBUS();

            var payment = _bus.Get(null); // Get all users

            listMethod = new ObservableCollection<PaymentMethod>(payment);
        }
    }
}
