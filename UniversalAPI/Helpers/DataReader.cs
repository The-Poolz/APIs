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

        private static List<string> GetTables(DynamicDBContext context)
        {
            var tables = context.APIRequestList.Select(i => i.Tables);
            List<string> tablesName = new List<string>();
            foreach (var table in tables)
                tablesName.AddRange(table.Split(","));

            tablesName = tablesName.Select(tableName => tableName.Replace(" ", string.Empty)).ToList();

            return tablesName;
        }
        private static List<string> GetCurrentTables(string commandQuery, List<string> allTablesName) =>
            allTablesName.Where(tableName => commandQuery.Contains(tableName)).ToList();
        private static List<string> GetColumns(string commandQuery, DynamicDBContext context, List<string> tablesName)
        {
            string tables = string.Join(", ", tablesName);

            var columns = context.APIRequestList.Where(i => i.Tables.Equals(tables)).Select(i => i.Columns);

            return columns.Where(columnName => commandQuery.Contains(columnName)).ToList();
        }
        private static List<PropertyInfo> GetPropertyInfos(string commandQuery, DynamicDBContext context)
        {
            var allTablesName = GetTables(context);
            var tablesName = GetCurrentTables(commandQuery, allTablesName);

            var columns = GetColumns(commandQuery, context, tablesName);
            List<string> _columns = new List<string>();
            var count = columns.Count;
            for (int i = 0; i < count; i++)
                _columns.AddRange(columns[i].Split(", "));
            columns.Clear();
            var _count = _columns.Count;
            for (int i = 0; i < _count; i++)
                columns.AddRange(_columns[i].Split('.'));

            List<PropertyInfo> properties = new List<PropertyInfo>();
            List<PropertyInfo> selectedProperties = new List<PropertyInfo>();
            foreach (var name in tablesName)
            {
                string tableName = name;
                if (name.LastIndexOf('s') == name.Length-1)
                    tableName = name.TrimEnd('s');

                Type type = Type.GetType($"Interfaces.DBModel.Models.{tableName}");
                properties.AddRange(type.GetProperties());
                foreach (var col in columns)
                {
                    if (col == "*")
                    {
                        selectedProperties.AddRange(properties);
                    }
                    else if (selectedProperties.FirstOrDefault(p => p.Name.Equals(col)) == null)
                    {
                        selectedProperties.AddRange(properties.Where(p => p.Name.Equals(col)));
                    }
                }
            }
            return selectedProperties;
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
