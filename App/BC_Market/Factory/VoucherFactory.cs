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
    public class VoucherFactory : IFactory<Voucher>
    {
        public IBUS<Voucher> CreateBUS()
        {
            return new VoucherBUS(CreateDAO());
        }

        public IDAO<Voucher> CreateDAO()
        {
            return new VoucherDatabaseDAO();
        }
    }
}
