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
    /// <summary>
    /// Represents a product in the shopping cart.
    /// </summary>
    public class CartProduct : ObservableObject
    {
        /// <summary>
        /// Gets or sets the product.
        /// </summary>
        public Product Product { get; set; }

        /// <summary>
        /// Gets or sets the quantity of the product.
        /// </summary>
        public int Quantity { get; set; } = 0;

        /// <summary>
        /// Gets the total price of the product based on the quantity.
        /// </summary>
        public double TotalPrice
        {
            get
            {
                return Product.Price * Quantity;
            }
        }

        private bool _isSelected = false;

        /// <summary>
        /// Gets or sets a value indicating whether the product is selected.
        /// </summary>
        public bool IsSelected
        {
            get => _isSelected;
            set => SetProperty(ref _isSelected, value);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CartProduct"/> class.
        /// </summary>
        public CartProduct()
        {
            // Constructor for CartProduct
        }
    }
}
