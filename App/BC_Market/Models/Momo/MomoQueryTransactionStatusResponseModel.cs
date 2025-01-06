using Newtonsoft.Json;

namespace BC_Market.Models
{
    /// <summary>
    /// Represents the response model for querying the transaction status with Momo.
    /// </summary>
    public class MomoQueryTransactionStatusResponseModel
    {
        /// <summary>
        /// Gets or sets the result code of the transaction query.
        /// </summary>
        [JsonProperty("resultCode")]
        public string resultCode { get; set; }

        /// <summary>
        /// Gets or sets the message of the transaction query.
        /// </summary>
        [JsonProperty("message")]
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the transaction identifier.
        /// </summary>
        [JsonProperty("transactionId")]
        public string TransactionId { get; set; }

        /// <summary>
        /// Gets or sets the status of the transaction.
        /// </summary>
        [JsonProperty("status")]
        public string Status { get; set; }

        // Additional properties if necessary
    }


}