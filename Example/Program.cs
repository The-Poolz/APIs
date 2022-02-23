using UniversalApi;
using Interfaces.DBModel;
using Interfaces.Helpers;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, dynamic> inputData = new Dictionary<string, dynamic>
            {
                { "Request", "mysignup" },
                { "Id", 3 },
                { "Address", "0x3a31ee5557c9369c35573496555b1bc93553b553" }
            };
            var jsonString = JsonConvert.SerializeObject(inputData);
            Console.WriteLine(jsonString);

            UniversalAPI UniversalAPI = new UniversalAPI(ConnectionString.connectionString, DynamicDB.ConnectToDb());

            string jsonTable = UniversalAPI.GetTable(jsonString);
            Console.WriteLine(jsonTable);

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
