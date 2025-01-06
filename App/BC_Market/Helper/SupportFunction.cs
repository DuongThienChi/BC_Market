using BC_Market.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC_Market.Helper
{
    /// <summary>
    /// Provides support functions for the BC Market application.
    /// </summary>
    public static class SupportFunction
    {
        /// <summary>
        /// Checks if the specified username exists.
        /// </summary>
        /// <param name="username">The username to check.</param>
        /// <returns>True if the username exists; otherwise, false.</returns>
        public static bool isUsernameExist(this string username)
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
