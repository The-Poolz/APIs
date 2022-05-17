using Xunit;
using System.Collections.ObjectModel;

namespace QuickSQL.Tests.QueryCreator
{
    /// <summary>
    /// SqlQueryCreator has implemented the BaseQueryCreator tests.
    /// </summary>
    public static class BaseQueryCreatorTests
    {
        [Fact]
        public static void GetCommandQuery()
        {
            string expected = "SELECT Token, Owner, Amount FROM TokenBalances FOR JSON PATH";
            var request = new Request("TokenBalances", "Token, Owner, Amount");

            var result = new SqlQueryCreator().CreateCommandQuery(request);

            Assert.NotNull(result);
            Assert.IsType<string>(result);
            Assert.Equal(expected, result);
        }

        [Fact]
        public static void GetCommandQueryWithCondition()
        {
            string expected = "SELECT Token, Owner, Amount FROM TokenBalances WHERE Id = 1 FOR JSON PATH";
            var request = new Request(
                "TokenBalances",
                "Token, Owner, Amount",
                new Collection<Condition>
                {
                    new Condition { ParamName = "Id", Operator = OperatorName.Equals, ParamValue = "1" }
                });

            var result = new SqlQueryCreator().CreateCommandQuery(request);

            Assert.NotNull(result);
            Assert.IsType<string>(result);
            Assert.Equal(expected, result);
        }

        [Fact]
        public static void GetCommandQueryInvalidRequest()
        {
            var request = new Request("", "Token, Owner, Amount");

            var result = new SqlQueryCreator().CreateCommandQuery(request);

            Assert.Null(result);
        }
    }
}
