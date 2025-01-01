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
    /// <summary>
    /// ViewModel for managing products in the admin panel.
    /// </summary>
    class ManageProductViewModel : ObservableObject
    {
        private IFactory<Product> _productFactory = new ProductFactory();
        private IBUS<Product> _productBus;

        /// <summary>
        /// Gets or sets the collection of products.
        /// </summary>
        public ObservableCollection<Product> ListProduct { get; set; }

        private IFactory<Category> _cateFactory = new CategoryFactory();
        private IBUS<Category> _cateBus;

        /// <summary>
        /// Gets or sets the collection of categories.
        /// </summary>
        public ObservableCollection<Category> ListCategories { get; set; }

        /// <summary>
        /// Gets or sets the collection of category IDs.
        /// </summary>
        public ObservableCollection<string> ListCategoriesId { get; set; } = new ObservableCollection<string>();

        /// <summary>
        /// Gets or sets the collection of products filtered by category.
        /// </summary>
        public ObservableCollection<Product> ProductByCategory { get; set; }

        /// <summary>
        /// Gets or sets the collection of chosen products.
        /// </summary>
        public ObservableCollection<Product> ChosenProduct { get; set; } = new ObservableCollection<Product>();

        /// <summary>
        /// Gets or sets the total sum of chosen products.
        /// </summary>
        public float sumTotal = 0;

        /// <summary>
        /// Initializes a new instance of the <see cref="ManageProductViewModel"/> class.
        /// </summary>
        public ManageProductViewModel()
        {
            LoadData();
            foreach (Product p in ListProduct)
            {
                if (p.OrderQuantity > 0)
                {
                    ChosenProduct.Add(p);
                    sumTotal += float.Parse(p.Total());
                }
            }
        }

        /// <summary>
        /// Loads the product and category data.
        /// </summary>
        public void LoadData()
        {
            _productBus = _productFactory.CreateBUS();
            var dict = new Dictionary<string, string>()
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
            foreach (Category c in ListCategories)
            {
                ListCategoriesId.Add(c.Id);
            }
        }

        /// <summary>
        /// Adds a new product.
        /// </summary>
        /// <param name="product">The product to add.</param>
        public void AddProduct(Product product)
        {
            var dao = _productBus.Dao();
            dao.Add(product);
            ListProduct.Add(product);
            ProductByCategory.Add(product);
        }

        /// <summary>
        /// Adds a new category.
        /// </summary>
        /// <param name="category">The category to add.</param>
        public void AddCategory(Category category)
        {
            var dao = _cateBus.Dao();
            dao.Add(category);
            ListCategories.Add(category);
            ListCategoriesId.Add(category.Id);
        }

        /// <summary>
        /// Updates an existing product.
        /// </summary>
        /// <param name="product">The product to update.</param>
        public void UpdateProduct(Product product)
        {
            var dao = _productBus.Dao();
            dao.Update(product);
        }

        /// <summary>
        /// Updates an existing category.
        /// </summary>
        /// <param name="category">The category to update.</param>
        public void UpdateCategory(Category category)
        {
            var dao = _cateBus.Dao();
            dao.Update(category);
        }

        /// <summary>
        /// Deletes a product.
        /// </summary>
        /// <param name="product">The product to delete.</param>
        public void DeleteProduct(Product product)
        {
            var dao = _productBus.Dao();
            dao.Delete(product);
            ListProduct.Remove(product);
        }

        /// <summary>
        /// Deletes a category.
        /// </summary>
        /// <param name="category">The category to delete.</param>
        public void DeleteCategory(Category category)
        {
            var dao = _cateBus.Dao();
            dao.Delete(category);
            ListCategories.Remove(category);
            ListCategoriesId.Remove(category.Id);
        }

        /// <summary>
        /// Sets the products filtered by the specified category.
        /// </summary>
        /// <param name="category">The category to filter by.</param>
        public void SetProductByCategory(string category)
        {
            if (category == "All")
            {
                ProductByCategory = new ObservableCollection<Product>(ListProduct);
            }
            else
            {
                ProductByCategory = new ObservableCollection<Product>();
                foreach (Product product in ListProduct)
                {
                    if (product.CategoryId == category)
                    {
                        ProductByCategory.Add(product);
                    }
                }
            }
        }

        /// <summary>
        /// Sets the chosen products based on their order quantity.
        /// </summary>
        public void SetChosenProduct()
        {
            float res = 0;
            ChosenProduct = new ObservableCollection<Product>();
            foreach (Product p in ListProduct)
            {
                if (p.OrderQuantity > 0)
                {
                    ChosenProduct.Add(p);
                    res += float.Parse(p.Total());
                }
            }
            sumTotal = res;
        }

        /// <summary>
        /// Creates an order with the specified payment method and user ID.
        /// </summary>
        /// <param name="paymentID">The payment method ID.</param>
        /// <param name="userID">The user ID.</param>
        public void CreateOrder(int paymentID, int userID)
        {
            Order order = new Order
            {
                createAt = DateTime.Now,
                totalPrice = sumTotal,
                paymentMethod = paymentID,
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

        /// <summary>
        /// Updates product information before adding the order.
        /// </summary>
        /// <param name="dao">The product DAO.</param>
        private void ChangeInfoBeforeAdd(IDAO<Product> dao)
        {
            foreach (Product p in ChosenProduct)
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
