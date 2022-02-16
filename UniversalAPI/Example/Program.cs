using Poolz;
using Interfaces.DBModel;
using Interfaces.Helpers;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, dynamic> inputData = new Dictionary<string, dynamic>
            {
                { "TableName", "SignUp" },
                { "Id", 3 }
            };
            var jsonString = JsonConvert.SerializeObject(inputData);

            UniversalAPI UniversalAPI = new UniversalAPI(ConnectionString.connectionString, DynamicDB.ConnectToDb());

            string jsonTable = UniversalAPI.GetTable(jsonString);

            List<object[]> table = JsonConvert.DeserializeObject<List<object[]>>(jsonTable);
            DrawHeader("GetTable(string data)");
            DrawTable(table);


            Console.ReadLine();
        }

        static void DrawHeader(string currentMethod)
        {
            string methodName = $"|  {currentMethod}  |";

            for (int i = 0; i < methodName.Length; i++)
                Console.Write("=");

            Console.WriteLine();

            Console.WriteLine(methodName);

            for (int i = 0; i < methodName.Length; i++)
                Console.Write("=");

            Console.WriteLine();
        }
        static void DrawTable(List<object[]> table)
        {
            foreach (var row in table)
            {
                string rowData = String.Empty;
                for (int i = 0; i < row.Length; i++)
                    rowData += $"| {row[i]} |";

                Console.WriteLine(rowData);
                for (int i = 0; i < rowData.Length; i++)
                    Console.Write("=");
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine();
        }
    }
}
