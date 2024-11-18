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
    public sealed partial class AdminVoucherPage : Page
    {
        private VoucherViewModel viewModel {get; set; }
        public AdminVoucherPage()
        {
            this.InitializeComponent();
            viewModel = new VoucherViewModel();
            this.DataContext = viewModel;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            if (e.Parameter is VoucherViewModel)
            {
                viewModel = e.Parameter as VoucherViewModel;
                return;
            }
        }

        private void edit_btn_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            var voucher = button.DataContext as Voucher;

            EditVoucherDialog.Visibility = Visibility.Visible;
            EditVoucherDialog.voucher = voucher;
        }

        private void delete_btn_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            var voucher = button.DataContext as Voucher;

            viewModel.DeleteVoucher(voucher);
        }

        private void addVoucher_Click(object sender, RoutedEventArgs e)
        {
            AddVoucherDialog.Visibility = Visibility.Visible;
        }

        private void AddVoucherDialog_VoucherAdded(object sender, Voucher e)
        {
            viewModel.AddVoucher(e);
            this.Frame.Navigate(typeof(AdminVoucherPage), viewModel);
        }

        private void EditVoucherDialog_VoucherEdited(object sender, Voucher e)
        {
            viewModel.UpdateVoucher(e);
            this.Frame.Navigate(typeof(AdminVoucherPage), viewModel);
        }
    }
}
