using System;
using System.Linq;
using UniversalAPI.Helpers;

namespace UniversalAPI
{
    /// <summary>
    /// Provides a method for working like API with EntityFramework Core.<br/>
    /// Makes you aware of the new context and inherit <see cref="APIContext"/> for it.
    /// </summary>
    public partial class APIClient
    {
        private readonly string ConnectionString;
        /// <summary>
        /// Create <see cref="APIClient"/> obj.
        /// </summary>
        /// <param name="connectionString">Database connection string, storing tables for data fetching.</param>
        public APIClient(string connectionString)
        {
            ConnectionString = connectionString;
        }

        /// <summary>
        /// By default, the console logging option is enabled.
        /// </summary>
        public bool ConsoleLogEnabled = true;

        /// <summary>Method getting JSON string data for the passed request.</summary>
        /// <param name="request">JSON string data./</param>
        /// <param name="apiContext">Pass <see cref="APIContext"/> or a context that inherits from <see cref="APIContext"/>.</param>
        /// <returns>Returns JSON string data if data read is success, or return null if operation failed.</returns>
        public string InvokeRequest(string request, APIContext apiContext)
        {
            // Check if context has APIRequests table
            if (!CheckTableAPIRequestsExists(apiContext))
                return null;

            // Create start program time
            var startTime = DateTime.UtcNow;
            // Log received request
            if (ConsoleLogEnabled)
                Console.WriteLine($"Received request data: {request}");

            //== Create SQL query ==//
            string commandQuery = QueryCreator.CreateCommandQuery(request.ToLower(), apiContext);
            // Log received SQL query string
            if (ConsoleLogEnabled)
                LogGetCommandQuery(commandQuery);
            if (commandQuery == null)
                return null;

            //== Reading data with SqlDataReader ==//
            string result = DataReader.GetJsonData(commandQuery, ConnectionString);

            // Log result data and execution time
            if (ConsoleLogEnabled)
                LogGetData(result, startTime);

            return result;
        }

        private bool CheckTableAPIRequestsExists(APIContext db)
        {
            try
            {
                db.Set<Request>().Count();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
