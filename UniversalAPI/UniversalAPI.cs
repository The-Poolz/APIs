using Interfaces.DBModel;
using Newtonsoft.Json;
using System;
using System.Linq;
using UniversalApi.Helpers;

namespace UniversalApi
{
    public class UniversalAPI
    {
        private readonly string ConnectionString;
        public UniversalAPI(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public string GetTable(string data, DynamicDBContext context)
        {
            var start = DateTime.UtcNow;
            Console.WriteLine("==== Start create query ====");
            string commandQuery = QueryCreator.GetCommandQuery(data.ToLower());
            if (commandQuery != null)
            {
                Console.WriteLine(commandQuery);
                Console.WriteLine("==== Create query done ====");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("==== Error ====");
                Console.WriteLine("An error occurred while creating the query string.");
                Console.WriteLine();
                return null;
            }

            Console.WriteLine("==== Start receiving data ====");
            object[] table = DataReader.GetData(commandQuery, ConnectionString, context);
            if (table != null || table.Count() != 0)
            {
                Console.WriteLine("==== Receiving data done ====");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("==== Error ====");
                Console.WriteLine("An error occurred while receiving data.");
                Console.WriteLine();
            }

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine($"Program execution time: {DateTime.UtcNow - start}");
            Console.ResetColor();

            if (table.Length == 1)
                return JsonConvert.SerializeObject(table.ToList().First());

            return JsonConvert.SerializeObject(table);
        }
    }
}
