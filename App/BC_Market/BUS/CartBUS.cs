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
    public class CartBUS : IBUS<Cart>
    {
        public readonly IDAO<Cart> _dao;

        public IBUS<Cart> CreateNew(IDAO<Cart> dao)
        {
            return new CartBUS(dao);
        }
        public CartBUS(IDAO<Cart> dao)
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

        public IDAO<Cart> Dao()
        {
            return _dao;
        }

        public dynamic Add(Cart obj)
        {
            return _dao.Add(obj);
        }

        public dynamic Update(Cart obj)
        {
            return _dao.Update(obj);
        }

        public dynamic Delete(Cart obj)
        {
            throw new NotImplementedException();
        }

        // Các phương thức khác...
    }

}
