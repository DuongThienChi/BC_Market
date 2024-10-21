using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using BC_Market.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;

namespace BC_Market.ViewModels
{
    public class ShopperOrderViewModel : ObservableObject
    {
        private ObservableCollection<KeyValuePair<Product, int>> _carts;

        public ObservableCollection<KeyValuePair<Product, int>> CartList
        {
            get => _carts;
            set
            {
                if (SetProperty(ref _carts, value))
                {
                    CartItems = new ObservableCollection<CartProduct>(
                       CartList.Select(x => new CartProduct { Product = x.Key, Quantity = x.Value })
                   );
                }
            }
        }
  
        public ObservableCollection<CartProduct> CartItems { get; set; }
        public double TotalPrice
        {
            get
            {
                double total = 0;
                foreach (var item in CartList)
                {
                    total += item.Key.Price * item.Value;
                }
                return total;
            }
        }

        public ShopperOrderViewModel()
        {
           
        }
    }
}
