using System;
using System.Linq;
using UniversalApi.Helpers;

namespace UniversalApi
{
    /// <summary>
    /// Provides a method for working like API with EntityFramework Core.
    /// Forces you to implement <see cref="IUniversalContext"/> for your context.
    /// </summary>
    public sealed class UniversalAPI
    {
        private readonly string ConnectionString;

        public UniversalAPI(string connectionString)
        {
            ConnectionString = connectionString;
        }

        /// <summary>
        /// By default, the logging option is enabled.
        /// </summary>
        public bool ConsoleLogEnabled = true;

        /// <summary>Method getting data for the passed request.</summary>
        /// <param name="request">JSON string data containing the name of the request.</param>
        /// <param name="context">Pass an EntityFramework context implementing an interface <see cref="IUniversalContext"/>.</param>
        /// <returns>Returns JSON string data if data read is success, or return null if operation failed.</returns>
        public string InvokeRequest(string request, IUniversalContext context)
        {
            // Create start program time
            var startTime = DateTime.UtcNow;
            // Log received request
            if (ConsoleLogEnabled)
                Console.WriteLine($"Received request data: {request}");
            

            //== Create SQL query ==//
            string commandQuery = QueryCreator.CreateCommandQuery(request.ToLower(), context);
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

        private void LogGetCommandQuery(string commandQuery)
        {
            if (commandQuery == null)
            {
                Console.WriteLine();
                Console.WriteLine(">=== Error ===<");
                Console.WriteLine(">--- An error occurred while creating the query string. ---<");
                Console.WriteLine();
            }

            Console.WriteLine();
            Console.WriteLine(">=== Create query done ===<");
            Console.WriteLine($"Query: {commandQuery}");
            Console.WriteLine();
        }
        private void LogGetData(string data, DateTime startTime)
        {
            if (data == null || data.Count() == 0)
            {
                Console.WriteLine();
                Console.WriteLine(">=== Error ===<");
                Console.WriteLine(">--- An error occurred while receiving data. ---<");
                Console.WriteLine();
            }

            Console.WriteLine();
            Console.WriteLine(">=== Receiving data done ===<");
            Console.WriteLine(data);
            Console.WriteLine();

            LogExecutionTime(startTime);
        }
        private void LogExecutionTime(DateTime startTime)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine($"Program execution time: {DateTime.UtcNow - startTime}");
            Console.ResetColor();

            for (int i = 0; i < 64; i++)
                Console.Write('=');
            Console.WriteLine();
        }
    }
}
