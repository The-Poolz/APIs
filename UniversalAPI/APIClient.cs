using Microsoft.EntityFrameworkCore;
using System;
using UniversalAPI.Helpers;

namespace UniversalAPI
{
    /// <summary>
    /// Provides a method for working like API with EntityFramework Core.<br/>
    /// </summary>
    public static partial class APIClient
    {
        /// <summary>
        /// By default, the console logging option is enabled.
        /// </summary>
        public static bool ConsoleLogEnabled = true;

        /// <summary>Method getting JSON string data for the passed request.</summary>
        /// <param name="requestSettings">Pass <see cref="APIRequest"/> object with request settings.</param>
        /// <param name="context">Context, storing tables for data fetching.</param>
        /// <returns>Returns JSON string data if data read is success, or return null if operation failed.</returns>
        public static string InvokeRequest(APIRequest requestSettings, DbContext context)
        {
            // Create start program time
            var startTime = DateTime.UtcNow;
            // Log received request
            if (ConsoleLogEnabled)
                Console.WriteLine($"Received request: {requestSettings}");

            //== Create SQL query ==//
            string commandQuery = QueryCreator.CreateCommandQuery(requestSettings);
            // Log received SQL query string
            LogGetCommandQuery(commandQuery);
            if (commandQuery == null)
                return null;

            //== Reading data with SqlDataReader ==//
            // create context object
            string result = DataReader.GetJsonData(commandQuery, context.Database.GetConnectionString());

            // Log result data and execution time
            LogGetData(result, startTime);

            return result;
        }
    }
}
