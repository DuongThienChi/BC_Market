using BC_Market.BUS;
using BC_Market.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BC_Market.Models;

namespace BC_Market.Factory
{
    public class DeliveryFactory : IFactory<DeliveryUnit>
    {
        public IBUS<DeliveryUnit> CreateBUS()
        {
            return new DeliveryBUS(CreateDAO());
        }
        public IDAO<DeliveryUnit> CreateDAO()
        {
            return new DeliveryUnitDatabaseDAO();
        }

    }
}
