using QuickSQL.Helpers;
using Xunit;

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
                SelectedColumns = "Token, Owner, Amount"
            };
            var expected = "[{\"Owner\": \"0x1a01ee5577c9d69c35a77496565b1bc95588b521\", \"Token\": \"ADH\", \"Amount\": \"400\"}, {\"Owner\": \"0x2a01ee5557c9d69c35577496555b1bc95558b552\", \"Token\": \"Poolz\", \"Amount\": \"300\"}, {\"Owner\": \"0x3a31ee5557c9369c35573496555b1bc93553b553\", \"Token\": \"ETH\", \"Amount\": \"200\"}, {\"Owner\": \"0x4a71ee5577c9d79c37577496555b1bc95558b554\", \"Token\": \"BTH\", \"Amount\": \"100\"}]";
            var commandQuery = QueryCreator.CreateCommandQuery(request);
            string connectionString = @$"server=127.0.0.1;user id=root;password=;database=QuickSQL.Test";

            var result = MySqlDataReader.GetJsonData(commandQuery, connectionString);

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            var resultType = Assert.IsType<string>(result);
            string resultJson = Assert.IsAssignableFrom<string>(resultType);
            Assert.Equal(expected, resultJson);
        }
    }
}
