using Xunit;
using System;
using System.Globalization;
using System.Collections.ObjectModel;

using QuickSQL.Tests.QueryCreator;

namespace QuickSQL.Tests.DataReader
{
    /// <summary>
    /// SqlDataReader has implemented the BaseDataReader tests.
    /// </summary>
    /// <remarks>
    /// All tests with databese use the MySql provider.
    /// </remarks>
    public static class BaseDataReaderTests
    {
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
            string commandQuery;
            string connectionString;
            string result;
            if (Convert.ToBoolean(isTravisCi, new CultureInfo("en-US")))
            {
                commandQuery = new MySqlQueryCreator().CreateCommandQuery(request);
                connectionString = Environment.GetEnvironmentVariable("TravisCIMySqlConnection");
                result = new MySqlDataReader().GetJsonData(commandQuery, connectionString);
            }
            else
            {
                commandQuery = new SqlQueryCreator().CreateCommandQuery(request);
                connectionString = LocalConnection.MicrosoftSqlServerConnection;
                result = new SqlDataReader().GetJsonData(commandQuery, connectionString);
            }

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
            string commandQuery;
            string connectionString;
            string result;
            if (Convert.ToBoolean(isTravisCi, new CultureInfo("en-US")))
            {
                commandQuery = new MySqlQueryCreator().CreateCommandQuery(request);
                connectionString = Environment.GetEnvironmentVariable("TravisCIMySqlConnection");
                result = new MySqlDataReader().GetJsonData(commandQuery, connectionString);
            }
            else
            {
                commandQuery = new SqlQueryCreator().CreateCommandQuery(request);
                connectionString = LocalConnection.MicrosoftSqlServerConnection;
                result = new SqlDataReader().GetJsonData(commandQuery, connectionString);
            }

            bool expected = result.StartsWith("[{") && result.EndsWith("}]");
            Assert.NotNull(result);
            Assert.IsType<string>(result);
            Assert.NotEqual("[]", result);
            Assert.True(expected);
        }
    }
}