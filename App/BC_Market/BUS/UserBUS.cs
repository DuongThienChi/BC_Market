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
    /// <summary>
    /// Provides business logic for managing users.
    /// </summary>
    public class UserBUS : IBUS<USER>
    {
        private readonly IDAO<USER> _dao;

        /// <summary>
        /// Creates a new instance of the <see cref="UserBUS"/> class with the specified DAO.
        /// </summary>
        /// <param name="dao">The data access object for users.</param>
        /// <returns>A new instance of <see cref="UserBUS"/>.</returns>
        public IBUS<USER> CreateNew(IDAO<USER> dao)
        {
            return new UserBUS(dao);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserBUS"/> class.
        /// </summary>
        /// <param name="dao">The data access object for users.</param>
        public UserBUS(IDAO<USER> dao)
        {
            _dao = dao;
        }

        /// <summary>
        /// Gets user data based on the specified configuration.
        /// </summary>
        /// <param name="configuration">The configuration dictionary.</param>
        /// <returns>The user data.</returns>
        public dynamic Get(Dictionary<string, string> configuration)
        {
            return _dao.Get(configuration);
        }

        /// <summary>
        /// Updates the user information.
        /// </summary>
        /// <param name="user">The user object.</param>
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

        /// <summary>
        /// Gets the data access object for users.
        /// </summary>
        /// <returns>The data access object for users.</returns>
        IDAO<USER> IBUS<USER>.Dao() => _dao;

        /// <summary>
        /// Adds a new user.
        /// </summary>
        /// <param name="obj">The user object to add.</param>
        /// <returns>The result of the add operation.</returns>
        public dynamic Add(USER obj)
        {
            return _dao.Add(obj);
        }

        /// <summary>
        /// Updates an existing user.
        /// </summary>
        /// <param name="obj">The user object to update.</param>
        /// <returns>The result of the update operation.</returns>
        public dynamic Update(USER obj)
        {
            return _dao.Update(obj);
        }

        /// <summary>
        /// Deletes a user.
        /// </summary>
        /// <param name="obj">The user object to delete.</param>
        /// <returns>The result of the delete operation.</returns>
        public dynamic Delete(USER obj)
        {
            return _dao.Delete(obj);
        }
    }

}
