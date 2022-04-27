﻿using System.Data.Common;
using System.Text;

namespace QuickSQL.DataReader
{
    public abstract class BaseDataReader
    {
        public abstract Providers Provider { get; }
        public string ProviderName => Provider.ToString();

        public string GetJsonData(string commandQuery, string connectionString)
        {
            string emptyJson = "[]";
            using var connection = CreateConnection(connectionString);
            connection.Open();
            var reader = CreateReader(commandQuery, connection);

            if (!reader.HasRows)
                return emptyJson;

            var jsonResult = new StringBuilder();
            while (reader.Read())
            {
                jsonResult.Append(reader.GetValue(0).ToString());
            }
            return jsonResult.ToString();
        }
        public abstract DbConnection CreateConnection(string connectionString);
        public abstract DbDataReader CreateReader(string commandQuery, DbConnection connection);
    }
}
