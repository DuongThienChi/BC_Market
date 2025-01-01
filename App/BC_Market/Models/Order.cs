using Microsoft.UI.Xaml;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC_Market.Models
{
    /// <summary>
    /// Represents an order in the market.
    /// </summary>
    public class Order
    {
        /// <summary>
        /// Gets or sets the unique identifier for the order.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the collection of products in the order.
        /// </summary>
        public ObservableCollection<CartProduct> Products { get; set; }

        /// <summary>
        /// Gets or sets the customer identifier.
        /// </summary>
        public int customerId { get; set; }

        /// <summary>
        /// Gets or sets the delivery identifier.
        /// </summary>
        public int deliveryId { get; set; }

        /// <summary>
        /// Gets or sets the total price of the order.
        /// </summary>
        public float totalPrice { get; set; }

        /// <summary>
        /// Gets or sets the address for the order delivery.
        /// </summary>
        public string address { get; set; }

        /// <summary>
        /// Gets or sets the payment method for the order.
        /// </summary>
        public int paymentMethod { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the order is paid.
        /// </summary>
        public Boolean isPaid { get; set; }

        /// <summary>
        /// Gets or sets the creation date and time of the order.
        /// </summary>
        public DateTime createAt { get; set; }

        /// <summary>
        /// Gets the order information as a formatted string.
        /// </summary>
        /// <returns>A string containing the order information.</returns>
        public string GetOrderInfo()
        {
            StringBuilder orderInfo = new StringBuilder();
            foreach (var product in Products)
            {
                orderInfo.AppendLine($"Product: {product.Product.Name}, Quantity: {product.Quantity}");
            }
            return orderInfo.ToString();
        }
    }
}