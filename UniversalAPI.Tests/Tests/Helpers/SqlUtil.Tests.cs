using Interfaces;
using Microsoft.Data.SqlClient;
using System.Data;
using UniversalAPI.Helpers;
using Xunit;

namespace UniversalAPI.Tests.Helpers
{
    //public class SqlUtilTests
    //{
    //    [Fact]
    //    public void GetConnection()
    //    {
    //        var connection = SqlUtil.GetConnection(ConnectionString.ConnectionToData);
    //        connection.Open();

    //        Assert.NotNull(connection);
    //        Assert.IsType<SqlConnection>(connection);
    //        Assert.True(connection.State == ConnectionState.Open);

    //        connection.Close();
    //    }

    //    [Fact]
    //    public void GetReader()
    //    {
    //        var connection = SqlUtil.GetConnection(ConnectionString.ConnectionToData);
    //        connection.Open();

    //        var reader = SqlUtil.GetReader("SELECT * FROM LeaderBoard", connection);

    //        Assert.NotNull(reader);
    //        Assert.IsType<SqlDataReader>(reader);
    //        Assert.True(reader.HasRows);
    //        Assert.Equal(4, reader.FieldCount);
    //        connection.Close();
    //    }
    //}
}
