using System.Data.Common;

using QuickSQL.DataReader;
using QuickSQL.QueryCreator;

namespace QuickSQL
{
    /// <summary>
    /// Provides a method for convenient work with SQL queries.<br/>
    /// </summary>
    public static class QuickSql
    {
        /// <summary>The method uses <see cref="Request"/> to create a SQL command. The result of the function is the JSON data received from the created request.</summary>
        /// <param name="request">Pass <see cref="Request"/> object with request settings.</param>
        /// <param name="connectionString">Connection string to database.</param>
        /// <param name="dataReader">DataReader defining <see cref="BaseDataReader.CreateConnection(string)"/> and <see cref="BaseDataReader.CreateReader(string, DbConnection)"/> methods for your provider.<br/>Supported providers <see cref="Providers"/>.</param>
        /// <returns>Returns JSON string data if data read is success, or return empty JSON if data not found. Return null if operation failed.</returns>
        public static string InvokeRequest(Request request, string connectionString, BaseDataReader dataReader, BaseQueryCreator queryCreator)
        {
            if (string.IsNullOrEmpty(connectionString.Trim())
                || dataReader == null
                || queryCreator == null)
            {
                return null;
            }

            string commandQuery = queryCreator.CreateCommandQuery(request);
            string result = dataReader.GetJsonData(commandQuery, connectionString);
            return result;
        }
    }
}
