using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BC_Market.Models;
using BC_Market.BUS;
using BC_Market.Factory;
using CommunityToolkit.Mvvm.ComponentModel;
using PropertyChanged;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using BC_Market.Services;
using BC_Market.Views;

namespace BC_Market.ViewModels
{
    /// <summary>
    /// ViewModel for managing orders in the admin panel.
    /// </summary>
    [AddINotifyPropertyChangedInterface]
    public class ManagerOrderPageViewModel : ObservableObject
    {
        private IFactory<Order> _orderFactory = new OrderFactory();
        public IBUS<Order> _orderBus;

        /// <summary>
        /// Gets or sets the collection of orders.
        /// </summary>
        public ObservableCollection<Order> Orders { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ManagerOrderPageViewModel"/> class.
        /// </summary>
        public ManagerOrderPageViewModel()
        {
            FilterOrdersByDateCommand = new RelayCommand<DateTimeOffset>(FilterOrdersByDate);
            LoadData();
        }

        /// <summary>
        /// Loads the order data into the Orders collection.
        /// </summary>
        private void LoadData()
        {
            _orderBus = _orderFactory.CreateBUS();
            Orders = new ObservableCollection<Order>(_orderBus.Get(null));
        }

        /// <summary>
        /// Gets or sets the command to filter orders by date.
        /// </summary>
        public ICommand FilterOrdersByDateCommand { get; set; }

        /// <summary>
        /// Gets or sets the command to view order details.
        /// </summary>
        public ICommand ViewDetailOrderCommand { get; }

        /// <summary>
        /// Filters the orders by the specified date.
        /// </summary>
        /// <param name="selectedDate">The date to filter orders by.</param>
        public void FilterOrdersByDate(DateTimeOffset selectedDate)
        {
            Dictionary<string, string> configuration = new Dictionary<string, string>
                {
                    { "date", selectedDate.ToString() }
                };

            var filteredOrders = _orderBus.Get(configuration);

            Orders.Clear();
            foreach (var order in filteredOrders)
            {
                Orders.Add(order);
            }
        }
    }
}
