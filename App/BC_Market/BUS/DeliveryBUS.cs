using BC_Market.DAO;
using BC_Market.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC_Market.BUS
{
    /// <summary>
    /// Provides business logic for managing delivery units.
    /// </summary>
    public class DeliveryBUS : IBUS<DeliveryUnit>
    {
        private readonly IDAO<DeliveryUnit> _dao;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeliveryBUS"/> class.
        /// </summary>
        /// <param name="dao">The data access object for delivery units.</param>
        public DeliveryBUS(IDAO<DeliveryUnit> dao)
        {
            _dao = dao;
        }

        /// <summary>
        /// Gets delivery unit data based on the specified configuration.
        /// </summary>
        /// <param name="configuration">The configuration dictionary.</param>
        /// <returns>The delivery unit data.</returns>
        public dynamic Get(Dictionary<string, string> configuration)
        {
            return _dao.Get(configuration);
        }

        /// <summary>
        /// Gets the data access object for delivery units.
        /// </summary>
        /// <returns>The data access object for delivery units.</returns>
        public IDAO<DeliveryUnit> Dao()
        {
            return _dao;
        }

        /// <summary>
        /// Creates a new instance of the <see cref="DeliveryBUS"/> class with the specified DAO.
        /// </summary>
        /// <param name="dao">The data access object for delivery units.</param>
        /// <returns>A new instance of <see cref="DeliveryBUS"/>.</returns>
        public IBUS<DeliveryUnit> CreateNew(IDAO<DeliveryUnit> dao)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Adds a new delivery unit.
        /// </summary>
        /// <param name="obj">The delivery unit object to add.</param>
        /// <returns>The result of the add operation.</returns>
        public dynamic Add(DeliveryUnit obj)
        {
            return _dao.Add(obj);
        }

        /// <summary>
        /// Updates an existing delivery unit.
        /// </summary>
        /// <param name="obj">The delivery unit object to update.</param>
        /// <returns>The result of the update operation.</returns>
        public dynamic Update(DeliveryUnit obj)
        {
            return _dao.Update(obj);
        }

        /// <summary>
        /// Deletes a delivery unit.
        /// </summary>
        /// <param name="obj">The delivery unit object to delete.</param>
        /// <returns>The result of the delete operation.</returns>
        public dynamic Delete(DeliveryUnit obj)
        {
            return _dao.Delete(obj);
        }

        // Other methods...
    }

}

