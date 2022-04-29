using QuickSQL.QueryCreators;
using Xunit;

namespace QuickSQL.Tests.QueryCreators
{
    public class SqlQueryCreatorTests
    {
        [Fact]
        public void GetCommandQuery()
        {
            var request = new Request
            {
                TableName = "TokenBalances",
                SelectedColumns = "Token, Owner, Amount"
            };
            string expected = "SELECT Token, Owner, Amount FROM TokenBalances FOR JSON PATH";

            // Act
            var result = SqlQueryCreator.CreateCommandQuery(request);

            Assert.NotNull(result);
            Assert.IsType<string>(result);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetCommandQueryWithCondition()
        {
            var request = new Request
            {
                TableName = "TokenBalances",
                SelectedColumns = "Token, Owner, Amount",
                WhereCondition = "Id = 1"
            };
            string expected = "SELECT Token, Owner, Amount FROM TokenBalances WHERE Id = 1 FOR JSON PATH";

            // Act
            var result = SqlQueryCreator.CreateCommandQuery(request);

            Assert.NotNull(result);
            Assert.IsType<string>(result);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetCommandQueryWithInvalidRequest()
        {
            var request = new Request
            {
                SelectedColumns = "Token, Owner, Amount"
            };

            // Act
            var result = SqlQueryCreator.CreateCommandQuery(request);

            Assert.Null(result);
        }
    }
}
