using System;
using System.Linq;
using UniversalAPI.Helpers;

namespace UniversalAPI
{
    /// <summary>
    /// Provides a method for working like API with EntityFramework Core.<br/>
    /// Makes you aware of the new context and inherit <see cref="APIContext"/> for it.
    /// </summary>
    public static partial class APIClient
    {
        /// <summary>
        /// By default, the console logging option is enabled.
        /// </summary>
        public static bool ConsoleLogEnabled = true;

        /// <summary>Method getting JSON string data for the passed request.</summary>
        /// <param name="request">JSON string data./</param>
        /// <param name="requestSettings">Pass <see cref="APIRequestSettings"/> object with request settings.</param>
        /// <param name="connectionString">Database connection string, storing tables for data fetching.</param>
        /// <returns>Returns JSON string data if data read is success, or return null if operation failed.</returns>
        public static string InvokeRequest(string request, APIRequestSettings requestSettings, string connectionString)
        {
            // Create start program time
            var startTime = DateTime.UtcNow;
            // Log received request
            if (ConsoleLogEnabled)
                Console.WriteLine($"Received request data: {request}");

            //== Create SQL query ==//
            string commandQuery = QueryCreator.CreateCommandQuery(request.ToLower(), requestSettings);
            // Log received SQL query string
            if (ConsoleLogEnabled)
                LogGetCommandQuery(commandQuery);
            if (commandQuery == null)
                return null;

            //== Reading data with SqlDataReader ==//
            string result = DataReader.GetJsonData(commandQuery, connectionString);

            // Log result data and execution time
            if (ConsoleLogEnabled)
                LogGetData(result, startTime);

            return result;
        }
    }
}
