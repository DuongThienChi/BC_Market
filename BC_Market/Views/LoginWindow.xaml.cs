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
using System.Security.Cryptography;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;

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

            // Load username and password
            var localSettings = ApplicationData.Current.LocalSettings;
            var localFolder = ApplicationData.Current.LocalFolder;

            var encryptedPasswordBase64 = (string)localSettings.Values["PasswordInBase64"];
            var entropyInBase64 = (string)localSettings.Values["EntropyInBase64"];
            var username = (string)localSettings.Values["Username"];

            if (encryptedPasswordBase64 != null && entropyInBase64 != null)
            {
                var encryptedPasswordInBytes = Convert.FromBase64String(encryptedPasswordBase64);
                var entropyInBytes = Convert.FromBase64String(entropyInBase64);

                var decryptedPasswordInBytes = ProtectedData.Unprotect(
                    encryptedPasswordInBytes,
                    entropyInBytes,
                    DataProtectionScope.CurrentUser
                );

                var password = Encoding.UTF8.GetString(decryptedPasswordInBytes);

                if (username != null)
                {
                    username_input.Text = username;
                    password_input.Password = password;
                }
            }
            
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

                    if (remember_me.IsChecked == true)
                    {
                        // Save username and password
                        var localSettings = ApplicationData.Current.LocalSettings;
                        var localFolder = ApplicationData.Current.LocalFolder;

                        var passwordRaw = password;
                        var passwordInBytes = Encoding.UTF8.GetBytes(passwordRaw);
                        var entropyInBytes = new byte[20];
                        using (var rng = RandomNumberGenerator.Create())
                        rng.GetBytes(entropyInBytes);
                        var encryptedPasswordInBytes = ProtectedData.Protect(
                        passwordInBytes,
                        entropyInBytes,
                        DataProtectionScope.CurrentUser
                        );

                        var encryptedPasswordBase64 = Convert.ToBase64String(encryptedPasswordInBytes);
                        var entropyInBase64 = Convert.ToBase64String(entropyInBytes);

                        localSettings.Values["PasswordInBase64"] = encryptedPasswordBase64;
                        localSettings.Values["EntropyInBase64"] = entropyInBase64;
                        localSettings.Values["Username"] = username;
                    }

                    this.Close();
                    return;
                }
                else
                {
                    notice_box.Text = "Username or password is incorrect!";
                }
            }
        }

        private void forgot_text_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var forgotWindow = new ForgotWindow();
            forgotWindow.Activate();

            this.Close();
        }

        private void register_button_Click(object sender, RoutedEventArgs e)
        {
            var registerWindow = new RegisterWindow();
            registerWindow.Activate();

            this.Close();
        }
    }
}
