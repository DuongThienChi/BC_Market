using BC_Market.DAO;
using BC_Market.Factory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BC_Market.Models;
using Windows.System;
using Windows.Devices.Bluetooth.Advertisement;
namespace BC_Market.BUS
{
    /// <summary>
    /// Provides business logic for managing orders.
    /// </summary>
    public class OrderBUS : IBUS<Order>
    {
        private readonly IDAO<Order> _dao;

        /// <summary>
        /// Creates a new instance of the <see cref="OrderBUS"/> class with the specified DAO.
        /// </summary>
        /// <param name="dao">The data access object for orders.</param>
        /// <returns>A new instance of <see cref="OrderBUS"/>.</returns>
        public IBUS<Order> CreateNew(IDAO<Order> dao)
        {
            return new OrderBUS(dao);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrderBUS"/> class.
        /// </summary>
        /// <param name="dao">The data access object for orders.</param>
        public OrderBUS(IDAO<Order> dao)
        {
            _dao = dao;
        }

        /// <summary>
        /// Adds a new order.
        /// </summary>
        /// <param name="order">The order object to add.</param>
        /// <returns>The result of the add operation.</returns>
        public dynamic Add(Order order)
        {
            return _dao.Add(order);
        }

        /// <summary>
        /// Gets the data access object for orders.
        /// </summary>
        /// <returns>The data access object for orders.</returns>
        public IDAO<Order> Dao()
        {
            return _dao;
        }

        /// <summary>
        /// Gets order data based on the specified configuration.
        /// </summary>
        /// <param name="configuration">The configuration dictionary.</param>
        /// <returns>The order data.</returns>
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
        /// Updates an existing order.
        /// </summary>
        /// <param name="obj">The order object to update.</param>
        /// <returns>The result of the update operation.</returns>
        public dynamic Update(Order obj)
        {
            return _dao.Update(obj);
        }

        /// <summary>
        /// Deletes an order.
        /// </summary>
        /// <param name="obj">The order object to delete.</param>
        /// <returns>The result of the delete operation.</returns>
        public dynamic Delete(Order obj)
        {
            return _dao.Delete(obj);
        }

        // Other methods...
    }

}
