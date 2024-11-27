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
    [AddINotifyPropertyChangedInterface]
    public class ManagerOrderPageViewModel : ObservableObject
    {
        private IFactory<Order> _orderFactory = new OrderFactory();
        public IBUS<Order> _orderBus;
        public ObservableCollection<Order> Orders { get; set; }


        public ManagerOrderPageViewModel()
        {
            FilterOrdersByDateCommand = new RelayCommand<DateTimeOffset>(FilterOrdersByDate);
            //ViewDetailOrderCommand = new RelayCommand<Order>(ViewDetailOrder);
            LoadData();
            
        }

        //private void ViewDetailOrder(Order order)
        //{
        //    Dictionary<string, string> configuration = new Dictionary<string, string>
        //    {
        //        { "OrderId", order.Id.ToString() }
        //    };
        //    order.Products = _orderBus.Get(configuration);
        //    NavigationService.Navigate(typeof(ViewDetailOrderPage), order);
        //}

        private void LoadData() {
            _orderBus = _orderFactory.CreateBUS();
            Orders = new ObservableCollection<Order>(_orderBus.Get(null));
        }
        public ICommand FilterOrdersByDateCommand { get; set; }
        public ICommand ViewDetailOrderCommand { get; }
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
