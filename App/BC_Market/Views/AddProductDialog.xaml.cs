using BC_Market.Helper;
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
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.Storage;
using Microsoft.UI.Xaml.Media.Imaging;
using BC_Market.ViewModels;
using System.Collections.ObjectModel;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BC_Market.Views
{ 
  /// <summary>
    /// A dialog for adding a new product.
    /// </summary>
    public sealed partial class AddProductDialog : UserControl
    {
        private ManageProductViewModel ViewModels { get; set; } = new ManageProductViewModel();
        private ObservableCollection<string> ListStatus { get; set; } = new ObservableCollection<string> { "Active", "Inactive" };

        /// <summary>
        /// Initializes a new instance of the <see cref="AddProductDialog"/> class.
        /// </summary>
        public AddProductDialog()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Closes the dialog.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void CloseDialog(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Event that is triggered when a new product is added.
        /// </summary>
        public event EventHandler<Product> ProductAdded;

        private string imagePath = "";

        /// <summary>
        /// Saves the new product.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void SaveCate(object sender, RoutedEventArgs e)
        {
            if (imagePath == "")
            {
                Notice.Text = "Please fill all fields";
                return;
            }

            string imagePathServer = UploadImage.UploadImagePath(imagePath).Result;

            if (string.IsNullOrEmpty(Name.Text) ||
                string.IsNullOrEmpty(Descript.Text) || string.IsNullOrEmpty(Price.Text) ||
                string.IsNullOrEmpty(Stock.Text) ||
                string.IsNullOrEmpty(CategoryId.SelectedItem as string) || string.IsNullOrEmpty(Status.SelectedItem as string))
            {
                Notice.Text = "Please fill all fields";
                return;
            }

            // Create new Product from entered information
            var product = new Product
            {
                Name = Name.Text,
                Description = Descript.Text,
                Price = Double.Parse(Price.Text),
                Stock = Int32.Parse(Stock.Text),
                ImagePath = imagePath,
                CategoryId = CategoryId.SelectedItem as string,
                Status = Status.SelectedItem as string,
            };

            // Check to confirm all fields are filled
            if (string.IsNullOrEmpty(product.Name) ||
                string.IsNullOrEmpty(product.Description) || string.IsNullOrEmpty(product.CategoryId) ||
                string.IsNullOrEmpty(product.Status))
            {
                Notice.Text = "Please fill all fields";
                return;
            }

            ProductAdded?.Invoke(this, product);

            this.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Handles the image upload click event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private async void UploadImage_Click(object sender, RoutedEventArgs e)
        {
            // Create a file picker
            var picker = new FileOpenPicker();
            picker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;

            // Set the file types to accept (images)
            picker.FileTypeFilter.Add(".png");
            picker.FileTypeFilter.Add(".jpg");
            picker.FileTypeFilter.Add(".jpeg");
            picker.FileTypeFilter.Add(".bmp");

            // Initialize picker for Desktop (required for WinUI)
            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(App.m_window);
            WinRT.Interop.InitializeWithWindow.Initialize(picker, hwnd);

            // Open the file picker
            StorageFile file = await picker.PickSingleFileAsync();
            if (file != null)
            {
                // Open the selected file as a stream
                using (IRandomAccessStream fileStream = await file.OpenAsync(FileAccessMode.Read))
                {
                    // Set the image source
                    BitmapImage bitmapImage = new BitmapImage();
                    await bitmapImage.SetSourceAsync(fileStream);
                    ProductImage.Source = bitmapImage;
                }
                imagePath = file.Path;
            }
        }
    }
}
