using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BC_Market.Models;
using BC_Market.BUS;
using BC_Market.Factory;
using Windows.System;

namespace BC_Market.DAO
{
    public class CategoryMockDAO : IDAO<Category>
    {
        public CategoryMockDAO() { }

        public void Add(Category obj)
        {
            throw new NotImplementedException();
        }

        public void Delete(Category obj)
        {
            throw new NotImplementedException();
        }

        public dynamic Get(Dictionary<string, string> configuration)
        {
            var categories = new List<Category>
            {
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
                    Description = "Beauty & Health", }
            };
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

        public void Update(Category obj)
        {
            throw new NotImplementedException();
        }
        //public PRODUCT GetByID(string id)
        //{
        //    return Get().FirstOrDefault(u => u.Id == id);
        //}
    }
}
