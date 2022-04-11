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
        /// <param name="jsonRequest">JSON data string with the name of the request, conditions optional.</param>
        /// <param name="requestSettings">Pass <see cref="APIRequestSettings"/> object with request settings.</param>
        /// <returns>Returns a SQL query string.</returns>
        public static string CreateCommandQuery(string jsonRequest, APIRequestSettings requestSettings)
        {
            var data = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(jsonRequest.ToLower());

            // Check data for specific parameters and validation
            if (!DataChecker.CheckId(data))
                return null;
            if (!DataChecker.CheckAddress(data))
                return null;
            if (!DataChecker.CheckOwner(data))
                return null;

            string columns = requestSettings.SelectedColumns;
            string joinCondition = requestSettings.JoinCondition;

            // Convert SelectedTables string to List<string>
            List<string> names = requestSettings.SelectedTables.Split(",").ToList();
            names = names.Select(name => name.Replace(" ", string.Empty)).ToList();     // Remove all whitespace
            List<string> tablesName = names;

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
            string commandQuery =
                $"SELECT {columns} " +
                $"FROM {firstTable} " +
                $"INNER JOIN {secondTable} " +
                $"ON {joinCondition} ";

            if (data.Count != 0)
            {
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
                commandQuery += $"WHERE {condition} ";
            }
            commandQuery += "FOR JSON PATH";

            return commandQuery;
        }
    }
}