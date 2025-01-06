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
using System.Diagnostics;

namespace BC_Market.ViewModels
{
    [AddINotifyPropertyChangedInterface]
    public class ShopperOrderViewModel : ObservableObject
    {
        /// <summary>
        /// The XamlRoot for the current view.
        /// </summary>
        public XamlRoot xamlRoot;

        private IFactory<DeliveryUnit> _deliveryFactory = new DeliveryFactory();
        private IBUS<DeliveryUnit> _deliveryBus;
        private IFactory<Voucher> _voucherFactory = new VoucherFactory();
        private IBUS<Voucher> _voucherBus;
        private IFactory<Order> _orderFactory = new OrderFactory();
        private IBUS<Order> _orderBus;
        private DeliveryUnit _selectedDelivery;
        private IFactory<Product> _productFactory = new ProductFactory();
        private IBUS<Product> _productBus;
        private IFactory<USER> _userFactory = new UserFactory();
        private IBUS<USER> _userBus;
        private IFactory<PaymentMethod> _paymentMethodFactory = new PaymentMethodFactory();
        private IBUS<PaymentMethod> _paymentMethodBus;
        private IFactory<Cart> _cartFactory = new CartFactory();
        private IBUS<Cart> _cartBus;
        private IMomoService momoService;

        /// <summary>
        /// Gets or sets the selected delivery unit.
        /// </summary>
        public DeliveryUnit selectedDelivery
        {
            get => _selectedDelivery;
            set => SetProperty(ref _selectedDelivery, value);
        }

        /// <summary>
        /// Gets or sets the selected voucher.
        /// </summary>
        public Voucher selectedVoucher { get; set; }

        private USER _curUser = SessionManager.Get("curCustomer") as USER;

        /// <summary>
        /// Gets or sets the cart.
        /// </summary>
        public Cart cart { get; set; }

        private string _address;

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        public string Address
        {
            get => _address;
            set => SetProperty(ref _address, value);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ShopperOrderViewModel"/> class.
        /// </summary>
        public ShopperOrderViewModel()
        {
            momoService = App.GetService<IMomoService>();
            cart = SessionManager.Get("Cart") as Cart;
            _cartBus = _cartFactory.CreateBUS();
            DeleteItemCommand = new RelayCommand<CartProduct>(DeleteItem);
            DeleteAllCommand = new RelayCommand(DeleteAll);
            OrderCommand = new RelayCommand(Order, CanOrder);
            LoadDelivery();
            LoadVoucher();
            LoadPaymentMethod();
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

        /// <summary>
        /// Gets or sets the payment methods.
        /// </summary>
        public ObservableCollection<PaymentMethod> PaymentMethods { get; set; }

        private PaymentMethod _selectedPaymentMethod;

        /// <summary>
        /// Gets or sets the selected payment method.
        /// </summary>
        public PaymentMethod SelectedPaymentMethod
        {
            get => _selectedPaymentMethod;
            set => SetProperty(ref _selectedPaymentMethod, value);
        }

        /// <summary>
        /// Gets or sets the list of delivery units.
        /// </summary>
        public List<DeliveryUnit> deliveries { get; set; } = new List<DeliveryUnit>();

        /// <summary>
        /// Loads the delivery units.
        /// </summary>
        public void LoadDelivery()
        {
            _deliveryBus = _deliveryFactory.CreateBUS();
            deliveries = _deliveryBus.Get(null);
        }

        /// <summary>
        /// Gets or sets the list of vouchers.
        /// </summary>
        public List<Voucher> Vouchers { get; set; } = new List<Voucher>();

        /// <summary>
        /// Loads the vouchers.
        /// </summary>
        public void LoadVoucher()
        {
            _voucherBus = _voucherFactory.CreateBUS();
            Dictionary<String, String> config = new Dictionary<String, String>();
            config.Add("rankid", _curUser.Rank);
            Vouchers = _voucherBus.Get(config);
        }

        /// <summary>
        /// Loads the payment methods.
        /// </summary>
        public void LoadPaymentMethod()
        {
            _paymentMethodBus = _paymentMethodFactory.CreateBUS();
            PaymentMethods = new ObservableCollection<PaymentMethod>(_paymentMethodBus.Get(null));
        }

        /// <summary>
        /// Gets or sets the selected products.
        /// </summary>
        public List<Product> selectedProducts { get; set; } = new List<Product>();

        private double _total;

        /// <summary>
        /// Gets or sets the total price for all products in the cart.
        /// </summary>
        public double Total
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

        /// <summary>
        /// Gets or sets the delivery cost.
        /// </summary>
        public double DeliveryCost
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

        /// <summary>
        /// Gets or sets the discount amount.
        /// </summary>
        public double DiscountAmount
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

        /// <summary>
        /// Gets or sets the final total price for cashing out.
        /// </summary>
        public double FinalTotal
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

        /// <summary>
        /// Command to delete an item from the cart.
        /// </summary>
        public ICommand DeleteItemCommand { get; }

        /// <summary>
        /// Command to delete all selected items from the cart.
        /// </summary>
        public ICommand DeleteAllCommand { get; }

        /// <summary>
        /// Command to place an order.
        /// </summary>
        public ICommand OrderCommand { get; }

        /// <summary>
        /// Deletes an item from the cart.
        /// </summary>
        /// <param name="product">The product to delete.</param>
        private void DeleteItem(CartProduct product)
        {
            var item = cart.CartProducts.FirstOrDefault(x => x.Product.Id == product.Product.Id);
            if (item != null)
            {
                if (item.Quantity > 1)
                {
                    item.Quantity--;
                    _cartBus.Update(cart);
                }
                else
                {
                    cart.CartProducts.Remove(item);
                    _cartBus.Update(cart);
                }
                OnPropertyChanged(nameof(Total));
                OnPropertyChanged(nameof(FinalTotal));
                OnPropertyChanged(nameof(DiscountAmount));
                OnPropertyChanged(nameof(DeliveryCost));
            }
        }

        /// <summary>
        /// Deletes all selected items from the cart.
        /// </summary>
        private void DeleteAll()
        {
            var itemsToRemove = cart.CartProducts.Where(item => item.IsSelected).ToList();

            foreach (var item in itemsToRemove)
            {
                var product = cart.CartProducts.FirstOrDefault(x => x.Product.Id == item.Product.Id);
                cart.CartProducts.Remove(product);
                _cartBus.Update(cart);
            }
            OnPropertyChanged(nameof(Total));
            OnPropertyChanged(nameof(FinalTotal));
            OnPropertyChanged(nameof(DiscountAmount));
            OnPropertyChanged(nameof(DeliveryCost));
        }

        /// <summary>
        /// Determines whether an order can be placed.
        /// </summary>
        /// <returns>True if an order can be placed; otherwise, false.</returns>
        private bool CanOrder()
        {
            return cart.CartProducts.Count > 0 && !string.IsNullOrEmpty(Address) && selectedDelivery != null && SelectedPaymentMethod != null;
        }

        /// <summary>
        /// Places an order.
        /// </summary>
        private async void Order()
        {
            if (!CanOrder())
            {
                // await ShowDialogAsync("Please ensure you have products in the cart, entered an address, and selected a DeliveryUnit method.", "Order Error");
                return;
            }

            var order = new Order
            {
                Products = new ObservableCollection<CartProduct>(cart.CartProducts),
                customerId = _curUser.Id,
                deliveryId = selectedDelivery.Id,
                totalPrice = (float)Math.Round(_finalTotal, 2),
                address = Address,
                paymentMethod = SelectedPaymentMethod.Id,
                isPaid = false,
                createAt = DateTime.Now
            };
            var param = new
            {
                Order = order,
                SelectedDelivery = selectedDelivery,
                DiscountAmount = DiscountAmount,
                Total = Total,
                PaymentStatus = true,
                Message = "Order created successfully"
            };
            if (SelectedPaymentMethod.Name == "Momo")
            {
                PaymentService.Initialize(momoService);
                bool isCreateOrder = await PaymentService.CreatePaymentAsync(order);
                if (isCreateOrder)
                {
                    Tuple<bool, string> res = await PaymentService.StartPollingAsync();
                    var newParam = new
                    {
                        Order = order,
                        SelectedDelivery = selectedDelivery,
                        DiscountAmount = DiscountAmount,
                        Total = Total,
                        PaymentStatus = res.Item1,
                        Message = res.Item2
                    };
                    NavigationService.Navigate(typeof(OrderSuccessPage), newParam);
                    if (res.Item1)
                    {
                        ProcessOrder(order, param);
                    }
                }
                else
                {
                    await ShowDialogAsync("Payment failed. Please try again.", "Payment Error");
                }
            }
            else
            {
                ProcessOrder(order, param);
            }
        }

        /// <summary>
        /// Processes the order.
        /// </summary>
        /// <param name="order">The order to process.</param>
        /// <param name="param">The parameters for the order.</param>
        private void ProcessOrder(Order order, object param)
        {
            try
            {
                if (_orderBus.Add(order))
                {
                    if (selectedVoucher != null && selectedVoucher.isCondition(Total))
                    {
                        selectedVoucher.Stock--;
                        _voucherBus.Update(selectedVoucher);
                    }

                    foreach (var item in cart.CartProducts)
                    {
                        item.Product.Stock -= item.Quantity;
                        _productBus.Update(item.Product);
                    }
                    _curUser.Point = (int)(_curUser.Point + (_finalTotal / 5));
                    _userBus.Update(_curUser);
                    NavigationService.Navigate(typeof(OrderSuccessPage), param);
                    cart.CartProducts.Clear();
                    // Clear the cart after successful order
                    OnPropertyChanged(nameof(Total));
                    OnPropertyChanged(nameof(FinalTotal));
                    OnPropertyChanged(nameof(DiscountAmount));
                    OnPropertyChanged(nameof(DeliveryCost));
                }
                else
                {
                    // await ShowDialogAsync("Order failed. Please try again.", "Order Error");
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        /// <summary>
        /// Shows a dialog with the specified message and title.
        /// </summary>
        /// <param name="message">The message to display.</param>
        /// <param name="title">The title of the dialog.</param>
        private async Task ShowDialogAsync(string message, string title)
        {
            var dialog = new ContentDialog
            {
                Title = title,
                Content = message,
                CloseButtonText = "OK",
                XamlRoot = this.xamlRoot
            };

            await dialog.ShowAsync();
        }

        /// <summary>
        /// Notifies that a property has changed.
        /// </summary>
        /// <param name="propertyName">The name of the property that changed.</param>
        public void NotifyPropertyChanged(string propertyName)
        {
            OnPropertyChanged(propertyName);
        }
    }

}
