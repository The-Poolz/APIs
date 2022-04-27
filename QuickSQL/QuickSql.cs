using QuickSQL.QueryCreators;
using QuickSQL.DataReader;

namespace QuickSQL
{
    /// <summary>
    /// Provides a method for convenient work with SQL queries.<br/>
    /// </summary>
    public static class QuickSql
    {
        /// <summary>The method uses the <see cref="Request"/> to create a SQL command.<br/>Forces you to implement a DataReader for your data provider. Define the methods of the <see cref="BaseDataReader"/> class.</summary>
        /// <param name="request">Pass <see cref="Request"/> object with request settings.</param>
        /// <param name="connectionString">Connection string to database.</param>
        /// <param name="dataReader">DataReader defining <see cref="BaseDataReader.CreateConnection(string)"/> and <see cref="BaseDataReader.CreateReader(string, System.Data.Common.DbConnection)"/> methods for your provider.<br/>Define your DataReader <see cref="BaseDataReader.Provider"/>, Supported providers <see cref="Providers"/>.</param>
        /// <returns>Returns JSON string data if data read is success, or return empty JSON if data not found. Return null if operation failed.</returns>
        public static string InvokeRequest(Request request, string connectionString, BaseDataReader dataReader)
        {
            string result = null;
            if (dataReader.Provider == Providers.MySql)
            {
                string commandQuery = MySqlQueryCreator.CreateCommandQuery(request);
                result = dataReader.GetJsonData(commandQuery, connectionString);
            }
            else if (dataReader.Provider == Providers.MicrosoftSqlServer)
            {
                string commandQuery = SqlQueryCreator.CreateCommandQuery(request);
                result = dataReader.GetJsonData(commandQuery, connectionString);
            }
            return result;
        }
    }
}
