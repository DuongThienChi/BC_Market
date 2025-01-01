using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC_Market.Models
{
    /// <summary>
    /// Represents a delivery unit in the market.
    /// </summary>
    public class DeliveryUnit
    {
        /// <summary>
        /// Gets or sets the unique identifier for the delivery unit.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the delivery unit.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the price of the delivery unit.
        /// </summary>
        public float Price { get; set; }
    }
}
