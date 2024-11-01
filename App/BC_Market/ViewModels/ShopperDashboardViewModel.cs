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
    public class ShopperDashboardViewModel : ObservableObject
    {
        public ObservableCollection<Product> Products { get; set; }
        private int PageSize = 15;
        private int currentPage = 1;
        private IFactory<Product> _factory = new ProductFactory();
        private IBUS<Product> _bus;
        private int totalPages;
        private Dictionary<Product, int> Carts = new Dictionary<Product, int>();
        public Dictionary<Product, int> CartList
        {
            get => Carts;
            set
            {
                if (SetProperty(ref Carts, value))
                {
                    //CartList = Carts;
                    OnPropertyChanged(nameof(CartList));
                }
            }
        }
        public int ProductInCart;
        private List<string> _suggestions;
        public List<string> Suggestions
        {
            get => _suggestions;
            set => SetProperty(ref _suggestions, value);
        }
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

        public bool CanGoPrevious => CurrentPage > 1;
        public bool CanGoNext => CurrentPage < TotalPages;
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
        public ICommand TextChangedCommand { get; }
        public ICommand SuggestionChosenCommand { get; }
        public ICommand QuerySubmittedCommand { get; }

        public ICommand AddCartCommand { get; }
        public ShopperDashboardViewModel()
        {
            PreviousPageCommand = new RelayCommand(GoPreviousPage);
            NextPageCommand = new RelayCommand(GoNextPage);
            _bus = _factory.CreateBUS();
            LoadProducts();
            if (CartList != null)
            {
                ProductInCart = CartList.Count;
            }
            else
            {
                ProductInCart = 0;
            }
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
            AddCartCommand = new RelayCommand<Product>(AddCart);

        }

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
        private void AddCart(Product product)
        {
            ProductInCart++;
            if (CartList.ContainsKey(product))
            {
                CartList[product]++;
            }
            else
            {
                CartList.Add(product, 1);
            }

            OnPropertyChanged(nameof(ProductInCart));
        }
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

    }
}
