using BC_Market.BUS;
using BC_Market.DAO;
using BC_Market.Factory;
using BC_Market.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BC_Market.ViewModels
{
    class ManageProductViewModel : ObservableObject
    {
        private IFactory<Product> _productFactory = new ProductFactory();
        private IBUS<Product> _productBus;
        public ObservableCollection<Product> ListProduct { get; set; }

        private IFactory<Category> _cateFactory = new CategoryFactory();
        private IBUS<Category> _cateBus;
        public ObservableCollection<Category> ListCategories { get; set; }

        public ObservableCollection<Product> ProductByCategory { get; set; }

        public ObservableCollection<Product> ChosenProduct { get; set; } = new ObservableCollection<Product>();

        public float sumTotal = 0;

        public ManageProductViewModel()
        {
            LoadData();
            foreach (Product p in ListProduct)
            {
                if(p.OrderQuantity > 0)
                {
                    ChosenProduct.Add(p);
                    sumTotal += float.Parse(p.Total());
                }
            }
        }

        public void LoadData() // Define the LoadData method
        {
            _productBus = _productFactory.CreateBUS();
            var dict = new Dictionary<string, string>() // Define the dict variable
            {
                {"searchKey", ""},
                {"category", ""},
                {"skip", "0"},
                {"take", "100"}
            };
            var products = _productBus.Get(dict); // Get all products
            ListProduct = new ObservableCollection<Product>(products);
            ProductByCategory = new ObservableCollection<Product>(products);

            _cateBus = _cateFactory.CreateBUS();
            var categories = _cateBus.Get(null);
            ListCategories = new ObservableCollection<Category>(categories); // Get all categories
        }

        public void AddProduct(Product product) // Add a product
        {
            var dao = _productBus.Dao();
            dao.Add(product);
            ListProduct.Add(product);
            ProductByCategory.Add(product);
        }

        public void AddCategory(Category category)  // Add a category
        {
            var dao = _cateBus.Dao();
            dao.Add(category);
            ListCategories.Add(category);
        }

        public void UpdateProduct(Product product) // Update a product
        {
            var dao = _productBus.Dao();
            dao.Update(product);
        }

        public void UpdateCategory(Category category) // Update a category
        {
            var dao = _cateBus.Dao();
            dao.Update(category);
        }

        public void DeleteProduct(Product product) // Delete a product
        {
            var dao = _productBus.Dao();
            dao.Delete(product);
            ListProduct.Remove(product);
            ProductByCategory.Remove(product);
        }

        public void DeleteCategory(Category category) // Delete a category
        {
            var dao = _cateBus.Dao();
            dao.Delete(category);
            ListCategories.Remove(category);
        }

        public void SetProductByCategory(string category) // Set products by category
        {
            if (category == "All") // If category is "All"
            {
                ProductByCategory = new ObservableCollection<Product>(ListProduct); // Set ProductByCategory to ListProduct
            }
            else
            {
                ProductByCategory = new ObservableCollection<Product>(); // Set ProductByCategory to an empty ObservableCollection
                foreach (Product product in ListProduct)
                {
                    if (product.CategoryId == category)
                    {
                        ProductByCategory.Add(product);
                    }
                }
            }
        }

        public void SetChosenProduct() // Choose a product
        {
            float res = 0;
            this.ChosenProduct = new ObservableCollection<Product>();
            foreach (Product p in ListProduct)
            {
                if (p.OrderQuantity > 0)
                {
                    this.ChosenProduct.Add(p);
                    res += float.Parse(p.Total());
                }
            }
            sumTotal = res;
        }

        public void CreateOrder(int paymentID, int userID)
        {
            Order order = new Order
            {
                createAt = DateTime.Now,
                totalPrice = sumTotal,
                paymentMethod = paymentID.ToString(),
                address = "At shop",
                isPaid = true,
                customerId = 3,
                deliveryId = 1,
                Products = new ObservableCollection<CartProduct>(),
            };

            foreach (Product p in ChosenProduct)
            {
                order.Products.Add(new CartProduct
                {
                    Product = p,
                    Quantity = p.OrderQuantity
                });
            }

            IFactory<Order> factory = new OrderFactory();
            IBUS<Order> bus = factory.CreateBUS();

            bus.Dao().Add(order);
            ChangeInfoBeforeAdd(_productBus.Dao());
        }

        private void ChangeInfoBeforeAdd(IDAO<Product> dao)
        {
            foreach(Product p in ChosenProduct)
            {
                p.Stock -= p.OrderQuantity;
                p.OrderQuantity = 0;
                dao.Update(p);
            }
            ChosenProduct.Clear();
            sumTotal = 0;
        }
    }
}
