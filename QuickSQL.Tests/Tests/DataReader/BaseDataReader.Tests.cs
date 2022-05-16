using Xunit;
using System;
using System.Globalization;
using System.Collections.Generic;

using QuickSQL.Tests.DataReaders;
using QuickSQL.Tests.QueryCreators;

namespace QuickSQL.Tests.DataReader
{
    public static class BaseDataReaderTests
    {
        [Fact]
        public static void GetJsonData()
        {
            MySqlDataReader reader = new MySqlDataReader();
            var request = new Request
            {
                TableName = "TokenBalances",
                SelectedColumns = "Token, Owner, Amount",
                WhereConditions = new List<Condition>
                {
                    new Condition { ParamName = "Id", Operator = OperatorNames.Equals, ParamValue = "1" }
                }
            };
            string expected = "[{\"Owner\": \"0x1a01ee5577c9d69c35a77496565b1bc95588b521\", \"Token\": \"ADH\", \"Amount\": \"400\"}]";
            var queryCreator = new MySqlQueryCreator();
            string commandQuery = queryCreator.CreateCommandQuery(request);
            string isTravisCi = Environment.GetEnvironmentVariable("IsTravisCI");
            string connectionString;
            if (Convert.ToBoolean(isTravisCi, new CultureInfo("en-US")))
                connectionString = Environment.GetEnvironmentVariable("TravisCIMySqlConnection");
            else
                connectionString = LocalConnection.MySqlConnection;

            // Act
            var result = reader.GetJsonData(commandQuery, connectionString);

            Assert.NotNull(result);
            Assert.IsType<string>(result);
            Assert.Equal(expected, result);
        }

        [Fact]
        public static void GetJsonDataEmptyJsonResult()
        {
            MySqlDataReader reader = new MySqlDataReader();
            var request = new Request
            {
                TableName = "TokenBalances",
                SelectedColumns = "Token, Owner, Amount",
                WhereConditions = new List<Condition>
                {
                    new Condition { ParamName = "Id", Operator = OperatorNames.Equals, ParamValue = "40" }
                }
            };
            string expected = "[]";
            var queryCreator = new MySqlQueryCreator();
            string commandQuery = queryCreator.CreateCommandQuery(request);
            string isTravisCi = Environment.GetEnvironmentVariable("IsTravisCI");
            string connectionString;
            if (Convert.ToBoolean(isTravisCi, new CultureInfo("en-US")))
                connectionString = Environment.GetEnvironmentVariable("TravisCIMySqlConnection");
            else
                connectionString = LocalConnection.MySqlConnection;

            // Act
            string result = reader.GetJsonData(commandQuery, connectionString);

            Assert.NotNull(result);
            Assert.IsType<string>(result);
            Assert.Equal(expected, result);
        }
    }
}