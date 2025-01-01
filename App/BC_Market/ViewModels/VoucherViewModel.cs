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
    /// <summary>
    /// ViewModel for managing vouchers.
    /// </summary>
    public class VoucherViewModel : ObservableObject
    {
        private ObservableCollection<Voucher> _listVoucher;

        private IFactory<Voucher> _factory = new VoucherFactory();
        private IBUS<Voucher> _bus;

        /// <summary>
        /// Gets or sets the list of vouchers.
        /// </summary>
        public ObservableCollection<Voucher> ListVoucher
        {
            get => _listVoucher;
            set => SetProperty(ref _listVoucher, value);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VoucherViewModel"/> class.
        /// </summary>
        public VoucherViewModel()
        {
            LoadData();
        }

        /// <summary>
        /// Loads the voucher data.
        /// </summary>
        private void LoadData()
        {
            _bus = _factory.CreateBUS();
            var vouchers = _bus.Get(null);
            ListVoucher = new ObservableCollection<Voucher>(vouchers);
        }

        /// <summary>
        /// Adds a new voucher.
        /// </summary>
        /// <param name="voucher">The voucher to add.</param>
        public void AddVoucher(Voucher voucher)
        {
            var dao = _bus.Dao();
            dao.Add(voucher);
            ListVoucher.Add(voucher);
        }

        /// <summary>
        /// Updates an existing voucher.
        /// </summary>
        /// <param name="voucher">The voucher to update.</param>
        public void UpdateVoucher(Voucher voucher)
        {
            var dao = _bus.Dao();
            dao.Update(voucher);
        }

        /// <summary>
        /// Deletes a voucher.
        /// </summary>
        /// <param name="voucher">The voucher to delete.</param>
        public void DeleteVoucher(Voucher voucher)
        {
            var dao = _bus.Dao();
            dao.Delete(voucher);
            ListVoucher.Remove(voucher);
        }
    }
}
