using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC_Market.Models
{
    /// <summary>
    /// Represents a product in the market.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Gets or sets the unique identifier for the product.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the product.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the product.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the price of the product.
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// Gets or sets the stock quantity of the product.
        /// </summary>
        public int Stock { get; set; }

        /// <summary>
        /// Gets or sets the category identifier for the product.
        /// </summary>
        public string CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the image path for the product.
        /// </summary>
        public string ImagePath { get; set; }

        /// <summary>
        /// Gets or sets the status of the product.
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets the order quantity of the product.
        /// </summary>
        public int OrderQuantity { get; set; }

        /// <summary>
        /// Returns a string that represents the current product.
        /// </summary>
        /// <returns>A string that represents the current product.</returns>
        public override string ToString()
        {
            return this.Name + " x " + this.OrderQuantity;
        }

        /// <summary>
        /// Calculates the total price for the ordered quantity of the product.
        /// </summary>
        /// <returns>A string representing the total price.</returns>
        public string Total()
        {
            return (this.Price * this.OrderQuantity).ToString();
        }
    }
}
