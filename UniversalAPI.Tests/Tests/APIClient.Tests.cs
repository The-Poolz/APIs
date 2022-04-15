using Interfaces;
using System.Collections.Generic;
using Xunit;
using Newtonsoft.Json;

namespace UniversalAPI.Tests
{
    public class APIClientTests : TestData
    {
        [Theory, MemberData(nameof(GetTestData))]
        public void InvokeRequest(APIRequest requestSettings, string expected)
        {
            // Arrange
            var dataContext = MockContext.GetTestDataContext();

            // Act
            var result = APIClient.InvokeRequest(requestSettings, dataContext);

            // Assert
            Assert.NotNull(result);
            var resultType = Assert.IsType<string>(result);
            var json = Assert.IsAssignableFrom<string>(resultType);
            Assert.NotEqual(string.Empty, json);
            Assert.Equal(expected, json);
        }
    }
}
