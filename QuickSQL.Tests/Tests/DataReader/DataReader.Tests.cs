using QuickSQL.QueryCreators;
using Xunit;
using System;

namespace QuickSQL.Tests.DataReader
{
    public class DataReaderTests
    {
        [Fact]
        public void GetJsonData()
        {
            var request = new Request
            {
                TableName = "TokenBalances",
                SelectedColumns = "Token, Owner, Amount",
                WhereCondition = "Id = 1"
            };
            string expected = "[{\"Owner\": \"0x1a01ee5577c9d69c35a77496565b1bc95588b521\", \"Token\": \"ADH\", \"Amount\": \"400\"}]";
            string commandQuery = MySqlQueryCreator.CreateCommandQuery(request);
            string isTravisCi = Environment.GetEnvironmentVariable("IsTravisCI");
            string connectionString;
            if (Convert.ToBoolean(isTravisCi))
                connectionString = Environment.GetEnvironmentVariable("TravisCIConnectionString");
            else
                connectionString = LocalConnection.ConnectionString;
            MySqlDataReader reader = new MySqlDataReader();

            // Act
            string result = reader.GetJsonData(commandQuery, connectionString);

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            var resultType = Assert.IsType<string>(result);
            string resultJson = Assert.IsAssignableFrom<string>(resultType);
            Assert.Equal(expected, resultJson);
        }
    }
}
