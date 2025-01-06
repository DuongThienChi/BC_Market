using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC_Market.Models
{
    /// <summary>
    /// Represents the options model for Momo payment configuration.
    /// </summary>
    public class MomoOptionModel
    {
        /// <summary>
        /// Gets or sets the Momo API URL.
        /// </summary>
        public string MomoApiUrl { get; set; }

        /// <summary>
        /// Gets or sets the secret key for Momo API.
        /// </summary>
        public string SecretKey { get; set; }

        /// <summary>
        /// Gets or sets the access key for Momo API.
        /// </summary>
        public string AccessKey { get; set; }

        /// <summary>
        /// Gets or sets the return URL for Momo payment.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        /// Gets or sets the notify URL for Momo payment.
        /// </summary>
        public string NotifyUrl { get; set; }

        /// <summary>
        /// Gets or sets the partner code for Momo API.
        /// </summary>
        public string PartnerCode { get; set; }

        /// <summary>
        /// Gets or sets the request type for Momo API.
        /// </summary>
        public string RequestType { get; set; }
    }

}
