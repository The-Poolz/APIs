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
            var queryCreator = new SqlQueryCreator();

            // Act
            var result = queryCreator.CreateCommandQuery(request);

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
            var queryCreator = new SqlQueryCreator();

            // Act
            var result = queryCreator.CreateCommandQuery(request);

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
            var queryCreator = new SqlQueryCreator();

            // Act
            var result = queryCreator.CreateCommandQuery(request);

            Assert.Null(result);
        }

        [Fact]
        public static void GetProviderName()
        {
            SqlQueryCreator reader = new SqlQueryCreator();
            string expected = Providers.MicrosoftSqlServer.ToString();

            // Act
            string result = reader.ProviderName;

            Assert.NotNull(result);
            Assert.IsType<string>(result);
            Assert.Equal(expected, result);
        }
    }
}
