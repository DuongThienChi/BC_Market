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
        public IdToColorConverter IdToColorConverter { get; set; } = new IdToColorConverter();

        public AdminManageAccountViewModel()
        {
            LoadData();
        }

        public void LoadData()
        {
            var factory = new UserFactory();
            var bus = factory.CreateBUS();

            var users = bus.Get(null);

            Items = new ObservableCollection<USER>(users);
        }

    }
}
