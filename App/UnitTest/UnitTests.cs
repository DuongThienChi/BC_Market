using Microsoft.UI.Xaml.Controls;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.AppContainer;
using System;
using System.Collections.Generic;
using System.Linq;
using BC_Market;
using BC_Market.ViewModels;
using BC_Market.Models;
using BC_Market.Views;
using System.Collections.ObjectModel;


namespace UnitTest
{
    [TestClass]
    public class UnitTests
    {
        [UITestMethod]
        public void TestMethod1()
        {
            var shopPage = new ShopperDashboardPage();
            var ViewModel = new ShopperOrderViewModel();
            shopPage.ViewModel.CartList = ViewModel.CartList.ToDictionary(item => item.Key, item => item.Value);
            var NumberProduct = 0;
            foreach (var item in ViewModel.CartList)
            {
                NumberProduct += item.Value;
            }
            shopPage.ViewModel.ProductInCart = NumberProduct;
            var Params = new
            {
                CartList = ViewModel.CartList,
                ProductInCart = NumberProduct,
            };
            Assert.IsNotNull(Params);
        }
    }
}
