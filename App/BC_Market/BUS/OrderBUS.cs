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
    public class OrderBUS : IBUS<Order>
    {
        public readonly IDAO<Order> _dao;

        public IBUS<Order> CreateNew(IDAO<Order> dao)
        {
            return new OrderBUS(dao);
        }

        public OrderBUS(IDAO<Order> dao)
        {
            _dao = dao;
        }
        public dynamic Add(Order order)
        {
            return _dao.Add(order);
        }


        public IDAO<Order> Dao()
        {
            return _dao;
        }

        public dynamic Get(Dictionary<string, string> configuration)
        {
            return _dao.Get(configuration);
        }

        public void UpdateUser(object user)
        {
            throw new NotImplementedException();
        }
        
        public IDAO<Order> Dao()
        {
            return _dao;
        }

        public dynamic Update(Order obj)
        {
            return _dao.Update(obj);
        }

        // Các phương thức khác...
    }

}
