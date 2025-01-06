using BC_Market.BUS;
using BC_Market.Factory;
using BC_Market.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC_Market.ViewModels
{

    /// <summary>
    /// ViewModel for the login page.
    /// </summary>
    class LoginPageViewModel : ObservableObject
    {
        private IFactory<USER> _factory = new UserFactory();
        private IBUS<USER> _bus;
        private IFactory<Cart> _cartFactory = new CartFactory();
        private IBUS<Cart> _cartBus;

        /// <summary>
        /// Gets or sets the collection of user accounts.
        /// </summary>
        public ObservableCollection<USER> ListAccount { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LoginPageViewModel"/> class.
        /// </summary>
        public LoginPageViewModel()
        {
            LoadData();
        }

        /// <summary>
        /// Loads the user data into the ListAccount collection.
        /// </summary>
        public void LoadData()
        {
            _bus = _factory.CreateBUS();
            var users = _bus.Get(null); // Get all users
            ListAccount = new ObservableCollection<USER>(users);
        }

        /// <summary>
        /// Adds a new user account.
        /// </summary>
        /// <param name="user">The user account to add.</param>
        public void AddAccount(USER user)
        {
            var dao = _bus.Dao();
            dao.Add(user);
            ListAccount.Add(user);
        }

        /// <summary>
        /// Loads the cart for the specified customer.
        /// </summary>
        /// <param name="customerID">The ID of the customer.</param>
        public void LoadCart(int customerID)
        {
            _cartBus = _cartFactory.CreateBUS();
            Dictionary<string, string> config = new Dictionary<string, string>
                {
                    { "userId", customerID.ToString() }
                };
            var carts = _cartBus.Get(config);
            SessionManager.Set("Cart", carts);
        }
    }
}
