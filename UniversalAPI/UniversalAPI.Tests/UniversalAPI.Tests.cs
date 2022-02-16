using Poolz;
using Interfaces.DBModel;
using Interfaces.DBModel.Models;
using Interfaces.Helpers;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using System;
using System.IO;
using Newtonsoft.Json;

namespace UniversalAPITests
{
    public class UniversalAPITests
    {
        [Fact]
        public void GetConnection()
        {
            var UniversalAPI = new UniversalAPI(ConnectionString.connectionString, GetTestContext());

            var result = UniversalAPI.GetConnection();
            result.Open();

            Assert.NotNull(result);
            Assert.IsType<SqlConnection>(result);
            result.Close();
        }

        [Fact]
        public void GetReader()
        {
            var UniversalAPI = new UniversalAPI(ConnectionString.connectionString, GetTestContext());
            var connection = UniversalAPI.GetConnection();
            connection.Open();

            var result = UniversalAPI.GetReader("SELECT * FROM LeaderBoard", connection);

            Assert.NotNull(result);
            Assert.IsType<SqlDataReader>(result);
            Assert.True(result.HasRows);
            connection.Close();
        }

        [Fact]
        public void GetTable()
        {
            // Arrange
            Dictionary<string, dynamic> inputData = new Dictionary<string, dynamic>
            {
                { "TableName", "SignUp" },
                { "Id", 3 }
            };
            var jsonString = JsonConvert.SerializeObject(inputData);
            var context = GetTestContext();
            var UniversalAPI = new UniversalAPI(ConnectionString.connectionString, context);
            
            // Act
            var result = UniversalAPI.GetTable(jsonString);

            // Assert
            Assert.NotNull(result);
            var resultType = Assert.IsType<string>(result);
            var json = Assert.IsAssignableFrom<string>(resultType);
            var table = JsonConvert.DeserializeObject<List<object[]>>(json);
            Assert.Single(table);
        }

        [Fact]
        public void InnerJoinTables()
        {
            var context = GetTestContext();
            var UniversalAPI = new UniversalAPI(ConnectionString.connectionString, context);

            var result = UniversalAPI.InnerJoinTables(
                leftTableName: "LeaderBoard",
                rightTableName: "Wallets",
                conditions: new string[] {
                    "LeaderBoard.rank = Wallets.id",
                    "LeaderBoard.walletId = Wallets.id"
                });

            Assert.NotNull(result);
            //var resultType = Assert.IsType<List<object[]>>(result);
            //var table = Assert.IsAssignableFrom<List<object[]>>(resultType);
            //Assert.Single(table);
            //Assert.Equal(7, table[0].Length);
        }

        [Fact]
        public void InnerJoinTablesWithSelectedColumns()
        {
            var context = GetTestContext();
            var UniversalAPI = new UniversalAPI(ConnectionString.connectionString, context);

            var result = UniversalAPI.InnerJoinTables(
                leftTableName: "LeaderBoard",
                rightTableName: "Wallets",
                conditions: new string[] { "LeaderBoard.rank = Wallets.id" },
                selectColumns: new string[] {
                    "LeaderBoard.rank",
                    "Wallets.amount"
                });

            Assert.NotNull(result);
            //var resultType = Assert.IsType<List<object[]>>(result);
            //var table = Assert.IsAssignableFrom<List<object[]>>(resultType);
            //Assert.Equal(2, table.Count);
            //Assert.Equal(2, table[0].Length);
        }

        /* Emulate DB with data */
        private DynamicDBContext GetTestContext()
        {
            /* Initialize Wallet table */
            var wallets = new List<Wallet>
            {
                new Wallet { Id = 1, Owner = "0x1a01ee5577c9d69c35a77496565b1bc95588b521" },
                new Wallet { Id = 2, Owner = "0x2a01ee5557c9d69c35577496555b1bc95558b552" },
                new Wallet { Id = 3, Owner = "0x3a31ee5557c9369c35573496555b1bc93553b553" },
                new Wallet { Id = 4, Owner = "0x4a71ee5577c9d79c37577496555b1bc95558b554" }
            }.AsQueryable();

            var mockSetWallet = new Mock<DbSet<Wallet>>();

            mockSetWallet.As<IQueryable<Wallet>>().Setup(m => m.Provider).Returns(wallets.Provider);
            mockSetWallet.As<IQueryable<Wallet>>().Setup(m => m.Expression).Returns(wallets.Expression);
            mockSetWallet.As<IQueryable<Wallet>>().Setup(m => m.ElementType).Returns(wallets.ElementType);
            mockSetWallet.As<IQueryable<Wallet>>().Setup(m => m.GetEnumerator()).Returns(wallets.GetEnumerator);

            /* Initialize LeaderBoard table */
            var leaderBoards = new List<LeaderBoard>
            {
                new LeaderBoard { Id = 1, Rank = 1, Owner = "0x1a01ee5577c9d69c35a77496565b1bc95588b521", Amount = Convert.ToDecimal(750.505823765680934368)},
                new LeaderBoard { Id = 2, Rank = 2, Owner = "0x2a01ee5557c9d69c35577496555b1bc95558b552", Amount = Convert.ToDecimal(251.795264077704686136)},
                new LeaderBoard { Id = 3, Rank = 3, Owner = "0x3a31ee5557c9369c35573496555b1bc93553b553", Amount = Convert.ToDecimal(250.02109769151781894)},
                new LeaderBoard { Id = 4, Rank = 4, Owner = "0x4a71ee5577c9d79c37577496555b1bc95558b554", Amount = Convert.ToDecimal(233.279855562249360519)}
            }.AsQueryable();

            var mockSetLeaderBoards = new Mock<DbSet<LeaderBoard>>();

            mockSetLeaderBoards.As<IQueryable<LeaderBoard>>().Setup(m => m.Provider).Returns(leaderBoards.Provider);
            mockSetLeaderBoards.As<IQueryable<LeaderBoard>>().Setup(m => m.Expression).Returns(leaderBoards.Expression);
            mockSetLeaderBoards.As<IQueryable<LeaderBoard>>().Setup(m => m.ElementType).Returns(leaderBoards.ElementType);
            mockSetLeaderBoards.As<IQueryable<LeaderBoard>>().Setup(m => m.GetEnumerator()).Returns(leaderBoards.GetEnumerator);

            /* Initialize SignUp table */
            var signUp = new List<SignUp>
            {
                new SignUp { Id = 1, Address = "0x1a01ee5577c9d69c35a77496565b1bc95588b521", PoolId = 1},
                new SignUp { Id = 2, Address = "0x2a01ee5557c9d69c35577496555b1bc95558b552", PoolId = 2},
                new SignUp { Id = 3, Address = "0x3a31ee5557c9369c35573496555b1bc93553b553", PoolId = 3},
                new SignUp { Id = 4, Address = "0x4a71ee5577c9d79c37577496555b1bc95558b554", PoolId = 4}
            }.AsQueryable();

            var mockSetSignUp = new Mock<DbSet<SignUp>>();

            mockSetSignUp.As<IQueryable<SignUp>>().Setup(m => m.Provider).Returns(signUp.Provider);
            mockSetSignUp.As<IQueryable<SignUp>>().Setup(m => m.Expression).Returns(signUp.Expression);
            mockSetSignUp.As<IQueryable<SignUp>>().Setup(m => m.ElementType).Returns(signUp.ElementType);
            mockSetSignUp.As<IQueryable<SignUp>>().Setup(m => m.GetEnumerator()).Returns(signUp.GetEnumerator);


            /* Create and setting context */
            var mockContext = new Mock<DynamicDBContext>();

            mockContext.Setup(t => t.Wallets).Returns(mockSetWallet.Object);
            mockContext.Setup(t => t.Set<Wallet>()).Returns(mockSetWallet.Object);

            mockContext.Setup(t => t.LeaderBoard).Returns(mockSetLeaderBoards.Object);
            mockContext.Setup(t => t.Set<LeaderBoard>()).Returns(mockSetLeaderBoards.Object);

            mockContext.Setup(t => t.SignUp).Returns(mockSetSignUp.Object);
            mockContext.Setup(t => t.Set<SignUp>()).Returns(mockSetSignUp.Object);

            return mockContext.Object;
        }
    }
}
