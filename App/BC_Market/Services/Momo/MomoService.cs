using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BC_Market.Models;
using Windows.Web.Http;
using BC_Market.Converter;
namespace BC_Market.Services
{
    /// <summary>
    /// Provides services for interacting with Momo payment API.
    /// </summary>
    public class MomoService : IMomoService
    {
        private readonly IOptions<MomoOptionModel> _options;

        /// <summary>
        /// Initializes a new instance of the <see cref="MomoService"/> class.
        /// </summary>
        /// <param name="options">The Momo options configuration.</param>
        public MomoService(IOptions<MomoOptionModel> options)
        {
            _options = options;
        }

        /// <summary>
        /// Creates a payment request to Momo API.
        /// </summary>
        /// <param name="model">The order model containing payment details.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the Momo payment response model.</returns>
        public async Task<MomoCreatePaymentResponseModel> CreatePaymentAsync(Order model)
        {
            string OrderInfo = "Nội dung: " + model.GetOrderInfo();
            var rawData =
                $"partnerCode={_options.Value.PartnerCode}" +
                $"&accessKey={_options.Value.AccessKey}" +
                $"&requestId={model.Id}" +
                $"&amount={CurrencyConverter.ConvertUsdToVnd(model.totalPrice)}" +
                $"&orderId={model.Id}" +
                $"&orderInfo={OrderInfo}" +
                $"&returnUrl={_options.Value.ReturnUrl}" +
                $"&notifyUrl={_options.Value.NotifyUrl}" +
                $"&extraData=";

            var signature = ComputeHmacSha256(rawData, _options.Value.SecretKey);

            var client = new RestClient(_options.Value.MomoApiUrl);
            var request = new RestRequest() { Method = Method.Post };
            request.AddHeader("Content-Type", "application/json; charset=UTF-8");

            // Create an object representing the request data
            var requestData = new
            {
                accessKey = _options.Value.AccessKey,
                partnerCode = _options.Value.PartnerCode,
                requestType = _options.Value.RequestType,
                notifyUrl = _options.Value.NotifyUrl,
                returnUrl = _options.Value.ReturnUrl,
                orderId = model.Id,
                amount = CurrencyConverter.ConvertUsdToVnd(model.totalPrice).ToString(),
                orderInfo = OrderInfo,
                requestId = model.Id,
                extraData = "",
                signature = signature
            };
            request.AddParameter("application/json", JsonConvert.SerializeObject(requestData), ParameterType.RequestBody);

            var response = await client.ExecuteAsync(request);
            var momoResponse = JsonConvert.DeserializeObject<MomoCreatePaymentResponseModel>(response.Content);
            return momoResponse;
        }

        /// <summary>
        /// Computes the HMAC SHA-256 hash of the given message using the specified secret key.
        /// </summary>
        /// <param name="message">The message to hash.</param>
        /// <param name="secretKey">The secret key to use for hashing.</param>
        /// <returns>The computed hash as a hexadecimal string.</returns>
        private string ComputeHmacSha256(string message, string secretKey)
        {
            var keyBytes = Encoding.UTF8.GetBytes(secretKey);
            var messageBytes = Encoding.UTF8.GetBytes(message);

            byte[] hashBytes;

            using (var hmac = new HMACSHA256(keyBytes))
            {
                hashBytes = hmac.ComputeHash(messageBytes);
            }

            var hashString = BitConverter.ToString(hashBytes).Replace("-", "").ToLower();

            return hashString;
        }

        /// <summary>
        /// Queries the transaction status from Momo API.
        /// </summary>
        /// <param name="orderId">The order identifier.</param>
        /// <param name="requestId">The request identifier.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the Momo transaction status response model.</returns>
        public async Task<MomoQueryTransactionStatusResponseModel> QueryTransactionStatusAsync(string orderId, string requestId)
        {
            // Data needed to compute the signature
            var rawData =
                $"accessKey={_options.Value.AccessKey}" +
                $"&orderId={orderId}" +
                $"&partnerCode={_options.Value.PartnerCode}" +
                $"&requestId={requestId}";

            // Compute HMAC_SHA256 signature
            var signature = ComputeHmacSha256(rawData, _options.Value.SecretKey);

            // Create RestClient and RestRequest
            var client = new RestClient("https://test-payment.momo.vn");
            var request = new RestRequest("/v2/gateway/api/query", Method.Post);
            request.AddHeader("Content-Type", "application/json; charset=UTF-8");
            Debug.WriteLine(request.ToString());

            // Data to send
            var requestData = new
            {
                partnerCode = _options.Value.PartnerCode,
                requestId = requestId,
                orderId = orderId,
                lang = "vi",
                signature = signature
            };

            // Send request
            request.AddParameter("application/json", JsonConvert.SerializeObject(requestData), ParameterType.RequestBody);
            Console.WriteLine(JsonConvert.SerializeObject(requestData)); // Debug data to send

            var response = await client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                // Parse response data
                var momoResponse = JsonConvert.DeserializeObject<MomoQueryTransactionStatusResponseModel>(response.Content);
                return momoResponse;
            }
            else
            {
                throw new Exception($"Error from Momo API: {response.StatusCode}, {response.Content}");
            }
        }
    }

}
