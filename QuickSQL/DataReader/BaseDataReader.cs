using System.Text;
using System.Data.Common;
using System.Data;

namespace QuickSQL.DataReader
{
    public abstract class BaseDataReader
    {
        public string GetJsonData(string commandQuery, string connectionString, IDataReader mock = null)
        {
            var jsonResult = new StringBuilder();

            if (mock != null)
            {
                jsonResult.Append(ReadData(mock));
                return GetResultData(jsonResult.ToString());
            }

            using (var connection = CreateConnection(connectionString))
            {
                connection.Open();

                var reader = CreateReader(commandQuery, connection);
                jsonResult.Append(ReadData(reader));
            }
            return GetResultData(jsonResult.ToString());
        }

        public string ReadData(IDataReader reader)
        {
            var jsonResult = new StringBuilder();
            while (reader.Read())
                jsonResult.Append(reader.GetValue(0).ToString());

            return jsonResult.ToString();
        }
        public string GetResultData(string jsonResult)
        {
            string emptyJson = "[]";
            if (string.IsNullOrEmpty(jsonResult.ToString()))
                return emptyJson;

            return jsonResult.ToString();
        }
        public abstract DbConnection CreateConnection(string connectionString);
        public abstract DbDataReader CreateReader(string commandQuery, DbConnection connection);
    }
}
