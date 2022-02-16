using Poolz;
using Interfaces.DBModel;
using Interfaces.Helpers;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;

namespace Example
{
    public class A
    {
        public int Age { get; set; }
        public string Name { get; set; }
    }

    class Program
    {
        public static void Run()
        {
            var a = new A
            {
                Age = 10,
                Name = "10"
            };

            var json = System.Text.Json.JsonSerializer.Serialize(a);

            var result = JsonConvert.DeserializeObject(json);



            Console.WriteLine(result.GetType());
        }

        static void Main(string[] args)
        {
            //Run();

            UniversalAPI UniversalAPI = new UniversalAPI(ConnectionString.connectionString, DynamicDB.ConnectToDb());
            StreamReader reader = new StreamReader("../../../../Interfaces/inputData.json");
            string jsonString = reader.ReadToEnd();

            string jsonTable = UniversalAPI.GetTable(jsonString);
            List<object[]> table = JsonConvert.DeserializeObject<List<object[]>>(jsonTable);
            DrawHeader("GetTable(string tableName)");
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
