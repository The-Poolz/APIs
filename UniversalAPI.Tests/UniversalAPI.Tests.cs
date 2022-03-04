using UniversalApi;
using Interfaces.DBModel;
using Interfaces.Helpers;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Newtonsoft.Json;
using UniversalApi.Helpers;
using System.Data;
using Interfaces.DBModel.Models;

namespace UniversalAPITests
{
    public class SqlUtilTests
    {
        [Fact]
        public void GetConnection()
        {
            var connection = SqlUtil.GetConnection(ConnectionString.connectionString);
            connection.Open();

            Assert.NotNull(connection);
            Assert.IsType<SqlConnection>(connection);
            Assert.True(connection.State == ConnectionState.Open);

            connection.Close();
        }

        [Fact]
        public void GetReader()
        {
            var connection = SqlUtil.GetConnection(ConnectionString.connectionString);
            connection.Open();

            var reader = SqlUtil.GetReader("SELECT * FROM LeaderBoard", connection);

            Assert.NotNull(reader);
            Assert.IsType<SqlDataReader>(reader);
            Assert.True(reader.HasRows);
            Assert.Equal(4, reader.FieldCount);
            connection.Close();
        }
    }

    public class QueryCreatorTests
    {
        [Fact]
        public void GetCommandQuery()
        {
            // Arrange
            Dictionary<string, dynamic> dataObj = new Dictionary<string, dynamic>
            {
                { "Request", "mysignup" },
                { "Id", 3 },
                { "address", "0x3a31ee5557c9369c35573496555b1bc93553b553" }
            };
            var jsonString = JsonConvert.SerializeObject(dataObj);

            // Act
            var result = QueryCreator.GetCommandQuery(jsonString);

            // Assert
            Assert.NotNull(result);
            var resultType = Assert.IsType<string>(result);
            var json = Assert.IsAssignableFrom<string>(resultType);
            Assert.NotEqual(string.Empty, json);
            Assert.Equal(
                "SELECT SignUp.PoolId, LeaderBoard.Rank, LeaderBoard.Owner, LeaderBoard.Amount " +
                "FROM SignUp INNER JOIN LeaderBoard " +
                "ON SignUp.Address = LeaderBoard.Owner" +
                " WHERE SignUp.Id = 3 AND SignUp.address = '0x3a31ee5557c9369c35573496555b1bc93553b553'", json);
        }
    }

    public class DataReaderTests : Mock
    {
        [Theory, MemberData(nameof(GetTestData))]
        public void GetData(Dictionary<string, dynamic> data, string expected)
        {
            var jsonString = JsonConvert.SerializeObject(data);
            var commandQuery = QueryCreator.GetCommandQuery(jsonString);
            var context = GetTestContext();

            var result = DataReader.GetData(commandQuery, ConnectionString.connectionString, context);

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            var resultType = Assert.IsType<object[]>(result);
            Assert.IsAssignableFrom<object[]>(resultType);
            var resultJson = JsonConvert.SerializeObject(result);
            Assert.Equal(expected, resultJson);
        }
    }

    public class UniversalAPITests : Mock
    {
        [Theory, MemberData(nameof(GetTestData))]
        public void GetTable(Dictionary<string, dynamic> data, string expected)
        {
            // Arrange
            var jsonString = JsonConvert.SerializeObject(data);
            var context = GetTestContext();
            var UniversalAPI = new UniversalAPI(ConnectionString.connectionString, context);
            
            // Act
            var result = UniversalAPI.GetTable(jsonString);

            // Assert
            Assert.NotNull(result);
            var resultType = Assert.IsType<string>(result);
            var json = Assert.IsAssignableFrom<string>(resultType);
            Assert.NotEqual(string.Empty, json);
            Assert.Equal(expected, json);
        }
    }

    public class Mock
    {
        /* Emulate DB with data */
        public DynamicDBContext GetTestContext()
        {
            /* Initialize TokenBalance table */
            var tokenBalance = new List<TokenBalance>
            {
                new TokenBalance { Id = 1, Token = "ADH", Amount = "400", Owner = "0x1a01ee5577c9d69c35a77496565b1bc95588b521" },
                new TokenBalance { Id = 2, Token = "Poolz", Amount = "300", Owner = "0x2a01ee5557c9d69c35577496555b1bc95558b552" },
                new TokenBalance { Id = 3, Token = "ETH", Amount = "200", Owner = "0x3a31ee5557c9369c35573496555b1bc93553b553" },
                new TokenBalance { Id = 4, Token = "BTH", Amount = "100", Owner = "0x4a71ee5577c9d79c37577496555b1bc95558b554" }
            }.AsQueryable();

            var mockSetTokenBalance = new Mock<DbSet<TokenBalance>>();

            mockSetTokenBalance.As<IQueryable<TokenBalance>>().Setup(m => m.Provider).Returns(tokenBalance.Provider);
            mockSetTokenBalance.As<IQueryable<TokenBalance>>().Setup(m => m.Expression).Returns(tokenBalance.Expression);
            mockSetTokenBalance.As<IQueryable<TokenBalance>>().Setup(m => m.ElementType).Returns(tokenBalance.ElementType);
            mockSetTokenBalance.As<IQueryable<TokenBalance>>().Setup(m => m.GetEnumerator()).Returns(tokenBalance.GetEnumerator);

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
                new LeaderBoard { Id = 1, Rank = "1", Owner = "0x1a01ee5577c9d69c35a77496565b1bc95588b521", Amount = "750.505823765680934368"},
                new LeaderBoard { Id = 2, Rank = "2", Owner = "0x2a01ee5557c9d69c35577496555b1bc95558b552", Amount = "251.795264077704686136"},
                new LeaderBoard { Id = 3, Rank = "3", Owner = "0x3a31ee5557c9369c35573496555b1bc93553b553", Amount = "250.02109769151781894"},
                new LeaderBoard { Id = 4, Rank = "4", Owner = "0x4a71ee5577c9d79c37577496555b1bc95558b554", Amount = "233.279855562249360519"}
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

            /* Initialize APIRequestList table */
            var APIRequestList = new List<APIRequestList>
            {
                new APIRequestList {
                    Id = 1,
                    Request = "mysignup",
                    Tables = "SignUp, LeaderBoard",
                    Columns = "SignUp.PoolId, LeaderBoard.Rank, LeaderBoard.Owner, LeaderBoard.Amount",
                    JoinCondition = "SignUp.Address = LeaderBoard.Owner"
                },
                new APIRequestList {
                    Id = 2,
                    Request = "wallet",
                    Tables = "Wallets",
                    Columns = "*"
                },
                new APIRequestList {
                    Id = 3,
                    Request = "tokenbalanse",
                    Tables = "TokenBalances",
                    Columns = "Token, Owner, Amount"
                }
            }.AsQueryable();

            var mockSetAPIRequestList = new Mock<DbSet<APIRequestList>>();

            mockSetAPIRequestList.As<IQueryable<APIRequestList>>().Setup(m => m.Provider).Returns(APIRequestList.Provider);
            mockSetAPIRequestList.As<IQueryable<APIRequestList>>().Setup(m => m.Expression).Returns(APIRequestList.Expression);
            mockSetAPIRequestList.As<IQueryable<APIRequestList>>().Setup(m => m.ElementType).Returns(APIRequestList.ElementType);
            mockSetAPIRequestList.As<IQueryable<APIRequestList>>().Setup(m => m.GetEnumerator()).Returns(APIRequestList.GetEnumerator);


            /* Create and setting context */
            var mockContext = new Mock<DynamicDBContext>();

            mockContext.Setup(t => t.TokenBalances).Returns(mockSetTokenBalance.Object);
            mockContext.Setup(t => t.Set<TokenBalance>()).Returns(mockSetTokenBalance.Object);

            mockContext.Setup(t => t.Wallets).Returns(mockSetWallet.Object);
            mockContext.Setup(t => t.Set<Wallet>()).Returns(mockSetWallet.Object);

            mockContext.Setup(t => t.LeaderBoard).Returns(mockSetLeaderBoards.Object);
            mockContext.Setup(t => t.Set<LeaderBoard>()).Returns(mockSetLeaderBoards.Object);

            mockContext.Setup(t => t.SignUp).Returns(mockSetSignUp.Object);
            mockContext.Setup(t => t.Set<SignUp>()).Returns(mockSetSignUp.Object);

            mockContext.Setup(t => t.APIRequestList).Returns(mockSetAPIRequestList.Object);
            mockContext.Setup(t => t.Set<APIRequestList>()).Returns(mockSetAPIRequestList.Object);

            return mockContext.Object;
        }

        public static IEnumerable<object[]> GetTestData()
        {
            var mysignupExpected = "[{\"PoolId\":3,\"Rank\":3,\"Owner\":\"0x3a31ee5557c9369c35573496555b1bc93553b553\",\"Amount\":250.02}]";
            var walletExpected = "[{\"Id\":3,\"Owner\":\"0x3a31ee5557c9369c35573496555b1bc93553b553\"}]";
            return new List<object[]>
            {
                new object[] {
                    new Dictionary<string, dynamic>
                    {
                        { "Request", "mysignup" },
                        { "Id", 3 },
                        { "Address", "0x3a31ee5557c9369c35573496555b1bc93553b553" }
                    },
                    mysignupExpected
                },
                new object[] {
                    new Dictionary<string, dynamic>
                    {
                        { "Request", "wallet" },
                        { "Id", 3 },
                        { "Owner", "0x3a31ee5557c9369c35573496555b1bc93553b553" }
                    },
                    walletExpected
                }
            };
        }
    }
}