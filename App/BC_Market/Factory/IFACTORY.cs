using BC_Market.BUS;
using BC_Market.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BC_Market.Factory
{
    /// <summary>
    /// Interface for factory classes that create instances of business and data access objects.
    /// </summary>
    /// <typeparam name="T">The type of the objects to be created.</typeparam>
    public interface IFactory <T>
    {
        /// <summary>
        /// Creates and returns an instance of a business object.
        /// </summary>
        /// <returns>An instance of IBUS&lt;T&gt;.</returns>
        IBUS<T> CreateBUS();
        /// <summary>
        /// Creates and returns an instance of a data access object.
        /// </summary>
        /// <returns>An instance of IDAO&lt;T&gt;.</returns>
        IDAO<T> CreateDAO();
    }
}
