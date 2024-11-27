using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BC_Market.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using Windows.Security.Cryptography.Core;
namespace BC_Market.Models
{
    public class CartProduct : ObservableObject // Define the CartProduct class
    {
        public Product Product { get; set; }
        public int Quantity { get; set; } = 0;

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
        // Constructor for CartProduct
    }
}
