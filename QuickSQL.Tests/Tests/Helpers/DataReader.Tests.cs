using QuickSQL.QueryCreators;
using QuickSQL.DataReaders;
using Xunit;
using System;

namespace QuickSQL.Tests.Helpers
{
    public class DataReaderTests
    {
        [Fact]
        public void GetJsonData()
        {
            var request = new Request
            {
                SelectedTable = "TokenBalances",
                SelectedColumns = "Token, Owner, Amount",
                WhereCondition = "Id = 1"
            };
            string expected = "[{\"Owner\": \"0x1a01ee5577c9d69c35a77496565b1bc95588b521\", \"Token\": \"ADH\", \"Amount\": \"400\"}]";
            string commandQuery = MySqlQueryCreator.CreateCommandQuery(request);
            string isTravisCi = Environment.GetEnvironmentVariable("IsTravisCI");
            string result;
            Console.WriteLine($"DataReaderTests IsTravisCI - {isTravisCi}");

            if (Convert.ToBoolean(isTravisCi))
            {
                string connectionString = Environment.GetEnvironmentVariable("TravisCIConnectionString");
                result = MySqlDataReader.GetJsonData(commandQuery, connectionString);
            }
            else
            {
                string connectionString = LocalConnection.ConnectionString;
                result = MySqlDataReader.GetJsonData(commandQuery, connectionString);
            }

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            var resultType = Assert.IsType<string>(result);
            string resultJson = Assert.IsAssignableFrom<string>(resultType);
            Assert.Equal(expected, resultJson);
        }
    }
}
