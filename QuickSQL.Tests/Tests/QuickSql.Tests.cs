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
                SelectedTable = "TokenBalances",
                SelectedColumns = "Token, Owner, Amount",
                WhereCondition = "Id = 1"
            };
            string expected = "[{\"Owner\": \"0x1a01ee5577c9d69c35a77496565b1bc95588b521\", \"Token\": \"ADH\", \"Amount\": \"400\"}]";
            string isTravisCi = Environment.GetEnvironmentVariable("IsTravisCI");
            string result;

            // Act
            if (Convert.ToBoolean(isTravisCi))
            {
                string connectionString = Environment.GetEnvironmentVariable("TravisCIConnectionString");
                result = QuickSql.InvokeRequest(request, connectionString, Providers.MySql);
            }
            else
            {
                string connectionString = LocalConnection.ConnectionString;
                result = QuickSql.InvokeRequest(request, connectionString, Providers.MySql);
            }

            // Assert
            Assert.NotNull(result);
            var resultType = Assert.IsType<string>(result);
            var json = Assert.IsAssignableFrom<string>(resultType);
            Assert.NotEqual(string.Empty, json);
            Assert.Equal(expected, json);
        }
    }
}
