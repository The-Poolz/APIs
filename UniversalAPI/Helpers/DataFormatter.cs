using Interfaces.DBModel;
using Nethereum.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UniversalApi.Helpers
{
    public static class DataFormatter
    {
        public static Dictionary<string, dynamic> FormatJson(string json) =>
            JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(json);

        private static bool HasRequest(Dictionary<string, dynamic> data, out string tables)
        {
            tables = string.Empty;
            if (data.ContainsKey("Request"))
            {
                string requestName = data["Request"];
                IsValidRequestName(requestName, out tables);
            }
            return false;
        }
        private static bool IsValidRequestName(string requestName, out string tables)
        {
            tables = string.Empty;
            using DynamicDBContext context = DynamicDB.ConnectToDb();
            var request = context.APIRequestList.FirstOrDefault(p => p.Request == requestName);
            if (request != null && request.Tables != string.Empty)
            {
                tables = request.Tables;
                return true;
            }
            return false;
        }
        private static bool IsValidId(int? id)
        {
            if (id == null)
                return false;
            if (id < 0)
                return false;

            return true;
        }
        private static bool IsValidAddress(string address)
        {
            bool result = AddressExtensions.IsValidEthereumAddressHexFormat(address);
            if (!result)
                return false;

            return true;
        }

        private static List<string> GetTablesName(string tables)
        {
            string[] names = tables.Split(", ");
            return names.ToList();
        }

        public static String CreateCommandQuery(string json)
        {
            Dictionary<string, dynamic> data = FormatJson(json);
            if (data == null || data.Count == 0)
                throw new ArgumentException("An error occurred while trying to generate a query string. Missing data.");

            string tables = string.Empty;
            string tableName = string.Empty;
            HasRequest(data, out tables);

            if (data.ContainsKey("Id"))
            {
                int? id = data["Id"];
                IsValidId(id);
            }
            if (data.ContainsKey("Address"))
            {
                var address = data["Address"];
                IsValidAddress(address);
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
