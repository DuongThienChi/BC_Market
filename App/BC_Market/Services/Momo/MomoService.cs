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
    public class MomoService : IMomoService
    {
        private readonly IOptions<MomoOptionModel> _options;

        public MomoService(IOptions<MomoOptionModel> options)
        {
            _options = options;

        }
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
        //public MomoExecuteResponseModel PaymentExecuteAsync(IQueryCollection collection)
        //{
        //    var amount = collection.First(s => s.Key == "amount").Value;
        //    var orderInfo = collection.First(s => s.Key == "orderInfo").Value;
        //    var orderId = collection.First(s => s.Key == "orderId").Value;

        //    return new MomoExecuteResponseModel()
        //    {
        //        Amount = amount,
        //        OrderId = orderId,
        //        OrderInfo = orderInfo

        //    };
        //}

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
        public async Task<MomoQueryTransactionStatusResponseModel> QueryTransactionStatusAsync(string orderId, string requestId)
        {
            // Dữ liệu cần để tính signature
            var rawData =
                $"accessKey={_options.Value.AccessKey}" +
                $"&orderId={orderId}" +
                $"&partnerCode={_options.Value.PartnerCode}" +
                $"&requestId={requestId}";

            // Tính chữ ký Hmac_SHA256
            var signature = ComputeHmacSha256(rawData, _options.Value.SecretKey);

            // Tạo RestClient và RestRequest
            var client = new RestClient("https://test-payment.momo.vn");
            var request = new RestRequest("/v2/gateway/api/query", Method.Post);
            request.AddHeader("Content-Type", "application/json; charset=UTF-8");
            Debug.WriteLine(request.ToString());
            // Dữ liệu gửi đi
            var requestData = new
            {
                partnerCode = _options.Value.PartnerCode,
                requestId = requestId,
                orderId = orderId,
                lang = "vi",
                signature = signature
            };

            // Gửi yêu cầu
            request.AddParameter("application/json", JsonConvert.SerializeObject(requestData), ParameterType.RequestBody);
            Console.WriteLine(JsonConvert.SerializeObject(requestData)); // Debug dữ liệu gửi đi

            var response = await client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                // Parse dữ liệu trả về
                var momoResponse = JsonConvert.DeserializeObject<MomoQueryTransactionStatusResponseModel>(response.Content);
                //Console.WriteLine(response.Content);
                return momoResponse;
            }
            else
            {
                throw new Exception($"Error from Momo API: {response.StatusCode}, {response.Content}");
            }
        }


    }

}
