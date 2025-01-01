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
    /// A dialog for adding a new category.
    /// </summary>
    public sealed partial class AddCateDialog : UserControl
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AddCateDialog"/> class.
        /// </summary>
        public AddCateDialog()
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
        /// Event that is triggered when a new category is added.
        /// </summary>
        public event EventHandler<Category> CategoryAdded;

        /// <summary>
        /// Saves the new category.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void SaveCate(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Id.Text) || string.IsNullOrEmpty(Name.Text) ||
                string.IsNullOrEmpty(Descript.Text))
            {
                Notice.Text = "Please fill all fields";
                return;
            }

            // Create new Category from entered information
            var cate = new Category
            {
                Id = Id.Text,
                Name = Name.Text,
                Description = Descript.Text
            };

            CategoryAdded?.Invoke(this, cate);

            this.Visibility = Visibility.Collapsed;
        }
    }
}
