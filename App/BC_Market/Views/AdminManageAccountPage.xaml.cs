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
        /// <summary>
        /// Gets or sets the ViewModel for managing accounts.
        /// </summary>
        public AdminManageAccountViewModel ViewModel { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminManageAccountPage"/> class.
        /// </summary>
        public AdminManageAccountPage()
        {
            this.InitializeComponent();
            ViewModel = new AdminManageAccountViewModel();
            this.DataContext = ViewModel;
        }

        /// <summary>
        /// Invoked when the Page is loaded and becomes the current source of a parent Frame.
        /// </summary>
        /// <param name="e">Event data that can be examined by overriding code. The event data is representative of the pending navigation that will load the current Page. Usually, the event data is a NavigationEventArgs.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ViewModel = new AdminManageAccountViewModel();
        }

        /// <summary>
        /// Handles the Add Account button click event to navigate to the AdminAddAccountPage.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void addAccount_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AdminAddAccountPage), ViewModel);
        }

        /// <summary>
        /// Handles the account list loaded event to set the width of the GridViewItem.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void accountList_Loaded(object sender, RoutedEventArgs e)
        {
            var gridViewItem = sender as GridViewItem;
            if (gridViewItem != null)
            {
                gridViewItem.Width = accountList.ActualWidth;
            }
        }

        /// <summary>
        /// Handles the Edit button click event to navigate to the AdminEditAccountPage.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
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

        /// <summary>
        /// Handles the Delete button click event to delete the selected account.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
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
            this.Frame.Navigate(typeof(AdminManageAccountPage), ViewModel);
        }
    }
}