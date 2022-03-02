using Interfaces.DBModel;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;

namespace UniversalApi.Helpers
{
    public static class DataReader
    {
        public static object[] GetData(string commandQuery, string connectionString, DynamicDBContext context)
        {
            List<object> table = new List<object>();
            using (context)
            {
                try
                {
                    var connection = SqlUtil.GetConnection(connectionString);
                    using (connection)
                    {
                        connection.Open();
                        var reader = SqlUtil.GetReader(commandQuery, connection);
                        if (!reader.HasRows)
                            return null;

                        var properties = GetPropertyInfos(commandQuery, context);

                        ExpandoObject DataObj = new ExpandoObject();
                        Object[] values = new Object[reader.FieldCount];
                        while (reader.Read())
                        {
                            for (int i = 0; i < properties.ToArray().Length; i++)
                            {
                                AddProperty(DataObj, properties[i].Name, reader.GetValue(i));
                            }
                            table.Add(DataObj);
                        }
                        reader.Close();
                    }
                    return table.ToArray();
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            return table.ToArray();
        }

        private static List<string> GetTables(DynamicDBContext context)
        {
            var tables = context.APIRequestList.Select(i => i.Tables);
            List<string> tablesName = new List<string>();
            foreach (var table in tables)
            {
                tablesName.AddRange(table.Split(","));
            }
            for (int i = 0; i < tablesName.Count(); i++)     // Remove all whitespace
                tablesName[i] = String.Concat(tablesName[i].Where(c => !Char.IsWhiteSpace(c)));
            return tablesName;
        }
        private static List<string> GetCurrentTables(string commandQuery, List<string> allTablesName)
        {
            List<string> tablesName = new List<string>();
            foreach (var tableName in allTablesName)
            {
                if (commandQuery.Contains(tableName))
                {
                    tablesName.Add(tableName);
                }
            }
            return tablesName;
        }
        private static List<PropertyInfo> GetPropertyInfos(string commandQuery, DynamicDBContext context)
        {
            var allTablesName = GetTables(context);
            var tablesName = GetCurrentTables(commandQuery, allTablesName);
            List<PropertyInfo> properties = new List<PropertyInfo>();
            foreach (var name in tablesName)
            {
                Type type = Type.GetType($"Interfaces.DBModel.Models.{name}");
                properties.AddRange(type.GetProperties());
            }
            return properties;
        }


        private static void AddProperty(ExpandoObject expando, string propertyName, object propertyValue)
        {
            // ExpandoObject supports IDictionary so we can extend it like this
            var expandoDict = expando as IDictionary<string, object>;
            if (expandoDict.ContainsKey(propertyName))
                expandoDict[propertyName] = propertyValue;
            else
                expandoDict.Add(propertyName, propertyValue);
        }
    }
}
