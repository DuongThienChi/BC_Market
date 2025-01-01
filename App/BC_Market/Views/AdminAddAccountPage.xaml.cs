using BC_Market.Factory;
using BC_Market.Helper;
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
    public sealed partial class AdminAddAccountPage : Page
    {
        private AdminManageAccountViewModel ViewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminAddAccountPage"/> class.
        /// </summary>
        public AdminAddAccountPage()
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
            ViewModel = e.Parameter as AdminManageAccountViewModel;
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
        /// Handles the Add Account button click event to add a new account.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void addAccount_btn_Click(object sender, RoutedEventArgs e)
        {
            var username = username_input.Text;
            var password = password_input.Password;
            var reenterPassword = reenter_password_input.Password;

            if (password != reenterPassword)
            {
                notice_box.Text = "Password and re-enter password are not the same!";
            }
            else if (username == "" || password == "" || reenterPassword == "")
            {
                notice_box.Text = "Please fill in all fields!";
            }
            else if (username.isUsernameExist())
            {
                notice_box.Text = "Username already exists!";
            }
            else
            {
                var role = RolesBox.SelectedItem as ComboBoxItem;

                var user = new USER()
                {
                    Username = username,
                    Password = password,
                    Roles = new List<Role>()
                        {
                            new Role()
                            {
                                Name = role.Content.ToString()
                            }
                        }
                };

                ViewModel.AddAccount(user);

                notice_box.Text = "Add successfully!";

                this.Frame.Navigate(typeof(AdminManageAccountPage), ViewModel);
            }
        }
    }
}
