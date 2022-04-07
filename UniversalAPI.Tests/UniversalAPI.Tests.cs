using UniversalAPI;
using Interfaces;
using Microsoft.Data.SqlClient;
using UniversalAPI.Tests;
using System.Collections.Generic;
using Xunit;
using Newtonsoft.Json;
using UniversalAPI.Helpers;
using System.Data;

namespace UniversalAPITests
{
    public class SqlUtilTests
    {
        [Fact]
        public void GetConnection()
        {
            var connection = SqlUtil.GetConnection(ConnectionString.ConnectionToData);
            connection.Open();

            Assert.NotNull(connection);
            Assert.IsType<SqlConnection>(connection);
            Assert.True(connection.State == ConnectionState.Open);

            connection.Close();
        }

        [Fact]
        public void GetReader()
        {
            var connection = SqlUtil.GetConnection(ConnectionString.ConnectionToData);
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
            var context = MockContext.GetTestAPIContext();

            // Act
            var result = QueryCreator.CreateCommandQuery(jsonString, context);

            // Assert
            Assert.NotNull(result);
            var resultType = Assert.IsType<string>(result);
            var json = Assert.IsAssignableFrom<string>(resultType);
            Assert.NotEqual(string.Empty, json);
            Assert.Equal(
                "SELECT SignUp.PoolId, LeaderBoard.Rank, LeaderBoard.Owner, LeaderBoard.Amount " +
                "FROM SignUp INNER JOIN LeaderBoard " +
                "ON SignUp.Address = LeaderBoard.Owner" +
                " WHERE SignUp.id = 3 AND SignUp.address = '0x3a31ee5557c9369c35573496555b1bc93553b553' FOR JSON PATH", json);
        }
    }

    public class DataReaderTests : TestData
    {
        [Theory, MemberData(nameof(GetTestData))]
        public void GetJsonData(Dictionary<string, dynamic> data, string expected)
        {
            var jsonString = JsonConvert.SerializeObject(data);
            var context = MockContext.GetTestAPIContext();
            var commandQuery = QueryCreator.CreateCommandQuery(jsonString, context);

            var result = DataReader.GetJsonData(commandQuery, ConnectionString.ConnectionToData);

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            var resultType = Assert.IsType<string>(result);
            string resultJson = Assert.IsAssignableFrom<string>(resultType);
            Assert.Equal(expected, resultJson);
        }
    }

    public class UniversalAPITests : TestData
    {
        [Theory, MemberData(nameof(GetTestData))]
        public void InvokeRequest(Dictionary<string, dynamic> data, string expected)
        {
            // Arrange
            var jsonString = JsonConvert.SerializeObject(data);
            var context = MockContext.GetTestAPIContext();
            var UniversalAPI = new APIClient(ConnectionString.ConnectionToData);

            // Act
            var result = UniversalAPI.InvokeRequest(jsonString, context);

            // Assert
            Assert.NotNull(result);
            var resultType = Assert.IsType<string>(result);
            var json = Assert.IsAssignableFrom<string>(resultType);
            Assert.NotEqual(string.Empty, json);
            Assert.Equal(expected, json);
        }
    }
}