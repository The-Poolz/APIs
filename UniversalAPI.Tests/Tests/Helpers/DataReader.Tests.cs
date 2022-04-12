using Interfaces;
using Newtonsoft.Json;
using System.Collections.Generic;
using UniversalAPI.Helpers;
using Xunit;

namespace UniversalAPI.Tests.Helpers
{
    public class DataReaderTests : TestData
    {
        [Theory, MemberData(nameof(GetTestData))]
        public void GetJsonData(Dictionary<string, dynamic> data, APIRequest requestSettings, string expected)
        {
            var jsonString = JsonConvert.SerializeObject(data);
            var commandQuery = QueryCreator.CreateCommandQuery(requestSettings);
            var context = MockContext.GetTestDataContext();

            var result = DataReader.GetJsonData(commandQuery, context);

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            var resultType = Assert.IsType<string>(result);
            string resultJson = Assert.IsAssignableFrom<string>(resultType);
            Assert.Equal(expected, resultJson);
        }
    }
}
