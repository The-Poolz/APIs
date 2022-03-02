using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Interfaces.DBModel;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using UniversalApi.Helpers;
using System.Reflection;
using System.Dynamic;

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
            string commandQuery = QueryCreator.GetCommandQuery(data);

            object[] table = DataReader.GetData(commandQuery, ConnectionString, Context);
            string json = JsonConvert.SerializeObject(table);

            return json;
        }
    }
}
