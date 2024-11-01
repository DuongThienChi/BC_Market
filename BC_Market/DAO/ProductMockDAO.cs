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
    public class ProductMockDAO : IDAO<Product>
    {
        private ObservableCollection<Product> products = new ObservableCollection<Product>
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
                    OrderQuantity = 0,
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
                    OrderQuantity = 0,
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
                },
                new Product {
                    Id = "21",
                    Name = "Salmon Roe",
                    Description = "100g Fresh Salmon Roe",
                    Price = 30,
                    ImagePath = "/Assets/Images/fish.jpg",
                    CategoryId = "SF01", // SeaFood
                    Stock = 100,
                    Status = "Available",
                    OrderQuantity = 0
                },
                new Product {
                    Id = "22",
                    Name = "Salmon Roe",
                    Description = "100g Fresh Salmon Roe",
                    Price = 30,
                    ImagePath = "/Assets/Images/fish.jpg",
                    CategoryId = "SF01", // SeaFood
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
                },
                new Product {
                    Id = "23",
                    Name = "Salmon Roe",
                    Description = "100g Fresh Salmon Roe",
                    Price = 30,
                    ImagePath = "/Assets/Images/fish.jpg",
                    CategoryId = "SF01", // SeaFood
                    Stock = 100,
                    Status = "Available",
                    OrderQuantity = 0
                },
                new Product {
                    Id = "24",
                    Name = "Salmon Roe",
                    Description = "100g Fresh Salmon Roe",
                    Price = 30,
                    ImagePath = "/Assets/Images/fish.jpg",
                    CategoryId = "SF01", // SeaFood
                    Stock = 100,
                    Status = "Available",
                    OrderQuantity = 0
                },
                new Product {
                    Id = "25",
                    Name = "Salmon Roe",
                    Description = "100g Fresh Salmon Roe",
                    Price = 30,
                    ImagePath = "/Assets/Images/fish.jpg",
                    CategoryId = "SF01", // SeaFood
                    Stock = 100,
                    Status = "Available",
                    OrderQuantity = 0
                },
                new Product {
                    Id = "26",
                    Name = "Salmon Roe",
                    Description = "100g Fresh Salmon Roe",
                    Price = 30,
                    ImagePath = "/Assets/Images/fish.jpg",
                    CategoryId = "SF01", // SeaFood
                    Stock = 100,
                    Status = "Available",
                    OrderQuantity = 0
                },
                new Product {
                    Id = "27",
                    Name = "Salmon Roe",
                    Description = "100g Fresh Salmon Roe",
                    Price = 30,
                    ImagePath = "/Assets/Images/fish.jpg",
                    CategoryId = "SF01", // SeaFood
                    Stock = 100,
                    Status = "Available",
                    OrderQuantity = 0
                },
                new Product {
                    Id = "28",
                    Name = "Carrot",
                    Description = "1kg Fresh Carrots",
                    Price = 5,
                    ImagePath = "/Assets/Images/carrot.jpg",
                    CategoryId = "V01", // Vegetables
                    Stock = 200,
                    Status = "Available",
                    OrderQuantity = 0
                },
                new Product {
                    Id = "29",
                    Name = "Broccoli",
                    Description = "500g Fresh Broccoli",
                    Price = 7,
                    ImagePath = "/Assets/Images/broccoli.jpg",
                    CategoryId = "V01", // Vegetables
                    Stock = 150,
                    Status = "Available",
                    OrderQuantity = 0
                },
                new Product {
                    Id = "30",
                    Name = "Spinach",
                    Description = "250g Fresh Spinach",
                    Price = 4,
                    ImagePath = "/Assets/Images/spinach.jpg",
                    CategoryId = "V01", // Vegetables
                    Stock = 180,
                    Status = "Available",
                    OrderQuantity = 0
                },
                new Product {
                    Id = "31",
                    Name = "Bell Pepper",
                    Description = "3pcs Mixed Bell Peppers",
                    Price = 6,
                    ImagePath = "/Assets/Images/bellpepper.jpg",
                    CategoryId = "V01", // Vegetables
                    Stock = 120,
                    Status = "Available",
                    OrderQuantity = 0
                }
            };
        public ProductMockDAO() { }

        public void Add(Product obj)
        {
            products.Add(obj);
        }

        public void Delete(Product obj)
        {
            products.Remove(obj);
        }

        public dynamic Get(Dictionary<string, string> configuration)
        {
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

        public void Update(Product obj)
        {
            products[products.IndexOf(products.FirstOrDefault(u => u.Id == obj.Id))] = obj;
        }
        //public PRODUCT GetByID(string id)
        //{
        //    return Get().FirstOrDefault(u => u.Id == id);
        //}
    }
}
