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
    public sealed partial class EditVoucherDialog : UserControl
    {
        public Voucher voucher { get; set; } = new Voucher();
        public EditVoucherDialog()
        {
            this.InitializeComponent();
        }

        private void closeDialog_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        public event EventHandler<Voucher> VoucherEdited;

        private void saveVoucher_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Id.Text) || string.IsNullOrEmpty(Name.Text) ||
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
                VoucherId = Id.Text,
                Name = Name.Text,
                Description = Descript.Text,
                Percent = Percent.Text,
                Amount = Int32.Parse(Amount.Text),
                Condition = Double.Parse(Condition.Text),
                Stock = Int32.Parse(Stock.Text),
                Validate = DateTime.Parse(Validate.SelectedDate.ToString()),
                RankId = RankId.Text
            };

            VoucherEdited?.Invoke(this, voucher);

            this.Visibility = Visibility.Collapsed;
        }
    }
}
