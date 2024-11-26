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
using Windows.UI.ApplicationSettings;
using BC_Market.ViewModels;
using BC_Market.Models;
using System.Collections.ObjectModel;
using Microsoft.UI;
using Windows.UI;

// To learn more about WinUI, the WinUI project structure,
// and more about our project templates, see: http://aka.ms/winui-project-info.

namespace BC_Market.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ShopperDashboardPage : Page
    {
        public ShopperDashboardViewModel ViewModel { get; set; }

        public ShopperDashboardPage()
        {
            this.InitializeComponent();
            ViewModel = new ShopperDashboardViewModel();
            this.DataContext = ViewModel;
        }
        // Animation for search icon
        private void Button_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            AnimatedIcon.SetState(this.SearchAnimatedIcon, "PointerOver");
        }

        private void Button_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            AnimatedIcon.SetState(this.SearchAnimatedIcon, "Normal");
        }
        // AutoSuggestBox
        private void AutoSuggestionsBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            ViewModel.QuerySubmittedCommand.Execute(args.QueryText);
        }
        private void AutoSuggestionsBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            ViewModel.SuggestionChosenCommand.Execute(args.SelectedItem.ToString());
        }
        private void AutoSuggestionsBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            ViewModel.TextChangedCommand.Execute(sender.Text);
        }
        private void OnPointerEntered(object sender, PointerRoutedEventArgs e)
        {
            if (sender is Grid grid)
            {
                grid.Background = new SolidColorBrush(Color.FromArgb(255, 240, 240, 245));
                ((CompositeTransform)grid.RenderTransform).TranslateY = -5;
            }
        }

        private void OnPointerExited(object sender, PointerRoutedEventArgs e)
        {
            if (sender is Grid grid)
            {
                grid.Background = new SolidColorBrush(Colors.White);
                ((CompositeTransform)grid.RenderTransform).TranslateY = 0;
            }
        }
        private void SearchButton_Clicked(object sender, RoutedEventArgs e)
        {
            ViewModel.QuerySubmittedCommand.Execute(suggestBox.Text);
        }
        private void CartButton_Clicked(object sender, RoutedEventArgs e)
        {
            var cartPage = new ShopperOrderPage();
            //SessionManager.Set("Cart", ViewModel.cart);
            this.Frame.Navigate(typeof(ShopperOrderPage));
        }
        private void SortPriceAscending_Clicked(object sender, RoutedEventArgs e)
        {
            //ViewModel.SortProductsByPriceAscending();
            typeSort.Text = "Price Ascending";
        }

        private void SortPriceDescending_Clicked(object sender, RoutedEventArgs e)
        {
            typeSort.Text = "Price Descending";
            // ViewModel.SortProductsByPriceDescending();
        }

        // Get data from previous page and set to ViewModel
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
        }
        private void logout_button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(LoginPage), ViewModel);
        }
    }
}
