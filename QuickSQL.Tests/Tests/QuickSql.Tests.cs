using Xunit;
using System;

namespace QuickSQL.Tests
{
    public class QuickSqlTests
    {
        [Fact]
        public void InvokeRequest()
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
            if (Convert.ToBoolean(isTravisCi))
                connectionString = Environment.GetEnvironmentVariable("TravisCIConnectionString");
            else
                connectionString = LocalConnection.MySqlConnection;
            MySqlDataReader reader = new MySqlDataReader();

            // Act
            string result = QuickSql.InvokeRequest(request, connectionString, reader);

            // Assert
            Assert.NotNull(result);
            var resultType = Assert.IsType<string>(result);
            var json = Assert.IsAssignableFrom<string>(resultType);
            Assert.NotEqual(string.Empty, json);
            Assert.Equal(expected, json);
        }
    }
}
