using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC_Market.Models
{
    /// <summary>
    /// Represents a shopping cart in the BC Market.
    /// </summary>
    public class Cart
    {
        /// <summary>
        /// Gets or sets the unique identifier for the cart.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the collection of products in the cart.
        /// </summary>
        public ObservableCollection<CartProduct> CartProducts { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier for the customer.
        /// </summary>
        public int customerId { get; set; }

        /// <summary>
        /// Gets the total count of products in the cart.
        /// </summary>
        public int count
        {
            get
            {
                int count = 0;
                foreach (var cartProduct in CartProducts)
                {
                    count += cartProduct.Quantity;
                }
                return count;
            }
        }
    }
}