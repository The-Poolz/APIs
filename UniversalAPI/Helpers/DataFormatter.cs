using Interfaces.DBModel;
using Interfaces.Helpers;
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
                return IsValidRequestName(requestName, out tables);
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
            string[] names = tables.Split(",");
            for (int i = 0; i < names.Count(); i++)
                names[i].Trim();
            return names.ToList();
        }

        public static string GetCommandQuery(string json)
        {
            Dictionary<string, dynamic> data = FormatJson(json);
            if (data == null || data.Count == 0)
                throw new ArgumentException("An error occurred while trying to generate a query string. Missing data.");

            string tables;
            if (!HasRequest(data, out tables))
                return null;
            else
                data.Remove("Request");


            if (data.ContainsKey("Id"))
            {
                int? id = (int?)data["Id"];
                if (!IsValidId(id))
                    return null;
            }
            if (data.ContainsKey("Address"))
            {
                var address = data["Address"];
                if (!IsValidAddress(address))
                    return null;
            }

            List<string> tablesName = GetTablesName(tables);
            if (tablesName.Count == 1)
                return CreateSelectQuery(tablesName.First(), data);
            else
                return CreateJoinQuery(tablesName, data);
        }
         
        private static string CreateSelectQuery(string tableName, Dictionary<string, dynamic> data)
        {
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
            }
            string condition = string.Join(" AND ", conditions);

            string commandQuery = $"SELECT * FROM {tableName} WHERE {condition}";
            return commandQuery;
        }

        private static string CreateJoinQuery(List<string> tablesName, Dictionary<string, dynamic> data)
        {
            List<string> conditions = new List<string>();
            string firstTable = tablesName.ToArray()[0];
            string secondTable = tablesName.ToArray()[1];

            foreach (var item in data)
            {
                var paramName = item.Key;
                var value = item.Value;
                if (value == null)
                    throw new ArgumentException($"Parameter '{paramName}' cannot be null.");

                if (value.GetType() == typeof(string))
                    conditions.Add($"{firstTable}.{paramName} = '{value}'");
                else
                    conditions.Add($"{firstTable}.{paramName} = {value}");
            }
            string condition = string.Join(" AND ", conditions);


            string commandQuery = $"SELECT * " +
                $"FROM {firstTable} " +
                $"INNER JOIN {secondTable} " +
                $"ON {firstTable}.Id = {secondTable}.Id " +
                $"WHERE {condition}";
            Console.WriteLine(commandQuery);
            return commandQuery;
        }
    }
}