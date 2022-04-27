using QuickSQL.QueryCreators;
using Xunit;

namespace QuickSQL.Tests.QueryCreators
{
    public class SqlQueryCreatorTests
    {
        [Fact]
        public void GetCommandQuery()
        {
            // Arrange
            var request = new Request
            {
                TableName = "TokenBalances",
                SelectedColumns = "Token, Owner, Amount"
            };
            string expected = "SELECT Token, Owner, Amount FROM TokenBalances FOR JSON PATH";

            // Act
            var result = SqlQueryCreator.CreateCommandQuery(request);

            // Assert
            Assert.NotNull(result);
            var resultType = Assert.IsType<string>(result);
            var json = Assert.IsAssignableFrom<string>(resultType);
            Assert.NotEqual(string.Empty, json);
            Assert.Equal(expected, json);
        }
    }
}
