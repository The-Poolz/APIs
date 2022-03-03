using Interfaces.DBModel;
using Newtonsoft.Json;
using System;
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
            var start = DateTime.UtcNow;
            Console.WriteLine("Start create query.");
            string commandQuery = QueryCreator.GetCommandQuery(data);
            if (commandQuery != null)
            {
                Console.WriteLine(commandQuery);
                Console.WriteLine("Create query done!");
                Console.WriteLine();
            }
            else
            {
                Console.WriteLine("An error occurred while creating the query string.");
            }

            object[] table = DataReader.GetData(commandQuery, ConnectionString, Context);

            Console.WriteLine(DateTime.UtcNow - start);
            return JsonConvert.SerializeObject(table);
        }
    }
}
