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
    public class ProductDatabaseDAOTests
    {
        private Mock<IDbConnection> mockConnection;
        private Mock<IDbCommand> mockCommand;
        private Mock<IDbTransaction> mockTransaction;
        private ProductDatabaseDAO productDatabaseDAO;

        [TestInitialize]
        public void Setup()
        {
            mockConnection = new Mock<IDbConnection>();
            mockCommand = new Mock<IDbCommand>();
            mockTransaction = new Mock<IDbTransaction>();
            productDatabaseDAO = new ProductDatabaseDAO();

            mockConnection.Setup(c => c.CreateCommand()).Returns(mockCommand.Object);
            mockCommand.Setup(c => c.Connection).Returns(mockConnection.Object);
            mockCommand.Setup(c => c.Transaction).Returns(mockTransaction.Object);
        }

        [TestMethod]
        public void Add_Product_Success()
        {
            // Arrange
            var product = new Product
            {
                Name = "Test Product",
                Price = 10.0f,
                Description = "Test Description",
                Stock = 100,
                CategoryId = "M01",
                ImagePath = "Test Image Path",
                Status = "Test Status",
                OrderQuantity = 0
            };

            mockConnection.Setup(c => c.Open());
            mockConnection.Setup(c => c.BeginTransaction()).Returns(mockTransaction.Object);
            mockCommand.Setup(c => c.ExecuteScalar()).Returns(product.Id);
            mockCommand.Setup(c => c.ExecuteNonQuery()).Returns(1);

            // Act
            var result = productDatabaseDAO.Add(product);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Update_Product_Success()
        {
            // Arrange
            var product = new Product
            {
                Name = "Test Product",
                Price = 10.0f,
                Description = "Test Description",
                Stock = 100,
                CategoryId = "M01",
                ImagePath = "Test Image Path",
                Status = "Test Status",
                OrderQuantity = 0
            };

            mockConnection.Setup(c => c.Open());
            mockConnection.Setup(c => c.BeginTransaction()).Returns(mockTransaction.Object);
            mockCommand.Setup(c => c.ExecuteNonQuery()).Returns(1);

            // Act
            var result = productDatabaseDAO.Update(product);

            // Assert
            Assert.IsTrue(result);
        }

        //[TestMethod]
        //public void Delete_Product_Success()
        //{
        //    // Arrange
        //    var product = new Product
        //    {
        //        Name = "Test Product",
        //        Price = 10.0f,
        //        Description = "Test Description",
        //        Stock = 100,
        //        CategoryId = "M01",
        //        ImagePath = "Test Image Path",
        //        Status = "Test Status",
        //        OrderQuantity = 0
        //    };

        //    mockConnection.Setup(c => c.Open());
        //    mockConnection.Setup(c => c.BeginTransaction()).Returns(mockTransaction.Object);
        //    mockCommand.Setup(c => c.ExecuteNonQuery()).Returns(1);

        //    // Act
        //    var result = productDatabaseDAO.Delete(product);

        //    // Assert
        //    Assert.IsTrue(result);
        //}

        [TestMethod]
        public void Get_Product_Success()
        {
            //Arrange
            Dictionary<string, string> configuration = new Dictionary<string, string>
            {
                { "searchKey", "" },
                { "category", "" },
                { "skip", "0" },
                { "take", "15" }
            };

            mockConnection.Setup(c => c.Open());
            mockCommand.Setup(c => c.ExecuteReader(It.IsAny<CommandBehavior>())).Returns(MockDataReader());

            // Act
            var result = productDatabaseDAO.Get(configuration);

            // Assert
            Assert.IsInstanceOfType(result, typeof(List<Product>));
        }

        private IDataReader MockDataReader()
        {
            var mockDataReader = new Mock<IDataReader>();
            mockDataReader.SetupSequence(r => r.Read())
                .Returns(true)
                .Returns(false);
            mockDataReader.Setup(r => r["id"]).Returns(1);
            mockDataReader.Setup(r => r["name"]).Returns("Test Product");
            mockDataReader.Setup(r => r["price"]).Returns(10.0);
            mockDataReader.Setup(r => r["description"]).Returns("Test Description");
            mockDataReader.Setup(r => r["stock"]).Returns(100);
            mockDataReader.Setup(r => r["cateid"]).Returns("M01");
            mockDataReader.Setup(r => r["imagepath"]).Returns("Test Image Path");
            mockDataReader.Setup(r => r["status"]).Returns("Test Status");
            mockDataReader.Setup(r => r["orderquantity"]).Returns(0);

            return mockDataReader.Object;
        }
    }
}