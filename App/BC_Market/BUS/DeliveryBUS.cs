using BC_Market.DAO;
using BC_Market.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC_Market.BUS
{
    public class DeliveryBUS : IBUS<Delivery>
    {
        public readonly IDAO<Delivery> _dao;

        public DeliveryBUS(IDAO<Delivery> dao)
        {
            _dao = dao;
        }
        public dynamic Get(Dictionary<string, string> configuration)
        {
            return _dao.Get(configuration);
        }

        public IDAO<Delivery> Dao()
        {
            return _dao;
        }

        public IBUS<Delivery> CreateNew(IDAO<Delivery> dao)
        {
            throw new NotImplementedException();
        }

        public dynamic Add(Delivery obj)
        {
            return _dao.Add(obj);
        }

        public dynamic Update(Delivery obj)
        {
            return _dao.Update(obj);
        }


        // Các phương thức khác...
    }

}

