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

namespace BC_Market.ViewModels 
{
    [AddINotifyPropertyChangedInterface]
    public class ShopperDashboardViewModel : ObservableObject
    {
        public ObservableCollection<Product> PagedProducts { get; set; } = new ObservableCollection<Product>();
        private List<Product> AllProducts;
        private List<Product> FilterProduct;
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
        public ShopperDashboardViewModel()
        {
            _bus = _factory.CreateBUS();
            LoadProducts();
            if(CartList !=null)
            {
                ProductInCart = CartList.Count;
            }
            else
            {
                ProductInCart = 0;
            }
            PreviousPageCommand = new RelayCommand(GoPreviousPage);
            NextPageCommand = new RelayCommand(GoNextPage);
            GetCategoryCommand = new Dictionary<string, ICommand> {
                     { "All", new RelayCommand(() => GetProductsByCategory("All")) },
                     { "Vegetable", new RelayCommand(() => GetProductsByCategory("Vet01")) },
                     { "Meat", new RelayCommand(() => GetProductsByCategory("M01")) },
                     { "Milk", new RelayCommand(() => GetProductsByCategory("Mk01")) },
                     { "Seafood", new RelayCommand(() => GetProductsByCategory("SF01")) },
                     { "BH", new RelayCommand(() => GetProductsByCategory("BH01")) }
                 };
            Suggestions = AllProducts.Select(p => p.Name).ToList();
            TextChangedCommand = new RelayCommand<string>(OnTextChanged);
            SuggestionChosenCommand = new RelayCommand<string>(OnSuggestionChosen);
            QuerySubmittedCommand = new RelayCommand<string>(OnQuerySubmitted);
            AddCartCommand = new RelayCommand<Product>(AddCart);
            UpdatePaging(FilterProduct);
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

        //public ICommand LogoutCommand { get; }
        public ICommand TextChangedCommand { get; }
        public ICommand SuggestionChosenCommand { get; }
        public ICommand QuerySubmittedCommand { get; }

        public ICommand AddCartCommand { get; }
        private void LoadProducts()
        {
            AllProducts = new List<Product>();
            AllProducts = _bus.Get(null);
            TotalPages = (int)Math.Ceiling((double)AllProducts.Count / PageSize);
            FilterProduct = AllProducts;
            Console.WriteLine(AllProducts.Count);
        }

        private void GetProductsByCategory(string categoryId)
        {
            try
            {
                // Check null
                if (AllProducts == null || AllProducts.Count == 0)
                    throw new InvalidOperationException("Danh sách sản phẩm trống hoặc null.");

                //Filter
                FilterProduct = categoryId == "All"
                    ? AllProducts
                    : AllProducts.Where(p => p.CategoryId == categoryId).ToList();

                TotalPages = (int)Math.Ceiling((double)FilterProduct.Count / PageSize);
                CurrentPage = 1;
                UpdatePaging(FilterProduct);
            }
            catch (Exception ex)
            {
                // Error
                Debug.WriteLine($"Error in GetProductsByCategory: {ex.Message}");
            }
        }
        private void UpdatePaging(List<Product> Filter)
        {
            PagedProducts.Clear();
            var productsOnPage = Filter.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
            productsOnPage.ForEach(p => PagedProducts.Add(p));

            ((RelayCommand)PreviousPageCommand).NotifyCanExecuteChanged();
            ((RelayCommand)NextPageCommand).NotifyCanExecuteChanged();

        }

        private void OnTextChanged(string text)
        {
           Suggestions = string.IsNullOrWhiteSpace(text) ? new List<string>() :
           Suggestions.Where(item => item.StartsWith(text, StringComparison.OrdinalIgnoreCase)).ToList();
           if(Suggestions.Count == 0)
            {
                Suggestions = AllProducts.Select(p => p.Name).Take(6).ToList();
            }      
        }
            private void OnSuggestionChosen(string chosenItem)
        {
            
            if (chosenItem == null) return;
            chosenItem = chosenItem.Trim().ToLower();
            FilterProduct = AllProducts.Where(p => p.Name.ToLower().Contains(chosenItem)).ToList();
            TotalPages = (int)Math.Ceiling((double)FilterProduct.Count / PageSize);
            CurrentPage = 1;
            UpdatePaging(FilterProduct);
        }

        private void OnQuerySubmitted(string query)
        {
          if(query == null) return;
            query = query.Trim().ToLower();
            FilterProduct = AllProducts.Where(p => p.Name.ToLower().Contains(query)).ToList();
            TotalPages = (int)Math.Ceiling((double)FilterProduct.Count / PageSize);
            CurrentPage = 1;
            UpdatePaging(FilterProduct);
        }
        private void AddCart(Product product)
        {
            ProductInCart++;
            if(CartList.ContainsKey(product))
            {
                CartList[product]++;
            }
            else
            {
                CartList.Add(product, 1);
            }

            OnPropertyChanged(nameof(ProductInCart));
        }
            private void Logout()
        {
            //TODO: Implement Logout
           

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
            UpdatePaging(FilterProduct);

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
            UpdatePaging(FilterProduct);
        }
        
    }
}
