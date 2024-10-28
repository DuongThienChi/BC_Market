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

        public void UpdateUser(object user)
        {
            var listUser = this.Get(null);

            foreach (USER item in listUser)
            {
                if (item.Username == ((USER)user).Username)
                {
                    item.Username = ((USER)user).Username;
                    item.Password = ((USER)user).Password;
                    item.Roles = ((USER)user).Roles;
                    _dao.Add(item);
                    break;
                }
            }
        }

        IDAO<USER> IBUS<USER>.Dao() => _dao;
    }

}
