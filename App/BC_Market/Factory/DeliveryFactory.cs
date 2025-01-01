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
    /// Factory class for creating instances of DeliveryUnit related objects.
    /// </summary>
    public class DeliveryFactory : IFactory<DeliveryUnit>
    {
        /// <summary>
        /// Creates and returns an instance of DeliveryBUS.
        /// </summary>
        /// <returns>An instance of IBUS&lt;DeliveryUnit&gt;.</returns>
        public IBUS<DeliveryUnit> CreateBUS()
        {
            return new DeliveryBUS(CreateDAO());
        }
        /// <summary>
        /// Creates and returns an instance of DeliveryUnitDatabaseDAO.
        /// </summary>
        /// <returns>An instance of IDAO&lt;DeliveryUnit&gt;.</returns>
        public IDAO<DeliveryUnit> CreateDAO()
        {
            return new DeliveryUnitDatabaseDAO();
        }

    }
}
