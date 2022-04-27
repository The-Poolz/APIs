using System.Data.SqlClient;
using System.Text;

namespace QuickSQL.DataReaders
{
    public static class MSQLSDataReader
    {
        const string emptyJson = "[]";
        public static string GetJsonData(string commandQuery, string connectionString)
        {
            using var connection = new SqlConnection(connectionString);
            connection.Open();
            var reader = new SqlCommand(commandQuery, connection).ExecuteReader();
            if (!reader.HasRows)
                return emptyJson;

            var jsonResult = new StringBuilder();
            while (reader.Read())
            {
                jsonResult.Append(reader.GetValue(0).ToString());
            }
            return jsonResult.ToString();
        }
    }
}
