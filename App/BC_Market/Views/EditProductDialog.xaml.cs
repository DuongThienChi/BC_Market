using BC_Market.Models;
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
    public sealed partial class EditProductDialog : UserControl
    {
        public Product product { get; set; } = new Product();
        public EditProductDialog()
        {
            this.InitializeComponent();
        }

        private void CloseDialog(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        public event EventHandler<Product> ProductEdited;
        private void SaveClick(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(Id.Text) || string.IsNullOrEmpty(Name.Text) ||
                string.IsNullOrEmpty(Descript.Text) || string.IsNullOrEmpty(Price.Text) ||
                string.IsNullOrEmpty(Stock.Text) || string.IsNullOrEmpty(CategoryId.Text) ||
                string.IsNullOrEmpty(Status.Text))
            {
                Notice.Text = "Please fill all fields";
                return;
            }

            // create new product from entered information
            var editedProduct = new Product
            {
                Id = Id.Text,
                Name = Name.Text,
                Description = Descript.Text,
                Price = Double.Parse(Price.Text),
                Stock = Int32.Parse(Stock.Text),
                CategoryId = CategoryId.Text,
                Status = Status.Text,
                ImagePath = product.ImagePath
            };

            ProductEdited?.Invoke(this, editedProduct);

            this.Visibility = Visibility.Collapsed;
        }
    }
}
