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
    /// All tests with databese use the Microsoft Sql Server provider.
    /// </remarks>
    public static class BaseDataReaderTests
    {
        [Fact]
        public static void GetJsonData()
        {
            string isTravisCi = Environment.GetEnvironmentVariable("IsTravisCI");
            string expected = "[{\"Token\":\"ADH\",\"Owner\":\"0x1a01ee5577c9d69c35a77496565b1bc95588b521\",\"Amount\":\"400\"}]";
            var request = new Request(
                "TokenBalances",
                "Token, Owner, Amount",
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
                "Token, Owner, Amount",
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
    }
}