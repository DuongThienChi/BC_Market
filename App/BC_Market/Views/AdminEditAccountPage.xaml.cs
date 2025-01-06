using BC_Market.BUS;
using BC_Market.Factory;
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
    public sealed partial class AdminEditAccountPage : Page
    {
        private USER user;
        private AdminManageAccountViewModel ViewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminEditAccountPage"/> class.
        /// </summary>
        public AdminEditAccountPage()
        {
            this.InitializeComponent();
            RolesBox.SelectedIndex = 0;
        }

        /// <summary>
        /// Invoked when the Page is loaded and becomes the current source of a parent Frame.
        /// </summary>
        /// <param name="e">Event data that can be examined by overriding code. The event data is representative of the pending navigation that will load the current Page. Usually, the event data is a NavigationEventArgs.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            // Unpack package and set default data for TextBoxes
            var package = e.Parameter as dynamic;
            user = package.user;
            ViewModel = package.ViewModel;
            username_input.Text = user.Username;
            password_input.Password = user.Password;
            email_input.Text = user.Email;
            // Set SelectedItem of ComboBox by current Role
            RolesBox.SelectedItem = RolesBox.Items.Cast<ComboBoxItem>().FirstOrDefault(x => x.Content.ToString() == user.Roles[0].Name);
        }

        /// <summary>
        /// Handles the Cancel button tap event to navigate back to the AdminManageAccountPage.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void Cancel_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AdminManageAccountPage));
        }

        /// <summary>
        /// Handles the Submit Edit button click event to update the account information.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void submitEdit_btn_Click(object sender, RoutedEventArgs e)
        {
            // Get all information of edited account
            var username = username_input.Text;
            var password = password_input.Password;
            var email = email_input.Text;
            var selectedRole = RolesBox.SelectedItem as ComboBoxItem;
            var role = selectedRole.Content.ToString();

            if (password == "")
            {
                notice_box.Text = "Password cannot be empty!";
                return;
            }
            if (role == "")
            {
                notice_box.Text = "Role cannot be empty!";
                return;
            }

            string hashedPassword;
            if (password != user.Password)
            {
                // Hash password (bcrypt) if it is different from the current password
                string salt = BCrypt.Net.BCrypt.GenerateSalt(12);
                hashedPassword = BCrypt.Net.BCrypt.HashPassword(password, salt);
            }
            else
            {
                hashedPassword = password;
            }

            // Create new account and update to ListAccount
            var editedUser = new USER()
            {
                Id = user.Id,
                Username = username,
                Password = hashedPassword,
                Email = email,
                Roles = new List<Role>()
                    {
                        new Role()
                        {
                            Name = role
                        }
                    }
            };

            ViewModel.Update(editedUser);
            this.Frame.Navigate(typeof(AdminManageAccountPage), ViewModel);
        }
    }
}
