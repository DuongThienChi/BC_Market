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
using BC_Market.Services;
// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BC_Market.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ShopperOrderPage : Page
    {
        /// <summary>
        /// Gets or sets the ViewModel for the ShopperOrderPage.
        /// </summary>
        public ShopperOrderViewModel ViewModel { get; set; }

        /// <summary>
        /// Gets or sets the list of CheckBox controls.
        /// </summary>
        private List<CheckBox> CheckBoxs { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShopperOrderPage"/> class.
        /// </summary>
        public ShopperOrderPage()
        {
            this.InitializeComponent();
            ViewModel = new ShopperOrderViewModel();
            this.DataContext = ViewModel;
            this.CheckBoxs = new List<CheckBox>();
        }

        /// <summary>
        /// Invoked when the Page is loaded and becomes the current source of a parent Frame.
        /// </summary>
        /// <param name="e">Event data that can be examined by overriding code. The event data is representative of the pending navigation that will load the current Page. Usually, the event data is a NavigationEventArgs.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
        }

        /// <summary>
        /// Handles the Add Product button click event to navigate back to the ShopperDashboardPage.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void AddProduct_Clicked(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ShopperDashboardPage));
        }

        /// <summary>
        /// Handles the item CheckBox click event to update the allCheckBox state.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void itemCheckBox_Click(object sender, RoutedEventArgs e)
        {
            bool allSelected = ViewModel.cart.CartProducts.All(item => item.IsSelected);
            allCheckBox.IsChecked = allSelected;
        }

        /// <summary>
        /// Handles the allCheckBox click event to select or deselect all items.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void allCheckBox_Click(object sender, RoutedEventArgs e)
        {
            bool isChecked = allCheckBox.IsChecked ?? false;
            foreach (var item in ViewModel.cart.CartProducts)
            {
                item.IsSelected = isChecked;
            }
        }

        /// <summary>
        /// Handles the Voucher button click event to show the voucher flyout.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void VoucherButton_Click(object sender, RoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout(VoucherStackPanel);
        }

        /// <summary>
        /// Handles the voucher selection changed event to update the selected voucher.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void VoucherSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                var selectedVoucher = (Voucher)e.AddedItems[0];
                if (selectedVoucher.isCondition(ViewModel.Total))
                {
                    SelectedVoucherTextBlock.Text = selectedVoucher.Name;
                    ViewModel.selectedVoucher = selectedVoucher;
                    FlyoutBase.GetAttachedFlyout(VoucherStackPanel).Hide();
                }
                else
                {
                    var dialog = new ContentDialog
                    {
                        Title = "Voucher Selection",
                        Content = "Total amount must be greater than the voucher condition.",
                        CloseButtonText = "OK",
                        XamlRoot = ((FrameworkElement)sender).XamlRoot
                    };
                    _ = dialog.ShowAsync();
                }
            }
        }

        /// <summary>
        /// Handles the Delivery button click event to show the delivery flyout.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void DeliveryButton_Click(object sender, RoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout(DeliveryStackPanel);
        }

        /// <summary>
        /// Handles the delivery selection changed event to update the selected delivery unit.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void DeliverySelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                var selectedDelivery = (DeliveryUnit)e.AddedItems[0];
                SelectedDeliveryTextBlock.Text = selectedDelivery.Name;
                ViewModel.selectedDelivery = selectedDelivery;
                ViewModel.NotifyPropertyChanged(nameof(ViewModel.DiscountAmount));
                FlyoutBase.GetAttachedFlyout(DeliveryStackPanel).Hide();
            }
        }
    }
}
