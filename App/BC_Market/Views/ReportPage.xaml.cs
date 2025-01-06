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
    public sealed partial class ReportPage : Page
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ReportPage"/> class.
        /// </summary>
        public ReportPage()
        {
            this.InitializeComponent();
            report_frame.Navigate(typeof(ReportProductPage));
        }

        /// <summary>
        /// Handles the Product Report button click event to navigate to the ReportProductPage.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void product_report_Click(object sender, RoutedEventArgs e)
        {
            report_frame.Navigate(typeof(ReportProductPage));
        }

        /// <summary>
        /// Handles the Order Report button click event to navigate to the ReportOrderPage.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void order_report_Click(object sender, RoutedEventArgs e)
        {
            report_frame.Navigate(typeof(ReportOrderPage));
        }
    }
}
