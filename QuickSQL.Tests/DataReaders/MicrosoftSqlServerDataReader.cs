using System.Data.Common;
using System.Data.SqlClient;

using QuickSQL.DataReader;

namespace QuickSQL.Tests.DataReaders
{
    public class MicrosoftSqlServerDataReader : BaseDataReader
    {
        public override Providers Provider => Providers.MicrosoftSqlServer;

        public override DbConnection CreateConnection(string connectionString)
            => new SqlConnection(connectionString);

        public override DbDataReader CreateReader(string commandQuery, DbConnection connection)
            => new SqlCommand(commandQuery, (SqlConnection)connection).ExecuteReader();
    }
}
