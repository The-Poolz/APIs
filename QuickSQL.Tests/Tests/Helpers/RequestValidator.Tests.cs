using QuickSQL.Helpers;
using Xunit;

namespace QuickSQL.Tests.Helpers
{
    public class RequestValidatorTests
    {
        [Fact]
        public void IsValidRequestDefault()
        {
            // Arrange
            var request = new Request
            {
                TableName = "TokenBalances",
                SelectedColumns = "Token, Owner, Amount",
                WhereCondition = "Id = 1"
            };

            // Act
            var result = RequestValidator.IsValidRequest(request);

            // Assert
            Assert.IsType<bool>(result);
            Assert.True(result);
        }

        [Fact]
        public void IsValidRequestWithoutWhereCondition()
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
        public void IsValidRequestWithoutSelectedColumns()
        {
            // Arrange
            var request = new Request
            {
                TableName = "TokenBalances",
                WhereCondition = "Id = 1"
            };

            // Act
            var result = RequestValidator.IsValidRequest(request);

            // Assert
            Assert.IsType<bool>(result);
            Assert.False(result);
        }

        [Fact]
        public void IsValidRequestWithoutTableName()
        {
            // Arrange
            var request = new Request
            {
                SelectedColumns = "Token, Owner, Amount",
                WhereCondition = "Id = 1"
            };

            // Act
            var result = RequestValidator.IsValidRequest(request);

            // Assert
            Assert.IsType<bool>(result);
            Assert.False(result);
        }

        [Fact]
        public void NotNullTableNameDetault()
        {
            // Arrange
            var request = new Request
            {
                TableName = "TokenBalances",
                SelectedColumns = "Token, Owner, Amount",
                WhereCondition = "Id = 1"
            };

            // Act
            var result = RequestValidator.NotNullTableName(request);

            // Assert
            Assert.IsType<bool>(result);
            Assert.True(result);
        }

        [Fact]
        public void NotNullTableNameWithoutTableName()
        {
            // Arrange
            var request = new Request
            {
                SelectedColumns = "Token, Owner, Amount",
                WhereCondition = "Id = 1"
            };

            // Act
            var result = RequestValidator.NotNullTableName(request);

            // Assert
            Assert.IsType<bool>(result);
            Assert.False(result);
        }

        [Fact]
        public void NotNullSelectedColumnsDetault()
        {
            // Arrange
            var request = new Request
            {
                TableName = "TokenBalances",
                SelectedColumns = "Token, Owner, Amount",
                WhereCondition = "Id = 1"
            };

            // Act
            var result = RequestValidator.NotNullSelectedColumns(request);

            // Assert
            Assert.IsType<bool>(result);
            Assert.True(result);
        }

        [Fact]
        public void NotNullSelectedColumnsWithoutSelectedColumns()
        {
            // Arrange
            var request = new Request
            {
                TableName = "TokenBalances",
                WhereCondition = "Id = 1"
            };

            // Act
            var result = RequestValidator.NotNullSelectedColumns(request);

            // Assert
            Assert.IsType<bool>(result);
            Assert.False(result);
        }
    }
}
