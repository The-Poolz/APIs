using System.Text;
using System.Data.Common;

namespace QuickSQL.DataReader
{
    public abstract class BaseDataReader
    {
        public string GetJsonData(string commandQuery, string connectionString)
        {
            string emptyJson = "[]";
            StringBuilder jsonResult = new StringBuilder();

            using (var connection = CreateConnection(connectionString))
            {
                connection.Open();
                var reader = CreateReader(commandQuery, connection);
                while (reader.Read())
                {
                    jsonResult.Append(reader.GetValue(0).ToString());
                    if (!reader.HasRows)
                    {
                        reader.Close();
                    }
                }
            }

            if (string.IsNullOrEmpty(jsonResult.ToString()))
                return emptyJson;
            return jsonResult.ToString();
        }
        public abstract DbConnection CreateConnection(string connectionString);
        public abstract DbDataReader CreateReader(string commandQuery, DbConnection connection);
    }
}
