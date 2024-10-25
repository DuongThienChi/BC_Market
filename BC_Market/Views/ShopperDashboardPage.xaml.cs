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
        private void Button_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            AnimatedIcon.SetState(this.SearchAnimatedIcon, "PointerOver");
        }

        private void Button_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            AnimatedIcon.SetState(this.SearchAnimatedIcon, "Normal");
        }
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
        private void SearchButton_Clicked(object sender, RoutedEventArgs e)
        {
            ViewModel.QuerySubmittedCommand.Execute(SuggestBox.Text);
        }
        private void CartButton_Clicked(object sender, RoutedEventArgs e)
        {
            var cartPage = new ShopperOrderPage();
            cartPage.ViewModel.CartList = new ObservableCollection<KeyValuePair<Product, int>>(ViewModel.CartList);
            this.Frame.Navigate(typeof(ShopperOrderPage), cartPage.ViewModel.CartList);
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var parameters = e.Parameter as dynamic;
            if (parameters != null)
            {
                ViewModel.ProductInCart = parameters.ProductInCart;
                var cartList = parameters.CartList as ObservableCollection<KeyValuePair<Product, int>>;
                if (cartList != null)
                {
                    ViewModel.CartList = cartList.ToDictionary(item => item.Key, item => item.Value);
                }
                else
                {
                    ViewModel.CartList = new Dictionary<Product, int>();
                }
            }
        }
    }
}
