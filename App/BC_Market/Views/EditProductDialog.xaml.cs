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
    /// <summary>
    /// A user control for editing a product.
    /// </summary>
    public sealed partial class EditProductDialog : UserControl
    {
        /// <summary>
        /// Gets or sets the product to be edited.
        /// </summary>
        public Product product { get; set; } = new Product();

        /// <summary>
        /// Initializes a new instance of the <see cref="EditProductDialog"/> class.
        /// </summary>
        public EditProductDialog()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Closes the dialog by setting its visibility to collapsed.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void CloseDialog(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Occurs when a product is edited.
        /// </summary>
        public event EventHandler<Product> ProductEdited;

        /// <summary>
        /// Handles the Save button click event to save the edited product.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void SaveClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Name.Text) ||
                string.IsNullOrEmpty(Descript.Text) || string.IsNullOrEmpty(Price.Text) ||
                string.IsNullOrEmpty(Stock.Text) || string.IsNullOrEmpty(CategoryId.Text) ||
                string.IsNullOrEmpty(Status.Text))
            {
                Notice.Text = "Please fill all fields";
                return;
            }

            // Create new product from entered information
            var editedProduct = new Product
            {
                Id = Int32.Parse(Id.Text),
                Name = Name.Text,
                Description = Descript.Text,
                Price = Double.Parse(Price.Text),
                Stock = Int32.Parse(Stock.Text),
                CategoryId = CategoryId.Text,
                Status = Status.Text,
                ImagePath = product.ImagePath
            };

            // Check to confirm all fields are filled
            ProductEdited?.Invoke(this, editedProduct);

            this.Visibility = Visibility.Collapsed;
        }
    }
}
