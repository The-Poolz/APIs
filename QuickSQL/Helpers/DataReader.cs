using Microsoft.Data.SqlClient;
using System.Text;

namespace QuickSQL.Helpers
{
    public static class DataReader
    {
        const string emptyJson = "[]";
        public static string GetJsonData(string commandQuery, string connectionString)
        {
            using (var connection = SqlUtil.GetConnection(connectionString))
            {
                connection.Open();
                var reader = SqlUtil.GetReader(commandQuery, connection);

                if (!reader.HasRows)
                    return emptyJson;

                return ReadSql(reader);
            }
        }
        public static string ReadSql(SqlDataReader reader)
        {
            var jsonResult = new StringBuilder();
            while (reader.Read())
            {
                jsonResult.Append(reader.GetValue(0).ToString());
            }
            return jsonResult.ToString();
        }
    }
}
