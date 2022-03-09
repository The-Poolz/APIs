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
        private readonly DynamicDBContext Context;
        public UniversalAPI(string connectionString, DynamicDBContext context)
        {
            ConnectionString = connectionString;
            Context = context;
        }

        public string GetTable(string data)
        {
            #if DEBUG
            var start = DateTime.UtcNow;
            #endif

            Console.WriteLine("==== Start create query ====");
            string commandQuery = QueryCreator.GetCommandQuery(data);
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

            }

            Console.WriteLine("==== Start receiving data ====");
            object[] table = DataReader.GetData(commandQuery, ConnectionString, Context);
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

            #if DEBUG
            Console.WriteLine($"Program execution time: {DateTime.UtcNow - start}");
            #endif

            if (table.Length == 1)
                return JsonConvert.SerializeObject(table.ToList().First());
            else
                return JsonConvert.SerializeObject(table);
        }
    }
}
