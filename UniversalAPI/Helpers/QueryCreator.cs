using Interfaces.DBModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UniversalApi.Helpers
{
    public static class QueryCreator
    {
        public static string GetCommandQuery(string json, DynamicDBContext context)
        {
            var data = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(json.ToLower());
            if (data == null || data.Count == 0)
                return null;

            string tables;
            if (DataChecker.HasRequest(data, out tables) == false)
                return null;

            var requestItem = DataChecker.GetDataItem(data, "request");
            var request = requestItem.Value.Value;
            string columns, joinCondition;
            columns = GetColumns(request, context);
            joinCondition = GetJoinCondition(request, context);
            DataChecker.RemoveRequest(data);

            if (!DataChecker.CheckId(data))
                return null;
            if (!DataChecker.CheckAddress(data))
                return null;
            if (!DataChecker.CheckOwner(data))
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

        private static List<string> GetTablesName(string tables)
        {
            List<string> names = tables.Split(",").ToList();

            // Remove all whitespace
            names = names.Select(name => name.Replace(" ", string.Empty)).ToList();

            return names;
        }
        private static string GetColumns(string requestName, DynamicDBContext context)
        {
            var request = context.APIRequestList.FirstOrDefault(i => i.Request == requestName);
            if (request == null)
                return null;

            return request.Columns;
        }
        private static string GetJoinCondition(string requestName, DynamicDBContext context)
        {
            var request = context.APIRequestList.FirstOrDefault(i => i.Request == requestName);
            if (request == null)
                return null;

            return request.JoinCondition;
        }
    }
}