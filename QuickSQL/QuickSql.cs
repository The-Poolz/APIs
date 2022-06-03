using System.Text.Json;

using QuickSQL.DataReader;
using QuickSQL.QueryCreator;

namespace QuickSQL
{
    /// <summary>
    /// Provides a method for convenient work with SQL queries.<br/>
    /// Need supported or your custom class implementations <see cref="BaseDataReader"/> and <see cref="BaseQueryCreator"/>.<br/>
    /// For add provider search NuGet package QuickSQL.{supported_provider_name}<br/>
    /// Supported providers <see cref="Providers"/>.
    /// </summary>
    public static class QuickSql
    {
        /// <summary>The method uses <see cref="Request"/> to create a SQL command.<br/>
        /// Uses default provider functions to read JSON data.<br/>
        /// The result of the function is the JSON data received from the created request.<br/>
        /// </summary>
        /// <param name="request">Pass <see cref="Request"/> object with required fields for database request.<br/>
        /// Based on this object, a SQL command is created.</param>
        /// <param name="connectionString">Connection string to database.</param>
        /// <param name="dataReader">Supported or your custom DataReader.<br/></param>
        /// <param name="queryCreator">Supported or your custom DataReader.</param>
        /// <returns>Returns JSON string data if data read is success, or return empty JSON "[]" if request is correctly but data not found.<br/>
        /// Return null if operation failed.</returns>
        public static object InvokeRequest(Request request, string connectionString, BaseDataReader dataReader, BaseQueryCreator queryCreator)
        {
            if (string.IsNullOrEmpty(connectionString.Trim())
                || dataReader == null
                || queryCreator == null)
            {
                return null;
            }

            string commandQuery = queryCreator.CreateCommandQuery(request);
            string jsonResult = dataReader.GetJsonData(commandQuery, connectionString);
            return JsonSerializer.Deserialize<object>(jsonResult);
        }
    }
}
