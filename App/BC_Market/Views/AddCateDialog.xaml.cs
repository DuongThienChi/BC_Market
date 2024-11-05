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
    public sealed partial class AddCateDialog : UserControl
    {
        public AddCateDialog()
        {
            this.InitializeComponent();
        }

        private void CloseDialog(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        public event EventHandler<Category> CategoryAdded;
        private void SaveCate(object sender, RoutedEventArgs e)
        {
            // create new Category from entered information
            var cate = new Category
            {
                Id = Id.Text,
                Name = Name.Text,
                Description = Descript.Text
            };

            if(string.IsNullOrEmpty(cate.Id) || string.IsNullOrEmpty(cate.Name) || string.IsNullOrEmpty(cate.Description))
            {
                Notice.Text = "Please fill all fields";
                return;
            }

            CategoryAdded?.Invoke(this, cate);

            this.Visibility = Visibility.Collapsed;
        }  
    }
}
