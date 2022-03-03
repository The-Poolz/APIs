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
            Dictionary<string, dynamic> data = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(json);
            if (data == null || data.Count == 0)
                return null;

            string tables;
            string columns;
            string joinCondition;
            if (HasRequest(data, out tables))
            {
                var request = data["Request"];
                columns = GetColumns(request);
                joinCondition = GetJoinCondition(request);
                data.Remove("Request");
            }
            else
            {
                return null;
            }
            

            if (!CheckId(data))
                return null;
            if (!CheckAddress(data))
                return null;

            List<string> tablesName = GetTablesName(tables);
            string commandQuery = null;
            if (tablesName.Count == 1)
                commandQuery = CreateSelectQuery(tablesName.First(), columns, data);
            else
                commandQuery = CreateJoinQuery(tablesName, columns, joinCondition, data);

            return commandQuery;
        }

        private static string CreateSelectQuery(string tableName, string columns, Dictionary<string, dynamic> data)
        {
            List<string> conditions = new List<string>();
            foreach (var param in data)
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
            string commandQuery = $"SELECT {columns} FROM {tableName} WHERE {condition}";

            return commandQuery;
        }
        private static string CreateJoinQuery(List<string> tablesName, string columns, string joinCondition, Dictionary<string, dynamic> data)
        {
            List<string> conditions = new List<string>();
            string firstTable = tablesName.ToArray()[0];
            string secondTable = tablesName.ToArray()[1];

            foreach (var param in data)
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
                $"SELECT {columns} " +
                $"FROM {firstTable} " +
                $"INNER JOIN {secondTable} " +
                $"ON {joinCondition} " +
                $"WHERE {condition}";

            return commandQuery;
        }

        private static bool HasRequest(Dictionary<string, dynamic> data, out string tables)
        {
            tables = string.Empty;
            if (data.ContainsKey("Request") || data.ContainsKey("request"))
            {
                dynamic requestName;
                if (data.TryGetValue("Request", out requestName))
                    return IsValidRequestName(requestName, out tables);
                if (data.TryGetValue("request", out requestName))
                    return IsValidRequestName(requestName, out tables);
            }
            return false;
        }
        private static bool CheckId(Dictionary<string, dynamic> data)
        {
            if (data.ContainsKey("Id") || data.ContainsKey("id"))
            {
                dynamic id;
                if (data.TryGetValue("Id", out id))
                    return IsValidId((int?)id);
                if (data.TryGetValue("id", out id))
                    return IsValidId((int?)id);
            }
            return true;
        }
        private static bool CheckAddress(Dictionary<string, dynamic> data)
        {
            if (data.ContainsKey("Address") || data.ContainsKey("address"))
            {
                dynamic address;
                if (data.TryGetValue("Address", out address))
                    return IsValidAddress((string)address);
                if (data.TryGetValue("address", out address))
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
            var namesCount = names.Count();
            for (int i = 0; i < namesCount; i++)     // Remove all whitespace
                names[i] = String.Concat(names[i].Where(c => !Char.IsWhiteSpace(c)));
            return names.ToList();
        }
        private static string GetColumns(string request)
        {
            using (var context = DynamicDB.ConnectToDb())
            {
                var columns = context.APIRequestList.FirstOrDefault(i => i.Request == request).Columns;
                return columns;
            }
        }
        private static string GetJoinCondition(string request)
        {
            using (var context = DynamicDB.ConnectToDb())
            {
                var condition = context.APIRequestList.FirstOrDefault(i => i.Request == request).JoinCondition;
                return condition;
            }
        }
    }
}