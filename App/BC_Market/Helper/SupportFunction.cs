using BC_Market.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC_Market.Helper
{
    public static class SupportFunction
    {
        public static bool isUsernameExist(this string username) // Check if username is exist
        {
            var userFactory = new UserFactory();
            var userBUS = userFactory.CreateBUS();

            var listUser = userBUS.Get(null);

            foreach (var user in listUser)
            {
                if (user.Username == username)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
