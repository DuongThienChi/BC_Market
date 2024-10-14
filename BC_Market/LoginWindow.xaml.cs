using BC_Market.Factory;
using BC_Market.Views;
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

namespace BC_Market
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginWindow : Window
    {
        public delegate void SendMessageDelegate(string message);
        public event SendMessageDelegate SendMessageEvent;
        public LoginWindow()
        {
            this.InitializeComponent();
        }

        private void login_button_Click(object sender, RoutedEventArgs e)
        {
            var username = username_input.Text;
            var password = password_input.Password;

            var userFactory = new UserFactory();
            var userBUS = userFactory.CreateBUS();

            var listUser = userBUS.Get(null);

            foreach (var user in listUser)
            {
                if (user.Username == username && user.Password == password)
                {
                    if (user.Roles[0].Name == "Admin")
                    {
                        login_button.Tag = typeof(AdminDashboardPage);
                    }
                    else if (user.Roles[0].Name == "Manager")
                    {
                        login_button.Tag = typeof(ManagerDashboardPage);
                    }
                    else if (user.Roles[0].Name == "Shopper")
                    {
                        login_button.Tag = typeof(ShopperDashboardPage);
                    }

                    var button = sender as Button;
                    var type = button.Tag as Type;

                    var dashboardWindow = new Dashboard(type);
                    dashboardWindow.Activate();

                    this.Close();
                    return;
                }
            }
        }

        private void forgot_text_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var forgotWindow = new ForgotWindow();
            forgotWindow.Activate();

            this.Close();
        }
    }
}
