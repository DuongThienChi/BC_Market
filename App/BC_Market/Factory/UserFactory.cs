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
    /// Factory class for creating instances of USER related objects.
    /// </summary>
    public class UserFactory : IFactory<USER>
    {
        /// <summary>
        /// Creates and returns an instance of UserBUS.
        /// </summary>
        /// <returns>An instance of IBUS&lt;USER&gt;.</returns>
        public IBUS<USER> CreateBUS()
        {
            return new UserBUS(CreateDAO()); // Tạo và trả về USERBUS
        }
        /// <summary>
        /// Creates and returns an instance of UserDatabaseDAO.
        /// </summary>
        /// <returns>An instance of IDAO&lt;USER&gt;.</returns>
        public IDAO<USER> CreateDAO()
        {
            return new UserDatabaseDAO(); // Tạo và trả về USERDAO
        }
    }
}
