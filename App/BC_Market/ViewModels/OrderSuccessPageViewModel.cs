using BC_Market.Models;
using System;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BC_Market.ViewModels
{
    public class OrderSuccessPageViewModel : ObservableObject
    {
        public Order Order { get; set; }
        public DeliveryUnit Delivery { get; set; }
        public double DiscountAmount { get; set; }
        public double Total { get; set; }
        public OrderSuccessPageViewModel()
        {

        }
        private bool _paymentStatus;
        public bool PaymentStatus
        {
            get => _paymentStatus;
            set => SetProperty(ref _paymentStatus, value);
        }

        private string _message;
        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }


    }
}
