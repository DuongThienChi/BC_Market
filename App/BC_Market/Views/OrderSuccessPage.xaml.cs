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
        public OrderSuccessPage()
        {
            this.InitializeComponent();

            ViewModel = new OrderSuccessPageViewModel();
            this.DataContext = ViewModel;
        }

        private void BackToShopping_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(ShopperDashboardPage));
        }
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
            }
        }
    }
}
