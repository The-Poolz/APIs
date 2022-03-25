using Microsoft.Data.SqlClient;
using Pluralize.NET.Core;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;

namespace UniversalApi.Helpers
{
    public static class DataReader
    {
        public static object[] GetData(string commandQuery, string connectionString, IUniversalContext context)
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
                    /* How it work */
                    // Function get properties count for table obj and make this obj with value
                    // We need this in order to correctly deserialize the created table object in JSON
                    while (reader.Read())
                    {
                        // Create dynamic obj with selected properties
                        var propCount = properties.Count;
                        for (int i = 0; i < propCount; i++)
                            AddProperty(dataObj, properties[i].Name, reader.GetValue(i));

                        // Adding obj to result list
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

        private static List<string> GetTables(string commandQuery, IUniversalContext context)
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
            // Use Distinct() as there may be matches in table names
            tablesName = tablesName.Where(
                tableName => commandQuery.Contains(tableName)).Distinct().ToList();

            return tablesName;
        }
        private static List<string> GetColumns(string commandQuery, IUniversalContext context, List<string> tablesName)
        {
            // Format List<string> tablesName to string
            string tables = string.Join(", ", tablesName);

            // Search selected columns by Tables parameter
            var columns = context.APIRequestList.Where(i => i.Tables.Equals(tables)).Select(i => i.Columns).ToList();

            // Make a list of strings containing column and table names
            // In GetPropertyInfos() compare model fields and selected fields
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
        private static List<PropertyInfo> GetPropertyInfos(string commandQuery, IUniversalContext context)
        {
            var tablesName = GetTables(commandQuery, context);
            var columns = GetColumns(commandQuery, context, tablesName);

            List<PropertyInfo> properties = new List<PropertyInfo>();
            foreach (var name in tablesName)
            {
                // Singularize table name, model has singular name but tablename in DB has pluar name
                string tableName = new Pluralizer().Singularize(name);


                var modelsNamespace = Environment.GetEnvironmentVariable("modelsNamespace");
                var modelsAssembly = Environment.GetEnvironmentVariable("modelsAssembly");


                // Get models type
                Type type = Type.GetType($"{modelsNamespace}.{tableName}, {modelsAssembly}");
                foreach (var col in columns)
                {
                    if (col == "*") 
                    {
                        // add all model properties
                        properties.AddRange(type.GetProperties());
                        break;
                    }
                    else if (properties.FirstOrDefault(p => p.Name.Equals(col)) == null) 
                    {
                        // add selected properties
                        properties.AddRange(type.GetProperties().Where(p => p.Name.Equals(col)));
                    }
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
