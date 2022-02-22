using Nethereum.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace UniversalApi.Helpers
{
    public static class DataFormatter
    {
        public static Dictionary<string, dynamic> FormatJson(string json) =>
            JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(json);

        public static String CreateCommandQuery(string json)
        {
            Dictionary<string, dynamic> data = FormatJson(json);
            if (data == null || data.Count == 0)
                throw new ArgumentException("An error occurred while trying to generate a query string. Missing data.");

            string tableName = string.Empty;
            if (data.ContainsKey("TableName"))
            {
                tableName = data["TableName"];
                if (tableName == null || tableName == string.Empty)
                    throw new ArgumentException("An error occurred while trying to generate a query string. Table name missing.");
                data.Remove("TableName");
            }

            if (data.ContainsKey("Id"))
            {
                var id = data["Id"];
                if (id == null)
                    throw new ArgumentException("Parameter 'Id' cannot be null.");
                if (id < 0)
                    throw new ArgumentException("Parameter 'Id' cannot be negative.");
            }

            if (data.ContainsKey("Address"))
            {
                var address = data["Address"];
                bool result = AddressExtensions.IsValidEthereumAddressHexFormat(address);
                if (result == false)
                    throw new ArgumentException("Parameter 'Address' not valid.");
            }
            

            List<string> selectColumns = new List<string>();
            List<string> conditions = new List<string>();
            foreach (var item in data)
            {
                var paramName = item.Key;
                var value = item.Value;
                if (value == null)
                    throw new ArgumentException($"Parameter '{paramName}' cannot be null.");
                
                if (value.GetType() == typeof(string))
                    conditions.Add($"{tableName}.{paramName} = '{value}'");
                else
                    conditions.Add($"{tableName}.{paramName} = {value}");

                selectColumns.Add($"{tableName}.{paramName}");
            }
            if (selectColumns == null || selectColumns.Count == 0)
                throw new ArgumentException("An error occurred while trying to generate a query string. Missing parameters.");

            string condition = string.Join(" AND ", conditions);

            string commandQuery = $"SELECT {string.Join(", ", selectColumns)} FROM {tableName} WHERE {condition}";
            return commandQuery;
        }
    }
}
