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
using BC_Market.ViewModels;
using BC_Market.Models;
using System.Collections.ObjectModel;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BC_Market.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AdminPage : Page
    {
        private ObservableCollection<USER> ListAccount = new ObservableCollection<USER>();
        private ManageProductViewModel ViewModel;
        public AdminPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var package = e.Parameter as dynamic;
            ListAccount = package.ListAccount;
            ViewModel = package.ManageProductViewModel;
            mainFrame.Navigate(typeof(AdminManageAccountPage), ListAccount);
        }

        private void logout_button_Click(object sender, RoutedEventArgs e)
        {
            var package = new { ListAccount, ManageProductViewModel = ViewModel };
            this.Frame.Navigate(typeof(LoginPage), package);
        }

        private void accountManagement_button_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(typeof(AdminManageAccountPage), ViewModel);
        }

        private void productManagement_button_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(typeof(AdminProductPage), ViewModel);
        }
    }
}
