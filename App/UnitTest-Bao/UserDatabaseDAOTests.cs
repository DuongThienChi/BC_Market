using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BC_Market.Models;
using BC_Market.DAO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Data;

namespace UnitTest_Bao
{
    [TestClass]
    public class UserDatabaseDAOTests
    {
        private Mock<IDbConnection> mockConnection;
        private Mock<IDbCommand> mockCommand;
        private Mock<IDbTransaction> mockTransaction;
        private UserDatabaseDAO userDatabaseDAO;

        [TestInitialize]
        public void Setup()
        {
            mockConnection = new Mock<IDbConnection>();
            mockCommand = new Mock<IDbCommand>();
            mockTransaction = new Mock<IDbTransaction>();
            userDatabaseDAO = new UserDatabaseDAO();

            mockConnection.Setup(c => c.CreateCommand()).Returns(mockCommand.Object);
            mockCommand.Setup(c => c.Connection).Returns(mockConnection.Object);
            mockCommand.Setup(c => c.Transaction).Returns(mockTransaction.Object);
        }

        [TestMethod]
        public void Add_User_Success()
        {
            // Arrange
            USER user = new USER
            {
                Username = "Test User",
                Password = "Test Password",
                Email = "Test Email",
                Roles = new List<Role> ()
                   { 
                        new Role ()
                        {
                            Name = "Shopper"
                        }
                   },
                CreatedAt = DateTime.Parse("01/01/2025"),
                Rank = "R01",
                Point = 0
            };

            mockConnection.Setup(c => c.Open());
            mockConnection.Setup(c => c.BeginTransaction()).Returns(mockTransaction.Object);
            mockCommand.Setup(c => c.ExecuteScalar()).Returns(user.Id);
            mockCommand.Setup(c => c.ExecuteNonQuery()).Returns(1);

            // Act
            var result = userDatabaseDAO.Add(user);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Update_User_Success()
        {
            // Arrange
            USER user = new USER
            {
                Username = "Test User",
                Password = "Test Password",
                Email = "Test Email",
                Roles = new List<Role>()
                   {
                        new Role ()
                        {
                            Name = "Shopper"
                        }
                   },
                CreatedAt = DateTime.Parse("01/01/2025"),
                Rank = "R01",
                Point = 0
            };

            mockConnection.Setup(c => c.Open());
            mockConnection.Setup(c => c.BeginTransaction()).Returns(mockTransaction.Object);
            mockCommand.Setup(c => c.ExecuteNonQuery()).Returns(1);

            // Act
            var result = userDatabaseDAO.Update(user);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Delete_User_Success()
        {
            // Arrange
            USER user = new USER
            {
                Username = "Test User",
                Password = "Test Password",
                Email = "Test Email",
                Roles = new List<Role>()
                   {
                        new Role ()
                        {
                            Name = "Shopper"
                        }
                   },
                CreatedAt = DateTime.Parse("01/01/2025"),
                Rank = "R01",
                Point = 0
            };

            mockConnection.Setup(c => c.Open());
            mockConnection.Setup(c => c.BeginTransaction()).Returns(mockTransaction.Object);
            mockCommand.Setup(c => c.ExecuteNonQuery()).Returns(1);

            // Act
            var result = userDatabaseDAO.Delete(user);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Get_User_Success()
        {
            //Arrange

            mockConnection.Setup(c => c.Open());
            mockCommand.Setup(c => c.ExecuteReader(It.IsAny<CommandBehavior>())).Returns(MockDataReader());

            // Act
            var result = userDatabaseDAO.Get(null);

            // Assert
            Assert.IsInstanceOfType(result, typeof(List<USER>));
        }

        private IDataReader MockDataReader()
        {
            var mockDataReader = new Mock<IDataReader>();
            mockDataReader.SetupSequence(r => r.Read())
                .Returns(true)
                .Returns(false);
            mockDataReader.Setup(r => r["id"]).Returns(1);
            mockDataReader.Setup(r => r["username"]).Returns("Test User");
            mockDataReader.Setup(r => r["password"]).Returns("Test Password");
            mockDataReader.Setup(r => r["email"]).Returns("Test Email");
            mockDataReader.Setup(r => r["roleid"]).Returns("R01");
            mockDataReader.Setup(r => r["rolename"]).Returns("Test Role");
            mockDataReader.Setup(r => r["roledescription"]).Returns("Test Description");
            mockDataReader.Setup(r => r["createdat"]).Returns(DateTime.Parse("01/01/2025"));
            mockDataReader.Setup(r => r["rank"]).Returns("R01");
            mockDataReader.Setup(r => r["point"]).Returns(0);

            return mockDataReader.Object;
        }
    }
}
