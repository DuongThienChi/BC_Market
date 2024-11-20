//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using BC_Market.Models;
//using BC_Market.BUS;
//using BC_Market.Factory;
//using Windows.System;
//using System.Collections.ObjectModel;

//namespace BC_Market.DAO
//{
//    public class UserMockDAO : IDAO<USER>
//    {

//        public ObservableCollection<USER> users = new ObservableCollection<USER>
//           {
//              new USER {
//                  Id = "1",
//                  Username = "admin",
//                  Password = "1234" ,
//                  Email = "admin@gmail.com",
//                  Roles = new List<Role> { new Role { Name = "Admin" }
//                  } },
//              new USER {
//                  Id = "2",
//                  Username = "manager",
//                  Password = "1234",
//                  Email = "manager@gmail.com",
//                  Roles = new List<Role> { new Role { Name = "Manager" }
//                  } },
//              new USER {
//                  Id = "3",
//                  Username = "shopper",
//                  Password = "1234",
//                  Email = "shopper@gmail.com",
//                  Roles = new List<Role> { new Role { Name = "Shopper" }
//                  } },
//            };
//        public UserMockDAO() { }

//        public void Add(USER obj)
//        {
//            users.Add(obj);
//        }

//        public void Delete(USER obj)
//        {
//            foreach (USER user in users)
//            {
//                if (user.Username == obj.Username)
//                {
//                    users.Remove(user);
//                    break;
//                }
//            }
//        }

//        public dynamic Get(Dictionary<string, string> configuration)
//        {
//            if (configuration != null)
//            {
//                string id;
//                if (configuration.TryGetValue("id", out id))
//                {
//                    return users.FirstOrDefault(u => u.Username == configuration["Id"]);
//                }
//                else return users;
//            }
//            else
//            {
//                return users;
//            }
//        }

//        public void Update(USER obj)
//        {
//            foreach (USER item in users)
//            {
//                if (item.Username == obj.Username)
//                {
//                    item.Username = obj.Username;
//                    item.Password = obj.Password;
//                    item.Roles = obj.Roles;
//                    break;
//                }
//            }
//        }
//        //public USER GetByID(string id)
//        //{
//        //    return Get().FirstOrDefault(u => u.Id == id);
//        //}
//    }
//}
