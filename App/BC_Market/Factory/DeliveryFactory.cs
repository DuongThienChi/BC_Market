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
    public class DeliveryFactory : IFactory<Delivery>
    {
        public IBUS<Delivery> CreateBUS()
        {
            return new DeliveryBUS(CreateDAO());
        }
        public IDAO<Delivery> CreateDAO()
        {
            return new DeliveryDatabaseDAO();
        }

    }
}
