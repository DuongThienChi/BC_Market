using System;
using System.Diagnostics;
using System.Threading.Tasks;
using BC_Market.Models;
using OpenQA.Selenium.DevTools.V129.Console;

namespace BC_Market.Services
{
    /// <summary>
    /// Provides payment services using the Momo payment gateway.
    /// </summary>
    public static class PaymentService
    {
        private static IMomoService _momoService;
        private static string _orderId;
        private static string _statusMessage;
        private static string _requestId;

        /// <summary>
        /// Initializes the payment service with the specified Momo service.
        /// </summary>
        /// <param name="momoService">The Momo service to use for payments.</param>
        public static void Initialize(IMomoService momoService)
        {
            _momoService = momoService;
        }

        /// <summary>
        /// Creates a payment asynchronously for the specified order.
        /// </summary>
        /// <param name="order">The order for which to create the payment.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a boolean indicating whether the payment was created successfully.</returns>
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

        /// <summary>
        /// Gets the status message of the payment service.
        /// </summary>
        public static string StatusMessage
        {
            get => _statusMessage;
            private set => _statusMessage = value;
        }

        /// <summary>
        /// Gets the request ID of the payment service.
        /// </summary>
        public static string RequestId
        {
            get => _requestId;
            private set => _requestId = value;
        }

        /// <summary>
        /// Sets the order ID for the payment service.
        /// </summary>
        /// <param name="orderId">The order ID to set.</param>
        private static void SetOrderId(string orderId)
        {
            _orderId = orderId;
        }

        /// <summary>
        /// Generates a new request ID for the payment service.
        /// </summary>
        private static void GenerateRequestId()
        {
            _requestId = DateTime.UtcNow.Ticks.ToString();
        }

        /// <summary>
        /// Starts polling the Momo service for the transaction status asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The task result contains a tuple with a boolean indicating success and a message.</returns>
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
