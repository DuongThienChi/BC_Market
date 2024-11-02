using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BC_Market.Models;
using CommunityToolkit.Mvvm.ComponentModel;
namespace BC_Market.Models
{
    public class CartProduct : ObservableObject
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }

        public double TotalPrice
        {
            get
            {
                return Product.Price * Quantity;
            }
        }
        private bool _isSelected = false;
        public bool IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        }

    }
}
