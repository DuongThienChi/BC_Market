using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BC_Market.Models;

namespace BC_Market.Services
{
    /// <summary>
    /// Defines the interface for Momo payment services.
    /// </summary>
    public interface IMomoService
    {
        /// <summary>
        /// Creates a payment request to Momo API.
        /// </summary>
        /// <param name="model">The order model containing payment details.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the Momo payment response model.</returns>
        Task<MomoCreatePaymentResponseModel> CreatePaymentAsync(Order model);

        // /// <summary>
        // /// Executes the payment with the provided query collection.
        // /// </summary>
        // /// <param name="collection">The query collection containing payment details.</param>
        // /// <returns>The Momo execute response model.</returns>
        // MomoExecuteResponseModel PaymentExecuteAsync(IQueryCollection collection);

        /// <summary>
        /// Queries the transaction status from Momo API.
        /// </summary>
        /// <param name="orderId">The order identifier.</param>
        /// <param name="requestId">The request identifier.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains the Momo transaction status response model.</returns>
        Task<MomoQueryTransactionStatusResponseModel> QueryTransactionStatusAsync(string orderId, string requestId);
    }
}
