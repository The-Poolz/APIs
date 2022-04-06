using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;

namespace UniversalAPI.Helpers
{
    /// <summary>
    /// Provides methods for creating SQL query string
    /// </summary>
    public static class QueryCreator
    {
        /// <summary>
        /// Creates an SQL query string. Validity checks for id, address, owner parameters.
        /// </summary>
        /// <param name="json">JSON data string with the name of the request, conditions optional.</param>
        /// <param name="context">Сontext that implements the IUniversalContext interface.</param>
        /// <returns>Returns a SQL query string.</returns>
        public static string CreateCommandQuery(string json, APIContext context)
        {
            var data = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(json.ToLower());
            if (data == null || data.Count == 0)
                return null;

            // Check has request name, get tables name
            if (DataChecker.HasRequest(data, context, out string tables) == false)
                return null;

            // Get request name
            var requestItem = DataChecker.GetDataItem(data, "request");
            var request = requestItem.Value.Value;

            // Check data for specific parameters and validation
            if (!DataChecker.CheckId(data))
                return null;
            if (!DataChecker.CheckAddress(data))
                return null;
            if (!DataChecker.CheckOwner(data))
                return null;

            // Removing the request name from the data to form a query string
            DataChecker.RemoveRequest(data);

            string columns = GetColumns(request, context);
            string joinCondition = GetJoinCondition(request, context);
            List<string> tablesName = GetTablesName(tables);

            string commandQuery;
            if (tablesName.Count == 1)
                commandQuery = CreateSelectQuery(tablesName.First(), columns, data);
            else
                commandQuery = CreateJoinQuery(tablesName, columns, joinCondition, data);

            return commandQuery;
        }

        private static string CreateSelectQuery(string tableName, string columns, Dictionary<string, dynamic> data)
        {
            string commandQuery = $"SELECT {columns} FROM {tableName} ";

            if (data.Count != 0)
            {
                List<string> conditions = new List<string>();
                foreach (var param in data)
                {
                    var paramName = param.Key;
                    var value = param.Value;
                    if (value == null)
                        return null;

                    if (value.GetType() == typeof(string))
                        conditions.Add($"{tableName}.{paramName} = '{value}'");
                    else
                        conditions.Add($"{tableName}.{paramName} = {value}");
                }
                string condition = string.Join(" AND ", conditions);
                commandQuery += ($"WHERE {condition} ");
            }
            
            commandQuery += "FOR JSON PATH";

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
                    return null;

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
                $"WHERE {condition} FOR JSON PATH";

            return commandQuery;
        }

        private static List<string> GetTablesName(string tables)
        {
            List<string> names = tables.Split(",").ToList();

            // Remove all whitespace
            names = names.Select(name => name.Replace(" ", string.Empty)).ToList();

            return names;
        }
        private static string GetColumns(string requestName, APIContext context)
        {
            var request = context.APIRequests.FirstOrDefault(i => i.Name == requestName);
            if (request == null)
                return null;

            return request.SelectedColumns;
        }
        private static string GetJoinCondition(string requestName, APIContext context)
        {
            var request = context.APIRequests.FirstOrDefault(i => i.Name == requestName);
            if (request == null)
                return null;

            return request.JoinCondition;
        }
    }
}