using Interfaces;
using System.Collections.Generic;
using Xunit;
using Newtonsoft.Json;

namespace UniversalAPI.Tests.Helpers
{
    public class APIClientTests : TestData
    {
        [Theory, MemberData(nameof(GetTestData))]
        public void InvokeRequest(Dictionary<string, dynamic> data, APIRequestSettings requestSettings, string expected)
        {
            // Arrange
            var jsonString = JsonConvert.SerializeObject(data);

            // Act
            var result = APIClient.InvokeRequest(jsonString, requestSettings, ConnectionString.ConnectionToData);

            // Assert
            Assert.NotNull(result);
            var resultType = Assert.IsType<string>(result);
            var json = Assert.IsAssignableFrom<string>(resultType);
            Assert.NotEqual(string.Empty, json);
            Assert.Equal(expected, json);
        }
    }
}
