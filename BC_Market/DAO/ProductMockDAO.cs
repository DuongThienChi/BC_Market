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
    public class ProductMockDAO : IDAO<Product>
    {
        public ProductMockDAO() { }
        public dynamic Get(Dictionary<string, string> configuration)
        {
            var products = new List<Product>
            {
                new Product {
                    Id = "1",
                    Name = "Fresh Pork",
                    Description = "1kg Fresh Pork",
                    Price = 10,
                    ImagePath = "/Assets/Images/Red meat.jpg",
                    CategoryId = "M01", // Meat
                    Stock = 100,
                    Status = "Available",
                    OrderQuantity = 0
                },
                new Product {
                    Id = "2",
                    Name = "Chicken Breast",
                    Description = "500g Chicken Breast",
                    Price = 7,
                    ImagePath = "/Assets/Images/Red meat.jpg",
                    CategoryId = "M01", // Meat
                    Stock = 100,
                    Status = "Available",
                    OrderQuantity = 0
                },
                new Product {
                    Id = "3",
                    Name = "Salmon Fillet",
                    Description = "500g Fresh Salmon",
                    Price = 15,
                    ImagePath = "/Assets/Images/fish.jpg",
                    CategoryId = "SF01", // SeaFood
                    Stock = 100,
                    Status = "Available",
                    OrderQuantity = 0
                },
                new Product {
                    Id = "4",
                    Name = "Shrimps",
                    Description = "1kg Shrimps",
                    Price = 12,
                    ImagePath = "/Assets/Images/fish.jpg",
                    CategoryId = "SF01", // SeaFood
                    Stock = 100,
                    Status = "Available",
                    OrderQuantity = 0
                },
                new Product {
                    Id = "5",
                    Name = "Whole Milk",
                    Description = "1L Fresh Whole Milk",
                    Price = 3,
                    ImagePath = "/Assets/Images/milk.png",
                    CategoryId = "Mk01", // Milk
                    Stock = 100,
                    Status = "Available",
                    OrderQuantity = 0
                },
                new Product {
                    Id = "6",
                    Name = "Skimmed Milk",
                    Description = "1L Skimmed Milk",
                    Price = 2.5,
                    ImagePath = "/Assets/Images/milk.png",
                    CategoryId = "Mk01", // Milk
                    Stock = 100,
                    Status = "Available",
                    OrderQuantity = 0
                },
                new Product {
                    Id = "7",
                    Name = "Broccoli",
                    Description = "500g Fresh Broccoli",
                    Price = 4,
                    ImagePath = "/Assets/Images/veget.png",
                    CategoryId = "Vet01", // Vegetable
                    Stock = 100,
                    Status = "Available",
                    OrderQuantity = 0
                },
                new Product {
                    Id = "8",
                    Name = "Carrots",
                    Description = "1kg Fresh Carrots",
                    Price = 3,
                    ImagePath = "/Assets/Images/veget.png",
                    CategoryId = "Vet01", // Vegetable
                    Stock = 100,
                    Status = "Available",
                    OrderQuantity = 0
                },
                new Product {
                    Id = "9",
                    Name = "Spinach",
                    Description = "500g Fresh Spinach",
                    Price = 5,
                    ImagePath = "/Assets/Images/veget.png",
                    CategoryId = "Vet01", // Vegetable
                    Stock = 100,
                    Status = "Available",
                    OrderQuantity = 0
                },
                new Product {
                    Id = "10",
                    Name = "Tomatoes",
                    Description = "1kg Fresh Tomatoes",
                    Price = 4,
                    ImagePath = "/Assets/Images/veget.png",
                    CategoryId = "Vet01", // Vegetable
                    Stock = 100,
                    Status = "Available",
                    OrderQuantity = 0
                },
                new Product {
                    Id = "11",
                    Name = "Ground Beef",
                    Description = "500g Ground Beef",
                    Price = 9,
                    ImagePath = "/Assets/Images/Red meat.jpg",
                    CategoryId = "M01", // Meat
                    Stock = 100,
                    Status = "Available",
                    OrderQuantity = 0
                },
                new Product {
                    Id = "12",
                    Name = "Yogurt",
                    Description = "500g Yogurt",
                    Price = 6,
                    ImagePath = "/Assets/Images/milk.png",
                    CategoryId = "Mk01", // Milk
                    Stock = 100,
                    Status = "Available",
                    OrderQuantity = 0
                },
                new Product {
                    Id = "13",
                    Name = "Shrimp Paste",
                    Description = "500g Shrimp Paste",
                    Price = 10,
                    ImagePath = "/Assets/Images/fish.jpg",
                    CategoryId = "SF01", // SeaFood
                    Stock = 100,
                    Status = "Available",
                    OrderQuantity = 0
                },
                new Product {
                    Id = "14",
                    Name = "Cucumber",
                    Description = "1kg Fresh Cucumber",
                    Price = 3,
                    ImagePath = "/Assets/Images/veget.png",
                    CategoryId = "Vet01", // Vegetable
                    Stock = 100,
                    Status = "Available",
                    OrderQuantity = 0
                },
                new Product {
                    Id = "15",
                    Name = "Tofu",
                    Description = "500g Fresh Tofu",
                    Price = 4,
                    ImagePath = "/Assets/Images/veget.png",
                    CategoryId = "Vet01", // Vegetable
                    Stock = 100,
                    Status = "Available",
                    OrderQuantity = 0
                },
                new Product {
                    Id = "16",
                    Name = "Cod Fillet",
                    Description = "500g Cod Fillet",
                    Price = 13,
                    ImagePath = "/Assets/Images/fish.jpg",
                    CategoryId = "SF01", // SeaFood
                    Stock = 100,
                    Status = "Available",
                    OrderQuantity = 0
                },
                new Product {
                    Id = "17",
                    Name = "Face Cream",
                    Description = "50ml Face Cream",
                    Price = 20,
                    ImagePath = "/Assets/Images/kemps.jpg",
                    CategoryId = "BH01", // Beauty & Health
                    Stock = 100,
                    Status = "Available",
                    OrderQuantity = 0
                },
                new Product {
                    Id = "18",
                    Name = "Shampoo",
                    Description = "500ml Hair Shampoo",
                    Price = 8,
                    ImagePath = "/Assets/Images/kemps.jpg",
                    CategoryId = "BH01", // Beauty & Health
                    Stock = 100,
                    Status = "Available",
                    OrderQuantity = 0
                },
                new Product {
                    Id = "19",
                    Name = "Body Lotion",
                    Description = "500ml Body Lotion",
                    Price = 10,
                    ImagePath = "/Assets/Images/kemps.jpg",
                    CategoryId = "BH01", // Beauty & Health
                    Stock = 100,
                    Status = "Available",
                    OrderQuantity = 0
                },
                new Product {
                    Id = "20",
                    Name = "Salmon Roe",
                    Description = "100g Fresh Salmon Roe",
                    Price = 30,
                    ImagePath = "/Assets/Images/fish.jpg",
                    CategoryId = "SF01", // SeaFood
                    Stock = 100,
                    Status = "Available",
                    OrderQuantity = 0
                }
            };
            if (configuration != null)
            {
                string id;
                if (configuration.TryGetValue("id", out id))
                {
                    return products.FirstOrDefault(u => u.Id == configuration["Id"]);
                }
                else return products;
            }
            else return products;
        }
        //public PRODUCT GetByID(string id)
        //{
        //    return Get().FirstOrDefault(u => u.Id == id);
        //}
    }
}
