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
    /// <summary>
    /// ViewModel for managing payment methods.
    /// </summary>
    class PaymentMethodViewModel : ObservableObject
    {
        private IFactory<PaymentMethod> _factory = new PaymentMethodFactory();
        private IBUS<PaymentMethod> _bus;

        /// <summary>
        /// Gets or sets the collection of payment methods.
        /// </summary>
        public ObservableCollection<PaymentMethod> listMethod;

        /// <summary>
        /// Initializes a new instance of the <see cref="PaymentMethodViewModel"/> class.
        /// </summary>
        public PaymentMethodViewModel()
        {
            LoadData();
        }

        /// <summary>
        /// Loads the payment method data into the listMethod collection.
        /// </summary>
        public void LoadData()
        {
            _bus = _factory.CreateBUS();
            var payment = _bus.Get(null); // Get all payment methods
            listMethod = new ObservableCollection<PaymentMethod>(payment);
        }
    }
}
