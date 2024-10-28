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

        public AdminEditAccountPage()
        {
            this.InitializeComponent();
            RolesBox.SelectedIndex = 0;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var package = e.Parameter as dynamic;
            user = package.user;
            ViewModel = package.ViewModel;
            username_input.Text = user.Username;
            password_input.Password = user.Password;
        }

        private void Cancel_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AdminManageAccountPage));
        }

        private void submitEdit_btn_Click(object sender, RoutedEventArgs e)
        {
            var username = username_input.Text;
            var password = password_input.Password;
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

            var user = new USER()
            {
                Username = username,
                Password = password,
                Roles = new List<Role>()
                {
                    new Role()
                    {
                        Name = role
                    }
                }
            };

            ViewModel.Update(user);
            this.Frame.Navigate(typeof(AdminManageAccountPage),ViewModel);
        }
    }
}
