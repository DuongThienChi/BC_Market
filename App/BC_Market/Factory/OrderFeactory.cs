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
    public class OrderFactory : IFactory<Order>
    {
        public IBUS<Order> CreateBUS()
        {
            return new OrderBUS(CreateDAO()); 
        }

        public IDAO<Order> CreateDAO()
        {
            return new OrderDatabaseDAO();
        }
    }
}
