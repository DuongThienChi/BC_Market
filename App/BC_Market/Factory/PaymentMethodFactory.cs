using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BC_Market.Models;
using BC_Market.BUS;
using BC_Market.DAO;

namespace BC_Market.Factory
{
    public class PaymentMethodFactory : IFactory<PaymentMethod>
    {
        public IBUS<PaymentMethod> CreateBUS()
        {
            return new PaymentMethodBUS(CreateDAO()); // Tạo và trả về PaymentMethodBUS
        }

        public IDAO<PaymentMethod> CreateDAO()
        {
            return new PaymentMethodDatabaseDAO(); // Tạo và trả về PaymentMethodDAO
        }
    }
}
