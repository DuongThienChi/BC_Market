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
using BC_Market.Models;
using BC_Market.ViewModels;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BC_Market.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RegisterPage : Page
    {
        private LoginPageViewModel ViewModel = new LoginPageViewModel();

        /// <summary>
        /// Initializes a new instance of the <see cref="RegisterPage"/> class.
        /// </summary>
        public RegisterPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when the Page is loaded and becomes the current source of a parent Frame.
        /// </summary>
        /// <param name="e">Event data that can be examined by overriding code. The event data is representative of the pending navigation that will load the current Page. Usually, the event data is a NavigationEventArgs.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            // get LoginPageViewModel to get ListAccount
            ViewModel = e.Parameter as LoginPageViewModel;
        }

        /// <summary>
        /// Handles the Register button click event to register a new user.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void register_button_Click(object sender, RoutedEventArgs e)
        {
            var username = username_input.Text;
            var password = password_input.Password;
            var reenterPassword = reenter_password_input.Password;

            // Confirm that all fields are filled
            if (password != reenterPassword)
            {
                notice_box.Text = "Password and re-enter password are not the same!";
            }
            else if (username == "" || password == "" || reenterPassword == "")
            {
                notice_box.Text = "Please fill in all fields!";
            }
            else if (isUsernameExist(username))
            {
                notice_box.Text = "Username already exists!";
            }
            else
            {
                // if information is proper, add new account to ListAccount
                var user = new USER()
                {
                    Username = username,
                    Password = password,
                    Roles = new List<Role>()
                        {
                            new Role()
                            {
                                Id = "R03",
                                // only Shopper Role can be created, Manager and Admin Role will be created by Admin account
                                Name = "Shopper"
                            }
                        }
                };

                ViewModel.AddAccount(user);

                notice_box.Text = "Register successfully!";

                this.Frame.Navigate(typeof(LoginPage), ViewModel);
            }
        }

        /// <summary>
        /// Checks if the username already exists in the list of accounts.
        /// </summary>
        /// <param name="username">The username to check.</param>
        /// <returns>True if the username exists, otherwise false.</returns>
        public bool isUsernameExist(string username)
        {
            var listUser = ViewModel.ListAccount;

            foreach (var user in listUser)
            {
                if (user.Username == username)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Handles the event when the Back to Login text is tapped to navigate back to the Login page.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void BackToLogin_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(LoginPage));
        }
    }
}
