using Xunit;
using System;
using System.Globalization;
using QuickSQL.DataReader;

namespace QuickSQL.Tests
{
    public static class QuickSqlTests
    {
        [Fact]
        public static void InvokeRequest()
        {
            var request = new Request
            {
                TableName = "TokenBalances",
                SelectedColumns = "Token, Owner, Amount",
                WhereCondition = "Id = 1"
            };
            string expected = "[{\"Owner\": \"0x1a01ee5577c9d69c35a77496565b1bc95588b521\", \"Token\": \"ADH\", \"Amount\": \"400\"}]";
            string isTravisCi = Environment.GetEnvironmentVariable("IsTravisCI");
            string connectionString;
            if (Convert.ToBoolean(isTravisCi, new CultureInfo("en-US")))
                connectionString = Environment.GetEnvironmentVariable("TravisCIMySqlConnection");
            else
                connectionString = LocalConnection.MySqlConnection;
            MySqlDataReader reader = new MySqlDataReader();

            // Act
            string result = QuickSql.InvokeRequest(request, connectionString, reader);

            Assert.NotNull(result);
            Assert.IsType<string>(result);
            Assert.Equal(expected, result);
        }

        [Fact]
        public static void InvokeRequestMicrosoftSqlServerProvider()
        {
            var request = new Request
            {
                TableName = "TokenBalances",
                SelectedColumns = "Token, Owner, Amount",
                WhereCondition = "Id = 1"
            };
            string expected;
            string isTravisCi = Environment.GetEnvironmentVariable("IsTravisCI");
            string connectionString;
            BaseDataReader reader;
            string result;

            // Act
            if (Convert.ToBoolean(isTravisCi, new CultureInfo("en-US")))
            {
                expected = "[{\"Token\":\"ADH\",\"Owner\":\"0x1a01ee5577c9d69c35a77496565b1bc95588b521\",\"Amount\":\"400\"}]";
                reader = new MicrosoftSqlServerDataReader();
                connectionString = Environment.GetEnvironmentVariable("TravisCIMicrosoftSqlServerConnection");
                result = QuickSql.InvokeRequest(request, connectionString, reader);
            }
            else
            {
                expected = "[{\"Token\":\"ADH\",\"Owner\":\"0x1a01ee5577c9d69c35a77496565b1bc95588b521\",\"Amount\":\"400\"}]";
                reader = new MicrosoftSqlServerDataReader();
                connectionString = LocalConnection.MicrosoftSqlServerConnection;
                result = QuickSql.InvokeRequest(request, connectionString, reader);
            }

            Assert.NotNull(result);
            Assert.IsType<string>(result);
            Assert.Equal(expected, result);
        }
    }
}
