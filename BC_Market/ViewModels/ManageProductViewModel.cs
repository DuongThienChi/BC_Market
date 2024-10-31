using BC_Market.BUS;
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

        public ManageProductViewModel()
        {
            LoadData();
        }

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
            var products = _productBus.Get(dict);
            ListProduct = new ObservableCollection<Product>(products);
            ProductByCategory = new ObservableCollection<Product>(products);

            _cateBus = _cateFactory.CreateBUS();
            var categories = _cateBus.Get(null);
            ListCategories = new ObservableCollection<Category>(categories);
        }

        public void AddProduct(Product product)
        {
            var dao = _productBus.Dao();
            dao.Add(product);
            ListProduct.Add(product);
            ProductByCategory.Add(product);
        }

        public void AddCategory(Category category)
        {
            var dao = _cateBus.Dao();
            dao.Add(category);
            ListCategories.Add(category);
        }

        public void UpdateProduct(Product product)
        {
            var dao = _productBus.Dao();
            dao.Update(product);
        }

        public void UpdateCategory(Category category)
        {
            var dao = _cateBus.Dao();
            dao.Update(category);
        }

        public void DeleteProduct(Product product)
        {
            var dao = _productBus.Dao();
            dao.Delete(product);
            ListProduct.Remove(product);
            ProductByCategory.Remove(product);
        }

        public void DeleteCategory(Category category)
        {
            var dao = _cateBus.Dao();
            dao.Delete(category);
            ListCategories.Remove(category);
        }

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

        
    }
}
