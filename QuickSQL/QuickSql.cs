using Microsoft.EntityFrameworkCore;
using System;
using QuickSQL.Helpers;

namespace QuickSQL
{
    /// <summary>
    /// Provides a method for dynamic working with EntityFramework Core.<br/>
    /// </summary>
    public static partial class QuickSql
    {
        /// <summary>
        /// By default, the option to output to the console is disabled.
        /// </summary>
        public static bool ConsoleOutputEnabled = false;

        /// <summary>The method uses SQL queries to dynamically fetch data from the context.</summary>
        /// <param name="request">Pass <see cref="Request"/> object with request settings.</param>
        /// <param name="context">Context, storing tables for data fetching.</param>
        /// <returns>Returns JSON string data if data read is success, or return null if operation failed.</returns>
        public static string InvokeRequest(Request request, DbContext context)
        {
            // Create start program time
            var startTime = DateTime.UtcNow;

            //== Create SQL query ==//
            string commandQuery = QueryCreator.CreateCommandQuery(request);
            if (commandQuery == null)
            {
                ConsoleOutput(request, commandQuery, null, startTime);
                return null;
            }

            //== Reading data with SqlDataReader ==//
            string result = MySqlDataReader.GetJsonData(commandQuery, context.Database.GetConnectionString());

            ConsoleOutput(request, commandQuery, result, startTime);
            return result;
        }
    }
}
