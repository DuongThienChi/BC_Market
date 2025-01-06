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
    public sealed partial class ManagerVoucherPage : Page
    {
        private VoucherViewModel viewModel { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ManagerVoucherPage"/> class.
        /// </summary>
        public ManagerVoucherPage()
        {
            this.InitializeComponent();
            viewModel = new VoucherViewModel();
            this.DataContext = viewModel;
        }

        /// <summary>
        /// Invoked when the Page is loaded and becomes the current source of a parent Frame.
        /// </summary>
        /// <param name="e">Event data that can be examined by overriding code. The event data is representative of the pending navigation that will load the current Page. Usually, the event data is a NavigationEventArgs.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter is VoucherViewModel)
            {
                viewModel = e.Parameter as VoucherViewModel;
                return;
            }
        }

        /// <summary>
        /// Handles the Add Voucher button click event to show the Add Voucher dialog.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void addVoucher_Click(object sender, RoutedEventArgs e)
        {
            AddVoucherDialog.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Handles the Edit button click event to show the Edit Voucher dialog.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void edit_btn_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            var voucher = button.DataContext as Voucher;

            EditVoucherDialog.Visibility = Visibility.Visible;
            EditVoucherDialog.voucher = voucher;
        }

        /// <summary>
        /// Handles the Delete button click event to delete a voucher.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void delete_btn_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            var voucher = button.DataContext as Voucher;

            viewModel.DeleteVoucher(voucher);
        }

        /// <summary>
        /// Handles the event when a new voucher is added from the Add Voucher dialog.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The new voucher that was added.</param>
        private void AddVoucherDialog_VoucherAdded(object sender, Models.Voucher e)
        {
            viewModel.AddVoucher(e);
            this.Frame.Navigate(typeof(ManagerVoucherPage), viewModel);
        }

        /// <summary>
        /// Handles the event when a voucher is edited from the Edit Voucher dialog.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The voucher that was edited.</param>
        private void EditVoucherDialog_VoucherEdited(object sender, Models.Voucher e)
        {
            viewModel.UpdateVoucher(e);
            this.Frame.Navigate(typeof(ManagerVoucherPage), viewModel);
        }
    }
}
