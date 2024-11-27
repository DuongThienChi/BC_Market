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

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel = e.Parameter as ManageProductViewModel;
            SumTotal.Text = ViewModel.sumTotal.ToString();
            Total = ViewModel.sumTotal;
        }

        private void cateButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var cate = button.DataContext as string;
            ViewModel.SetProductByCategory(cate);
            this.Frame.Navigate(typeof(OrderPage), ViewModel);
        }

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

        private void isNewCus_Checked(object sender, RoutedEventArgs e)
        {
            userEmail.Visibility = Visibility.Collapsed;
        }

        private void isNewCus_Unchecked(object sender, RoutedEventArgs e)
        {
            userEmail.Visibility = Visibility.Visible;
        }

        private async void Order_Click(object sender, RoutedEventArgs e)
        {
            if(ViewModel.ChosenProduct.Count == 0)
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
                user.Point += (int)Total/5;
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
