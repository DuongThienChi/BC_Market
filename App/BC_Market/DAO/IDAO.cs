using BC_Market.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BC_Market.BUS;
namespace BC_Market.DAO
{
    /// <summary>
    /// Defines the basic operations for data access objects.
    /// </summary>
    /// <typeparam name="T">The type of the object that the DAO will manage.</typeparam>
    public interface IDAO <T>
    {
        /// <summary>
        /// Retrieves objects based on the provided configuration.
        /// </summary>
        /// <param name="configuration">A dictionary containing configuration parameters.</param>
        /// <returns>A collection of objects or specific object details based on the configuration.</returns>
        public dynamic Get(Dictionary<string, string> configuration);
        /// <summary>
        /// Adds a new object to the database.
        /// </summary>
        /// <param name="obj">The object to add.</param>
        /// <returns>True if the operation is successful, otherwise false.</returns>
        public dynamic Add(T obj);
        /// <summary>
        /// Updates an existing object in the database.
        /// </summary>
        /// <param name="obj">The object to update.</param>
        /// <returns>True if the operation is successful, otherwise false.</returns>
        public dynamic Update(T obj);
        /// <summary>
        /// Removes an object from the database.
        /// </summary>
        /// <param name="obj">The object to delete.</param>
        /// <returns>True if the operation is successful, otherwise false.</returns>
        public dynamic Delete(T obj);
    }
}
