using Xunit;

namespace QuickSQL.Tests
{
    public class QuickSqlTests : TestData
    {
        [Theory, MemberData(nameof(GetTestData))]
        public void InvokeRequest(Request requestSettings, string expected)
        {
            // Arrange
            var dataContext = MockContext.GetTestDataContext();

            // Act
            var result = QuickSql.InvokeRequest(requestSettings, dataContext);

            // Assert
            Assert.NotNull(result);
            var resultType = Assert.IsType<string>(result);
            var json = Assert.IsAssignableFrom<string>(resultType);
            Assert.NotEqual(string.Empty, json);
            Assert.Equal(expected, json);
        }
    }
}
