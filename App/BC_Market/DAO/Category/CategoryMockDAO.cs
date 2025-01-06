using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BC_Market.Models;
using BC_Market.BUS;
using BC_Market.Factory;
using Windows.System;
using System.Collections.ObjectModel;

namespace BC_Market.DAO
{
    /// <summary>
    /// Provides mock data access logic for managing categories.
    /// </summary>
    public class CategoryMockDAO : IDAO<Category>
    {
        private ObservableCollection<Category> categories = new ObservableCollection<Category>
            {
                new Category {
                    Id = "All",
                    Name = "All",
                    Description = "All",
                },
                new Category {
                    Id = "M01",
                    Name = "Meat",
                    Description = "Meat",
                },
                new Category {
                    Id = "V01",
                    Name = "Vegetable",
                    Description = "Vegetable",
                },
                new Category {
                    Id = "Mk01",
                    Name = "Milk",
                    Description = "Milk",
                },
                new Category {
                    Id = "SF01",
                    Name = "Seafood",
                    Description = "Seafood",
                },
                new Category {
                    Id = "BH01",
                    Name = "Beauty & Health",
                    Description = "Beauty & Health",
                }
            };

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryMockDAO"/> class.
        /// </summary>
        public CategoryMockDAO() { }

        /// <summary>
        /// Adds a new category to the mock data collection.
        /// </summary>
        /// <param name="obj">The category object to add.</param>
        /// <returns>The added category object.</returns>
        public dynamic Add(Category obj)
        {
            categories.Add(obj);
            return obj;
        }

        /// <summary>
        /// Deletes a category from the mock data collection.
        /// </summary>
        /// <param name="obj">The category object to delete.</param>
        /// <returns>Throws a <see cref="NotImplementedException"/>.</returns>
        public dynamic Delete(Category obj)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets categories from the mock data collection based on the specified configuration.
        /// </summary>
        /// <param name="configuration">The configuration dictionary.</param>
        /// <returns>A list of categories or a specific category if specified in the configuration.</returns>
        public dynamic Get(Dictionary<string, string> configuration)
        {
            if (configuration != null)
            {
                string id;
                if (configuration.TryGetValue("id", out id))
                {
                    return categories.FirstOrDefault(u => u.Id == configuration["Id"]);
                }
                else return categories;
            }
            else return categories;
        }

        /// <summary>
        /// Updates a category in the mock data collection.
        /// </summary>
        /// <param name="obj">The category object to update.</param>
        /// <returns>Throws a <see cref="NotImplementedException"/>.</returns>
        public void Update(Category obj)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Updates a category in the mock data collection.
        /// </summary>
        /// <param name="obj">The category object to update.</param>
        /// <returns>Throws a <see cref="NotImplementedException"/>.</returns>
        dynamic IDAO<Category>.Update(Category obj)
        {
            throw new NotImplementedException();
        }
    }
}
