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
        public ShopperOrderPage()
        {
            this.InitializeComponent();
            ViewModel = new ShopperOrderViewModel();
        
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter is ObservableCollection<KeyValuePair<Product, int>> cartList)
            {
                ViewModel.CartList = cartList;
            }
        }
        private void AddProduct_Clicked(object sender, RoutedEventArgs e)
        {
            var shopPage = new ShopperDashboardPage();
            shopPage.ViewModel.CartList = ViewModel.CartList.ToDictionary(item => item.Key, item => item.Value);
            shopPage.ViewModel.ProductInCart = ViewModel.CartList.Count;
            var Params = new
            {
                CartList = ViewModel.CartList,
                ProductInCart = ViewModel.CartList.Count
            };
            this.Frame.Navigate(typeof(ShopperDashboardPage), Params);
        }

    }
}
