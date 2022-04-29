using QuickSQL.QueryCreators;
using Xunit;

namespace QuickSQL.Tests.QueryCreators
{
    public static class MySqlQueryCreatorTests
    {
        [Fact]
        public static void GetCommandQuery()
        {
            var request = new Request
            {
                TableName = "TokenBalances",
                SelectedColumns = "Token, Owner, Amount"
            };
            string expected = "SELECT JSON_ARRAYAGG(JSON_OBJECT('Token',Token, 'Owner',Owner, 'Amount',Amount)) FROM TokenBalances";

            // Act
            var result = MySqlQueryCreator.CreateCommandQuery(request);

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
            string expected = "SELECT JSON_ARRAYAGG(JSON_OBJECT('Token',Token, 'Owner',Owner, 'Amount',Amount)) FROM TokenBalances WHERE Id = 1";

            // Act
            var result = MySqlQueryCreator.CreateCommandQuery(request);

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
            var result = MySqlQueryCreator.CreateCommandQuery(request);

            Assert.Null(result);
        }
    }
}
