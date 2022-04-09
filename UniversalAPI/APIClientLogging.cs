using System;
using System.Linq;

namespace UniversalAPI
{
    public partial class APIClient
    {
        private static void LogGetCommandQuery(string commandQuery)
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
        private static void LogGetData(string data, DateTime startTime)
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
        private static void LogExecutionTime(DateTime startTime)
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
