using BC_Market.Models;
using System;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BC_Market.ViewModels
{
    /// <summary>
    /// ViewModel for the order success page.
    /// </summary>
    public class OrderSuccessPageViewModel : ObservableObject
    {
        /// <summary>
        /// Gets or sets the order details.
        /// </summary>
        public Order Order { get; set; }

        /// <summary>
        /// Gets or sets the delivery unit details.
        /// </summary>
        public DeliveryUnit Delivery { get; set; }

        /// <summary>
        /// Gets or sets the discount amount.
        /// </summary>
        public double DiscountAmount { get; set; }

        /// <summary>
        /// Gets or sets the total amount.
        /// </summary>
        public double Total { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderSuccessPageViewModel"/> class.
        /// </summary>
        public OrderSuccessPageViewModel()
        {
        }

        private bool _paymentStatus;

        /// <summary>
        /// Gets or sets the payment status.
        /// </summary>
        public bool PaymentStatus
        {
            get => _paymentStatus;
            set => SetProperty(ref _paymentStatus, value);
        }

        private string _message;

        /// <summary>
        /// Gets or sets the message to be displayed.
        /// </summary>
        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }
    }
}
