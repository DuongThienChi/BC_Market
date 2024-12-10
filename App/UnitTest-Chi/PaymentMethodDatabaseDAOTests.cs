using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Npgsql;
using System;
using System.Collections.Generic;
using BC_Market.Models;
using BC_Market.DAO;
using System.Data;

namespace UnitTest_Chi
{
    [TestClass]
    public class PaymentMethodDatabaseDAOTests
    {
        private Mock<IDbConnection> mockConnection;
        private Mock<IDbCommand> mockCommand;
        private Mock<IDbTransaction> mockTransaction;
        private PaymentMethodDatabaseDAO paymentMethodDatabaseDAO;

        [TestInitialize]
        public void Setup()
        {
            mockConnection = new Mock<IDbConnection>();
            mockCommand = new Mock<IDbCommand>();
            mockTransaction = new Mock<IDbTransaction>();
            paymentMethodDatabaseDAO = new PaymentMethodDatabaseDAO();

            mockConnection.Setup(c => c.CreateCommand()).Returns(mockCommand.Object);
            mockCommand.Setup(c => c.Connection).Returns(mockConnection.Object);
            mockCommand.Setup(c => c.Transaction).Returns(mockTransaction.Object);
        }

        [TestMethod]
        public void GetAllPaymentMethods_NoParameters_ReturnsAllPaymentMethods()
        {
            // Arrange
            mockConnection.Setup(c => c.Open());
            mockCommand.Setup(c => c.ExecuteReader(It.IsAny<CommandBehavior>())).Returns(MockDataReader());

            // Act
            var result = paymentMethodDatabaseDAO.GetAll();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(List<PaymentMethod>));
        }

        [TestMethod]
        public void GetPaymentMethodById_ValidId_ReturnsPaymentMethod()
        {
            // Arrange
            var configuration = new Dictionary<string, string> { { "paymentMethodId", "1" } };

            mockConnection.Setup(c => c.Open());
            mockCommand.Setup(c => c.ExecuteReader(It.IsAny<CommandBehavior>())).Returns(MockDataReader());

            // Act
            var result = paymentMethodDatabaseDAO.Get(configuration);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOfType(result, typeof(PaymentMethod));
        }

        private IDataReader MockDataReader()
        {
            var mockDataReader = new Mock<IDataReader>();
            mockDataReader.SetupSequence(r => r.Read())
                .Returns(true)
                .Returns(false);
            mockDataReader.Setup(r => r["id"]).Returns(1);
            mockDataReader.Setup(r => r["name"]).Returns("Test Payment Method");

            return mockDataReader.Object;
        }
    }
}
