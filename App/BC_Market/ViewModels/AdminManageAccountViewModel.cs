using BC_Market.BUS;
using BC_Market.Converter;
using BC_Market.Factory;
using BC_Market.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.UI.Xaml.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC_Market.ViewModels
{
    public class AdminManageAccountViewModel : ObservableObject
    {
        public ObservableCollection<USER> Items { get; set; }
        private IFactory<USER> _factory = new UserFactory();
        private IBUS<USER> _bus;
        public IdToColorConverter IdToColorConverter { get; set; } = new IdToColorConverter();

        public AdminManageAccountViewModel()
        {
            LoadData();
        }

        public void LoadData()
        {

            _bus = _factory.CreateBUS();

            var users = _bus.Get(null);

            Items = new ObservableCollection<USER>(users);
        }

        public void DeleteAccount(USER user)
        {
            var dao = _bus.Dao();
            dao.Delete(user);
            Items.Remove(user);
        }

        public void Update(USER user)
        {
            var dao = _bus.Dao();
            dao.Update(user);
            foreach(USER item in Items)
            {
                if(item.Id == user.Id)
                {
                    item.Id = user.Id;
                    item.Username = user.Username;
                    item.Password = user.Password;
                    item.Email = user.Email;
                    item.CreatedAt = user.CreatedAt;
                    item.Roles = user.Roles;
                    break;
                }
            }
        }

        public void AddAccount(USER user)
        {
            var dao = _bus.Dao();
            dao.Add(user);
            Items.Add(user);
        }
    }
}
