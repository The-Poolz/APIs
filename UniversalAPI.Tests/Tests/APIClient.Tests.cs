using Interfaces;
using System.Collections.Generic;
using Xunit;
using Newtonsoft.Json;

namespace UniversalAPI.Tests.Helpers
{
    public class APIClientTests : TestData
    {
        [Theory, MemberData(nameof(GetTestData))]
        public void InvokeRequest(Dictionary<string, dynamic> data, APIRequest requestSettings, string expected)
        {
            // Arrange
            var jsonString = JsonConvert.SerializeObject(data);

            // Act
            var result = APIClient.InvokeRequest(requestSettings, ConnectionString.ConnectionToData);

            // Assert
            Assert.NotNull(result);
            var resultType = Assert.IsType<string>(result);
            var json = Assert.IsAssignableFrom<string>(resultType);
            Assert.NotEqual(string.Empty, json);
            Assert.Equal(expected, json);
        }

        [Fact]
        public void RunInvokeRequestWithoutConnectionString()
        {
            var requestSettings = new APIRequest
            {
                SelectedTables = "SignUp, LeaderBoard",
                SelectedColumns = "SignUp.PoolId, LeaderBoard.Rank, LeaderBoard.Owner, LeaderBoard.Amount",
                WhereCondition = "SignUp.Id = 3",
            };
            // Act
            var result = APIClient.InvokeRequest(requestSettings, "");

            Assert.Null(result);
        }
    }
}
