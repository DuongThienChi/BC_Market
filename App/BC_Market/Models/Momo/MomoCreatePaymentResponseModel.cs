using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC_Market.Models
{
    /// <summary>
    /// Represents the response model for creating a payment with Momo.
    /// </summary>
    public class MomoCreatePaymentResponseModel
    {
        /// <summary>
        /// Gets or sets the request identifier.
        /// </summary>
        public string RequestId { get; set; }

        /// <summary>
        /// Gets or sets the error code.
        /// </summary>
        public int ErrorCode { get; set; }

        /// <summary>
        /// Gets or sets the order identifier.
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the local message.
        /// </summary>
        public string LocalMessage { get; set; }

        /// <summary>
        /// Gets or sets the request type.
        /// </summary>
        public string RequestType { get; set; }

        /// <summary>
        /// Gets or sets the payment URL.
        /// </summary>
        public string PayUrl { get; set; }

        /// <summary>
        /// Gets or sets the signature.
        /// </summary>
        public string Signature { get; set; }

        /// <summary>
        /// Gets or sets the QR code URL.
        /// </summary>
        public string QrCodeUrl { get; set; }

        /// <summary>
        /// Gets or sets the deeplink.
        /// </summary>
        public string Deeplink { get; set; }

        /// <summary>
        /// Gets or sets the deeplink for web in app.
        /// </summary>
        public string DeeplinkWebInApp { get; set; }
    }

}
