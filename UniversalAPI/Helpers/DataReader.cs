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
            List<object> data = new List<object>();
            try
            {
                using (var connection = SqlUtil.GetConnection(connectionString))
                {
                    connection.Open();

                    var reader = SqlUtil.GetReader(commandQuery, connection);
                    if (!reader.HasRows)
                        return null;

                    var properties = GetPropertyInfos(commandQuery, context);
                    ExpandoObject dataObj = new ExpandoObject();
                    while (reader.Read())
                    {
                        var propCount = properties.Count;
                        for (int i = 0; i < propCount; i++)
                            AddProperty(dataObj, properties[i].Name, reader.GetValue(i));

                        data.Add(dataObj);
                    }
                    reader.Close();
                }
                return data.ToArray();
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return data.ToArray();
        }


        private static List<string> GetTables(string commandQuery, DynamicDBContext context)
        {
            // Get all Tables name
            var tables = context.APIRequestList.Select(i => i.Tables);
            // Format string tablesName to List<string>
            List<string> tablesName = new List<string>();
            foreach (var table in tables)
                tablesName.AddRange(table.Split(","));

            // Remove all whitespase
            tablesName = tablesName.Select(tableName => tableName.Replace(" ", string.Empty)).ToList();

            // Search current tables by query string and return List<string> current tables name
            return tablesName.Where(tableName => commandQuery.Contains(tableName)).ToList();
        }
        private static List<string> GetColumns(string commandQuery, DynamicDBContext context, List<string> tablesName)
        {
            // Format List<string> tablesName to string
            string tables = string.Join(", ", tablesName);

            // Search selected columns by Tables parameter
            var columns = context.APIRequestList.Where(i => i.Tables.Equals(tables)).Select(i => i.Columns);

            // Make a list of strings containing column and table names
            // In GetPropertyInfos compare model fields and selected fields
            List<string> currentColumns = columns.Where(columnName => commandQuery.Contains(columnName)).ToList();
            List<string> _columns = new List<string>();
            var count = currentColumns.Count();
            for (int i = 0; i < count; i++)
                _columns.AddRange(currentColumns[i].Split(", "));
            currentColumns.Clear();
            var _count = _columns.Count;
            for (int i = 0; i < _count; i++)
                currentColumns.AddRange(_columns[i].Split('.'));

            return currentColumns;
        }
        private static List<PropertyInfo> GetPropertyInfos(string commandQuery, DynamicDBContext context)
        {
            var tablesName = GetTables(commandQuery, context);
            var columns = GetColumns(commandQuery, context, tablesName);

            List<PropertyInfo> properties = new List<PropertyInfo>();
            foreach (var name in tablesName)
            {
                // If 
                string tableName = name;
                if (name.IsSingular())
                {

                }
                if (name.LastIndexOf('s') == name.Length-1)
                    tableName = name.TrimEnd('s');

                Type type = Type.GetType($"UniversalApi.Models.{tableName}");
                foreach (var col in columns)
                {
                    if (col == "*")
                        properties.AddRange(type.GetProperties());
                    else if (properties.FirstOrDefault(p => p.Name.Equals(col)) == null)
                        properties.AddRange(type.GetProperties().Where(p => p.Name.Equals(col)));
                }
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
