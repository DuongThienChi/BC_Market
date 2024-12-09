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
using System.Collections.ObjectModel;
using BC_Market.Models;
using Microsoft.UI;
using BC_Market.ViewModels;
using Windows.System;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BC_Market.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AdminManageAccountPage : Page
    {
        public AdminManageAccountViewModel ViewModel { get; set; }
        public AdminManageAccountPage()
        {
            this.InitializeComponent();
            ViewModel = new AdminManageAccountViewModel();
            this.DataContext = ViewModel;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel = new AdminManageAccountViewModel();
        }

        private void addAccount_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AdminAddAccountPage), ViewModel);
        }

        private void accountList_Loaded(object sender, RoutedEventArgs e)
        {
            var gridViewItem = sender as GridViewItem;
            if (gridViewItem != null)
            {
                gridViewItem.Width = accountList.ActualWidth;
            }
        }

        // edit chosen Account
        private void edit_btn_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;

            var user = button.DataContext as USER;
            if (user == null)
            {
                return;
            }
            var package = new
            {
                user,
                ViewModel
            };

            if (user != null)
            {
                this.Frame.Navigate(typeof(AdminEditAccountPage), package);
            }
        }

        // delete chosen Account
        private void delete_btn_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var user = button.DataContext as USER;

            var listUser = ViewModel.Items;

            foreach (USER item in listUser)
            {
                if (item.Username == user.Username)
                {
                    ViewModel.DeleteAccount(item);
                    break;
                }
            }
        }
    }
}