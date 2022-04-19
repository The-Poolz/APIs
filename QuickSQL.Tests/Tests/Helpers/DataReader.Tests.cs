using Microsoft.EntityFrameworkCore;
using QuickSQL.Helpers;
using Xunit;
using System;

namespace QuickSQL.Tests.Helpers
{
    public class DataReaderTests : TestData
    {
        [Fact]
        public void GetJsonData()
        {
            var request = new Request
            {
                SelectedTables = "SignUp, LeaderBoard",
                SelectedColumns = "SignUp.PoolId, LeaderBoard.Rank, LeaderBoard.Owner, LeaderBoard.Amount",
                WhereCondition = "SignUp.Id = 3, SignUp.Address = '0x3a31ee5557c9369c35573496555b1bc93553b553'",
                JoinCondition = "SignUp.Address = LeaderBoard.Owner"
            };
            var expected = "[{\"PoolId\":3,\"Rank\":\"3\",\"Owner\":\"0x3a31ee5557c9369c35573496555b1bc93553b553\",\"Amount\":\"250.02109769151781894\"}]";
            var commandQuery = QueryCreator.CreateCommandQuery(request);
            var context = MockContext.GetTestDataContext();
            Console.WriteLine($"Test. Connection: {Environment.GetEnvironmentVariable("DATABASE_URL")}");
            context.Database.SetConnectionString(Environment.GetEnvironmentVariable("DATABASE_URL"));
            context.Database.OpenConnection();

            var result = DataReader.GetJsonData(commandQuery, context.Database.GetConnectionString());

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            var resultType = Assert.IsType<string>(result);
            string resultJson = Assert.IsAssignableFrom<string>(resultType);
            Assert.Equal(expected, resultJson);
            context.Database.CloseConnection();
        }
    }
}
