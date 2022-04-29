﻿using QuickSQL.DataReader;
using System.Data.Common;
using System.Data.SqlClient;

namespace QuickSQL.Tests
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
