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
            // Arrange
            var request = new Request
            {
                TableName = "TokenBalances",
                SelectedColumns = "Token, Owner, Amount",
                WhereConditions = new List<Condition>
                {
                    new Condition { ParamName = "Id", Operator = OperatorNames.Equals, ParamValue = "1" }
                }
            };

            // Act
            var result = RequestValidator.IsValidRequest(request);

            // Assert
            Assert.IsType<bool>(result);
            Assert.True(result);
        }

        [Fact]
        public static void IsValidRequestWithoutWhereCondition()
        {
            // Arrange
            var request = new Request
            {
                TableName = "TokenBalances",
                SelectedColumns = "Token, Owner, Amount"
            };

            // Act
            var result = RequestValidator.IsValidRequest(request);

            // Assert
            Assert.IsType<bool>(result);
            Assert.True(result);
        }

        [Fact]
        public static void IsValidRequestWithoutSelectedColumns()
        {
            // Arrange
            var request = new Request
            {
                TableName = "TokenBalances",
                WhereConditions = new List<Condition>
                {
                    new Condition { ParamName = "Id", Operator = OperatorNames.Equals, ParamValue = "1" }
                }
            };

            // Act
            var result = RequestValidator.IsValidRequest(request);

            // Assert
            Assert.IsType<bool>(result);
            Assert.False(result);
        }

        [Fact]
        public static void IsValidRequestWithoutTableName()
        {
            // Arrange
            var request = new Request
            {
                SelectedColumns = "Token, Owner, Amount",
                WhereConditions = new List<Condition>
                {
                    new Condition { ParamName = "Id", Operator = OperatorNames.Equals, ParamValue = "1" }
                }
            };

            // Act
            var result = RequestValidator.IsValidRequest(request);

            // Assert
            Assert.IsType<bool>(result);
            Assert.False(result);
        }

        [Fact]
        public static void NotNullTableNameDetault()
        {
            // Arrange
            var request = new Request
            {
                TableName = "TokenBalances",
                SelectedColumns = "Token, Owner, Amount",
                WhereConditions = new List<Condition>
                {
                    new Condition { ParamName = "Id", Operator = OperatorNames.Equals, ParamValue = "1" }
                }
            };

            // Act
            var result = RequestValidator.NotNullTableName(request);

            // Assert
            Assert.IsType<bool>(result);
            Assert.True(result);
        }

        [Fact]
        public static void NotNullTableNameWithoutTableName()
        {
            // Arrange
            var request = new Request
            {
                SelectedColumns = "Token, Owner, Amount",
                WhereConditions = new List<Condition>
                {
                    new Condition { ParamName = "Id", Operator = OperatorNames.Equals, ParamValue = "1" }
                }
            };

            // Act
            var result = RequestValidator.NotNullTableName(request);

            // Assert
            Assert.IsType<bool>(result);
            Assert.False(result);
        }

        [Fact]
        public static void NotNullSelectedColumnsDetault()
        {
            // Arrange
            var request = new Request
            {
                TableName = "TokenBalances",
                SelectedColumns = "Token, Owner, Amount",
                WhereConditions = new List<Condition>
                {
                    new Condition { ParamName = "Id", Operator = OperatorNames.Equals, ParamValue = "1" }
                }
            };

            // Act
            var result = RequestValidator.NotNullSelectedColumns(request);

            // Assert
            Assert.IsType<bool>(result);
            Assert.True(result);
        }

        [Fact]
        public static void NotNullSelectedColumnsWithoutSelectedColumns()
        {
            // Arrange
            var request = new Request
            {
                TableName = "TokenBalances",
                WhereConditions = new List<Condition>
                {
                    new Condition { ParamName = "Id", Operator = OperatorNames.Equals, ParamValue = "1" }
                }
            };

            // Act
            var result = RequestValidator.NotNullSelectedColumns(request);

            // Assert
            Assert.IsType<bool>(result);
            Assert.False(result);
        }
    }
}
