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
    /// Defines the interface for business logic services.
    /// </summary>
    /// <typeparam name="T">The type of the entity.</typeparam>
    public interface IBUS<T>
    {
        /// <summary>
        /// Gets the data access object for the entity.
        /// </summary>
        /// <returns>The data access object for the entity.</returns>
        IDAO<T> Dao();

        /// <summary>
        /// Creates a new instance of the business logic service with the specified DAO.
        /// </summary>
        /// <param name="dao">The data access object for the entity.</param>
        /// <returns>A new instance of the business logic service.</returns>
        IBUS<T> CreateNew(IDAO<T> dao);

        /// <summary>
        /// Gets entity data based on the specified configuration.
        /// </summary>
        /// <param name="configuration">The configuration dictionary.</param>
        /// <returns>The entity data.</returns>
        dynamic Get(Dictionary<string, string> configuration);

        /// <summary>
        /// Adds a new entity.
        /// </summary>
        /// <param name="obj">The entity object to add.</param>
        /// <returns>The result of the add operation.</returns>
        dynamic Add(T obj);

        /// <summary>
        /// Updates an existing entity.
        /// </summary>
        /// <param name="obj">The entity object to update.</param>
        /// <returns>The result of the update operation.</returns>
        dynamic Update(T obj);

        /// <summary>
        /// Deletes an entity.
        /// </summary>
        /// <param name="obj">The entity object to delete.</param>
        /// <returns>The result of the delete operation.</returns>
        dynamic Delete(T obj);
    }
}
