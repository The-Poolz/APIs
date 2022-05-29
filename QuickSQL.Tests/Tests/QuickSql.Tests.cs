using Xunit;
using System;
using Serilog;
using Xunit.Abstractions;
using System.Globalization;
using System.Collections.ObjectModel;

using QuickSQL.Tests.DataReader;
using QuickSQL.Tests.QueryCreator;

namespace QuickSQL.Tests
{
    /// <summary>
    /// QuickSql tests.
    /// </summary>
    /// <remarks>
    /// All tests with databese use the MySql provider.
    /// </remarks>
    public class QuickSqlTests
    {
        public QuickSqlTests(ITestOutputHelper output)
        {
            Log.Logger = new LoggerConfiguration()
            // add the xunit test output sink to the serilog logger
            // https://github.com/trbenning/serilog-sinks-xunit#serilog-sinks-xunit
            .WriteTo.TestOutput(output)
            .CreateLogger();
        }

        [Fact]
        public void InvokeRequest()
        {
            // Test logging
            TestLog log = new TestLog("InvokeRequest");
            log.StartTest();

            // Is TravisCI settings
            bool isTravis = Convert.ToBoolean(Environment.GetEnvironmentVariable("IsTravisCI"), new CultureInfo("en-US"));
            string connectionString;
            if (Convert.ToBoolean(isTravis, new CultureInfo("en-US")))
                connectionString = Environment.GetEnvironmentVariable("TravisCIMySqlConnection");
            else
                connectionString = LocalConnection.MySqlConnection;
            log.WriteConnection(isTravis, connectionString);

            // Arrange
            string expected = "[{\"Owner\": \"0x1a01ee5577c9d69c35a77496565b1bc95588b521\", \"Token\": \"ADH\", \"Amount\": \"400\"}]";
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
            log.WriteRequest(request);

            // Act
            var result = QuickSql.InvokeRequest(request, connectionString,
                new MySqlDataReader(), new MySqlQueryCreator());
            log.WriteResult(expected, result);

            Assert.NotNull(result);
            Assert.IsType<string>(result);
            Assert.Equal(expected, result);
            log.EndTest();
        }

        [Fact]
        public void InvokeRequestWithoutConnectionString()
        {
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
            string connectionString = "   ";

            var result = QuickSql.InvokeRequest(request, connectionString,
                new SqlDataReader(), new SqlQueryCreator());

            Assert.Null(result);
        }

        [Fact]
        public void InvokeRequestInvalidConnectionString()
        {
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
                    new Condition { ParamName = "Id", Operator = OperatorName.Equals, ParamValue = "1" }
                });
            string connectionString = "not connection string";
            string expected = "Format of the initialization string does not conform to specification starting at index 0.";

            void result() => QuickSql.InvokeRequest(request, connectionString,
                new SqlDataReader(), new SqlQueryCreator());

            ArgumentException exception = Assert.Throws<ArgumentException>(result);
            Assert.Equal(expected, exception.Message);
        }

        [Fact]
        public void InvokeRequestInvalidProviders()
        {
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
                    new Condition { ParamName = "Id", Operator = OperatorName.Equals, ParamValue = "1" }
                });
            string connectionString = "not connection string";

            var result = QuickSql.InvokeRequest(request, connectionString, null, null);

            Assert.Null(result);
        }
    }
}
