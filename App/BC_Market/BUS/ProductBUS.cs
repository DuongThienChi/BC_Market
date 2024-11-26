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
    public class ProductBUS : IBUS<Product>
    {
        public readonly IDAO<Product> _dao;

        public IBUS<Product> CreateNew(IDAO<Product> dao)
        {
            return new ProductBUS(dao);
        }
        public ProductBUS(IDAO<Product> dao)
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

        public IDAO<Product> Dao()
        {
            return _dao;
        }

        public dynamic Add(Product obj)
        {
             return _dao.Add(obj);
        }

        public dynamic Update(Product obj)
        {
            return _dao.Update(obj);
        }

        // Các phương thức khác...
    }

}
