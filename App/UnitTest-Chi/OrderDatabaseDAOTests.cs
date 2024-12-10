using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using BC_Market.Models;
using BC_Market.DAO;
using System.Data;
using System.Diagnostics;

namespace UnitTest_Chi
{
    [TestClass]
    public class OrderDatabaseDAOTests
    {
        private Mock<IDbConnection> mockConnection;
        private Mock<IDbCommand> mockCommand;
        private Mock<IDbTransaction> mockTransaction;
        private OrderDatabaseDAO orderDatabaseDAO;

        [TestInitialize]
        public void Setup()
        {
            mockConnection = new Mock<IDbConnection>();
            mockCommand = new Mock<IDbCommand>();
            mockTransaction = new Mock<IDbTransaction>();
            orderDatabaseDAO = new OrderDatabaseDAO();

            mockConnection.Setup(c => c.CreateCommand()).Returns(mockCommand.Object);
            mockCommand.Setup(c => c.Connection).Returns(mockConnection.Object);
            mockCommand.Setup(c => c.Transaction).Returns(mockTransaction.Object);
        }

        [TestMethod]
        public void Add_Order_Success()
        {
            // Arrange
            var order = new Order
            {
                customerId = 1,
                deliveryId = 1,
                totalPrice = 100.0f,
                address = "123 Street",
                paymentMethod = 1,
                isPaid = true,
                createAt = DateTime.Now,
                Products = new ObservableCollection<CartProduct>
                {
                    new CartProduct { Product = new Product { Id = 1 }, Quantity = 2 }
                }
            };

            mockConnection.Setup(c => c.Open());
            mockConnection.Setup(c => c.BeginTransaction()).Returns(mockTransaction.Object);
            mockCommand.Setup(c => c.ExecuteScalar()).Returns(Guid.NewGuid().ToString());
            mockCommand.Setup(c => c.ExecuteNonQuery()).Returns(1);

            // Act
            var result = orderDatabaseDAO.Add(order);
            // Assert
            Assert.IsTrue(result);
        }

        //[TestMethod]
        //public void Add_Order_Failure()
        //{
        //    // Arrange
        //    var order = new Order
        //    {
        //        customerId = 1,
        //        deliveryId = 1,
        //        totalPrice = 100.0f,
        //        address = "123 Street",
        //        paymentMethod = 1,
        //        isPaid = true,
        //        createAt = DateTime.Now,
        //        Products = new ObservableCollection<CartProduct>
        //        {
        //            new CartProduct { Product = new Product { Id = 1 }, Quantity = 2 }
        //        }
        //    };
        //    mockConnection.Setup(c => c.Open());
        //    mockConnection.Setup(c => c.BeginTransaction()).Returns(mockTransaction.Object);
        //    mockTransaction.Setup(t => t.Rollback()).Verifiable();

        //    mockCommand.Setup(c => c.ExecuteScalar()).Throws(new Exception());

        //    var result = orderDatabaseDAO.Add(order);
        //    Assert.IsFalse(result);

        //}

        [TestMethod]
        public void Get_OrderById_Success()
        {
            // Arrange
            var orderId = Guid.NewGuid().ToString();
            var configuration = new Dictionary<string, string> { { "OrderId", orderId } };

            mockConnection.Setup(c => c.Open());
            mockCommand.Setup(c => c.ExecuteReader(It.IsAny<CommandBehavior>())).Returns(MockDataReader());

            // Act
            var result = orderDatabaseDAO.Get(configuration);

            // Assert
            Assert.IsInstanceOfType(result, typeof(ObservableCollection<CartProduct>));
        }

        [TestMethod]
        public void Get_OrderByDate_Success()
        {
            // Arrange
            var date = DateTime.Now.ToString("yyyy-MM-dd");
            var configuration = new Dictionary<string, string> { { "date", date } };

            mockConnection.Setup(c => c.Open());
            mockCommand.Setup(c => c.ExecuteReader(It.IsAny<CommandBehavior>())).Returns(MockDataReader());

            // Act
            var result = orderDatabaseDAO.Get(configuration);

            // Assert
            Assert.IsInstanceOfType(result, typeof(List<Order>));
        }

        private IDataReader MockDataReader()
        {
            var mockDataReader = new Mock<IDataReader>();
            mockDataReader.SetupSequence(r => r.Read())
                .Returns(true)
                .Returns(false);
            mockDataReader.Setup(r => r["id"]).Returns(Guid.NewGuid().ToString());
            mockDataReader.Setup(r => r["userid"]).Returns(1);
            mockDataReader.Setup(r => r["shipid"]).Returns(1);
            mockDataReader.Setup(r => r["totalprice"]).Returns(100.0);
            mockDataReader.Setup(r => r["address"]).Returns("123 Street");
            mockDataReader.Setup(r => r["paymentmethod"]).Returns(1);
            mockDataReader.Setup(r => r["ispaid"]).Returns(true);
            mockDataReader.Setup(r => r["createat"]).Returns(DateTime.Now);

            return mockDataReader.Object;
        }
    }
}
