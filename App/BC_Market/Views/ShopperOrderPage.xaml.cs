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
        private List<CheckBox> CheckBoxs { get; set; }
        public ShopperOrderPage()
        {
            this.InitializeComponent();
            ViewModel = new ShopperOrderViewModel();
            this.DataContext = ViewModel;
            this.CheckBoxs = new List<CheckBox>();

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
            var NumberProduct = 0;
            foreach (var item in ViewModel.CartList)
            {
                NumberProduct += item.Value;
            }
            shopPage.ViewModel.ProductInCart = NumberProduct;
            var Params = new
            {
                CartList = ViewModel.CartList,
                ProductInCart = NumberProduct,
            };
            this.Frame.Navigate(typeof(ShopperDashboardPage), Params);
        }

        private void itemCheckBox_Click(object sender, RoutedEventArgs e)
        {
            bool allSelected = ViewModel.CartItems.All(item => item.IsSelected);
            allCheckBox.IsChecked = allSelected;
        }



        private void allCheckBox_Click(object sender, RoutedEventArgs e)
        {
            bool isChecked = allCheckBox.IsChecked ?? false;
            foreach (var item in ViewModel.CartItems)
            {
                item.IsSelected = isChecked;
            }
        }


    }
}
