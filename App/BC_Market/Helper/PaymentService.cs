using System;
using System.Diagnostics;
using System.Threading.Tasks;
using BC_Market.Models;
using OpenQA.Selenium.DevTools.V129.Console;

namespace BC_Market.Services
{
    public static class PaymentService
    {
        private static IMomoService _momoService;
        private static string _orderId;
        private static string _statusMessage;
        private static string _requestId;

        public static void Initialize(IMomoService momoService)
        {
            _momoService = momoService;
        }

        public static async Task<bool> CreatePaymentAsync(Order order)
        {
            order.Id = DateTime.UtcNow.Ticks.ToString();
            SetOrderId(order.Id);
            var response = await _momoService.CreatePaymentAsync(order);

            if (response != null)
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = response.PayUrl,
                    UseShellExecute = true
                });
                return true;
            }
            else
            {
                return false;
            }
        }

        public static string StatusMessage
        {
            get => _statusMessage;
            private set => _statusMessage = value;
        }

        public static string RequestId
        {
            get => _requestId;
            private set => _requestId = value;
        }

        private static void SetOrderId(string orderId)
        {
            _orderId = orderId;
        }

        private static void GenerateRequestId()
        {
            _requestId = DateTime.UtcNow.Ticks.ToString();
        }

        public static async Task<Tuple<bool, string>> StartPollingAsync()
        {
            if (string.IsNullOrEmpty(_orderId))
            {
                return Tuple.Create(false, "Order Id is not set.");
            }

            try
            {
                while (true)
                {
                    GenerateRequestId();
                    var response = await _momoService.QueryTransactionStatusAsync(_orderId, _requestId);
                    if (response != null)
                    {
                        StatusMessage = $"Status: {response.resultCode}, Message: {response.Message}";
                        if (response.resultCode != "1000" && response.resultCode != "7000" 
                            && response.resultCode != "7002" && response.resultCode != "9000")
                        {
                            if (response.resultCode == "0")
                            {
                                return Tuple.Create(true, response.Message);
                            }
                            else
                            {
                                return Tuple.Create(false, response.Message);
                            }
                        }
                    }
                    await Task.Delay(2000);
                }
            }
            catch (Exception ex)
            {
                StatusMessage = $"Error: {ex.Message}";
                return Tuple.Create(false, ex.Message);
            }
        }
    }
}
