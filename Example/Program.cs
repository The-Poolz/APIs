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
        static void Main()
        {
            List<Dictionary<string, dynamic>> listData = new List<Dictionary<string, dynamic>>
            {
                new Dictionary<string, dynamic>
                {
                    { "Request", "wallet" },
                    { "Id", 3 },
                    { "Owner", "0x3a31ee5557c9369c35573496555b1bc93553b553" }
                },
                new Dictionary<string, dynamic>
                {
                    { "Request", "mysignup" },
                    { "Id", 3 },
                    { "address", "0x3a31ee5557c9369c35573496555b1bc93553b553" }
                },
                new Dictionary<string, dynamic>
                {
                    { "Request", "tokenbalanse" },
                    { "Id", 1 },
                    { "Owner", "0x1a01ee5577c9d69c35a77496565b1bc95588b521" }
                }
            };
            using (var context = DynamicDB.ConnectToDb())
            {
                foreach (var data in listData)
                {
                    var inputData = JsonConvert.SerializeObject(data);

                    UniversalAPI UniversalAPI = new UniversalAPI(ConnectionString.connectionString);

                    string jsonTable = UniversalAPI.GetTable(inputData, context);
                }
            }
            Console.ReadLine();
        }
    }
}
