using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC_Market.Models
{
    /// <summary>
    /// Represents the response model for executing a payment with Momo.
    /// </summary>
    public class MomoExecuteResponseModel
    {
        /// <summary>
        /// Gets or sets the order identifier.
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// Gets or sets the amount of the payment.
        /// </summary>
        public string Amount { get; set; }

        /// <summary>
        /// Gets or sets the full name of the payer.
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Gets or sets the order information.
        /// </summary>
        public string OrderInfo { get; set; }
    }

}
