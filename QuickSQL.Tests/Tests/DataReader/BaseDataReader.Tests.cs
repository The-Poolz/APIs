using Moq;
using Xunit;
using System;
using System.Data;
using System.Globalization;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using QuickSQL.Tests.Mock;
using QuickSQL.DataReader;
using QuickSQL.Tests.QueryCreator;

namespace QuickSQL.Tests.DataReader
{
    /// <summary>
    /// SqlDataReader has implemented the BaseDataReader tests.
    /// </summary>
    /// <remarks>
    /// All tests with databese use the Sql provider.
    /// </remarks>
    public static class BaseDataReaderTests
    {
        [Fact]
        public static void CreateResultSingleObject()
        {
            string json = "{\"Token\":\"ADH\",\"Owner\":\"0x1a01ee5577c9d69c35a77496565b1bc95588b521\",\"Amount\":\"400\"}";

            var result = BaseDataReader.CreateResult(json);

            Assert.NotNull(result);
            Assert.IsType<string>(result);
            Assert.Equal(json, result);
        }

        [Fact]
        public static void CreateResultArray()
        {
            string json = "{\"Token\":\"ADH\",\"Owner\":\"0x1a01ee5577c9d69c35a77496565b1bc95588b521\",\"Amount\":\"400\"}," +
                "{\"Token\":\"Poolz\",\"Owner\":\"0x2a01ee5557c9d69c35577496555b1bc95558b552\",\"Amount\":\"300\"}";

            var result = BaseDataReader.CreateResult(json);

            Assert.NotNull(result);
            Assert.IsType<string>(result);
            Assert.True(result.StartsWith("[{"), result);
            Assert.True(result.EndsWith("}]"), result);
        }

        [Fact]
        public static void CreateResultEmptyJson()
        {
            string json = null;

            var result = BaseDataReader.CreateResult(json);

            Assert.NotNull(result);
            Assert.IsType<string>(result);
            Assert.Equal("[]", result);
        }

        [Fact]
        public static void ReadData()
        {
            string expected = "{\"Id\":1,\"Token\":\"ADH\",\"Owner\":\"0x1\",\"Amount\":\"400\"}" +
                "{\"Id\":2,\"Token\":\"Poolz\",\"Owner\":\"0x2\",\"Amount\":\"300\"}";

            List<TokenBalances> emulated = new()
            {
                new TokenBalances { Id = 1, Amount = "400", Owner = "0x1", Token = "ADH" },
                new TokenBalances { Id = 2, Amount = "300", Owner = "0x2", Token = "Poolz" }
            };
            var mock = new Mock<IDataReader>();
            MockDataReader<TokenBalances> setup = new(emulated);
            setup.SetupDataReader(mock);

            var result = BaseDataReader.ReadData(mock.Object);

            Assert.NotNull(result);
            Assert.IsType<string>(result);
            Assert.Equal(expected, result);
        }

        [Fact]
        public static void GetJsonData()
        {
            string isTravisCi = Environment.GetEnvironmentVariable("IsTravisCI");
            string expected = "{\"Owner\":\"0x1a01ee5577c9d69c35a77496565b1bc95588b521\",\"Token\":\"ADH\",\"Amount\":\"400\"}";
            var request = new Request(
                "TokenBalances",
                new Collection<string>
                {
                    { "Owner" }, { "Token" }, { "Amount" }
                },
                new Collection<Condition>
                {
                    new Condition { ParamName = "Id", Operator = OperatorName.Equals, ParamValue = "1" }
                });
            string commandQuery = new SqlQueryCreator().CreateCommandQuery(request);
            string connectionString;
            if (Convert.ToBoolean(isTravisCi, new CultureInfo("en-US")))
                connectionString = Environment.GetEnvironmentVariable("TravisCIMicrosoftSqlServerConnection");
            else
                connectionString = LocalConnection.MicrosoftSqlServerConnection;

            var result = new SqlDataReader().GetJsonData(commandQuery, connectionString);

            Assert.NotNull(result);
            Assert.IsType<string>(result);
            Assert.Equal(expected, result);
        }

        [Fact]
        public static void GetJsonDataEmptyJsonResult()
        {
            string isTravisCi = Environment.GetEnvironmentVariable("IsTravisCI");
            string expected = "[]";
            var request = new Request(
                "TokenBalances",
                new Collection<string>
                {
                    { "Owner" }, { "Token" }, { "Amount" }
                },
                new Collection<Condition>
                {
                    new Condition { ParamName = "Id", Operator = OperatorName.Equals, ParamValue = "40" }
                });
            string commandQuery = new SqlQueryCreator().CreateCommandQuery(request);
            string connectionString;
            if (Convert.ToBoolean(isTravisCi, new CultureInfo("en-US")))
                connectionString = Environment.GetEnvironmentVariable("TravisCIMicrosoftSqlServerConnection");
            else
                connectionString = LocalConnection.MicrosoftSqlServerConnection;

            string result = new SqlDataReader().GetJsonData(commandQuery, connectionString);

            Assert.NotNull(result);
            Assert.IsType<string>(result);
            Assert.Equal(expected, result);
        }

        [Fact]
        public static void GetJsonDataArrayResult()
        {
            string isTravisCi = Environment.GetEnvironmentVariable("IsTravisCI");
            var request = new Request(
                "TokenBalances",
                new Collection<string>
                {
                    { "Owner" }, { "Token" }, { "Amount" }
                });
            string commandQuery = new SqlQueryCreator().CreateCommandQuery(request);
            string connectionString;
            if (Convert.ToBoolean(isTravisCi, new CultureInfo("en-US")))
                connectionString = Environment.GetEnvironmentVariable("TravisCIMicrosoftSqlServerConnection");
            else
                connectionString = LocalConnection.MicrosoftSqlServerConnection;

            var result = new SqlDataReader().GetJsonData(commandQuery, connectionString);

            bool expected = result.StartsWith("[{") && result.EndsWith("}]");
            Assert.NotNull(result);
            Assert.IsType<string>(result);
            Assert.NotEqual("[]", result);
            Assert.True(expected);
        }
    }
}