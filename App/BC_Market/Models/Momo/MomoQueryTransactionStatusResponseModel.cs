using Newtonsoft.Json;

namespace BC_Market.Models
{
    public class MomoQueryTransactionStatusResponseModel
    {
        [JsonProperty("resultCode")]
        public string resultCode { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("transactionId")]
        public string TransactionId { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        // Các thuộc tính khác nếu cần thiết
    }


}