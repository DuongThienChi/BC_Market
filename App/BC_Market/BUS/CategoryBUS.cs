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
    /// Provides business logic for managing categories.
    /// </summary>
    public class CategoryBUS : IBUS<Category>
    {
        private readonly IDAO<Category> _dao;

        /// <summary>
        /// Creates a new instance of the <see cref="CategoryBUS"/> class with the specified DAO.
        /// </summary>
        /// <param name="dao">The data access object for categories.</param>
        /// <returns>A new instance of <see cref="CategoryBUS"/>.</returns>
        public IBUS<Category> CreateNew(IDAO<Category> dao)
        {
            return new CategoryBUS(dao);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryBUS"/> class.
        /// </summary>
        /// <param name="dao">The data access object for categories.</param>
        public CategoryBUS(IDAO<Category> dao)
        {
            _dao = dao;
        }

        /// <summary>
        /// Gets category data based on the specified configuration.
        /// </summary>
        /// <param name="configuration">The configuration dictionary.</param>
        /// <returns>The category data.</returns>
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
        /// Gets the data access object for categories.
        /// </summary>
        /// <returns>The data access object for categories.</returns>
        public IDAO<Category> Dao()
        {
            return _dao;
        }

        /// <summary>
        /// Adds a new category.
        /// </summary>
        /// <param name="obj">The category object to add.</param>
        /// <returns>The result of the add operation.</returns>
        public dynamic Add(Category obj)
        {
            return _dao.Add(obj);
        }

        /// <summary>
        /// Updates an existing category.
        /// </summary>
        /// <param name="obj">The category object to update.</param>
        /// <returns>The result of the update operation.</returns>
        public dynamic Update(Category obj)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes a category.
        /// </summary>
        /// <param name="obj">The category object to delete.</param>
        /// <returns>The result of the delete operation.</returns>
        public dynamic Delete(Category obj)
        {
            return _dao.Delete(obj);
        }

        // Other methods...
    }

}
