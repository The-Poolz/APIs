using Xunit;
using System;
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
    /// All tests with databese use the Microsoft Sql Server provider.
    /// </remarks>
    public static class QuickSqlTests
    {
        //[Fact]
        //public static void InvokeRequest()
        //{
        //    string isTravisCi = Environment.GetEnvironmentVariable("IsTravisCI");
        //    string expected = "[{\"Token\":\"ADH\",\"Owner\":\"0x1a01ee5577c9d69c35a77496565b1bc95588b521\",\"Amount\":\"400\"}]";
        //    var request = new Request(
        //        "TokenBalances",
        //        "Token, Owner, Amount");
        //    string connectionString;

        //    if (Convert.ToBoolean(isTravisCi, new CultureInfo("en-US")))
        //        connectionString = Environment.GetEnvironmentVariable("TravisCIMicrosoftSqlServerConnection");
        //    else
        //        connectionString = LocalConnection.MicrosoftSqlServerConnection;

        //    var result = QuickSql.InvokeRequest(request, connectionString,
        //        new SqlDataReader(), new SqlQueryCreator());

        //    Assert.NotNull(result);
        //    Assert.IsType<string>(result);
        //    Assert.Equal(expected, result);
        //}

        //[Fact]
        //public static void InvokeRequestWithoutConnectionString()
        //{
        //    var request = new Request(
        //        "TokenBalances",
        //        "Token, Owner, Amount",
        //        new Collection<Condition>
        //        {
        //            new Condition { ParamName = "Id", Operator = OperatorName.Equals, ParamValue = "1" }
        //        });
        //    string connectionString = "   ";

        //    var result = QuickSql.InvokeRequest(request, connectionString,
        //        new SqlDataReader(), new SqlQueryCreator());

        //    Assert.Null(result);
        //}

        //[Fact]
        //public static void InvokeRequestInvalidConnectionString()
        //{
        //    var request = new Request(
        //        "TokenBalances",
        //        "Token, Owner, Amount",
        //        new Collection<Condition>
        //        {
        //            new Condition { ParamName = "Id", Operator = OperatorName.Equals, ParamValue = "1" }
        //        });
        //    string connectionString = "not connection string";
        //    string expected = "Format of the initialization string does not conform to specification starting at index 0.";

        //    void result() => QuickSql.InvokeRequest(request, connectionString,
        //        new SqlDataReader(), new SqlQueryCreator());

        //    ArgumentException exception = Assert.Throws<ArgumentException>(result);
        //    Assert.Equal(expected, exception.Message);
        //}

        //[Fact]
        //public static void InvokeRequestInvalidProviders()
        //{
        //    var request = new Request(
        //        "TokenBalances",
        //        "Token, Owner, Amount",
        //        new Collection<Condition>
        //        {
        //            new Condition { ParamName = "Id", Operator = OperatorName.Equals, ParamValue = "1" }
        //        });
        //    string connectionString = "not connection string";

        //    var result = QuickSql.InvokeRequest(request, connectionString, null, null);

        //    Assert.Null(result);
        //}
    }
}
