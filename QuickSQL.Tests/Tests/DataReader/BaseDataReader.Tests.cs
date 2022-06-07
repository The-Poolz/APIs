using Xunit;
using System;
using System.Data;
using System.Globalization;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using QuickSQL.Tests.DataAccess;
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
            string expected = "[{\"Owner\": \"0x1a01ee5577c9d69c35a77496565b1bc95588b521\", \"Token\": \"ADH\", \"Amount\": \"400\"}]";
            var request = new Request(
                "TokenBalances",
                new Collection<string>
                {
                    { "Token" }, { "Owner" }, { "Amount" }
                },
                new Collection<Condition>
                {
                    new Condition { ParamName = "Id", Operator = OperatorName.Equals, ParamValue = "1" }
                });
            string commandQuery = new MySqlQueryCreator().CreateCommandQuery(request);
            string connectionString;
            if (Convert.ToBoolean(isTravisCi, new CultureInfo("en-US")))
                connectionString = Environment.GetEnvironmentVariable("TravisCIMySqlConnection");
            else
                connectionString = LocalConnection.MySqlConnection;

            var result = new MySqlDataReader().GetJsonData(commandQuery, connectionString);

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
                    { "Token" },
                    { "Owner" },
                    { "Amount" }
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
        public static void GetJsonDataWithMock()
        {
            string expected = "{\"Id\":1,\"Token\":\"ADH\",\"Owner\":\"0x1a01ee5577c9d69c35a77496565b1bc95588b521\",\"Amount\":\"400\"}";
            var request = new Request(
                "TokenBalances",
                new Collection<string>
                {
                    { "Token" }, { "Owner" }, { "Amount" }
                },
                new Collection<Condition>
                {
                    new Condition { ParamName = "Id", Operator = OperatorName.Equals, ParamValue = "2" }
                });
            List<TokenBalances> emulated = new List<TokenBalances>()
            {
                new TokenBalances { Id = 1, Amount = "400", Owner = "0x1a01ee5577c9d69c35a77496565b1bc95588b521", Token = "ADH" },
                new TokenBalances { Id = 2, Amount = "300", Owner = "0x2", Token = "Poolz" },
                new TokenBalances { Id = 3, Amount = "200", Owner = "0x3", Token = "None" }
            };
            IDataReader mock = TestMock.MockIDataReader(emulated.ToArray(), request);

            string result = new SqlDataReader().GetJsonData(null, null, mock);

            Assert.NotNull(result);
            Assert.IsType<string>(result);
            Assert.Equal(expected, result);
        }
    }
}