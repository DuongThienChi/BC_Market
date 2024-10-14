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
    public class ProductFactory : IFactory<Product>
    {
        public IBUS<Product> CreateBUS()
        {
            return new ProductBUS(CreateDAO()); // Tạo và trả về ProductBUS
        }

        public IDAO<Product> CreateDAO()
        {
            return new ProductMockDAO(); // Tạo và trả về ProductDAO
        }
    }
}
