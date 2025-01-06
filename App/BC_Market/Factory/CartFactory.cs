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
    /// Factory class for creating instances of Cart business and data access objects.
    /// </summary>
    public class CartFactory : IFactory<Cart>
    {
        /// <summary>
        /// Creates and returns an instance of CartBUS.
        /// </summary>
        /// <returns>An instance of CartBUS.</returns>
        public IBUS<Cart> CreateBUS()
        {
            return new CartBUS(CreateDAO()); // Tạo và trả về CartBUS
        }
        /// <summary>
        /// Creates and returns an instance of CartDatabaseDAO.
        /// </summary>
        /// <returns>An instance of CartDatabaseDAO.</returns>
        public IDAO<Cart> CreateDAO()
        {
            return new CartDatabaseDAO(); // Tạo và trả về CartDAO
        }
    }
}
