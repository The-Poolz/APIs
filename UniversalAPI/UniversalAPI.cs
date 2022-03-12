using Interfaces.DBModel;
using Newtonsoft.Json;
using System;
using System.Linq;
using UniversalApi.Helpers;

namespace UniversalApi
{
    /// <summary>
    /// Provides a method for working flexibly with EntityFramework Core.
    /// Forces you to implement IUniversalContext for your context.
    /// Stores a database connection string.
    /// </summary>
    public sealed class UniversalAPI
    {
        private readonly string ConnectionString;
        /// <summary>
        /// By default, the logging option is enabled.
        /// </summary>
        public bool ConsoleLogEnabled = true;

        public UniversalAPI(string connectionString)
        {
            ConnectionString = connectionString;
        }

        /// <summary>Method getting data for the passed request.</summary>
        /// <param name="data">JSON string data containing the name of the request.</param>
        /// <param name="context">Pass an EntityFramework context implementing an interface IUniversalContext.</param>
        /// <returns>Returns JSON string data if data read is success, or return null if operation failed.</returns>
        public string GetTable(string data, IUniversalContext context)
        {
            // Create start program time
            // Log received data
            var startTime = DateTime.UtcNow;
            if (ConsoleLogEnabled)
                Console.WriteLine($"Received data: {data}");
            

            // Create SQL query
            string commandQuery = QueryCreator.CreateCommandQuery(data.ToLower(), context);
            // Log received SQL query string
            if (ConsoleLogEnabled)
                LogGetCommandQuery(commandQuery);
            if (commandQuery == null)
                return null;


            // Reading data with SqlDataReader
            object[] table = DataReader.GetData(commandQuery, ConnectionString, context);
            if (table == null || table.Count() == 0)
                return null;

            // If result single row, serialize like obj. Else serialize like array
            string result;
            if (table.Count() == 1)
                result = JsonConvert.SerializeObject(table.ToList().First());
            else
                result = JsonConvert.SerializeObject(table);

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
