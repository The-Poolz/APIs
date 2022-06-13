using System.Text;
using System.Data.Common;

namespace QuickSQL.DataReader
{
    public abstract class BaseDataReader
    {
        public string GetJsonData(string commandQuery, string connectionString)
        {
            string emptyJson = "[]";
            int readCount = 0;
            using var connection = CreateConnection(connectionString);
            connection.Open();
            var reader = CreateReader(commandQuery, connection);

            var jsonResult = new StringBuilder();
            while (reader.Read())
            {
                jsonResult.Append(reader.GetValue(0).ToString());
                readCount++;
            }
            if (string.IsNullOrEmpty(jsonResult.ToString()))
                return emptyJson;
            if (readCount == 1)
            {
                jsonResult.ToString().Remove(
                    jsonResult.ToString().IndexOf('['));
                jsonResult.ToString().Remove(
                    jsonResult.ToString().IndexOf(']'));
            }
            return jsonResult.ToString();
        }

        public abstract DbConnection CreateConnection(string connectionString);
        public abstract DbDataReader CreateReader(string commandQuery, DbConnection connection);
    }
}
