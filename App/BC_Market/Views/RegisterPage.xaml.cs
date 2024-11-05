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
        public RegisterPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            // get LoginPageViewModel to get ListAccount
            ViewModel = e.Parameter as LoginPageViewModel;
        }

        private void register_button_Click(object sender, RoutedEventArgs e)
        {
            var username = username_input.Text;
            var password = password_input.Password;
            var reenterPassword = reenter_password_input.Password;

            // Confirm that all field are filled
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
                // if information are proper, add new account to ListAccount
                var user = new USER()
                {
                    Id = (ViewModel.ListAccount.Count + 1).ToString(),
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

        // check isExist Account
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

        private void BackToLogin_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(LoginPage));
        }
    }
}
