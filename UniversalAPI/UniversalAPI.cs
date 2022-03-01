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

        private object[] GetData(string commandQuery)
        {
            List<object> table = new List<object>();
            using (Context)
            {
                try
                {
                    var connection = SqlUtil.GetConnection(ConnectionString);
                    using (connection)
                    {
                        connection.Open();
                        var reader = SqlUtil.GetReader(commandQuery, connection);
                        if (!reader.HasRows)
                            return null;


                        var allTables = Context.APIRequestList.Select(i => i.Tables);
                        List<string> allTablesName = new List<string>();
                        foreach (var tables in allTables)
                        {
                            allTablesName.AddRange(tables.Split(", "));
                        }

                        

                        List<string> tablesName = new List<string>();
                        foreach (var tableName in allTablesName)
                        {
                            if (commandQuery.Contains(tableName))
                            {
                                tablesName.Add(tableName);
                            }
                        }



                        List<PropertyInfo> properties = new List<PropertyInfo>();
                        foreach (var name in tablesName)
                        {
                            Type type = Type.GetType($"Interfaces.DBModel.Models.{name}");
                            properties.AddRange(type.GetProperties());
                        }



                        ExpandoObject expando = new ExpandoObject();
                        Object[] values = new Object[reader.FieldCount];
                        while (reader.Read())
                        {
                            for (int i = 0; i < properties.ToArray().Length; i++)
                            {
                                DynamicPropObj.AddProperty(expando, properties[i].Name, reader.GetValue(i));
                            }
                            table.Add(expando);
                        }
                        reader.Close();
                    }
                    return table.ToArray();
                }
                catch (SqlException e)
                {
                    Console.WriteLine(e.ToString());
                }
            }
            return table.ToArray();
        }

        public String GetTable(string data)
        {
            string commandQuery = QueryCreator.GetCommandQuery(data);

            object[] table = GetData(commandQuery);
            string json = JsonConvert.SerializeObject(table);

            return json;
        }
    }
}
