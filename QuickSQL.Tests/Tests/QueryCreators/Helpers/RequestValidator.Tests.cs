using Xunit;
using System.Collections.ObjectModel;

using QuickSQL.QueryCreator.Helpers;

namespace QuickSQL.Tests.QueryCreators.Helpers
{
    public static class RequestValidatorTests
    {
        [Fact]
        public static void IsValidRequestDefault()
        {
            var request = new Request(
                "TokenBalances",
                new Collection<string> { { "Token" }, { "Owner" }, { "Amount" } },
                new Collection<Condition>
                {
                    new Condition { ParamName = "Id", Operator = OperatorName.Equals, ParamValue = "1" }
                });

            var result = RequestValidator.IsValidRequest(request);

            Assert.IsType<bool>(result);
            Assert.True(result);
        }

        [Fact]
        public static void IsValidRequestWithoutWhereCondition()
        {
            var request = new Request("TokenBalances",
                new Collection<string>
                {
                    { "Token" }, { "Owner" }, { "Amount" }
                });

            var result = RequestValidator.IsValidRequest(request);

            Assert.IsType<bool>(result);
            Assert.True(result);
        }

        [Fact]
        public static void IsValidRequestWithoutSelectedColumns()
        {
            var request = new Request(
                "TokenBalances",
                new Collection<string> { },
                new Collection<Condition>
                {
                    new Condition { ParamName = "Id", Operator = OperatorName.Equals, ParamValue = "1" }
                });

            var result = RequestValidator.IsValidRequest(request);

            Assert.IsType<bool>(result);
            Assert.False(result);
        }

        [Fact]
        public static void IsValidRequestWithoutOrderBy()
        {
            var request = new Request(
                "TokenBalances",
                new Collection<string> { { "Token" }, { "Owner" }, { "Amount" } },
                new Collection<Condition>
                {
                    new Condition { ParamName = "Id", Operator = OperatorName.Equals, ParamValue = "1" }
                });

            var result = RequestValidator.IsValidRequest(request);

            Assert.IsType<bool>(result);
            Assert.True(result);
        }

        [Fact]
        public static void IsValidRequestOrderBy()
        {
            var request = new Request(
                "TokenBalances",
                new Collection<string> { { "Token" }, { "Owner" }, { "Amount" } },
                new Collection<Condition>
                {
                    new Condition { ParamName = "Id", Operator = OperatorName.Equals, ParamValue = "1" }
                },
                new Collection<OrderRule>
                {
                    new OrderRule("Id"),
                    new OrderRule("Amount", SortBy.DESC)
                });

            var result = RequestValidator.IsValidRequest(request);

            Assert.IsType<bool>(result);
            Assert.True(result);
        }

        [Fact]
        public static void IsValidRequestWithoutTableName()
        {
            var request = new Request(
                "    ",
                new Collection<string> { { "Token" }, { "Owner" }, { "Amount" } },
                new Collection<Condition>
                {
                    new Condition { ParamName = "Id", Operator = OperatorName.Equals, ParamValue = "1" }
                });

            var result = RequestValidator.IsValidRequest(request);

            Assert.IsType<bool>(result);
            Assert.False(result);
        }

        [Fact]
        public static void NotNullTableNameDetault()
        {
            var tableName = "TokenBalances";

            var result = RequestValidator.NotNullTableName(tableName);

            Assert.IsType<bool>(result);
            Assert.True(result);
        }

        [Fact]
        public static void NotNullTableNameEmptyParam()
        {
            var tableName = "    ";

            var result = RequestValidator.NotNullTableName(tableName);

            Assert.IsType<bool>(result);
            Assert.False(result);
        }

        [Fact]
        public static void NotNullSelectedColumnsDetault()
        {
            var selectedColumns = new Collection<string>
            {
                { "Token" }, { "Owner" }, { "Amount" }
            };

            var result = RequestValidator.NotNullSelectedColumns(selectedColumns);

            Assert.IsType<bool>(result);
            Assert.True(result);
        }

        [Fact]
        public static void NotNullSelectedColumnsEmptyParam()
        {
            var selectedColumns = new Collection<string> { };

            var result = RequestValidator.NotNullSelectedColumns(selectedColumns);

            Assert.IsType<bool>(result);
            Assert.False(result);
        }
    }
}
