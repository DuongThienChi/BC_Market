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
    /// Factory class for creating instances of Product related objects.
    /// </summary>
    public class ProductFactory : IFactory<Product>
    {
        /// <summary>
        /// Creates and returns an instance of ProductBUS.
        /// </summary>
        /// <returns>An instance of IBUS&lt;Product&gt;.</returns>
        public IBUS<Product> CreateBUS()
        {
            return new ProductBUS(CreateDAO()); // Tạo và trả về ProductBUS
        }
        /// <summary>
        /// Creates and returns an instance of ProductDatabaseDAO.
        /// </summary>
        /// <returns>An instance of IDAO&lt;Product&gt;.</returns>
        public IDAO<Product> CreateDAO()
        {
            return new ProductDatabaseDAO(); // Tạo và trả về ProductDAO
        }
    }
}
