using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BC_Market.Models;
using BC_Market.DAO;
using BC_Market.Factory;
using BC_Market.ViewModels;

namespace BC_Market.BUS
{
    public class PaymentMethodBUS : IBUS<PaymentMethod>
    {
        public readonly IDAO<PaymentMethod> _dao;

        public IBUS<PaymentMethod> CreateNew(IDAO<PaymentMethod> dao)
        {
            return new PaymentMethodBUS(dao);
        }

        public PaymentMethodBUS(IDAO<PaymentMethod> dao)
        {
            _dao = dao;
        }


        public IDAO<PaymentMethod> Dao()
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
    }
}
