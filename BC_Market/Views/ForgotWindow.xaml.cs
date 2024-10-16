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

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BC_Market
{
    /// <summary>
    /// An empty window that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ForgotWindow : Window
    {
        public ForgotWindow()
        {
            this.InitializeComponent();
        }

        private void submit_button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void TextBlock_Tapped(object sender, TappedRoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Closed(object sender, WindowEventArgs args)
        {
            var loginWindow = new LoginWindow();
            loginWindow.Activate();

            this.Close();
        }
    }
}
