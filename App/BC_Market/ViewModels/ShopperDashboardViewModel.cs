using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.ComponentModel;
using BC_Market.Models;
using BC_Market.Factory;
using CommunityToolkit.Mvvm.Input;
using System.Windows.Input;
using BC_Market.BUS;
using BC_Market.Views;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;
using CommunityToolkit.Mvvm.ComponentModel;
using PropertyChanged;
using Windows.ApplicationModel.Store;
using Microsoft.UI.Xaml.Controls;

namespace BC_Market.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class ShopperDashboardViewModel : ObservableObject // Define the ShopperDashboardViewModel class
    {
        public ObservableCollection<Product> Products { get; set; } // Products property
        private int PageSize = 15; // Define the PageSize field
        private int currentPage = 1; // Define the currentPage field
        private IFactory<Product> _factory = new ProductFactory();
        private IBUS<Product> _bus;
        private int totalPages; // Define the totalPages field
        public Cart cart { get; set; } // Define the cart field
        public int ProductInCart; // Number of products in the cart
        private List<string> _suggestions; 
        public List<string> Suggestions
        {
            get => _suggestions;
            set => SetProperty(ref _suggestions, value);
        }
        public int Skip // Skip pagee
        {
            get => (CurrentPage - 1) * PageSize;
            set
            {
                if (Skip != value)
                {
                    Skip = value;
                    OnPropertyChanged(nameof(Skip));
                }
            }
        }
        public int CurrentPage //Current page
        {
            get => currentPage;
            set
            {
                if (currentPage != value)
                {
                    currentPage = value;
                    OnPropertyChanged(nameof(CurrentPage));
                }

            }
        }
        public int TotalPages // Total pages
        {
            get => totalPages;
            set
            {
                if (totalPages != value)
                {
                    totalPages = value;
                    OnPropertyChanged(nameof(TotalPages));  // Notify the TotalPages property has changed => Update the UI
                }
            }
        }

        public bool CanGoPrevious => CurrentPage > 1; // Check if can go to the previous page
        public bool CanGoNext => CurrentPage < TotalPages; // Check if can go to the next page
        public ICommand PreviousPageCommand { get; }
        public ICommand NextPageCommand { get; }
        public Dictionary<string, ICommand> GetCategoryCommand { set; get; }

        private Dictionary<string, string> _configuration = new Dictionary<string, string>
            {
                { "searchKey", "" },
                { "category", "" },
                { "skip", "0" },
                { "take", "15" }
            };

        public Dictionary<string, string> configuration // Configuration property
        {
            get => _configuration;
            set
            {
                if (_configuration != value)
                {
                    _configuration = value;
                    OnPropertyChanged(nameof(configuration));
                }
            }
        }
        public ICommand TextChangedCommand { get; }
        public ICommand SuggestionChosenCommand { get; }
        public ICommand QuerySubmittedCommand { get; }

        public ICommand AddCartCommand { get; }
        public ShopperDashboardViewModel()
        {
            PreviousPageCommand = new RelayCommand(GoPreviousPage);
            NextPageCommand = new RelayCommand(GoNextPage);
            cart = SessionManager.Get("Cart") as Cart;
            _bus = _factory.CreateBUS();
            LoadProducts(); // Load products
            ProductInCart = cart.count;
            // Define the Commands for UI
            GetCategoryCommand = new Dictionary<string, ICommand> {
                     { "All", new RelayCommand(() => GetProductsByCategory("All")) },
                     { "Vegetable", new RelayCommand(() => GetProductsByCategory("Vegetable")) },
                     { "Meat", new RelayCommand(() => GetProductsByCategory("Meat")) },
                     { "Milk", new RelayCommand(() => GetProductsByCategory("Milk")) },
                     { "Seafood", new RelayCommand(() => GetProductsByCategory("Seafood")) },
                     { "BH", new RelayCommand(() => GetProductsByCategory("Beauty & Health")) }
                 };
            Suggestions = Products.Select(p => p.Name).ToList();
            TextChangedCommand = new RelayCommand<string>(OnTextChanged);
            SuggestionChosenCommand = new RelayCommand<string>(OnSuggestionChosen);
            QuerySubmittedCommand = new RelayCommand<string>(OnQuerySubmitted);
            AddCartCommand = new RelayCommand<Product>(AddCart, CanAddToCart);

        }

        private void LoadProducts()  // Load products
        {
            var configCount = new Dictionary<string, string>(configuration);
            configCount["take"] = "100000";
            configCount["skip"] = "0";
            var count = _bus.Get(configCount); // Get the count of products
            var res = _bus.Get(configuration); // Get the products
            Products = new ObservableCollection<Product>(res);
            TotalPages = (int)Math.Ceiling((double)count / PageSize);
            ((RelayCommand)PreviousPageCommand).NotifyCanExecuteChanged();
            ((RelayCommand)NextPageCommand).NotifyCanExecuteChanged();
        }
        private void GetProductsByCategory(string category)
        {
            try
            {

                //Filter
                CurrentPage = 1;
                if (category != "All")
                    configuration["category"] = category;
                else
                    configuration["category"] = "";
                configuration["skip"] = Skip.ToString();
                LoadProducts();
            }
            catch (Exception ex)
            {
                // Error
                Debug.WriteLine($"Error in GetProductsByCategory: {ex.Message}");
            }
        }
        private void OnTextChanged(string text) // OnTextChanged method for AutoSuggestBox
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                Suggestions = new List<string>();
            }
            else
            {
                var newConfig = new Dictionary<string, string>(configuration);
                newConfig["skip"] = "0";
                newConfig["take"] = "6";
                newConfig["searchKey"] = text;
                var suggestProduct = _bus.Get(newConfig); // Get the suggested products
                Suggestions = ((IEnumerable<Product>)suggestProduct).Select(p => p.Name).ToList();
            }
        }
        private void OnSuggestionChosen(string chosenItem) // OnSuggestionChosen method for AutoSuggestBox
        {

            if (chosenItem == null) return;
            CurrentPage = 1;
            chosenItem = chosenItem.Trim();
            configuration = new Dictionary<string, string>
            {
                { "searchKey", chosenItem },
                { "category", "" },
                { "skip", Skip.ToString() },
                { "take", PageSize.ToString() }
            };
            LoadProducts();
        }
        private void OnQuerySubmitted(string query) // OnQuerySubmitted method for AutoSuggestBox
        {
            if (query == null) return;
            CurrentPage = 1;
            query = query.Trim();
            configuration = new Dictionary<string, string>
                {
                    { "searchKey", query },
                    { "category", "" },
                    { "skip", Skip.ToString() },
                    { "take", PageSize.ToString() }
                };
            LoadProducts();

        }
        private void AddCart(Product product) // Add a product to the cart
        {
            foreach (var item in cart.CartProducts)
            {
                if (item.Product.Id == product.Id) // Check if the product is already in the cart
                {
                    if (item.Quantity >= product.Stock) // Check if the quantity exceeds the stock
                    {
                        ((RelayCommand<Product>)AddCartCommand).NotifyCanExecuteChanged();
                        return;
                    }

                    item.Quantity += 1;
                    OnPropertyChanged(nameof(ProductInCart));
                    ((RelayCommand<Product>)AddCartCommand).NotifyCanExecuteChanged();
                    return;
                }
            }

            if (product.Stock > 0) // Check if the product is in stock
            {
                cart.CartProducts.Add(new CartProduct { Product = product, Quantity = 1 });
                ProductInCart++;
                OnPropertyChanged(nameof(ProductInCart));
                ((RelayCommand<Product>)AddCartCommand).NotifyCanExecuteChanged();
            }
            else
            {
                ((RelayCommand<Product>)AddCartCommand).NotifyCanExecuteChanged();
            }
        }
        private void GoPreviousPage() // Go to the previous page
        {

            if (CanGoPrevious)
            {
                CurrentPage--;
            }
            else
            {
                CurrentPage = TotalPages;
            }
            configuration["skip"] = Skip.ToString();
            LoadProducts();

        }
        private void GoNextPage() // Go to the next page
        {
            if (CanGoNext)
            {
                CurrentPage++;
            }
            else
            {
                CurrentPage = 1;
            }

            configuration["skip"] = Skip.ToString();
            LoadProducts();
        }
        private bool CanAddToCart(Product product)
        {
            var cartProduct = cart.CartProducts.FirstOrDefault(item => item.Product.Id == product.Id);
            if (cartProduct == null)
            {
                return product.Stock > 0;
            }
            return cartProduct.Quantity < product.Stock;
        }
    }
}
