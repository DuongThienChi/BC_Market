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
    /// <summary>
    /// ViewModel for the shopper dashboard.
    /// </summary>
    [AddINotifyPropertyChangedInterface]
    public class ShopperDashboardViewModel : ObservableObject
    {
        /// <summary>
        /// Gets or sets the collection of products.
        /// </summary>
        public ObservableCollection<Product> Products { get; set; }

        private int PageSize = 15;
        private int currentPage = 1;
        private IFactory<Product> _factory = new ProductFactory();
        private IBUS<Product> _bus;
        private IFactory<Category> _categoryFactory = new CategoryFactory();
        private IBUS<Category> _categoryBUS;
        private IFactory<Cart> _cartFactory = new CartFactory();
        private IBUS<Cart> _cartBus;
        private int totalPages;

        /// <summary>
        /// Gets or sets the cart.
        /// </summary>
        public Cart cart { get; set; }

        /// <summary>
        /// Gets or sets the number of products in the cart.
        /// </summary>
        public int ProductInCart;

        /// <summary>
        /// Gets or sets the list of categories.
        /// </summary>
        public List<Category> Categories { get; set; }

        private List<string> _suggestions;

        /// <summary>
        /// Gets or sets the list of suggestions.
        /// </summary>
        public List<string> Suggestions
        {
            get => _suggestions;
            set => SetProperty(ref _suggestions, value);
        }

        /// <summary>
        /// Gets or sets the number of items to skip.
        /// </summary>
        public int Skip
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

        /// <summary>
        /// Gets or sets the current page.
        /// </summary>
        public int CurrentPage
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

        /// <summary>
        /// Gets or sets the total number of pages.
        /// </summary>
        public int TotalPages
        {
            get => totalPages;
            set
            {
                if (totalPages != value)
                {
                    totalPages = value;
                    OnPropertyChanged(nameof(TotalPages));
                }
            }
        }

        /// <summary>
        /// Gets a value indicating whether the user can go to the previous page.
        /// </summary>
        public bool CanGoPrevious => CurrentPage > 1;

        /// <summary>
        /// Gets a value indicating whether the user can go to the next page.
        /// </summary>
        public bool CanGoNext => CurrentPage < TotalPages;

        /// <summary>
        /// Gets the command to go to the previous page.
        /// </summary>
        public ICommand PreviousPageCommand { get; }

        /// <summary>
        /// Gets the command to go to the next page.
        /// </summary>
        public ICommand NextPageCommand { get; }

        private Dictionary<string, string> _configuration = new Dictionary<string, string>
            {
                { "searchKey", "" },
                { "category", "" },
                { "skip", "0" },
                { "take", "15" }
            };

        /// <summary>
        /// Gets or sets the configuration dictionary.
        /// </summary>
        public Dictionary<string, string> configuration
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

        /// <summary>
        /// Gets the command to handle text changes.
        /// </summary>
        public ICommand TextChangedCommand { get; }

        /// <summary>
        /// Gets the command to handle suggestion chosen.
        /// </summary>
        public ICommand SuggestionChosenCommand { get; }

        /// <summary>
        /// Gets the command to handle query submission.
        /// </summary>
        public ICommand QuerySubmittedCommand { get; }

        /// <summary>
        /// Gets the command to add a product to the cart.
        /// </summary>
        public ICommand AddCartCommand { get; }

        /// <summary>
        /// Gets the command to get products by category.
        /// </summary>
        public ICommand GetCategoryCommand { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShopperDashboardViewModel"/> class.
        /// </summary>
        public ShopperDashboardViewModel()
        {
            PreviousPageCommand = new RelayCommand(GoPreviousPage);
            NextPageCommand = new RelayCommand(GoNextPage);
            cart = SessionManager.Get("Cart") as Cart;
            _bus = _factory.CreateBUS();
            _cartBus = _cartFactory.CreateBUS();
            LoadProducts();
            LoadCategory();
            ProductInCart = cart.count;
            GetCategoryCommand = new RelayCommand<Category>(GetProductsByCategory);
            Suggestions = Products.Select(p => p.Name).ToList();
            TextChangedCommand = new RelayCommand<string>(OnTextChanged);
            SuggestionChosenCommand = new RelayCommand<string>(OnSuggestionChosen);
            QuerySubmittedCommand = new RelayCommand<string>(OnQuerySubmitted);
            AddCartCommand = new RelayCommand<Product>(AddCart, CanAddToCart);
        }

        /// <summary>
        /// Loads the categories.
        /// </summary>
        public void LoadCategory()
        {
            _categoryBUS = _categoryFactory.CreateBUS();
            Categories = new List<Category>(_categoryBUS.Get(null));
        }

        /// <summary>
        /// Loads the products.
        /// </summary>
        private void LoadProducts()
        {
            var configCount = new Dictionary<string, string>(configuration);
            configCount["take"] = "100000";
            configCount["skip"] = "0";
            var count = _bus.Get(configCount);
            var res = _bus.Get(configuration);
            Products = new ObservableCollection<Product>(res);
            TotalPages = (int)Math.Ceiling((double)count / PageSize);
            ((RelayCommand)PreviousPageCommand).NotifyCanExecuteChanged();
            ((RelayCommand)NextPageCommand).NotifyCanExecuteChanged();
        }

        /// <summary>
        /// Gets the products by category.
        /// </summary>
        /// <param name="category">The category to filter by.</param>
        private void GetProductsByCategory(Category category)
        {
            try
            {
                CurrentPage = 1;
                if (category != null)
                    configuration["category"] = category.Name;
                else
                    configuration["category"] = "";
                configuration["skip"] = Skip.ToString();
                LoadProducts();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in GetProductsByCategory: {ex.Message}");
            }
        }

        /// <summary>
        /// Handles text changes for the AutoSuggestBox.
        /// </summary>
        /// <param name="text">The text that changed.</param>
        private void OnTextChanged(string text)
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
                var suggestProduct = _bus.Get(newConfig);
                Suggestions = ((IEnumerable<Product>)suggestProduct).Select(p => p.Name).ToList();
            }
        }

        /// <summary>
        /// Handles suggestion chosen for the AutoSuggestBox.
        /// </summary>
        /// <param name="chosenItem">The chosen suggestion.</param>
        private void OnSuggestionChosen(string chosenItem)
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

        /// <summary>
        /// Handles query submission for the AutoSuggestBox.
        /// </summary>
        /// <param name="query">The submitted query.</param>
        private void OnQuerySubmitted(string query)
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

        /// <summary>
        /// Adds a product to the cart.
        /// </summary>
        /// <param name="product">The product to add.</param>
        private void AddCart(Product product)
        {
            foreach (var item in cart.CartProducts)
            {
                if (item.Product.Id == product.Id)
                {
                    if (item.Quantity >= product.Stock)
                    {
                        ((RelayCommand<Product>)AddCartCommand).NotifyCanExecuteChanged();
                        return;
                    }
                    item.Quantity += 1;
                    _cartBus.Update(cart);
                    OnPropertyChanged(nameof(ProductInCart));
                    ((RelayCommand<Product>)AddCartCommand).NotifyCanExecuteChanged();
                    return;
                }
            }

            if (product.Stock > 0)
            {
                cart.CartProducts.Add(new CartProduct { Product = product, Quantity = 1 });
                _cartBus.Update(cart);
                ProductInCart++;
                OnPropertyChanged(nameof(ProductInCart));
                ((RelayCommand<Product>)AddCartCommand).NotifyCanExecuteChanged();
            }
            else
            {
                ((RelayCommand<Product>)AddCartCommand).NotifyCanExecuteChanged();
            }
        }

        /// <summary>
        /// Goes to the previous page.
        /// </summary>
        private void GoPreviousPage()
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

        /// <summary>
        /// Goes to the next page.
        /// </summary>
        private void GoNextPage()
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

        /// <summary>
        /// Determines whether a product can be added to the cart.
        /// </summary>
        /// <param name="product">The product to check.</param>
        /// <returns>True if the product can be added; otherwise, false.</returns>
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
