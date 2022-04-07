using Microsoft.Data.SqlClient;
using System;
using System.Text;

namespace UniversalAPI.Helpers
{
    public static class DataReader
    {
        public static string GetJsonData(string commandQuery, string connectionString)
        {
            var jsonResult = new StringBuilder();
            try
            {
                using (var connection = SqlUtil.GetConnection(connectionString))
                {
                    connection.Open();
                    var reader = SqlUtil.GetReader(commandQuery, connection);
                    if (!reader.HasRows)
                        jsonResult.Append("[]");

                    while (reader.Read())
                    {
                        jsonResult.Append(reader.GetValue(0).ToString());
                    }
                    reader.Close();
                }
                return jsonResult.ToString();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return jsonResult.ToString();
        }
    }
}
