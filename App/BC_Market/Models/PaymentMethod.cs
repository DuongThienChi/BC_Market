using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC_Market.Models
{
    /// <summary>
    /// Represents a payment method in the market.
    /// </summary>
    public class PaymentMethod
    {
        /// <summary>
        /// Gets or sets the unique identifier for the payment method.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the payment method.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the payment method.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the image path for the payment method.
        /// </summary>
        public string ImagePath { get; set; }

        /// <summary>
        /// Gets or sets the status of the payment method.
        /// </summary>
        public string Status { get; set; }
    }
}
