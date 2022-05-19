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

            System.Console.WriteLine("Start read Json data");
            System.Console.WriteLine("Received SQL query: " + commandQuery);

            using (var connection = CreateConnection(connectionString))
            {
                System.Console.WriteLine("Created connection: " + connection.State);
                connection.Open();
                System.Console.WriteLine("Connection opened: " + connection.State);

                var reader = CreateReader(commandQuery, connection);
                System.Console.WriteLine("Reader created, reader IsClosed: " + reader.IsClosed);

                while (reader.Read())
                {
                    System.Console.WriteLine("Has reader more rows: " + reader.Read());
                    jsonResult.Append(reader.GetValue(0).ToString());
                    System.Console.WriteLine("Row result: " + jsonResult);
                }
                reader.Close();
                System.Console.WriteLine("End reading.");
            }
            System.Console.WriteLine("End connection.");

            if (string.IsNullOrEmpty(jsonResult.ToString()))
                return emptyJson;

            string cwRes = "BaseDataReader result: " + jsonResult;
            int lengthCwRes = cwRes.Length;
            System.Console.WriteLine(cwRes);
            for (int i = 0; i < lengthCwRes; i++)
            {
                System.Console.Write('=');
            }
            System.Console.WriteLine("");

            return jsonResult.ToString();
        }
        public abstract DbConnection CreateConnection(string connectionString);
        public abstract DbDataReader CreateReader(string commandQuery, DbConnection connection);
    }
}
