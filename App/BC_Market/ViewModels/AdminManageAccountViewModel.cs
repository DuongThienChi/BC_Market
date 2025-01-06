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
    /// <summary>
    /// ViewModel for managing user accounts in the admin panel.
    /// </summary>
    public class AdminManageAccountViewModel : ObservableObject
    {
        /// <summary>
        /// Gets or sets the collection of user items.
        /// </summary>
        public ObservableCollection<USER> Items { get; set; }

        private IFactory<USER> _factory = new UserFactory();
        private IBUS<USER> _bus;

        /// <summary>
        /// Gets or sets the converter for converting user IDs to colors.
        /// </summary>
        public IdToColorConverter IdToColorConverter { get; set; } = new IdToColorConverter();

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminManageAccountViewModel"/> class.
        /// </summary>
        public AdminManageAccountViewModel()
        {
            LoadData();
        }

        /// <summary>
        /// Loads the user data into the Items collection.
        /// </summary>
        public void LoadData()
        {
            _bus = _factory.CreateBUS();
            var users = _bus.Get(null); // Get all users
            Items = new ObservableCollection<USER>(users);
        }

        /// <summary>
        /// Deletes the specified user account.
        /// </summary>
        /// <param name="user">The user account to delete.</param>
        public void DeleteAccount(USER user)
        {
            var dao = _bus.Dao();
            dao.Delete(user);
            Items.Remove(user);
        }

        /// <summary>
        /// Updates the specified user account.
        /// </summary>
        /// <param name="user">The user account to update.</param>
        public void Update(USER user)
        {
            var dao = _bus.Dao();
            dao.Update(user);
            foreach (USER item in Items)
            {
                if (item.Id == user.Id)
                {
                    item.Id = user.Id;
                    item.Username = user.Username;
                    item.Password = user.Password;
                    item.Email = user.Email;
                    item.CreatedAt = user.CreatedAt;
                    item.Roles = user.Roles;
                    item.Point = user.Point;
                    break;
                }
            }
        }

        /// <summary>
        /// Adds a new user account.
        /// </summary>
        /// <param name="user">The user account to add.</param>
        public void AddAccount(USER user)
        {
            var dao = _bus.Dao();
            dao.Add(user);
            Items.Add(user);
        }
    }
}
