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
        public LoginPage()
        {
            this.InitializeComponent();
            ViewModel = new LoginPageViewModel();
            ManageProductViewModel = new ManageProductViewModel();

            // Load username and password
            var localSettings = ApplicationData.Current.LocalSettings;

            var password = (string)localSettings.Values["Password"];
            var username = (string)localSettings.Values["Username"];

            if(password != null && username != null)
            {
                username_input.Text = username;
                password_input.Password = password;
            }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            // Get only LoginPageView from previous page
            if (e.Parameter is LoginPageViewModel)
            {
                ViewModel = e.Parameter as LoginPageViewModel;
                return;
            }
            // Get only ListAccount from previous page
            if (e.Parameter is ObservableCollection<USER>)
            {
                var listUser = e.Parameter as ObservableCollection<USER>;
                ViewModel.ListAccount = listUser;
                return;
            }
            // Get only ManageProductViewModel from previous page
            if (e.Parameter is ManageProductViewModel)
            {
                ManageProductViewModel = e.Parameter as ManageProductViewModel;
                return;
            }
            // Get only ShopperDashboardViewModel from previous page
            if (e.Parameter is ShopperDashboardViewModel)
            {
                return;
            }
            // Get ListAccount and ManageProductViewModel from previous page
            var package = e.Parameter as dynamic;
            if (package != null)
            {
                if (package.ListAccount != null)
                    ViewModel.ListAccount = package.ListAccount;
                if (package.ManageProductViewModel != null)
                    ManageProductViewModel = package.ManageProductViewModel;
            }
        }

        private void login_button_Click(object sender, RoutedEventArgs e)
        {
            // get current username and password
            var username = username_input.Text;
            var password = password_input.Password;

            var listUser = ViewModel.ListAccount;

            foreach (var user in listUser)
            {
                // check username and password
                if (user.Username == username && (BCrypt.Net.BCrypt.Verify(password,user.Password) || password == user.Password))
                {
                    // store username and password if remember_me.IsChecked
                    if (remember_me.IsChecked == true)
                    {
                        var localSettings = ApplicationData.Current.LocalSettings;

                        localSettings.Values["Password"] = user.Password;
                        localSettings.Values["Username"] = username;
                    }

                    // navigate to proper Frame and send data
                    if (user.Roles[0].Name == "Admin")
                    {
                        var package = new
                        {
                            ViewModel.ListAccount,
                            ManageProductViewModel
                        };
                        this.Frame.Navigate(typeof(AdminPage), package);
                    }
                    else if (user.Roles[0].Name == "Manager")
                    {
                        this.Frame.Navigate(typeof(ManagerPage), ManageProductViewModel);
                    }
                    else if (user.Roles[0].Name == "Shopper")
                    {
                        this.Frame.Navigate(typeof(ShopperDashboardPage));
                    }
                    SessionManager.Set("User", user);
                    return;
                }
            }
            notice_box.Text = "Username or password is incorrect!";
        }

        private void forgot_text_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ForgotPage), ViewModel);
        }

        private void register_button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(RegisterPage), ViewModel);
        }
    }
}
