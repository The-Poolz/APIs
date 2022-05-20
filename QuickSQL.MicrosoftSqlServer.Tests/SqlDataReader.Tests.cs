using Xunit;
using System;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.Data.SqlClient;
using System.Collections.ObjectModel;

namespace QuickSQL.MicrosoftSqlServer.Tests
{
    public class SqlDataReaderTests
    {
        [Fact]
        public void CreateConnection()
        {
            ConnectionState expectedState = ConnectionState.Closed;
            string connectionString;
            string isTravisCi = Environment.GetEnvironmentVariable("IsTravisCI");
            if (Convert.ToBoolean(isTravisCi, new CultureInfo("en-US")))
                connectionString = Environment.GetEnvironmentVariable("TravisCIMicrosoftSqlServerConnection");
            else
                connectionString = LocalConnection.MicrosoftSqlServerConnection;

            var result = new SqlDataReader().CreateConnection(connectionString);

            Assert.NotNull(result);
            Assert.IsType<SqlConnection>(result);
            Assert.Equal(expectedState, result.State);
        }

        [Fact]
        public void CreateReader()
        {
            string connectionString;
            string isTravisCi = Environment.GetEnvironmentVariable("IsTravisCI");
            if (Convert.ToBoolean(isTravisCi, new CultureInfo("en-US")))
                connectionString = Environment.GetEnvironmentVariable("TravisCIMicrosoftSqlServerConnection");
            else
                connectionString = LocalConnection.MicrosoftSqlServerConnection;
            var request = new Request(
                "TokenBalances",
                new Collection<string> { { "Token" }, { "Owner" }, { "Amount" } },
                new Collection<Condition>
                {
                    new Condition { ParamName = "Id", Operator = OperatorName.Equals, ParamValue = "1" }
                });
            var commandQuery = new SqlQueryCreator().CreateCommandQuery(request);

            DbDataReader result;
            using (var connection = new SqlDataReader().CreateConnection(connectionString))
            {
                connection.Open();
                result = new SqlDataReader().CreateReader(commandQuery, connection);
                Assert.True(result.HasRows);
            }

            Assert.NotNull(result);
            Assert.IsType<System.Data.SqlClient.SqlDataReader>(result);
        }

        [Fact]
        public void GetJsonDataDefault()
        {
            string expected = "[{\"Token\":\"ADH\",\"Owner\":\"0x1a01ee5577c9d69c35a77496565b1bc95588b521\",\"Amount\":\"400\"}]";
            string connectionString;
            string isTravisCi = Environment.GetEnvironmentVariable("IsTravisCI");
            if (Convert.ToBoolean(isTravisCi, new CultureInfo("en-US")))
                connectionString = Environment.GetEnvironmentVariable("TravisCIMicrosoftSqlServerConnection");
            else
                connectionString = LocalConnection.MicrosoftSqlServerConnection;
            var request = new Request(
                "TokenBalances",
                new Collection<string> { { "Token" }, { "Owner" }, { "Amount" } },
                new Collection<Condition>
                {
                    new Condition { ParamName = "Id", Operator = OperatorName.Equals, ParamValue = "1" }
                });
            var commandQuery = new SqlQueryCreator().CreateCommandQuery(request);

            var result = new SqlDataReader().GetJsonData(commandQuery, connectionString);

            Assert.NotNull(result);
            Assert.IsType<string>(result);
            Assert.Equal(expected, result);
        }

        [Fact]
        public void GetJsonDataInvalidCommand()
        {
            string expected = "Invalid object name 'Token'.";
            string connectionString;
            string isTravisCi = Environment.GetEnvironmentVariable("IsTravisCI");
            if (Convert.ToBoolean(isTravisCi, new CultureInfo("en-US")))
                connectionString = Environment.GetEnvironmentVariable("TravisCIMicrosoftSqlServerConnection");
            else
                connectionString = LocalConnection.MicrosoftSqlServerConnection;
            var request = new Request(
                "Token",    // Invalid table name in command query
                new Collection<string> { { "Token" }, { "Owner" }, { "Amount" } },
                new Collection<Condition>
                {
                    new Condition { ParamName = "Id", Operator = OperatorName.Equals, ParamValue = "1" }
                });
            var commandQuery = new SqlQueryCreator().CreateCommandQuery(request);

            void result() => new SqlDataReader().GetJsonData(commandQuery, connectionString);

            SqlException exception = Assert.Throws<SqlException>(result);
            Assert.Equal(expected, exception.Message);
        }

        [Fact]
        public void GetJsonDataInvalidConnectionString()
        {
            string expected = "Format of the initialization string does not conform to specification starting at index 0.";
            string connectionString = "not connection string";
            var request = new Request(
                "TokenBalances",
                new Collection<string> { { "Token" }, { "Owner" }, { "Amount" } },
                new Collection<Condition>
                {
                    new Condition { ParamName = "Id", Operator = OperatorName.Equals, ParamValue = "1" }
                });
            var commandQuery = new SqlQueryCreator().CreateCommandQuery(request);

            void result() => new SqlDataReader().GetJsonData(commandQuery, connectionString);

            ArgumentException exception = Assert.Throws<ArgumentException>(result);
            Assert.Equal(expected, exception.Message);
        }
    }
}
