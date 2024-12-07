using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BC_Market.Models;

namespace BC_Market.Services
{
    public interface IMomoService
    {
        Task<MomoCreatePaymentResponseModel> CreatePaymentAsync(Order model);
       // MomoExecuteResponseModel PaymentExecuteAsync(IQueryCollection collection);
        Task<MomoQueryTransactionStatusResponseModel> QueryTransactionStatusAsync(string orderId, string requestId);
    }
}
