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
using BC_Market.ViewModels;
using BC_Market.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BC_Market.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ShopperOrderPage : Page
    {
        public ShopperOrderViewModel ViewModel { get; set; }
        private List<CheckBox> CheckBoxs { get; set; }
        public ShopperOrderPage()
        {
            this.InitializeComponent();
            ViewModel = new ShopperOrderViewModel();
            this.DataContext = ViewModel;
            this.CheckBoxs = new List<CheckBox>();

        }
        // Receive CartList from ShopperDashboardPage
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            //if (e.Parameter is ObservableCollection<KeyValuePair<Product, int>> cartList)
            //{
            //    ViewModel.CartList = cartList;
            //}
        }
        // Remove item from cart
        private void AddProduct_Clicked(object sender, RoutedEventArgs e)
        {
            var shopPage = new ShopperDashboardPage();
            //shopPage.ViewModel.CartList = ViewModel.CartList.ToDictionary(item => item.Key, item => item.Value);
            //var NumberProduct = 0;
            //foreach (var item in ViewModel.CartList)
            //{
            //    NumberProduct += item.Value;
            //}
            //shopPage.ViewModel.ProductInCart = NumberProduct;
            //var Params = new
            //{
            //    CartList = ViewModel.CartList,
            //    ProductInCart = NumberProduct,
            //};
            //this.Frame.Navigate(typeof(ShopperDashboardPage), Params);
            this.Frame.Navigate(typeof(ShopperDashboardPage));
        }

        // Remove item from cart
        private void itemCheckBox_Click(object sender, RoutedEventArgs e)
        {
            bool allSelected = ViewModel.cart.CartProducts.All(item => item.IsSelected);
            allCheckBox.IsChecked = allSelected;
        }



        // Select all items
        private void allCheckBox_Click(object sender, RoutedEventArgs e)
        {
            bool isChecked = allCheckBox.IsChecked ?? false;
            foreach (var item in ViewModel.cart.CartProducts)
            {
                item.IsSelected = isChecked;
            }
        }
        private void VoucherButton_Click(object sender, RoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout(VoucherStackPanel);
        }

        private void VoucherSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                var selectedVoucher = (Voucher)e.AddedItems[0];
                if (selectedVoucher.isCondition(ViewModel.Total))
                {
                    SelectedVoucherTextBlock.Text = selectedVoucher.Name;
                    ViewModel.selectedVoucher = selectedVoucher;
                    // Close the flyout after selection
                    FlyoutBase.GetAttachedFlyout(VoucherStackPanel).Hide();
                }
                else
                {
                    // Show a message or handle the case where the condition is not met
                    var dialog = new ContentDialog
                    {
                        Title = "Voucher Selection",
                        Content = "Total amount must be greater than the voucher condition.",
                        CloseButtonText = "OK",
                        XamlRoot = ((FrameworkElement)sender).XamlRoot // Set the XamlRoot property
                    };
                    _ = dialog.ShowAsync();
                }
            }
        }
        private void DeliveryButton_Click(object sender, RoutedEventArgs e)
        {
            
             FlyoutBase.ShowAttachedFlyout(DeliveryStackPanel);
        }

        private void DeliverySelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                var selectedDelivery = (Delivery)e.AddedItems[0];
                SelectedDeliveryTextBlock.Text = selectedDelivery.Name;
                ViewModel.selectedDelivery = selectedDelivery;

                // Notify that DiscountAmount has changed
                ViewModel.NotifyPropertyChanged(nameof(ViewModel.DiscountAmount));

                // Close the flyout after selection
                FlyoutBase.GetAttachedFlyout(DeliveryStackPanel).Hide();
            }
        }


    }
}
