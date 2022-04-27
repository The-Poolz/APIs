using MySql.Data.MySqlClient;
using System.Text;

namespace QuickSQL.DataReaders
{
    public static class MySqlDataReader
    {
        const string emptyJson = "[]";
        public static string GetJsonData(string commandQuery, string connectionString)
        {
            using var connection = new MySqlConnection(connectionString);
            connection.Open();
            var reader = new MySqlCommand(commandQuery, connection).ExecuteReader();
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
