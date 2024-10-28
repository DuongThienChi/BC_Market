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
using BC_Market.Models;
using BC_Market.ViewModels;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BC_Market.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AdminProductPage : Page
    {
        private ManageProductViewModel ViewModel;
        public AdminProductPage()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter is ManageProductViewModel)
            {
                ViewModel = e.Parameter as ManageProductViewModel;
            }
        }

        private void cateButton_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var cate = button.DataContext as string;
            ViewModel.SetProductByCategory(cate);
            this.Frame.Navigate(typeof(AdminProductPage), ViewModel);
        }

        private void AddCateButton_Click(object sender, RoutedEventArgs e)
        {
            AddCateDialog.Visibility = Visibility.Visible;
        }

        private void AddCateDialog_CategoryAdded(object sender, Models.Category newCate)
        {
            ViewModel.AddCategory(newCate);
        }

        private void edit_btn_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;

            var curProduct = button.DataContext as Product;
            if (curProduct == null)
            {
                return;
            }

            EditProductDialog.Visibility = Visibility.Visible;
            EditProductDialog.product = curProduct;

        }

        private void delete_btn_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var product = button.DataContext as Product;

            var listProduct = ViewModel.ListProduct;

            foreach (Product item in listProduct)
            {
                if (item.Id == product.Id)
                {
                    ViewModel.DeleteProduct(item);
                    break;
                }
            }
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            AddProductDialog.Visibility = Visibility.Visible;
        }

        private void AddProductDialog_ProductAdded(object sender, Models.Product newProduct)
        {
            ViewModel.AddProduct(newProduct);
            this.Frame.Navigate(typeof(AdminProductPage), ViewModel);
        }

        private void EditProductDialog_ProductEdited(object sender, Product newProduct)
        {
            ViewModel.UpdateProduct(newProduct);
            this.Frame.Navigate(typeof(AdminProductPage), ViewModel);
        }
    }
}