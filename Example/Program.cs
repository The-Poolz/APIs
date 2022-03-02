using Interfaces.DBModel;
using Interfaces.Helpers;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Linq;
using UniversalApi;

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
                { "address", "0x3a31ee5557c9369c35573496555b1bc93553b553" }
            };
            //Dictionary<string, dynamic> inputData = new Dictionary<string, dynamic>
            //{
            //    { "Request", "wallet" },
            //    { "Id", 3 },
            //    { "Owner", "0x3a31ee5557c9369c35573496555b1bc93553b553" }
            //};
            var jsonString = JsonConvert.SerializeObject(inputData);
            Console.WriteLine(jsonString);

            UniversalAPI UniversalAPI = new UniversalAPI(ConnectionString.connectionString, DynamicDB.ConnectToDb());

            string jsonTable = UniversalAPI.GetTable(jsonString);
            Console.WriteLine(jsonTable);

            Console.ReadLine();
        }

    }
}
