using System.Text;
using System.Data.Common;
using System;

namespace QuickSQL.DataReader
{
    public abstract class BaseDataReader
    {
        public string GetJsonData(string commandQuery, string connectionString)
        {
            using var connection = CreateConnection(connectionString);
            connection.Open();
            var reader = CreateReader(commandQuery, connection);

            StringBuilder jsonResult = new();
            while (reader.Read())
            {
                jsonResult.Append(reader.GetValue(0).ToString());
            }

            string stringJson = jsonResult.ToString();
            // Return empty json if data nullable or empty
            string emptyJson = "[]";
            if (string.IsNullOrEmpty(stringJson))
                return emptyJson;
            // Return array
            if ((stringJson.Contains("},{", StringComparison.Ordinal)
                || stringJson.Contains("}, {", StringComparison.Ordinal))
                && !stringJson.StartsWith("[", StringComparison.Ordinal)) // This rule for MySql provider. MySql return always [result]
            {
                stringJson = $"[{stringJson}]";
            }
            // Return single object
            return stringJson.Replace(" ", "", StringComparison.Ordinal);
        }

        public abstract DbConnection CreateConnection(string connectionString);
        public abstract DbDataReader CreateReader(string commandQuery, DbConnection connection);
    }
}
