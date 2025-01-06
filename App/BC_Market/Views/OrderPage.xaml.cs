using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;
using BC_Market.Factory;
using System.Security.Cryptography;
using System.Text;
using Windows.Storage;
using BC_Market.ViewModels;
using System.Collections.ObjectModel;
using BC_Market.Models;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BC_Market.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class OrderPage : Page
    {
        private ManageProductViewModel ViewModel { get; set; }
        private PaymentMethodViewModel paymentMethodViewModel { get; set; } = new PaymentMethodViewModel();
        private AdminManageAccountViewModel AccountViewModel { get; set; } = new AdminManageAccountViewModel();
        float Total = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderPage"/> class.
        /// </summary>
        public OrderPage()
        {
            this.InitializeComponent();
            var items = paymentMethodViewModel.listMethod;

            var _string = new ObservableCollection<string>();

            foreach (var item in items)
            {
                _string.Add(item.Name);
            }

            paymentMethodList.ItemsSource = _string;
            paymentMethodList.SelectedIndex = 0;
            this.DataContext = ViewModel;
        }

        /// <summary>
        /// Invoked when the Page is loaded and becomes the current source of a parent Frame.
        /// </summary>
        /// <param name="e">Event data that can be examined by overriding code. The event data is representative of the pending navigation that will load the current Page. Usually, the event data is a NavigationEventArgs.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is ManageProductViewModel)
            {
                ViewModel = e.Parameter as ManageProductViewModel;
            }
            else
            {
                ViewModel = new ManageProductViewModel();
            }
            SumTotal.Text = ViewModel.sumTotal.ToString();
            Total = ViewModel.sumTotal;
        }

        /// <summary>
        /// Handles the Category button click event to filter products by category.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void cateButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var cate = button.DataContext as string;
            ViewModel.SetProductByCategory(cate);
            this.Frame.Navigate(typeof(OrderPage), ViewModel);
        }

        /// <summary>
        /// Handles the Decrease Quantity button click event to decrease the order quantity of a product.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void descQuantity_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            Product product = button.DataContext as Product;

            if (product.OrderQuantity > 0)
            {
                product.OrderQuantity--;
                ViewModel.UpdateProduct(product);
                ViewModel.SetChosenProduct();
            }
            this.Frame.Navigate(typeof(OrderPage), ViewModel);
        }

        /// <summary>
        /// Handles the Increase Quantity button click event to increase the order quantity of a product.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void incQuantity_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            Product product = button.DataContext as Product;

            if (product.OrderQuantity < product.Stock)
            {
                product.OrderQuantity++;
                ViewModel.UpdateProduct(product);
                ViewModel.SetChosenProduct();
            }
            this.Frame.Navigate(typeof(OrderPage), ViewModel);
        }

        /// <summary>
        /// Handles the event when the New Customer checkbox is checked.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void isNewCus_Checked(object sender, RoutedEventArgs e)
        {
            userEmail.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Handles the event when the New Customer checkbox is unchecked.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void isNewCus_Unchecked(object sender, RoutedEventArgs e)
        {
            userEmail.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Handles the Order button click event to create a new order.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private async void Order_Click(object sender, RoutedEventArgs e)
        {
            if (ViewModel.ChosenProduct.Count == 0)
            {
                var dialog = new ContentDialog
                {
                    Title = "Error",
                    Content = "No product chosen",
                    CloseButtonText = "Ok",
                    XamlRoot = this.Content.XamlRoot
                };
                await dialog.ShowAsync();
                return;
            }

            if (isNewCus.IsChecked == false)
            {
                var users = AccountViewModel.Items;
                var user = users.FirstOrDefault(u => u.Email == userEmail.Text);
                if (user == null)
                {
                    var dialog1 = new ContentDialog
                    {
                        Title = "Error",
                        Content = "User not found",
                        CloseButtonText = "Ok",
                        XamlRoot = this.Content.XamlRoot
                    };
                    await dialog1.ShowAsync();
                    return;
                }
                ViewModel.CreateOrder(paymentID: paymentMethodList.SelectedIndex, userID: user.Id);
                user.Point += (int)Total / 5;
                AccountViewModel.Update(user);
            }
            else
            {
                ViewModel.CreateOrder(paymentID: paymentMethodList.SelectedIndex, userID: 3);
            }

            var dialog2 = new ContentDialog
            {
                Title = "Success",
                Content = "Order success",
                CloseButtonText = "Ok",
                XamlRoot = this.Content.XamlRoot
            };
            await dialog2.ShowAsync();
            this.Frame.Navigate(typeof(OrderPage), ViewModel);
        }
    }
}
