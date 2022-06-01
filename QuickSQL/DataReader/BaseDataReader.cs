using System.Text;
using System.Data;
using System.Data.Common;

namespace QuickSQL.DataReader
{
    public abstract class BaseDataReader
    {
        public string GetJsonData(string commandQuery, string connectionString, IDataReader reader = null)
        {
            var jsonResult = new StringBuilder();

            if (reader != null) // If Mock or if user
            {
                while (reader.Read())
                    jsonResult.Append(reader.GetValue(0).ToString());
            }
            else
            {
                using var connection = CreateConnection(connectionString);
                connection.Open();

                reader = CreateReader(commandQuery, connection);

                while (reader.Read())
                    jsonResult.Append(reader.GetValue(0).ToString());
            }

            string emptyJson = "[]";
            if (string.IsNullOrEmpty(jsonResult.ToString()))
                return emptyJson;

            return jsonResult.ToString();
        }
        public abstract DbConnection CreateConnection(string connectionString);
        public abstract DbDataReader CreateReader(string commandQuery, DbConnection connection);
    }
}
