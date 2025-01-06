using BC_Market.Models;
using BC_Market.ViewModels;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BC_Market.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class OrderSuccessPage : Page
    {
        public OrderSuccessPageViewModel ViewModel { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderSuccessPage"/> class.
        /// </summary>
        public OrderSuccessPage()
        {
            this.InitializeComponent();

            ViewModel = new OrderSuccessPageViewModel();
            this.DataContext = ViewModel;
        }

        /// <summary>
        /// Handles the Back to Shopping button click event to navigate back to the Shopper Dashboard page.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void BackToShopping_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ShopperDashboardPage));
        }

        /// <summary>
        /// Invoked when the Page is loaded and becomes the current source of a parent Frame.
        /// </summary>
        /// <param name="e">Event data that can be examined by overriding code. The event data is representative of the pending navigation that will load the current Page. Usually, the event data is a NavigationEventArgs.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var param = e.Parameter as dynamic;
            if (param != null)
            {
                ViewModel.Order = param.Order;
                ViewModel.Delivery = param.SelectedDelivery;
                ViewModel.Total = param.Total;
                ViewModel.DiscountAmount = param.DiscountAmount;
                ViewModel.PaymentStatus = param.PaymentStatus;
                ViewModel.Message = param.Message;
            }
        }
    }
}
