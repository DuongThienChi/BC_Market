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

    class LoginPageViewModel : ObservableObject
    {
        private IFactory<USER> _factory = new UserFactory();
        private IBUS<USER> _bus;
        public ObservableCollection<USER> ListAccount { get; set; }

        public LoginPageViewModel()
        {
            LoadData();
        }

        public void LoadData() // Define the LoadData method
        {
            _bus = _factory.CreateBUS();

            var users = _bus.Get(null); // Get all users

            ListAccount = new ObservableCollection<USER>(users);
        }

        public void AddAccount(USER user) // Add an account
        {
            var dao = _bus.Dao();
            dao.Add(user);
            ListAccount.Add(user);
        }

    }
}
