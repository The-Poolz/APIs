using Xunit;
using System.Collections.Generic;

using QuickSQL.QueryCreator.Helpers;

namespace QuickSQL.Tests.QueryCreators.Helpers
{
    public static class RequestValidatorTests
    {
        [Fact]
        public static void IsValidRequestDefault()
        {
            var request = new Request
            {
                TableName = "TokenBalances",
                SelectedColumns = "Token, Owner, Amount",
                WhereConditions = new List<Condition>
                {
                    new Condition { ParamName = "Id", Operator = OperatorNames.Equals, ParamValue = "1" }
                }
            };

            var result = RequestValidator.IsValidRequest(request);

            Assert.IsType<bool>(result);
            Assert.True(result);
        }

        [Fact]
        public static void IsValidRequestWithoutWhereCondition()
        {
            var request = new Request
            {
                TableName = "TokenBalances",
                SelectedColumns = "Token, Owner, Amount"
            };

            var result = RequestValidator.IsValidRequest(request);

            Assert.IsType<bool>(result);
            Assert.True(result);
        }

        [Fact]
        public static void IsValidRequestWithoutSelectedColumns()
        {
            var request = new Request
            {
                TableName = "TokenBalances",
                WhereConditions = new List<Condition>
                {
                    new Condition { ParamName = "Id", Operator = OperatorNames.Equals, ParamValue = "1" }
                }
            };

            var result = RequestValidator.IsValidRequest(request);

            Assert.IsType<bool>(result);
            Assert.False(result);
        }

        [Fact]
        public static void IsValidRequestWithoutTableName()
        {
            var request = new Request
            {
                SelectedColumns = "Token, Owner, Amount",
                WhereConditions = new List<Condition>
                {
                    new Condition { ParamName = "Id", Operator = OperatorNames.Equals, ParamValue = "1" }
                }
            };

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
            var selectedColumns = "Token, Owner, Amount";

            var result = RequestValidator.NotNullSelectedColumns(selectedColumns);

            Assert.IsType<bool>(result);
            Assert.True(result);
        }

        [Fact]
        public static void NotNullSelectedColumnsEmptyParam()
        {
            var selectedColumns = "      ";

            var result = RequestValidator.NotNullSelectedColumns(selectedColumns);

            Assert.IsType<bool>(result);
            Assert.False(result);
        }
    }
}
