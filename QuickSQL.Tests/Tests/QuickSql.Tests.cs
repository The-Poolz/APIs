using Xunit;
using System;
using System.Globalization;
using System.Collections.Generic;

using QuickSQL.DataReader;
using QuickSQL.QueryCreator;
using QuickSQL.Tests.DataReaders;
using QuickSQL.Tests.QueryCreators;

namespace QuickSQL.Tests
{
    public static class QuickSqlTests
    {
        [Fact]
        public static void InvokeRequest()
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
            string expected = "[{\"Owner\": \"0x1a01ee5577c9d69c35a77496565b1bc95588b521\", \"Token\": \"ADH\", \"Amount\": \"400\"}]";
            string isTravisCi = Environment.GetEnvironmentVariable("IsTravisCI");
            string connectionString;
            if (Convert.ToBoolean(isTravisCi, new CultureInfo("en-US")))
                connectionString = Environment.GetEnvironmentVariable("TravisCIMySqlConnection");
            else
                connectionString = LocalConnection.MySqlConnection;
            var reader = new MySqlDataReader();
            var queryCreator = new MySqlQueryCreator();

            // Act
            string result = QuickSql.InvokeRequest(request, connectionString, reader, queryCreator);

            Assert.NotNull(result);
            Assert.IsType<string>(result);
            Assert.Equal(expected, result);
        }

        [Fact]
        public static void InvokeRequestMicrosoftSqlServerProvider()
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
            string expected;
            string isTravisCi = Environment.GetEnvironmentVariable("IsTravisCI");
            string connectionString;
            BaseDataReader reader;
            BaseQueryCreator queryCreator;
            string result;

            // Act
            if (Convert.ToBoolean(isTravisCi, new CultureInfo("en-US")))
            {
                expected = "[{\"Token\":\"ADH\",\"Owner\":\"0x1a01ee5577c9d69c35a77496565b1bc95588b521\",\"Amount\":\"400\"}]";
                reader = new MicrosoftSqlServerDataReader();
                queryCreator = new SqlQueryCreator();
                connectionString = Environment.GetEnvironmentVariable("TravisCIMicrosoftSqlServerConnection");
                result = QuickSql.InvokeRequest(request, connectionString, reader, queryCreator);
            }
            else
            {
                expected = "[{\"Token\":\"ADH\",\"Owner\":\"0x1a01ee5577c9d69c35a77496565b1bc95588b521\",\"Amount\":\"400\"}]";
                reader = new MicrosoftSqlServerDataReader();
                queryCreator = new SqlQueryCreator();
                connectionString = LocalConnection.MicrosoftSqlServerConnection;
                result = QuickSql.InvokeRequest(request, connectionString, reader, queryCreator);
            }

            Assert.NotNull(result);
            Assert.IsType<string>(result);
            Assert.Equal(expected, result);
        }

        [Fact]
        public static void InvokeRequestWithoutConnectionString()
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
            string connectionString = "   ";

            // Act
            var result = QuickSql.InvokeRequest(request, connectionString, new MySqlDataReader(), new MySqlQueryCreator());

            Assert.Null(result);
        }

        [Fact]
        public static void InvokeRequestInvalidProviders()
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
            string connectionString = "not connection string";

            // Act
            var result = QuickSql.InvokeRequest(request, connectionString, null, null);

            Assert.Null(result);
        }
    }
}
