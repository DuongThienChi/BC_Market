using BC_Market.Factory;
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
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AdminAddAccountPage : Page
    {
        public AdminAddAccountPage()
        {
            this.InitializeComponent();
            RolesBox.SelectedIndex = 0;
        }

        private void Cancel_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AdminManageAccountPage));
        }

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
                var userFactory = new UserFactory();
                var userBUS = userFactory.CreateBUS();
                var userDAO = userFactory.CreateDAO();

                var user = new USER()
                {
                    Username = username,
                    Password = password,
                    Roles = new List<Role>()
                    {
                        new Role()
                        {
                            Name = RolesBox.SelectedItem.ToString()
                        }
                    }
                };

                userDAO.Add(user);

                notice_box.Text = "Add successfully!";

                this.Frame.Navigate(typeof(AdminManageAccountPage));
            }
        }
    }
}
