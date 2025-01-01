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
using BC_Market.BUS;
using BC_Market.Models;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BC_Market.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ManagerOrderPage : Page
    {
        /// <summary>
        /// Gets or sets the ViewModel for the ManagerOrderPage.
        /// </summary>
        public ManagerOrderPageViewModel ViewModel { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ManagerOrderPage"/> class.
        /// </summary>
        public ManagerOrderPage()
        {
            this.InitializeComponent();
            ViewModel = new ManagerOrderPageViewModel();
            this.DataContext = ViewModel;
        }

        /// <summary>
        /// Handles the DatePicker date changed event to filter orders by the selected date.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void OrderDatePicker_DateChanged(object sender, DatePickerValueChangedEventArgs e)
        {
            var selectedDate = new DateTimeOffset(e.NewDate.Date);
            ViewModel.FilterOrdersByDateCommand.Execute(selectedDate);
        }

        /// <summary>
        /// Handles the View button click event to navigate to the ViewDetailOrderPage.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void view_btn_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var order = button.DataContext as Order;
            Dictionary<string, string> configuration = new Dictionary<string, string>
                {
                    { "OrderId", order.Id.ToString() }
                };
            order.Products = ViewModel._orderBus.Get(configuration);
            this.Frame.Navigate(typeof(ViewDetailOrderPage), order);
        }
    }
}
