using System.Data.Common;
using System.Data.SqlClient;

using QuickSQL.DataReader;

namespace QuickSQL.MicrosoftSqlServer.DataReader
{
    public class SqlDataReader : BaseDataReader
    {
        public override Providers Provider => Providers.MicrosoftSqlServer;

        public override DbConnection CreateConnection(string connectionString)
            => new SqlConnection(connectionString);

        public override DbDataReader CreateReader(string commandQuery, DbConnection connection)
            => new SqlCommand(commandQuery, (SqlConnection)connection).ExecuteReader();
    }
}
