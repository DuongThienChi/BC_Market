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
    public class CategoryFactory : IFactory<Category>
    {
        public IBUS<Category> CreateBUS()
        {
            return new CategoryBUS(CreateDAO()); // Tạo và trả về CategoryBUS
        }

        public IDAO<Category> CreateDAO()
        {
            return new CategoryMockDAO(); // Tạo và trả về CategoryDAO
        }
    }
}
