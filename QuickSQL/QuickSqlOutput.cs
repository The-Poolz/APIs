using System;

namespace QuickSQL
{
    public partial class QuickSql
    {
        private static void ConsoleOutput(Request request, string commandQuery, string result, DateTime startTime)
        {
            if (!CheckConsoleOutputEnabled())
                return;
            Console.WriteLine("QuickSql started...");

            DisplayRequest(request);
            Console.WriteLine();

            Console.WriteLine("QuickSql started create SQL query...");
            DisplayQuery(commandQuery);
            Console.WriteLine();

            Console.WriteLine("QuickSql started received data...");
            DisplayReceivedData(result);
            Console.WriteLine();

            Console.WriteLine("QuickSql finished...");
            Console.WriteLine();

            LogExecutionTime(startTime);
        }

        private static void DisplayQuery(string commandQuery)
        {
            if (commandQuery == null)
                Console.WriteLine("An error occurred while creating the query string.");
            else
                Console.WriteLine($"Created query: {commandQuery}");
        }
        private static void DisplayReceivedData(string data)
        {
            if (data == null)
                Console.WriteLine("An error occurred while receiving data. Received data null.");
            else
                Console.WriteLine($"Received data: {data}");
        }
        private static void LogExecutionTime(DateTime startTime)
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            string executionTime = $"Program execution time: {DateTime.UtcNow - startTime}";
            Console.WriteLine(executionTime);
            Console.ResetColor();

            int count = executionTime.Length;
            for (int i = 0; i < count; i++)
                Console.Write('=');
            Console.WriteLine();
        }
        private static void DisplayRequest(Request request)
        {
            if (request == null)
            {
                Console.WriteLine("Error, request cannot be null.:");
            }
            else
            {
                Console.WriteLine("Received request:");
                Console.WriteLine($"    SelectedTables: {request.SelectedTables}");
                Console.WriteLine($"    SelectedColumns: {request.SelectedColumns}");
                if (request.WhereCondition != null)
                    Console.WriteLine($"    WhereCondition: {request.WhereCondition}");
                if (request.JoinCondition != null)
                    Console.WriteLine($"    JoinCondition: {request.JoinCondition}");
            }
        }
        private static bool CheckConsoleOutputEnabled() =>
            ConsoleOutputEnabled ? true : false;
    }
}
