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

    public class DataCheckerTests
    {
        [Fact]
        public void GetDataItem()
        {
            Dictionary<string, dynamic> dataObj = new Dictionary<string, dynamic>
            {
                { "Request", "mysignup" },
                { "Id", 3 },
                { "address", "0x3a31ee5557c9369c35573496555b1bc93553b553" }
            };
            string key = "Address";

            var result = DataChecker.GetDataItem(dataObj, key);

            Assert.NotNull(result);
            var resultType = Assert.IsType<KeyValuePair<string, dynamic>>(result);
            Assert.IsAssignableFrom<KeyValuePair<string, dynamic>>(resultType);
            Assert.Equal(result.Value.Value, dataObj["address"]);
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
                { "Id", 3 },
                { "address", "0x3a31ee5557c9369c35573496555b1bc93553b553" }
            };
            var jsonRequest = JsonConvert.SerializeObject(dataObj);
            var requestSEttings = new APIRequestSettings
            {
                SelectedTables = "SignUp, LeaderBoard",
                SelectedColumns = "SignUp.PoolId, LeaderBoard.Rank, LeaderBoard.Owner, LeaderBoard.Amount",
                JoinCondition = "SignUp.Address = LeaderBoard.Owner"
            };

            // Act
            var result = QueryCreator.CreateCommandQuery(jsonRequest, requestSEttings);

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
        public void GetJsonData(Dictionary<string, dynamic> data, APIRequestSettings requestSettings, string expected)
        {
            var jsonString = JsonConvert.SerializeObject(data);
            var commandQuery = QueryCreator.CreateCommandQuery(jsonString, requestSettings);

            var result = DataReader.GetJsonData(commandQuery, ConnectionString.ConnectionToData);

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            var resultType = Assert.IsType<string>(result);
            string resultJson = Assert.IsAssignableFrom<string>(resultType);
            Assert.Equal(expected, resultJson);
        }
    }

    public class APIClientTests : TestData
    {
        [Theory, MemberData(nameof(GetTestData))]
        public void InvokeRequest(Dictionary<string, dynamic> data, APIRequestSettings requestSettings, string expected)
        {
            // Arrange
            var jsonString = JsonConvert.SerializeObject(data);

            // Act
            var result = APIClient.InvokeRequest(jsonString, requestSettings, ConnectionString.ConnectionToData);

            // Assert
            Assert.NotNull(result);
            var resultType = Assert.IsType<string>(result);
            var json = Assert.IsAssignableFrom<string>(resultType);
            Assert.NotEqual(string.Empty, json);
            Assert.Equal(expected, json);
        }
    }
}