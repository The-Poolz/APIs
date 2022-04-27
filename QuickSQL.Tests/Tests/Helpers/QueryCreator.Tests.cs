using QuickSQL.QueryCreators;
using Xunit;

namespace QuickSQL.Tests.Helpers
{
    public class QueryCreatorTests
    {
        [Fact]
        public void GetCommandQuery()
        {
            // Arrange
            var request = new Request
            {
                SelectedTable = "TokenBalances",
                SelectedColumns = "Token, Owner, Amount"
            };
            string expected = "SELECT JSON_ARRAYAGG(JSON_OBJECT('Token',Token, 'Owner',Owner, 'Amount',Amount)) FROM TokenBalances";

            // Act
            var result = MySqlQueryCreator.CreateCommandQuery(request);

            // Assert
            Assert.NotNull(result);
            var resultType = Assert.IsType<string>(result);
            var json = Assert.IsAssignableFrom<string>(resultType);
            Assert.NotEqual(string.Empty, json);
            Assert.Equal(expected, json);
        }
    }
}
