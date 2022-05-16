using Xunit;
using System.Collections.Generic;

namespace QuickSQL.Tests.QueryCreators
{
    public static class MySqlQueryCreatorTests
    {
        [Fact]
        public static void GetCommandQuery()
        {
            var request = new Request
            {
                TableName = "TokenBalances",
                SelectedColumns = "Token, Owner, Amount"
            };
            string expected = "SELECT JSON_ARRAYAGG(JSON_OBJECT('Token',Token, 'Owner',Owner, 'Amount',Amount)) FROM TokenBalances";
            var queryCreator = new MySqlQueryCreator();

            // Act
            var result = queryCreator.CreateCommandQuery(request);

            Assert.NotNull(result);
            Assert.IsType<string>(result);
            Assert.Equal(expected, result);
        }

        [Fact]
        public static void GetCommandQueryWithCondition()
        {
            var request = new Request
            {
                TableName = "TokenBalances",
                SelectedColumns = "Token, Owner, Amount",
                WhereConditions = new List<Condition>
                {
                    new Condition { ParamName = "Id", Operator = OperatorNames.Equals, ParamValue = "1" }
                }
            };
            string expected = "SELECT JSON_ARRAYAGG(JSON_OBJECT('Token',Token, 'Owner',Owner, 'Amount',Amount)) FROM TokenBalances WHERE Id = 1";
            var queryCreator = new MySqlQueryCreator();

            // Act
            var result = queryCreator.CreateCommandQuery(request);

            Assert.NotNull(result);
            Assert.IsType<string>(result);
            Assert.Equal(expected, result);
        }

        [Fact]
        public static void GetCommandQueryWithInvalidRequest()
        {
            var request = new Request
            {
                SelectedColumns = "Token, Owner, Amount"
            };
            var queryCreator = new MySqlQueryCreator();

            // Act
            var result = queryCreator.CreateCommandQuery(request);

            Assert.Null(result);
        }
    }
}
