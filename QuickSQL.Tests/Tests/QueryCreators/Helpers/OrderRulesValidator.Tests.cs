using Xunit;
using System.Collections.ObjectModel;

using QuickSQL.QueryCreator.Helpers;

namespace QuickSQL.Tests.QueryCreators.Helpers
{
    public static class OrderRulesValidatorTests
    {
        [Fact]
        public static void IsValidOrderRulesDefault()
        {
            var conditions = new Collection<OrderRule>
            {
                new OrderRule("Id"),
                new OrderRule("Amount", SortBy.DESC)
            };

            var result = OrderRulesValidator.IsValidOrderRules(conditions);

            Assert.True(result);
        }

        [Fact]
        public static void IsValidOrderRulesEmptyParams()
        {
            var conditions = new Collection<OrderRule> { };

            var result = OrderRulesValidator.IsValidOrderRules(conditions);

            Assert.True(result);
        }

        [Fact]
        public static void IsValidOrderRulesWithoutParam()
        {
            var request = new Request(
                "TokenBalances",
                new Collection<string> { { "Token" }, { "Owner" }, { "Amount" } },
                new Collection<Condition>
                {
                    new Condition { ParamName = "Id", Operator = OperatorName.Equals, ParamValue = "1" }
                });

            var result = OrderRulesValidator.IsValidOrderRules(request.OrderRules);

            Assert.True(result);
        }

        [Fact]
        public static void NotNullColumnNameDefault()
        {
            var orderRule = new OrderRule("Amount", SortBy.DESC);

            var result = OrderRulesValidator.NotNullColumnName(orderRule.ColumnName);

            Assert.True(result);
        }

        [Fact]
        public static void NotNullColumnNameInvalid()
        {
            var orderRule = new OrderRule("  ", SortBy.DESC);

            var result = OrderRulesValidator.NotNullColumnName(orderRule.ColumnName);

            Assert.False(result);
        }

        [Fact]
        public static void IsValidSortDefault()
        {
            var orderRule = new OrderRule("Amount", SortBy.DESC);

            var result = OrderRulesValidator.IsValidSort(orderRule.Sort.ToString());

            Assert.True(result);
        }

        [Fact]
        public static void IsValidSortDefaultValue()
        {
            var orderRule = new OrderRule("Amount");

            var result = OrderRulesValidator.IsValidSort(orderRule.Sort.ToString());

            Assert.True(result);
        }

        [Fact]
        public static void IsValidSortInvalid()
        {
            var orderRule = new OrderRule("Amount", (SortBy)88);

            var result = OrderRulesValidator.IsValidSort(orderRule.Sort.ToString());

            Assert.False(result);
        }
    }
}
