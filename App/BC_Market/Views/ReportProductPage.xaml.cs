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
    public sealed partial class ReportProductPage : Page
    {
        /// <summary>
        /// Gets or sets the ViewModel for the ReportProductPage.
        /// </summary>
        private ReportProductViewModel ViewModel { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReportProductPage"/> class.
        /// </summary>
        public ReportProductPage()
        {
            this.InitializeComponent();
            ViewModel = new ReportProductViewModel();
            this.DataContext = ViewModel;
        }
    }
}
