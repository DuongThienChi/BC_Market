using BC_Market.Models;
using System;

namespace BC_Market.ViewModels
{
    public class OrderSuccessPageViewModel 
    {
        public Order Order { get; set; }
        public Delivery Delivery { get; set; }
        public double DiscountAmount { get; set; }
        public double Total { get; set; }
        public OrderSuccessPageViewModel()
        {

        }

    }
}
