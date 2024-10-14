using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BC_Market.Models;
using BC_Market.BUS;
using BC_Market.Factory;
using Windows.System;

namespace BC_Market.DAO
{
    public class UserMockDAO : IDAO<USER>
    {
        public UserMockDAO() { }
        public dynamic Get(Dictionary<string, string> configuration)
        {
            var users = new List<USER>
          {
              new USER {
                  Id = "1",
                  Username = "admin",
                  Password = "1234" ,
                  Email = "admin@gmail.com",
                  Roles = new List<Role> { new Role { Name = "Admin" }
                  } },
              new USER {
                  Id = "2",
                  Username = "manager",
                  Password = "1234",
                  Email = "manager@gmail.com",
                  Roles = new List<Role> { new Role { Name = "Manager" }
                  } },
              new USER {
                  Id = "3",
                  Username = "shopper",
                  Password = "1234",
                  Email = "shopper@gmail.com",
                  Roles = new List<Role> { new Role { Name = "Shoper" }
                  } },
          };
            if (configuration != null)
            {
                string id;
                if (configuration.TryGetValue("id", out id))
                {
                    return users.FirstOrDefault(u => u.Username == configuration["Id"]);
                }
                else return users;
            }
            else
            {
                return users;
            }
        }
        //public USER GetByID(string id)
        //{
        //    return Get().FirstOrDefault(u => u.Id == id);
        //}
    }
}
