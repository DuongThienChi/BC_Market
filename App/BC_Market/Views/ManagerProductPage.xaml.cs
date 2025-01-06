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
    public sealed partial class ManagerProductPage : Page
    {
        private ManageProductViewModel ViewModel = new ManageProductViewModel();

        /// <summary>
        /// Initializes a new instance of the <see cref="ManagerProductPage"/> class.
        /// </summary>
        public ManagerProductPage()
        {
            this.InitializeComponent();
            this.DataContext = ViewModel;
        }

        /// <summary>
        /// Invoked when the Page is loaded and becomes the current source of a parent Frame.
        /// </summary>
        /// <param name="e">Event data that can be examined by overriding code. The event data is representative of the pending navigation that will load the current Page. Usually, the event data is a NavigationEventArgs.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter is ManageProductViewModel)
            {
                ViewModel = e.Parameter as ManageProductViewModel;
            }
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
            this.Frame.Navigate(typeof(ManagerProductPage), ViewModel);
        }

        /// <summary>
        /// Handles the Add Category button click event to show the Add Category dialog.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void AddCateButton_Click(object sender, RoutedEventArgs e)
        {
            AddCateDialog.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Handles the event when a new category is added from the Add Category dialog.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="newCate">The new category that was added.</param>
        private void AddCateDialog_CategoryAdded(object sender, Models.Category newCate)
        {
            ViewModel.AddCategory(newCate);
        }

        /// <summary>
        /// Handles the Edit button click event to show the Edit Product dialog.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
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

        /// <summary>
        /// Handles the Delete button click event to delete a product.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
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
            this.Frame.Navigate(typeof(ManagerProductPage), ViewModel);
        }

        /// <summary>
        /// Handles the Add Product button click event to show the Add Product dialog.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            AddProductDialog.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Handles the event when a new product is added from the Add Product dialog.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="newProduct">The new product that was added.</param>
        private void AddProductDialog_ProductAdded(object sender, Models.Product newProduct)
        {
            ViewModel.AddProduct(newProduct);
            this.Frame.Navigate(typeof(ManagerProductPage), ViewModel);
        }

        /// <summary>
        /// Handles the event when a product is edited from the Edit Product dialog.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="newProduct">The product that was edited.</param>
        private void EditProductDialog_ProductEdited(object sender, Product newProduct)
        {
            ViewModel.UpdateProduct(newProduct);
            this.Frame.Navigate(typeof(ManagerProductPage), ViewModel);
        }
    }
}
