//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Moq;
//using Npgsql;
//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using BC_Market.Models;
//using BC_Market.DAO;

//namespace UnitTest_Chi
//{
//    [TestClass]
//    public class CartDatabaseDAOTests
//    {
//        private Mock<NpgsqlConnection> mockConnection;
//        private Mock<NpgsqlCommand> mockCommand;
//        private Mock<NpgsqlTransaction> mockTransaction;
//        private CartDatabaseDAO cartDatabaseDAO;

//        [TestInitialize]
//        public void Setup()
//        {
//            mockConnection = new Mock<NpgsqlConnection>();
//            mockCommand = new Mock<NpgsqlCommand>();
//            mockTransaction = new Mock<NpgsqlTransaction>();
//            cartDatabaseDAO = new CartDatabaseDAO();
//        }

//        [TestMethod]
//        public void Add_Cart_Success()
//        {
//            // Arrange
//            var cart = new Cart
//            {
//                customerId = 1,
//                CartProducts = new ObservableCollection<CartProduct>
//                {
//                    new CartProduct { Product = new Product { Id = 1 }, Quantity = 2 }
//                }
//            };

//            mockConnection.Setup(c => c.Open());
//            mockConnection.Setup(c => c.BeginTransaction()).Returns(mockTransaction.Object);
//            mockCommand.Setup(c => c.ExecuteScalar()).Returns(1);
//            mockCommand.Setup(c => c.ExecuteNonQuery()).Returns(1);

//            // Act
//            var result = cartDatabaseDAO.Add(cart);

//            // Assert
//            Assert.IsTrue(result);
//        }

//        [TestMethod]
//        public void Add_Cart_Failure()
//        {
//            // Arrange
//            var cart = new Cart
//            {
//                customerId = 1,
//                CartProducts = new ObservableCollection<CartProduct>
//        {
//            new CartProduct { Product = new Product { Id = 1 }, Quantity = 2 }
//        }
//            };

//            mockConnection.Setup(c => c.Open());
//            mockConnection.Setup(c => c.BeginTransaction()).Returns(mockTransaction.Object);
//            mockCommand.Setup(c => c.ExecuteScalar()).Throws(new Exception());

//            // Act
//            var result = cartDatabaseDAO.Add(cart);

//            // Assert
//            Assert.IsFalse(result);
//        }

//        [TestMethod]
//        public void Get_CartByUserId_Success()
//        {
//            // Arrange
//            var userId = 1;
//            var configuration = new Dictionary<string, string> { { "userId", userId.ToString() } };

//            mockConnection.Setup(c => c.Open());
//            mockCommand.Setup(c => c.ExecuteReader()).Returns(MockDataReader());

//            // Act
//            var result = cartDatabaseDAO.Get(configuration);

//            // Assert
//            Assert.IsInstanceOfType(result, typeof(Cart));
//        }

//        [TestMethod]
//        public void Update_Cart_Success()
//        {
//            // Arrange
//            var cart = new Cart
//            {
//                Id = 1,
//                customerId = 1,
//                CartProducts = new ObservableCollection<CartProduct>
//        {
//            new CartProduct { Product = new Product { Id = 1 }, Quantity = 2 }
//        }
//            };

//            mockConnection.Setup(c => c.Open());
//            mockConnection.Setup(c => c.BeginTransaction()).Returns(mockTransaction.Object);
//            mockCommand.Setup(c => c.ExecuteNonQuery()).Returns(1);

//            // Act
//            var result = cartDatabaseDAO.Update(cart);

//            // Assert
//            Assert.IsTrue(result);
//        }

//        [TestMethod]
//        public void Update_Cart_Failure()
//        {
//            // Arrange
//            var cart = new Cart
//            {
//                Id = 1,
//                customerId = 1,
//                CartProducts = new ObservableCollection<CartProduct>
//        {
//            new CartProduct { Product = new Product { Id = 1 }, Quantity = 2 }
//        }
//            };

//            mockConnection.Setup(c => c.Open());
//            mockConnection.Setup(c => c.BeginTransaction()).Returns(mockTransaction.Object);
//            mockCommand.Setup(c => c.ExecuteNonQuery()).Throws(new Exception());

//            // Act
//            var result = cartDatabaseDAO.Update(cart);

//            // Assert
//            Assert.IsFalse(result);
//        }

//        private NpgsqlDataReader MockDataReader()
//        {
//            var mockDataReader = new Mock<NpgsqlDataReader>();
//            mockDataReader.SetupSequence(r => r.Read())
//                .Returns(true)
//                .Returns(false);
//            mockDataReader.Setup(r => r["uniqueid"]).Returns(1);
//            mockDataReader.Setup(r => r["productid"]).Returns(1);
//            mockDataReader.Setup(r => r["name"]).Returns("Product Name");
//            mockDataReader.Setup(r => r["description"]).Returns("Product Description");
//            mockDataReader.Setup(r => r["price"]).Returns(100.0);
//            mockDataReader.Setup(r => r["stock"]).Returns(10);
//            mockDataReader.Setup(r => r["cateid"]).Returns("Category1");
//            mockDataReader.Setup(r => r["imagepath"]).Returns("image/path");
//            mockDataReader.Setup(r => r["status"]).Returns(true);
//            mockDataReader.Setup(r => r["orderquantity"]).Returns(5);
//            mockDataReader.Setup(r => r["amount"]).Returns(2);

//            return mockDataReader.Object;
//        }
//    }
//}
