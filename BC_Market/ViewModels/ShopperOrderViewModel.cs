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
using PropertyChanged;

namespace BC_Market.ViewModels
{
    [AddINotifyPropertyChangedInterface]
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
        public ShopperOrderViewModel()
        {
            DeleteItemCommand = new RelayCommand<CartProduct>(DeleteItem);
        }
        public ICommand DeleteItemCommand { get; }
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
        private void DeleteItem(CartProduct product)
        {
            var item = CartList.FirstOrDefault(x => x.Key.Id == product.Product.Id);
            if (item.Value > 1)
            {
                int index = CartList.IndexOf(item);
                CartList[index] = new KeyValuePair<Product, int>(item.Key, item.Value - 1);
                CartItems[CartItems.IndexOf(product)].Quantity -= 1;
            }
            else
            {
                CartList.Remove(item);
                CartItems.Remove(product);
            }
            OnPropertyChanged(nameof(TotalPrice));
            OnPropertyChanged(nameof(CartItems));
            OnPropertyChanged(nameof(CartList));

        }

        
    }
}
