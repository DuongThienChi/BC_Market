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

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminPage"/> class.
        /// </summary>
        public AdminPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when the Page is loaded and becomes the current source of a parent Frame.
        /// </summary>
        /// <param name="e">Event data that can be examined by overriding code. The event data is representative of the pending navigation that will load the current Page. Usually, the event data is a NavigationEventArgs.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            // Navigate to AdminManageAccountPage - default page
            mainFrame.Navigate(typeof(AdminManageAccountPage), ListAccount);
        }

        /// <summary>
        /// Handles the Logout button click event to navigate back to the LoginPage.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void logout_button_Click(object sender, RoutedEventArgs e)
        {
            var package = new { ListAccount, ManageProductViewModel = ViewModel };
            this.Frame.Navigate(typeof(LoginPage), package);
        }

        /// <summary>
        /// Handles the Account Management button click event to navigate to the AdminManageAccountPage.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void accountManagement_button_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(typeof(AdminManageAccountPage), ViewModel);
        }

        /// <summary>
        /// Handles the Product Management button click event to navigate to the ManagerProductPage.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void productManagement_button_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(typeof(ManagerProductPage));
        }

        /// <summary>
        /// Handles the Voucher Management button click event to navigate to the ManagerVoucherPage.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void voucherManagement_button_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(typeof(ManagerVoucherPage));
        }

        /// <summary>
        /// Handles the Order button click event to navigate to the ManagerOrderPage.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void order_btn_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(typeof(ManagerOrderPage));
        }

        /// <summary>
        /// Handles the Report button click event to navigate to the ReportPage.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void report_btn_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.Navigate(typeof(ReportPage));
        }
    }
}
