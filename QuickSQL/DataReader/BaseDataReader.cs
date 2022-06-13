using System.Text;
using System.Data.Common;

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
            if (stringJson.Contains("},{")
                || stringJson.Contains("}, {"))
            {
                stringJson = $"[{stringJson}]";
            }
            // Return single object
            return stringJson.Replace(" ", "");
        }

        public abstract DbConnection CreateConnection(string connectionString);
        public abstract DbDataReader CreateReader(string commandQuery, DbConnection connection);
    }
}
