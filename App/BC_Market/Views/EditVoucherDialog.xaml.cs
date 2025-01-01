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
    /// <summary>
    /// A user control for editing a voucher.
    /// </summary>
    public sealed partial class EditVoucherDialog : UserControl
    {
        /// <summary>
        /// Gets or sets the voucher to be edited.
        /// </summary>
        public Voucher voucher { get; set; } = new Voucher();

        /// <summary>
        /// Initializes a new instance of the <see cref="EditVoucherDialog"/> class.
        /// </summary>
        public EditVoucherDialog()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Closes the dialog by setting its visibility to collapsed.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void closeDialog_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Occurs when a voucher is edited.
        /// </summary>
        public event EventHandler<Voucher> VoucherEdited;

        /// <summary>
        /// Handles the Save button click event to save the edited voucher.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
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

            // Create new voucher from entered information
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

            // Check to confirm all fields are filled
            VoucherEdited?.Invoke(this, voucher);

            this.Visibility = Visibility.Collapsed;
        }
    }
}
