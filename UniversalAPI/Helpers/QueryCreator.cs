using Interfaces.DBModel;
using Nethereum.Util;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UniversalApi.Helpers
{
    public static class QueryCreator
    {
        public static string GetCommandQuery(string json)
        {
            Dictionary<string, dynamic> Data = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(json);
            if (Data == null || Data.Count == 0)
                return null;

            string tables;
            if (!HasRequest(Data, out tables))
                return null;
            else
                Data.Remove("Request");

            if (!CheckId(Data))
                return null;
            if (!CheckAddress(Data))
                return null;

            List<string> tablesName = GetTablesName(tables);
            string commandQuery = string.Empty;
            if (tablesName.Count == 1)
                commandQuery = CreateSelectQuery(tablesName.First(), Data);
            else
                commandQuery = CreateJoinQuery(tablesName, Data);

            #if DEBUG
            Console.WriteLine(commandQuery);
            #endif

            return commandQuery;
        }

        private static string CreateSelectQuery(string tableName, Dictionary<string, dynamic> Data)
        {
            List<string> conditions = new List<string>();
            foreach (var param in Data)
            {
                var paramName = param.Key;
                var value = param.Value;
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
        private static string CreateJoinQuery(List<string> tablesName, Dictionary<string, dynamic> Data)
        {
            List<string> conditions = new List<string>();
            string firstTable = tablesName.ToArray()[0];
            string secondTable = tablesName.ToArray()[1];

            foreach (var param in Data)
            {
                var paramName = param.Key;
                var value = param.Value;
                if (value == null)
                    throw new ArgumentException($"Parameter '{paramName}' cannot be null.");

                if (value.GetType() == typeof(string))
                    conditions.Add($"{firstTable}.{paramName} = '{value}'");
                else
                    conditions.Add($"{firstTable}.{paramName} = {value}");
            }

            string condition = string.Join(" AND ", conditions);
            string commandQuery =
                $"SELECT * " +
                $"FROM {firstTable} " +
                $"INNER JOIN {secondTable} " +
                $"ON {firstTable}.Id = {secondTable}.Id " +
                $"WHERE {condition}";

            return commandQuery;
        }

        private static bool HasRequest(Dictionary<string, dynamic> Data, out string tables)
        {
            tables = string.Empty;
            if (Data.ContainsKey("Request") || Data.ContainsKey("request"))
            {
                dynamic requestName;
                if (Data.TryGetValue("Request", out requestName))
                    return IsValidRequestName(requestName, out tables);
                if (Data.TryGetValue("request", out requestName))
                    return IsValidRequestName(requestName, out tables);
            }
            return false;
        }
        private static bool CheckId(Dictionary<string, dynamic> Data)
        {
            if (Data.ContainsKey("Id") || Data.ContainsKey("id"))
            {
                dynamic id;
                if (Data.TryGetValue("Id", out id))
                    return IsValidId((int?)id);
                if (Data.TryGetValue("id", out id))
                    return IsValidId((int?)id);
            }
            return true;
        }
        private static bool CheckAddress(Dictionary<string, dynamic> Data)
        {
            if (Data.ContainsKey("Address") || Data.ContainsKey("address"))
            {
                dynamic address;
                if (Data.TryGetValue("Address", out address))
                    return IsValidAddress((string)address);
                if (Data.TryGetValue("address", out address))
                    return IsValidAddress((string)address);
            }
            return true;
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
            for (int i = 0; i < names.Count(); i++)     // Remove all whitespace
                names[i] = String.Concat(names[i].Where(c => !Char.IsWhiteSpace(c)));
            return names.ToList();
        }
    }
}