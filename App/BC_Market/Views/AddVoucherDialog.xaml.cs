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
    public sealed partial class AddVoucherDialog : UserControl
    {
        public AddVoucherDialog()
        {
            this.InitializeComponent();
        }

        private void closeDialog_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        public event EventHandler<Voucher> VoucherAdded;

        private void saveVoucher_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(Id.Text) || string.IsNullOrEmpty(Name.Text) || 
                string.IsNullOrEmpty(Descript.Text) || string.IsNullOrEmpty(Percent.Text) || 
                string.IsNullOrEmpty(Amount.Text) || string.IsNullOrEmpty(Condition.Text) || 
                string.IsNullOrEmpty(Stock.Text) || !Validate.SelectedDate.HasValue || 
                string.IsNullOrEmpty(RankId.Text))
            {
                Notice.Text = "Please fill all fields";
                return;
            }

            var voucher = new Voucher
            {
                VoucherId = Int32.Parse(Id.Text),
                Name = Name.Text,
                Description = Descript.Text,
                Percent = Int32.Parse(Percent.Text),
                Amount = Int32.Parse(Amount.Text),
                Condition = Double.Parse(Condition.Text),
                Stock = Int32.Parse(Stock.Text),
                Validate = DateTime.Parse(Validate.SelectedDate.ToString()),
                RankId = RankId.Text
            };

            // Raise event to notify that a new voucher has been added
            VoucherAdded?.Invoke(this, voucher);

            this.Visibility = Visibility.Collapsed;
        }
    }
}
