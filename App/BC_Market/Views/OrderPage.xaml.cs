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
        public OrderPage()
        {
            this.InitializeComponent();
            this.DataContext = ViewModel;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel = e.Parameter as ManageProductViewModel;
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
            }
            this.Frame.Navigate(typeof(OrderPage), ViewModel);
        }
    }
}
