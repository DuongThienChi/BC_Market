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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BC_Market.Views
{
    public sealed partial class AddProductDialog : UserControl
    {
        public AddProductDialog()
        {
            this.InitializeComponent();
        }
        private void CloseDialog(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        public event EventHandler<Product> ProductAdded;
        private string imagePath = "";
        private void SaveCate(object sender, RoutedEventArgs e)
        {
            if(imagePath == "")
            {
                Notice.Text = "Please fill all fields";
                return;
            }

            string imagePathServer = UploadImage.UploadImagePath(imagePath).Result;

            if( string.IsNullOrEmpty(Name.Text) ||
                string.IsNullOrEmpty(Descript.Text) || string.IsNullOrEmpty(Price.Text) ||
                string.IsNullOrEmpty(Stock.Text) ||
                string.IsNullOrEmpty(CategoryId.Text) || string.IsNullOrEmpty(Status.Text))
            {
                Notice.Text = "Please fill all fields";
                return;
            }

            // create new Product from entered information
            var product = new Product
            {
                Name = Name.Text,
                Description = Descript.Text,
                Price = Double.Parse(Price.Text),
                Stock = Int32.Parse(Stock.Text),
                ImagePath = imagePath,
                CategoryId = CategoryId.Text,
                Status = Status.Text
            };

            // check to confirm all fields are filled
            if ( string.IsNullOrEmpty(product.Name) || 
                string.IsNullOrEmpty(product.Description) || string.IsNullOrEmpty(product.CategoryId) || 
                string.IsNullOrEmpty(product.Status))
            {
                Notice.Text = "Please fill all fields";
                return;
            }

            ProductAdded?.Invoke(this, product);

            this.Visibility = Visibility.Collapsed;
        }

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
