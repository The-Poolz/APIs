using Microsoft.EntityFrameworkCore;
using QuickSQL.Helpers;
using Xunit;

namespace QuickSQL.Tests.Helpers
{
    public class DataReaderTests : TestData
    {
        [Theory, MemberData(nameof(GetTestData))]
        public void GetJsonData(Request requestSettings, string expected)
        {
            var commandQuery = QueryCreator.CreateCommandQuery(requestSettings);
            var context = MockContext.GetTestDataContext();

            var result = DataReader.GetJsonData(commandQuery, context.Database.GetConnectionString());

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            var resultType = Assert.IsType<string>(result);
            string resultJson = Assert.IsAssignableFrom<string>(resultType);
            Assert.Equal(expected, resultJson);
        }
    }
}
