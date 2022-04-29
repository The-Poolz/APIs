using QuickSQL.QueryCreators;
using Xunit;

namespace QuickSQL.Tests.QueryCreators
{
    public static class SqlQueryCreatorTests
    {
        [Fact]
        public static void GetCommandQuery()
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
        public static void GetCommandQueryWithCondition()
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
        public static void GetCommandQueryWithInvalidRequest()
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
