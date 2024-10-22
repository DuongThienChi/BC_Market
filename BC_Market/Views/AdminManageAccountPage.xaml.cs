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
using BC_Market.Factory;
using System.Collections.ObjectModel;
using BC_Market.Models;
using Microsoft.UI;
using BC_Market.ViewModels;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BC_Market.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AdminManageAccountPage : Page
    {
        public AdminManageAccountViewModel ViewModel { get; set; }
        public AdminManageAccountPage()
        {
            this.InitializeComponent();
            ViewModel = new AdminManageAccountViewModel();
            this.DataContext = ViewModel;
        }

        private void backButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private async void addAccount_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog addAccountDialog = new ContentDialog
            {
                Title = "Add Account",
                PrimaryButtonText = "Add",
                CloseButtonText = "Cancel",
                XamlRoot = this.Content.XamlRoot
            };

            StackPanel panel = new StackPanel();

            TextBox id = new TextBox { PlaceholderText = "ID", Margin = new Thickness(10, 0, 10, 0) };
            TextBox username = new TextBox { PlaceholderText = "Username", Margin = new Thickness(10, 0, 10, 0) };
            TextBox password = new TextBox { PlaceholderText = "Password", Margin = new Thickness(10, 0, 10, 0) };
            TextBox email = new TextBox { PlaceholderText = "Email", Margin = new Thickness(10, 0, 10, 0) };
            ComboBox role = new ComboBox { PlaceholderText = "Role", Margin = new Thickness(10, 0, 10, 0) };

            panel.Children.Add(id);
            panel.Children.Add(username);
            panel.Children.Add(password);
            panel.Children.Add(email);
            panel.Children.Add(role);

            addAccountDialog.Content = panel;

            addAccountDialog.PrimaryButtonClick += (sender, args) =>
            {
                // Handle the Add button click event
                string user = username.Text;
                string pass = password.Text;
                string mail = email.Text;
                string userRole = role.SelectedItem?.ToString();

                // Add your logic to handle the account addition here

                // For example, you can save these details to a database or a file
                var factory = new UserFactory();
                var bus = factory.CreateBUS();
                var roles = new List<Role> { new Role { Name = userRole } };
                var User = new USER().CreateUser(user, pass, roles);
                
            };

            addAccountDialog.CloseButtonClick += (sender, args) =>
            {
                // Handle the Cancel button click event
            };

            await addAccountDialog.ShowAsync();
        }


        private void accountList_Loaded(object sender, RoutedEventArgs e)
        {
            var gridViewItem = sender as GridViewItem;
            if (gridViewItem != null)
            {
                gridViewItem.Width = accountList.ActualWidth;
            }
        }

        private void addAccountDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {

        }

        private void addAccountDialog_CloseButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {

        }
    }
}
