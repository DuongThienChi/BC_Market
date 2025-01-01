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
    /// Factory class for creating instances of Category related objects.
    /// </summary>
    public class CategoryFactory : IFactory<Category>
    {
        /// <summary>
        /// Creates and returns an instance of CategoryBUS.
        /// </summary>
        /// <returns>An instance of IBUS&lt;Category&gt;.</returns>
        public IBUS<Category> CreateBUS()
        {
            return new CategoryBUS(CreateDAO()); // Tạo và trả về CategoryBUS
        }
        /// <summary>
        /// Creates and returns an instance of CategoryDatabaseDAO.
        /// </summary>
        /// <returns>An instance of IDAO&lt;Category&gt;.</returns>
        public IDAO<Category> CreateDAO()
        {
            return new CategoryDatabaseDAO(); // Tạo và trả về CategoryDAO
        }
    }
}
