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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BC_Market.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ManagerPage : Page
    {
        private ManageProductViewModel ViewModel;
        public ManagerPage()
        {
            this.InitializeComponent();
            ViewModel = new ManageProductViewModel();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter is ManageProductViewModel)
            {
                ViewModel = e.Parameter as ManageProductViewModel;
            }
            mainFrame.Navigate(typeof(ManagerProductPage), ViewModel);
        }

        private void logout_button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(LoginPage),ViewModel);
        }

        private void product_btn_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(typeof(ManagerProductPage), ViewModel);
        }

        private void voucher_btn_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(typeof(ManagerVoucherPage), ViewModel);
        }
        private void order_btn_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(typeof(ManagerOrderPage));
        }
    }
}
