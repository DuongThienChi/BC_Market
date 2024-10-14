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
    public class UserBUS : IBUS<USER>
    {
        public readonly IDAO<USER> _dao;

        public IBUS<USER> CreateNew(IDAO<USER> dao)
        {
            return new UserBUS(dao);
        }
        public UserBUS(IDAO<USER> dao)
        {
            _dao = dao;
        }

        public dynamic Get(Dictionary<string, string> configuration)
        {
            return _dao.Get(configuration);
        }
        // Các phương thức khác...
    }

}
