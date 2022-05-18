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

            System.Console.WriteLine("connection string: " + connectionString);

            using (var connection = CreateConnection(connectionString))
            {
                connection.Open();

                System.Console.WriteLine(connection.State.ToString());

                var reader = CreateReader(commandQuery, connection);

                System.Console.WriteLine("reader is: " + reader.IsClosed);

                while (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        jsonResult.Append(reader.GetValue(0).ToString());
                        System.Console.WriteLine(jsonResult.ToString());
                    }

                    reader.NextResult();
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
