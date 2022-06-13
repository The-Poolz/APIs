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
            string expected = "SELECT Token, Owner, Amount FROM TokenBalances FOR JSON AUTO, WITHOUT_ARRAY_WRAPPER";
            var request = new Request("TokenBalances",
                new Collection<string>
                {
                    { "Token" }, { "Owner" }, { "Amount" }
                });

            var result = new SqlQueryCreator().CreateCommandQuery(request);

            Assert.NotNull(result);
            Assert.IsType<string>(result);
            Assert.Equal(expected, result);
        }

        [Fact]
        public static void GetCommandQueryWithCondition()
        {
            string expected = "SELECT Token, Owner, Amount FROM TokenBalances WHERE Id = 1 FOR JSON AUTO, WITHOUT_ARRAY_WRAPPER";
            var request = new Request(
                "TokenBalances",
                new Collection<string> { { "Token" }, { "Owner" }, { "Amount" } },
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
        public static void GetCommandQueryWithOrderRules()
        {
            string expected = "SELECT Token, Owner, Amount FROM TokenBalances ORDER BY Id ASC, Amount DESC FOR JSON AUTO, WITHOUT_ARRAY_WRAPPER";
            var request = new Request(
                "TokenBalances",
                new Collection<string> { { "Token" }, { "Owner" }, { "Amount" } },
                new Collection<OrderRule>
                {
                    new OrderRule("Id"),
                    new OrderRule("Amount", SortBy.DESC)
                });

            var result = new SqlQueryCreator().CreateCommandQuery(request);

            Assert.NotNull(result);
            Assert.IsType<string>(result);
            Assert.Equal(expected, result);
        }

        [Fact]
        public static void GetCommandQueryInvalidRequest()
        {
            var request = new Request("",
                new Collection<string>
                {
                    { "Token" }, { "Owner" }, { "Amount" }
                });

            var result = new SqlQueryCreator().CreateCommandQuery(request);

            Assert.Null(result);
        }
    }
}
