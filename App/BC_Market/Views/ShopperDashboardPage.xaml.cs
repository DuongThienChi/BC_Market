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
        /// <summary>
        /// Gets or sets the ViewModel for the ShopperDashboardPage.
        /// </summary>
        public ShopperDashboardViewModel ViewModel { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShopperDashboardPage"/> class.
        /// </summary>
        public ShopperDashboardPage()
        {
            this.InitializeComponent();
            ViewModel = new ShopperDashboardViewModel();
            this.DataContext = ViewModel;
        }

        /// <summary>
        /// Handles the PointerEntered event for the search button to animate the icon.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void Button_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            AnimatedIcon.SetState(this.SearchAnimatedIcon, "PointerOver");
        }

        /// <summary>
        /// Handles the PointerExited event for the search button to animate the icon.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void Button_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            AnimatedIcon.SetState(this.SearchAnimatedIcon, "Normal");
        }

        /// <summary>
        /// Handles the QuerySubmitted event for the AutoSuggestBox.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The event data.</param>
        private void AutoSuggestionsBox_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            ViewModel.QuerySubmittedCommand.Execute(args.QueryText);
        }

        /// <summary>
        /// Handles the SuggestionChosen event for the AutoSuggestBox.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The event data.</param>
        private void AutoSuggestionsBox_SuggestionChosen(AutoSuggestBox sender, AutoSuggestBoxSuggestionChosenEventArgs args)
        {
            ViewModel.SuggestionChosenCommand.Execute(args.SelectedItem.ToString());
        }

        /// <summary>
        /// Handles the TextChanged event for the AutoSuggestBox.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="args">The event data.</param>
        private void AutoSuggestionsBox_TextChanged(AutoSuggestBox sender, AutoSuggestBoxTextChangedEventArgs args)
        {
            ViewModel.TextChangedCommand.Execute(sender.Text);
        }

        /// <summary>
        /// Handles the PointerEntered event for the grid to change its background and translate it.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void OnPointerEntered(object sender, PointerRoutedEventArgs e)
        {
            if (sender is Grid grid)
            {
                grid.Background = new SolidColorBrush(Color.FromArgb(255, 240, 240, 245));
                ((CompositeTransform)grid.RenderTransform).TranslateY = -5;
            }
        }

        /// <summary>
        /// Handles the PointerExited event for the grid to reset its background and translation.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void OnPointerExited(object sender, PointerRoutedEventArgs e)
        {
            if (sender is Grid grid)
            {
                grid.Background = new SolidColorBrush(Colors.White);
                ((CompositeTransform)grid.RenderTransform).TranslateY = 0;
            }
        }

        /// <summary>
        /// Handles the Search button click event to execute the search command.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void SearchButton_Clicked(object sender, RoutedEventArgs e)
        {
            ViewModel.QuerySubmittedCommand.Execute(suggestBox.Text);
        }

        /// <summary>
        /// Handles the Cart button click event to navigate to the ShopperOrderPage.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void CartButton_Clicked(object sender, RoutedEventArgs e)
        {
            var cartPage = new ShopperOrderPage();
            this.Frame.Navigate(typeof(ShopperOrderPage));
        }

        /// <summary>
        /// Handles the Sort Price Ascending button click event to sort products by price in ascending order.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void SortPriceAscending_Clicked(object sender, RoutedEventArgs e)
        {
            typeSort.Text = "Price Ascending";
        }

        /// <summary>
        /// Handles the Sort Price Descending button click event to sort products by price in descending order.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void SortPriceDescending_Clicked(object sender, RoutedEventArgs e)
        {
            typeSort.Text = "Price Descending";
        }

        /// <summary>
        /// Invoked when the Page is loaded and becomes the current source of a parent Frame.
        /// </summary>
        /// <param name="e">Event data that can be examined by overriding code. The event data is representative of the pending navigation that will load the current Page. Usually, the event data is a NavigationEventArgs.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
        }

        /// <summary>
        /// Handles the Logout button click event to navigate back to the LoginPage.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void logout_button_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(LoginPage), ViewModel);
        }
    }
}
