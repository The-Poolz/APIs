using Microsoft.Data.SqlClient;

namespace UniversalApi.Helpers
{
    public static class SqlHelpers
    {
        public static SqlConnection GetConnection(string connectionString) => new SqlConnection(connectionString);
        public static SqlDataReader GetReader(string commandQuery, SqlConnection connection) =>
            new SqlCommand(commandQuery, connection).ExecuteReader();
    }
}
