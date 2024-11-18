using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using BC_Market.Models;
using BC_Market.Factory;
using BC_Market.BUS;

namespace BC_Market.ViewModels
{
    public class VoucherViewModel : ObservableObject
    {
        private ObservableCollection<Voucher> _listVoucher;

        private IFactory<Voucher> _factory = new VoucherFactory();
        private IBUS<Voucher> _bus;

        public ObservableCollection<Voucher> ListVoucher
        {
            get => _listVoucher;
            set => SetProperty(ref _listVoucher, value);
        }

        public VoucherViewModel()
        {
            LoadData();
        }

        private void LoadData()
        {
            _bus = _factory.CreateBUS();
            var vouchers = _bus.Get(null);
            ListVoucher = new ObservableCollection<Voucher>(vouchers);
        }

        public void AddVoucher(Voucher voucher)
        {
            var dao = _bus.Dao();
            dao.Add(voucher);
            ListVoucher.Add(voucher);
        }

        public void UpdateVoucher(Voucher voucher)
        {
            var dao = _bus.Dao();
            dao.Update(voucher);
        }

        public void DeleteVoucher(Voucher voucher)
        {
            var dao = _bus.Dao();
            dao.Delete(voucher);
            ListVoucher.Remove(voucher);
        }
    }
}
