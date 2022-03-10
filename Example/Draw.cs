using System;

namespace Example
{
    public static class Draw
    {
        public static void DrawInputData(string inputData)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("==== Input data ====");
            Console.WriteLine(inputData);
            Console.WriteLine();
            Console.ResetColor();
        }
        public static void DrawResult(string jsonTable)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("==== Find result ====");
            Console.WriteLine(jsonTable);
            Console.WriteLine();
            Console.ResetColor();

            for (int i = 0; i < jsonTable.Length; i++)
                Console.Write('=');
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}
