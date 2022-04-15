using Microsoft.Data.SqlClient;

namespace QuickSQL.Helpers
{
    public static class SqlUtil
    {
        public static SqlConnection GetConnection(string connectionString) =>
            new SqlConnection(connectionString);
        public static SqlDataReader GetReader(string commandQuery, SqlConnection connection) =>
            new SqlCommand(commandQuery, connection).ExecuteReader();
    }
}
