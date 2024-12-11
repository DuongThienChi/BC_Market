using BC_Market.DAO;
using BC_Market.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC_Market.BUS
{
    public class DeliveryBUS : IBUS<DeliveryUnit>
    {
        public readonly IDAO<DeliveryUnit> _dao;

        public DeliveryBUS(IDAO<DeliveryUnit> dao)
        {
            _dao = dao;
        }
        public dynamic Get(Dictionary<string, string> configuration)
        {
            return _dao.Get(configuration);
        }

        public IDAO<DeliveryUnit> Dao()
        {
            return _dao;
        }

        public IBUS<DeliveryUnit> CreateNew(IDAO<DeliveryUnit> dao)
        {
            throw new NotImplementedException();
        }

        public dynamic Add(DeliveryUnit obj)
        {
            return _dao.Add(obj);
        }

        public dynamic Update(DeliveryUnit obj)
        {
            return _dao.Update(obj);
        }

        public dynamic Delete(DeliveryUnit obj)
        {
            return (_dao.Delete(obj));
        }


        // Các phương thức khác...
    }

}

