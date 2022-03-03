using Interfaces.DBModel;
using Interfaces.Helpers;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UniversalApi;

namespace Example
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Dictionary<string, dynamic>> listData = new List<Dictionary<string, dynamic>>
            {
                new Dictionary<string, dynamic>
                {
                    { "Request", "mysignup" },
                    { "Id", 3 },
                    { "address", "0x3a31ee5557c9369c35573496555b1bc93553b553" }
                },
                new Dictionary<string, dynamic>
                {
                    { "Request", "wallet" },
                    { "Id", 3 },
                    { "Owner", "0x3a31ee5557c9369c35573496555b1bc93553b553" }
                },
                new Dictionary<string, dynamic>
                {
                    { "Request", "leaderboard" },
                    { "Id", 1 },
                    { "Owner", "0x1a01ee5577c9d69c35a77496565b1bc95588b521" }
                }
            };
            foreach (var data in listData)
            {
                for (int i = 0; i < 64; i++)
                    Console.Write('=');
                Console.WriteLine();


                var jsonString = JsonConvert.SerializeObject(data);
                Console.WriteLine("Input data");
                Console.WriteLine(jsonString);
                Console.WriteLine();

                UniversalAPI UniversalAPI = new UniversalAPI(ConnectionString.connectionString, DynamicDB.ConnectToDb());

                string jsonTable = UniversalAPI.GetTable(jsonString);
                Console.WriteLine("Find result");
                Console.WriteLine(jsonTable);
                Console.WriteLine();
            }

            Console.ReadLine();
        }
    }
}
