using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using BC_Market.Models;
using BC_Market.DAO;
using BC_Market.BUS;
using System.Configuration;

namespace UnitTest_Bao
{
    [TestClass]
    public class DeliveryDatabaseDAOTests
    {
        private Mock<IDbConnection> mockConnection;
        private Mock<IDbCommand> mockCommand;
        private Mock<IDbTransaction> mockTransaction;
        private DeliveryUnitDatabaseDAO deliveryUnitDatabaseDAO;

        [TestInitialize]
        public void Setup()
        {
            mockConnection = new Mock<IDbConnection>();
            mockCommand = new Mock<IDbCommand>();
            mockTransaction = new Mock<IDbTransaction>();
            deliveryUnitDatabaseDAO = new DeliveryUnitDatabaseDAO();

            mockConnection.Setup(c => c.CreateCommand()).Returns(mockCommand.Object);
            mockCommand.Setup(c => c.Connection).Returns(mockConnection.Object);
            mockCommand.Setup(c => c.Transaction).Returns(mockTransaction.Object);
        }

        [TestMethod]
        public void Add_Delivery_Success()
        {
            // Arrange
            var delivery = new DeliveryUnit
            {
                Name = "Test Delivery",
                Price = 10.0f
            };

            mockConnection.Setup(c => c.Open());
            mockConnection.Setup(c => c.BeginTransaction()).Returns(mockTransaction.Object);
            mockCommand.Setup(c => c.ExecuteScalar()).Returns(delivery.Id);
            mockCommand.Setup(c => c.ExecuteNonQuery()).Returns(1);

            // Act
            var result = deliveryUnitDatabaseDAO.Add(delivery);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Update_Delivery_Success()
        {
            // Arrange
            var delivery = new DeliveryUnit
            {
                Name = "Test Delivery",
                Price = 10.0f
            };

            mockConnection.Setup(c => c.Open());
            mockConnection.Setup(c => c.BeginTransaction()).Returns(mockTransaction.Object);
            mockCommand.Setup(c => c.ExecuteNonQuery()).Returns(1);

            // Act
            var result = deliveryUnitDatabaseDAO.Update(delivery);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Delete_Delivery_Success()
        {
            // Arrange
            var delivery = new DeliveryUnit
            {
                Name = "Test Delivery",
                Price = 10.0f
            };

            mockConnection.Setup(c => c.Open());
            mockConnection.Setup(c => c.BeginTransaction()).Returns(mockTransaction.Object);
            mockCommand.Setup(c => c.ExecuteNonQuery()).Returns(1);

            // Act
            var result = deliveryUnitDatabaseDAO.Delete(delivery);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Get_Delivery_Success()
        {
            // Arrange
            var delivery = new DeliveryUnit
            {
                Name = "Test Delivery",
                Price = 10.0f
            };

            mockConnection.Setup(c => c.Open());
            mockConnection.Setup(c => c.BeginTransaction()).Returns(mockTransaction.Object);
            mockCommand.Setup(c => c.ExecuteNonQuery()).Returns(1);

            // Act
            var result = deliveryUnitDatabaseDAO.Get(null);

            // Assert
            Assert.IsInstanceOfType(result, typeof(List<DeliveryUnit>));
        }
    }
}
