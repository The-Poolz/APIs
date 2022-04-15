using QuickSQL.Helpers;
using Xunit;

namespace QuickSQL.Tests.Helpers
{
    public class QueryCreatorTests
    {
        [Fact]
        public void GetCommandQuery()
        {
            // Arrange
            var requestSEttings = new Request
            {
                SelectedTables = "SignUp, LeaderBoard",
                SelectedColumns = "SignUp.PoolId, LeaderBoard.Rank, LeaderBoard.Owner, LeaderBoard.Amount",
                WhereCondition = "SignUp.id = 3, SignUp.address = '0x3a31ee5557c9369c35573496555b1bc93553b553'",
                JoinCondition = "SignUp.Address = LeaderBoard.Owner"
            };

            // Act
            var result = QueryCreator.CreateCommandQuery(requestSEttings);

            // Assert
            Assert.NotNull(result);
            var resultType = Assert.IsType<string>(result);
            var json = Assert.IsAssignableFrom<string>(resultType);
            Assert.NotEqual(string.Empty, json);
            Assert.Equal(
                "SELECT SignUp.PoolId, LeaderBoard.Rank, LeaderBoard.Owner, LeaderBoard.Amount " +
                "FROM SignUp INNER JOIN LeaderBoard " +
                "ON SignUp.Address = LeaderBoard.Owner" +
                " WHERE SignUp.id = 3 AND SignUp.address = '0x3a31ee5557c9369c35573496555b1bc93553b553' FOR JSON PATH", json);
        }
    }
}
