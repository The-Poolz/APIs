using Xunit;
using System;
using System.Data;
using System.Globalization;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using QuickSQL.Tests.QueryCreator;

namespace QuickSQL.Tests.DataReader
{
    /// <summary>
    /// SqlDataReader has implemented the BaseDataReader tests.
    /// </summary>
    /// <remarks>
    /// All tests with databese use the MySql provider.
    /// </remarks>
    public static class BaseDataReaderTests
    {
        [Fact]
        public static void GetJsonData()
        {
            string isTravisCi = Environment.GetEnvironmentVariable("IsTravisCI");
            var request = new Request(
                "TokenBalances",
                new Collection<string>
                {
                    { "Token" }, { "Owner" }, { "Amount" }
                },
                new Collection<Condition>
                {
                    new Condition { ParamName = "Id", Operator = OperatorName.Equals, ParamValue = "1" }
                });
            string commandQuery = new MySqlQueryCreator().CreateCommandQuery(request);
            string connectionString = null;
            IDataReader reader = null;
            if (Convert.ToBoolean(isTravisCi, new CultureInfo("en-US")))
            {
                connectionString = Environment.GetEnvironmentVariable("TravisCIMySqlConnection");
            }
            else
            {
                List<TokenBalances> tokens = new List<TokenBalances>()
                {
                    new TokenBalances
                    {
                        Id = 1,
                        Token = "ADH",
                        Owner = "0x1a01ee5577c9d69c35a77496565b1bc95588b521",
                        Amount = "400"
                    },
                    new TokenBalances
                    {
                        Id = 2,
                        Token = "ADH",
                        Owner = "0x2a01ee5557c9d69c35577496555b1bc95558b552",
                        Amount = "300"
                    }
                };

                reader = Mock.MockIDataReader(tokens);
            }

            var result = new MySqlDataReader().GetJsonData(commandQuery, connectionString, reader);

            Assert.NotNull(result);
            Assert.IsType<string>(result);
            Assert.NotEqual<string>("[]",result);
        }

        [Fact]
        public static void GetJsonDataEmptyJsonResult()
        {
            string isTravisCi = Environment.GetEnvironmentVariable("IsTravisCI");
            string expected = "[]";
            var request = new Request(
                "TokenBalances",
                new Collection<string>
                {
                    { "Token" },
                    { "Owner" },
                    { "Amount" }
                },
                new Collection<Condition>
                {
                    new Condition { ParamName = "Id", Operator = OperatorName.Equals, ParamValue = "40" }
                });
            string commandQuery = new SqlQueryCreator().CreateCommandQuery(request);
            string connectionString;
            if (Convert.ToBoolean(isTravisCi, new CultureInfo("en-US")))
                connectionString = Environment.GetEnvironmentVariable("TravisCIMicrosoftSqlServerConnection");
            else
                connectionString = LocalConnection.MicrosoftSqlServerConnection;

            string result = new SqlDataReader().GetJsonData(commandQuery, connectionString);

            Assert.NotNull(result);
            Assert.IsType<string>(result);
            Assert.Equal(expected, result);
        }
    }
}