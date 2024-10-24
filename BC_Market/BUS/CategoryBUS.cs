using BC_Market.DAO;
using BC_Market.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BC_Market.Models;
using Windows.System;
namespace BC_Market.BUS
{
    public class CategoryBUS : IBUS<Category>
    {
        public readonly IDAO<Category> _dao;

        public IBUS<Category> CreateNew(IDAO<Category> dao)
        {
            return new CategoryBUS(dao);
        }
        public CategoryBUS(IDAO<Category> dao)
        {
            _dao = dao;
        }

        public dynamic Get(Dictionary<string, string> configuration)
        {
            return _dao.Get(configuration);
        }

        public void UpdateUser(object user)
        {
            throw new NotImplementedException();
        }

        // Các phương thức khác...
    }

}
