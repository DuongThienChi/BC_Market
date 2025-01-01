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
    /// Provides business logic for managing products.
    /// </summary>
    public class ProductBUS : IBUS<Product>
    {
        private readonly IDAO<Product> _dao;

        /// <summary>
        /// Creates a new instance of the <see cref="ProductBUS"/> class with the specified DAO.
        /// </summary>
        /// <param name="dao">The data access object for products.</param>
        /// <returns>A new instance of <see cref="ProductBUS"/>.</returns>
        public IBUS<Product> CreateNew(IDAO<Product> dao)
        {
            return new ProductBUS(dao);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductBUS"/> class.
        /// </summary>
        /// <param name="dao">The data access object for products.</param>
        public ProductBUS(IDAO<Product> dao)
        {
            _dao = dao;
        }

        /// <summary>
        /// Gets product data based on the specified configuration.
        /// </summary>
        /// <param name="configuration">The configuration dictionary.</param>
        /// <returns>The product data.</returns>
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
        /// Gets the data access object for products.
        /// </summary>
        /// <returns>The data access object for products.</returns>
        public IDAO<Product> Dao()
        {
            return _dao;
        }

        /// <summary>
        /// Adds a new product.
        /// </summary>
        /// <param name="obj">The product object to add.</param>
        /// <returns>The result of the add operation.</returns>
        public dynamic Add(Product obj)
        {
            return _dao.Add(obj);
        }

        /// <summary>
        /// Updates an existing product.
        /// </summary>
        /// <param name="obj">The product object to update.</param>
        /// <returns>The result of the update operation.</returns>
        public dynamic Update(Product obj)
        {
            return _dao.Update(obj);
        }

        /// <summary>
        /// Deletes a product.
        /// </summary>
        /// <param name="obj">The product object to delete.</param>
        /// <returns>The result of the delete operation.</returns>
        public dynamic Delete(Product obj)
        {
            return _dao.Delete(obj);
        }

        // Other methods...
    }

}
