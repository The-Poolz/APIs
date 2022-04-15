using Microsoft.Data.SqlClient;
using System;
using System.Text;

namespace QuickSQL.Helpers
{
    public static class DataReader
    {
        public static string GetJsonData(string commandQuery, string connectionString)
        {
            if (!CheckSqlInjection(commandQuery))
                return null;

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

        private static bool CheckSqlInjection(string commandQuery)
        {
            string inj_str = "'|and|exec|insert|select|delete|update|count|*|%|chr|mid|master|truncate|char|declare|;|or|-|+|,";
            string[] inj_stra = inj_str.Split("|");

            int inj_count = inj_stra.Length;
            for (int i = 0; i < inj_count; i++)
            {
                if (commandQuery.IndexOf(inj_stra[i]) >= 0)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
