using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using BC_Market.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;
using PropertyChanged;
using BC_Market.Factory;
using BC_Market.BUS;
using BC_Market.DAO;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;
using System.Net.WebSockets;
using Microsoft.UI.Dispatching;
using BC_Market.Views;
using BC_Market.Services;

namespace BC_Market.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class ShopperOrderViewModel : ObservableObject
    {
        public XamlRoot xamlRoot;
        private IFactory<Delivery> _deliveryFactory = new DeliveryFactory();
        private IBUS<Delivery> _deliveryBus;
        private IFactory<Voucher> _voucherFactory = new VoucherFactory();
        private IBUS<Voucher> _voucherBus;
        private IFactory<Order> _orderFactory = new OrderFactory();
        private IBUS<Order> _orderBus;
        private Delivery _selectedDelivery;
        private IFactory<Product> _productFactory = new ProductFactory();
        private IBUS<Product> _productBus;
        private IFactory<USER> _userFactory = new UserFactory();
        private IBUS<USER> _userBus;
        public Delivery selectedDelivery
        {
            get => _selectedDelivery;
            set => SetProperty(ref _selectedDelivery, value);
        }
        public Voucher selectedVoucher { get; set; } // Define the selectedVoucher field
        private USER _curUser = SessionManager.Get("curCustomer") as USER; // Get the current user from the session
        public Cart cart { get; set; } // Define the cart field
        private string _address;
        public string Address
        {
            get => _address;
            set => SetProperty(ref _address, value);
        }

        public ShopperOrderViewModel()
        {
            cart = SessionManager.Get("Cart") as Cart;
            DeleteItemCommand = new RelayCommand<CartProduct>(DeleteItem);
            DeleteAllCommand = new RelayCommand(DeleteAll);
            OrderCommand = new RelayCommand(Order, CanOrder);
            LoadDelivery();
            LoadVoucher();
            _orderBus = _orderFactory.CreateBUS();
            PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(Address) ||
                    args.PropertyName == nameof(selectedDelivery) ||
                    args.PropertyName == nameof(SelectedPaymentMethod) ||
                    args.PropertyName == nameof(cart.CartProducts))
                {
                    ((RelayCommand)OrderCommand).NotifyCanExecuteChanged();
                }
            };
            _productBus = _productFactory.CreateBUS();
            _userBus = _userFactory.CreateBUS();
        }
        public ObservableCollection<string> PaymentMethods { get; } = new ObservableCollection<string>
             {
                    "Cash",
                    "Credit Card",
                    "Banking"
             };

        private int _selectedPaymentMethod;
        public int SelectedPaymentMethod
        {
            get => _selectedPaymentMethod;
            set => SetProperty(ref _selectedPaymentMethod, value);
        }
        // Delivery
        public List<Delivery> deliveries { get; set; } = new List<Delivery>();
        public void LoadDelivery()
        {
            _deliveryBus = _deliveryFactory.CreateBUS();
            deliveries = _deliveryBus.Get(null);
        }

        // Voucher
        public List<Voucher> Vouchers { get; set; } = new List<Voucher>();
        public void LoadVoucher()
        {
            _voucherBus = _voucherFactory.CreateBUS();
            Dictionary<String, String> config = new Dictionary<String, String>();
            config.Add("rankid", _curUser.Rank);
            Vouchers = _voucherBus.Get(config);
        }
        public List<Product> selectedProducts { get; set; } = new List<Product>(); // Define the selectedProducts for the selected products
        private double _total;

        public ICommand DeleteItemCommand { get; }
        public ICommand DeleteAllCommand { get; }
        public ICommand OrderCommand { get; }
        public double Total // Price for all products in the cart
        {
            get
            {
                _total = 0;
                foreach (var item in cart.CartProducts)
                {
                    _total += item.TotalPrice;
                }
                return _total;
            }
            set => SetProperty(ref _total, value);
        }
        private double deliveryCost = 0;
        public double DeliveryCost // Delivery cost
        {
            get
            {
                if (cart.CartProducts.Count == 0)
                {
                    return 0;
                }
                if (selectedDelivery != null)
                {
                    deliveryCost = selectedDelivery.Price;
                }
                return deliveryCost;
            }
            set => SetProperty(ref deliveryCost, value);
        }
        private double _discountAmount = 0;
        public double DiscountAmount // Discount amount
        {
            get
            {
                if (cart.CartProducts.Count == 0)
                {
                    return 0;
                }
                _discountAmount = 0;
                if (selectedVoucher != null)
                {

                    if (selectedVoucher.isCondition(Total))
                    {
                        if (selectedVoucher.Name.Equals("Free Shipping") && selectedDelivery != null)
                        {
                            return selectedDelivery.Price;
                        }
                        else if (selectedVoucher.Amount > 0)
                        {
                            _discountAmount = selectedVoucher.Amount;
                        }
                        else if (selectedVoucher.Percent > 0)
                        {
                            if (selectedDelivery != null)
                            {
                                _discountAmount = (Total + selectedDelivery.Price) * selectedVoucher.Percent / 100.0;
                            }
                            else
                                _discountAmount = Total * selectedVoucher.Percent / 100.0;
                        }
                    }
                }
                return _discountAmount;
            }
            set => SetProperty(ref _discountAmount, value);
        }
        private double _finalTotal;
        public double FinalTotal // Price for cashing out
        {
            get
            {
                if (cart.CartProducts.Count == 0)
                {
                    return 0;
                }
                _finalTotal = Total;
                if (selectedDelivery != null)
                {
                    _finalTotal += selectedDelivery.Price;
                }
                if (selectedVoucher != null)
                {
                    if (selectedVoucher.isCondition(Total))
                    {
                        if (selectedVoucher.Name.Equals("Free Shipping") && selectedDelivery != null)
                        {
                            _finalTotal -= selectedDelivery.Price;
                        }
                        else if (selectedVoucher.Amount > 0)
                        {
                            _finalTotal -= selectedVoucher.Amount;
                        }
                        else if (selectedVoucher.Percent > 0)
                        {
                            _finalTotal = Math.Round(_finalTotal * (1 - (selectedVoucher.Percent / 100.0)), 2);
                        }
                    }
                }
                return Math.Round(_finalTotal, 2);
            }
            set => SetProperty(ref _finalTotal, value);
        }
        private void DeleteItem(CartProduct product)
        {
            var item = cart.CartProducts.FirstOrDefault(x => x.Product.Id == product.Product.Id);
            if (item != null)
            {
                if (item.Quantity > 1)
                {
                    item.Quantity--;
                }
                else
                {
                    cart.CartProducts.Remove(item);
                }
                OnPropertyChanged(nameof(Total));
                OnPropertyChanged(nameof(FinalTotal));
                OnPropertyChanged(nameof(DiscountAmount));
                OnPropertyChanged(nameof(DeliveryCost));
            }
        }
        private void DeleteAll() // Delete all products from the cart
        {
            var itemsToRemove = cart.CartProducts.Where(item => item.IsSelected).ToList();

            foreach (var item in itemsToRemove)
            {
                var product = cart.CartProducts.FirstOrDefault(x => x.Product.Id == item.Product.Id);
                cart.CartProducts.Remove(product);
            }
            OnPropertyChanged(nameof(Total));
            OnPropertyChanged(nameof(FinalTotal));
            OnPropertyChanged(nameof(DiscountAmount));
            OnPropertyChanged(nameof(DeliveryCost));
        }

        private bool CanOrder()
        {
            return cart.CartProducts.Count > 0 && !string.IsNullOrEmpty(Address) && selectedDelivery != null;
        }

        // Inside the Order method
        private void Order()
        {
            if (!CanOrder())
            {
               // await ShowDialogAsync("Please ensure you have products in the cart, entered an address, and selected a delivery method.", "Order Error");
                return;
            }

            var order = new Order
            {
                Products = new ObservableCollection<CartProduct>(cart.CartProducts),
                customerId = _curUser.Id,
                deliveryId = selectedDelivery.ID,
                totalPrice = (float)Math.Round(_finalTotal, 2),
                address = Address,
                paymentMethod = SelectedPaymentMethod,
                isPaid = false,
                createAt = DateTime.Now
            };

          
            _orderBus.Add(order);
            if (selectedVoucher != null && selectedVoucher.isCondition(Total) )
            {
                selectedVoucher.Stock--;
            }
            _voucherBus.Update(selectedVoucher);
            foreach (var item in cart.CartProducts)
            {
                item.Product.Stock -= item.Quantity;
                _productBus.Update(item.Product);
            }
            _curUser.Point = (int)(_curUser.Point + (_finalTotal / 5));
            _userBus.Update(_curUser);
            //await ShowDialogAsync("Order placed successfully!", "Order Success");
            var param = new
            {
                Order = order,
                SelectedDelivery = selectedDelivery,
                DiscountAmount = DiscountAmount,
                Total = Total
            };
            NavigationService.Navigate(typeof(OrderSuccessPage),param);
            cart.CartProducts.Clear();
            // Clear the cart after successful order
            OnPropertyChanged(nameof(Total));
            OnPropertyChanged(nameof(FinalTotal));
            OnPropertyChanged(nameof(DiscountAmount));
            OnPropertyChanged(nameof(DeliveryCost));
        }
        public void NotifyPropertyChanged(string propertyName)
        {
            OnPropertyChanged(propertyName);
        }

        //private async Task ShowDialogAsync(string message, string title)
        //{
        //    var dialog = new ContentDialog
        //    {
        //        Title = title,
        //        Content = message,
        //        CloseButtonText = "OK",
        //        XamlRoot = this.xamlRoot // Ensure the XamlRoot is set
        //    };

        //    await dialog.ShowAsync();
        //}
    }

}
