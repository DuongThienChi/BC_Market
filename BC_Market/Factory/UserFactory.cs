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
    public class UserFactory : IFactory<USER>
    {
        public  IBUS<USER> CreateBUS()
        {
            return new UserBUS(CreateDAO()); // Tạo và trả về USERBUS
        }

        public IDAO<USER> CreateDAO()
        {
            return new UserDatabaseDAO(); // Tạo và trả về USERDAO
        }
    }
}
