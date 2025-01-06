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
using System.Security.Cryptography;
using System.Text;
using Windows.Storage;
using BC_Market.ViewModels;
using System.Collections.ObjectModel;
using BC_Market.Models;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BC_Market.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : Page
    {
        private LoginPageViewModel ViewModel;
        private ManageProductViewModel ManageProductViewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginPage"/> class.
        /// </summary>
        public LoginPage()
        {
            this.InitializeComponent();
            ViewModel = new LoginPageViewModel();
            ManageProductViewModel = new ManageProductViewModel();

            // Load username and password
            var localSettings = ApplicationData.Current.LocalSettings;

            var password = (string)localSettings.Values["Password"];
            var username = (string)localSettings.Values["Username"];

            if (password != null && username != null)
            {
                username_input.Text = username;
                password_input.Password = password;
            }
        }

        /// <summary>
        /// Invoked when the Page is loaded and becomes the current source of a parent Frame.
        /// </summary>
        /// <param name="e">Event data that can be examined by overriding code. The event data is representative of the pending navigation that will load the current Page. Usually, the event data is a NavigationEventArgs.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // Add logic for handling navigation to this page
        }

        /// <summary>
        /// Handles the Login button click event.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void login_button_Click(object sender, RoutedEventArgs e)
        {
            // Get current username and password
            var username = username_input.Text;
            var password = password_input.Password;

            var listUser = ViewModel.ListAccount;

            foreach (var user in listUser)
            {
                // Check username and password
                if (user.Username == username && (BCrypt.Net.BCrypt.Verify(password, user.Password) || password == user.Password))
                {
                    // Store username and password if remember_me.IsChecked
                    if (remember_me.IsChecked == true)
                    {
                        var localSettings = ApplicationData.Current.LocalSettings;

                        localSettings.Values["Password"] = user.Password;
                        localSettings.Values["Username"] = username;
                    }

                    // Navigate to proper Frame and send data
                    if (user.Roles[0].Name == "Admin")
                    {
                        var package = new
                        {
                            ViewModel.ListAccount,
                            ManageProductViewModel
                        };
                        this.Frame.Navigate(typeof(AdminPage));
                    }
                    else if (user.Roles[0].Name == "Manager")
                    {
                        this.Frame.Navigate(typeof(ManagerPage));
                    }
                    else if (user.Roles[0].Name == "Shopper")
                    {
                        SessionManager.Set("curCustomer", user);
                        ViewModel.LoadCart(user.Id);
                        this.Frame.Navigate(typeof(ShopperDashboardPage));
                    }
                    else if (user.Roles[0].Name == "Cashier")
                    {
                        this.Frame.Navigate(typeof(CashierPage));
                    }
                    SessionManager.Set("User", user);
                    return;
                }
            }
            notice_box.Text = "Username or password is incorrect!";
        }

        /// <summary>
        /// Handles the Forgot Password text tap event to navigate to the ForgotPage.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void forgot_text_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ForgotPage), ViewModel);
        }

        /// <summary>
        /// Handles the Register button click event to navigate to the RegisterPage.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void register_button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(RegisterPage), ViewModel);
        }
    }
}
