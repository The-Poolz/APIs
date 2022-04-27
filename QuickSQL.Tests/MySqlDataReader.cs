using MySql.Data.MySqlClient;
using QuickSQL.DataReader;
using System.Data.Common;

namespace QuickSQL.Tests
{
    public class MySqlDataReader : BaseDataReader
    {
        public override Providers Provider => Providers.MySql;

        public override DbConnection CreateConnection(string connectionString)
            => new MySqlConnection(connectionString);

        public override DbDataReader CreateReader(string commandQuery, DbConnection connection)
            => new MySqlCommand(commandQuery, (MySqlConnection)connection).ExecuteReader();
    }
}
