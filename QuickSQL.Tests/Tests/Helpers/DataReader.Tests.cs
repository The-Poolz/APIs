using Microsoft.EntityFrameworkCore;
using QuickSQL.Helpers;
using System;
using Xunit;

namespace QuickSQL.Tests.Helpers
{
    public class DataReaderTests
    {
        [Fact]
        public void GetJsonData()
        {
            //string connectionString = @"server=127.0.0.1;user id=root;password=;database=QuickSQL.Test";
            //string connectionString = @"Server=127.0.0.1;Uid=test;Pwd=test;Database=QuickSQL.Test";
            //var contextOptions = new DbContextOptionsBuilder<DbContext>()
            //    .UseSqlServer(connectionString).Options;
            //var context = new DbContext(contextOptions);

            var request = new Request
            {
                SelectedTables = "SignUp, LeaderBoard",
                SelectedColumns = "SignUp.PoolId, LeaderBoard.Rank, LeaderBoard.Owner, LeaderBoard.Amount",
                WhereCondition = "SignUp.Id = 3, SignUp.Address = '0x3a31ee5557c9369c35573496555b1bc93553b553'",
                JoinCondition = "SignUp.Address = LeaderBoard.Owner"
            };
            var expected = "[{\"PoolId\":3,\"Rank\":\"3\",\"Owner\":\"0x3a31ee5557c9369c35573496555b1bc93553b553\",\"Amount\":\"250.02109769151781894\"}]";
            var commandQuery = QueryCreator.CreateCommandQuery(request);

            string connectionString = $"{Environment.GetEnvironmentVariable("DATABASE_URL")};user id=root;password=;database=QuickSQL.Test;";

            var result = DataReader.GetJsonData(commandQuery, connectionString);

            Assert.NotNull(result);
            Assert.NotEmpty(result);
            var resultType = Assert.IsType<string>(result);
            string resultJson = Assert.IsAssignableFrom<string>(resultType);
            Assert.Equal(expected, resultJson);
        }
    }
}
