using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BC_Market.BUS;
using BC_Market.DAO;
using BC_Market.Models;

namespace BC_Market.Factory
{
    /// <summary>
    /// Factory class for creating instances of Voucher related objects.
    /// </summary>
    public class VoucherFactory : IFactory<Voucher>
    {
        /// <summary>
        /// Creates and returns an instance of VoucherBUS.
        /// </summary>
        /// <returns>An instance of IBUS&lt;Voucher&gt;.</returns>
        public IBUS<Voucher> CreateBUS()
        {
            return new VoucherBUS(CreateDAO());
        }

        /// <summary>
        /// Creates and returns an instance of VoucherDatabaseDAO.
        /// </summary>
        /// <returns>An instance of IDAO&lt;Voucher&gt;.</returns>
        public IDAO<Voucher> CreateDAO()
        {
            return new VoucherDatabaseDAO();
        }
    }
}
