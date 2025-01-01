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
    /// <summary>
    /// Factory class for creating instances of Order related objects.
    /// </summary>
    public class OrderFactory : IFactory<Order>
    {
        /// <summary>
        /// Creates and returns an instance of OrderBUS.
        /// </summary>
        /// <returns>An instance of IBUS&lt;Order&gt;.</returns>
        public IBUS<Order> CreateBUS()
        {
            return new OrderBUS(CreateDAO()); // Tạo và trả về OrderBUS
        }
        /// <summary>
        /// Creates and returns an instance of OrderDatabaseDAO.
        /// </summary>
        /// <returns>An instance of IDAO&lt;Order&gt;.</returns>
        public IDAO<Order> CreateDAO()
        {
            return new OrderDatabaseDAO(); // Tạo và trả về OrderDAO
        }
    }
}
