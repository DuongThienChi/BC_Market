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
    /// Provides business logic for managing carts.
    /// </summary>
    public class CartBUS : IBUS<Cart>
    {
        private readonly IDAO<Cart> _dao;

        /// <summary>
        /// Creates a new instance of the <see cref="CartBUS"/> class with the specified DAO.
        /// </summary>
        /// <param name="dao">The data access object for carts.</param>
        /// <returns>A new instance of <see cref="CartBUS"/>.</returns>
        public IBUS<Cart> CreateNew(IDAO<Cart> dao)
        {
            return new CartBUS(dao);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CartBUS"/> class.
        /// </summary>
        /// <param name="dao">The data access object for carts.</param>
        public CartBUS(IDAO<Cart> dao)
        {
            _dao = dao;
        }

        /// <summary>
        /// Gets cart data based on the specified configuration.
        /// </summary>
        /// <param name="configuration">The configuration dictionary.</param>
        /// <returns>The cart data.</returns>
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
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the data access object for carts.
        /// </summary>
        /// <returns>The data access object for carts.</returns>
        public IDAO<Cart> Dao()
        {
            return _dao;
        }

        /// <summary>
        /// Adds a new cart.
        /// </summary>
        /// <param name="obj">The cart object to add.</param>
        /// <returns>The result of the add operation.</returns>
        public dynamic Add(Cart obj)
        {
            return _dao.Add(obj);
        }

        /// <summary>
        /// Updates an existing cart.
        /// </summary>
        /// <param name="obj">The cart object to update.</param>
        /// <returns>The result of the update operation.</returns>
        public dynamic Update(Cart obj)
        {
            return _dao.Update(obj);
        }

        /// <summary>
        /// Deletes a cart.
        /// </summary>
        /// <param name="obj">The cart object to delete.</param>
        /// <returns>The result of the delete operation.</returns>
        public dynamic Delete(Cart obj)
        {
            throw new NotImplementedException();
        }

        // Other methods...
    }

}
