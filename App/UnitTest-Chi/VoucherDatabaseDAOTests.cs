using BC_Market.DAO;
using BC_Market.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;

namespace UnitTest_Chi
{
    [TestClass]
    public class VoucherDatabaseDAOTests
    {
        private Mock<IDbConnection> mockConnection;
        private Mock<IDbCommand> mockCommand;
        private Mock<IDbTransaction> mockTransaction;
        private VoucherDatabaseDAO voucherDatabaseDAO;

        [TestInitialize]
        public void Setup()
        {
            mockConnection = new Mock<IDbConnection>();
            mockCommand = new Mock<IDbCommand>();
            mockTransaction = new Mock<IDbTransaction>();
            voucherDatabaseDAO = new VoucherDatabaseDAO();

            mockConnection.Setup(c => c.CreateCommand()).Returns(mockCommand.Object);
            mockCommand.Setup(c => c.Connection).Returns(mockConnection.Object);
            mockCommand.Setup(c => c.Transaction).Returns(mockTransaction.Object);
        }

        [TestMethod]
        public void AddVoucher_ValidVoucher_ReturnsTrue()
        {
            // Arrange
            var voucher = new Voucher
            {
                Name = "Test Voucher",
                Description = "Test Description",
                Percent = 10,
                Amount = 100,
                Condition = 50,
                Stock = 10,
                Validate = DateTime.Now.AddDays(10),
                RankId = "R01"
            };

            mockConnection.Setup(c => c.Open());
            mockCommand.Setup(c => c.ExecuteScalar()).Returns(Guid.NewGuid().ToString());
            mockCommand.Setup(c => c.ExecuteNonQuery()).Returns(1);

            // Act
            var result = voucherDatabaseDAO.Add(voucher);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void DeleteVoucher_ValidVoucher_ReturnsTrue()
        {
            // Arrange
            var voucher = new Voucher { VoucherId = 1 };

            mockConnection.Setup(c => c.Open());
            mockCommand.Setup(c => c.ExecuteNonQuery()).Returns(1);

            // Act
            var result = voucherDatabaseDAO.Delete(voucher);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void GetAllVouchers_NoParameters_ReturnsAllVouchers()
        {
            // Arrange
            mockConnection.Setup(c => c.Open());
            mockCommand.Setup(c => c.ExecuteReader(It.IsAny<CommandBehavior>())).Returns(MockDataReader());

            // Act
            var result = voucherDatabaseDAO.GetAll();

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetVoucherByRankId_ValidRankId_ReturnsVoucher()
        {
            // Arrange
            var configuration = new Dictionary<string, string> { { "rankid", "1" } };

            mockConnection.Setup(c => c.Open());
            mockCommand.Setup(c => c.ExecuteReader(It.IsAny<CommandBehavior>())).Returns(MockDataReader());

            // Act
            var result = voucherDatabaseDAO.Get(configuration);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void UpdateVoucher_ValidVoucher_ReturnsTrue()
        {
            // Arrange
            var voucher = new Voucher
            {
                VoucherId = 1,
                Name = "Updated Voucher",
                Description = "Updated Description",
                Percent = 15,
                Amount = 150,
                Condition = 75,
                Stock = 20,
                Validate = DateTime.Now.AddDays(20),
                RankId = "2"
            };

            mockConnection.Setup(c => c.Open());
            mockCommand.Setup(c => c.ExecuteNonQuery()).Returns(1);

            // Act
            var result = voucherDatabaseDAO.Update(voucher);

            // Assert
            Assert.IsTrue(result);
        }

        private IDataReader MockDataReader()
        {
            var mockDataReader = new Mock<IDataReader>();
            mockDataReader.SetupSequence(r => r.Read())
                .Returns(true)
                .Returns(false);
            mockDataReader.Setup(r => r["id"]).Returns(Guid.NewGuid().ToString());
            mockDataReader.Setup(r => r["name"]).Returns("Test Voucher");
            mockDataReader.Setup(r => r["description"]).Returns("Test Description");
            mockDataReader.Setup(r => r["percent"]).Returns(10);
            mockDataReader.Setup(r => r["amount"]).Returns(100);
            mockDataReader.Setup(r => r["condition"]).Returns(50);
            mockDataReader.Setup(r => r["stock"]).Returns(10);
            mockDataReader.Setup(r => r["validate"]).Returns(DateTime.Now.AddDays(10));
            mockDataReader.Setup(r => r["rankid"]).Returns("1");

            return mockDataReader.Object;
        }
    }
}
