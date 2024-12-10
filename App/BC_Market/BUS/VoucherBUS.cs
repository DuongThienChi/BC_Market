using BC_Market.DAO;
using BC_Market.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BC_Market.Models;
using Windows.System;

namespace BC_Market.BUS
{
    public class VoucherBUS : IBUS<Voucher>
    {
        public readonly IDAO<Voucher> _dao;
        public IBUS<Voucher> CreateNew(IDAO<Voucher> dao)
        {
            return new VoucherBUS(dao);
        }

        public VoucherBUS(IDAO<Voucher> dao)
        {
            _dao = dao;
        }

        public IDAO<Voucher> Dao()
        {
            return _dao;
        }

        public dynamic Get(Dictionary<string, string> configuration)
        {
            return _dao.Get(configuration);
        }

        public void UpdateUser(object user)
        {
            throw new NotImplementedException();
        }

        public dynamic Add(Voucher obj)
        {
            return _dao.Add(obj);
        }
        public dynamic Update(Voucher obj)
        {
            return _dao.Update(obj);
        }

        public dynamic Delete(Voucher obj)
        {
            return (_dao.Delete(obj));
        }
    }
}
