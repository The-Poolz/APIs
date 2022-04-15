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
        /// Creates an SQL query string.
        /// </summary>
        /// <param name="requestSettings">Pass <see cref="APIRequest"/> object with request settings.</param>
        /// <returns>Returns a SQL query string.</returns>
        public static string CreateCommandQuery(APIRequest requestSettings)
        {
            if (!APIRequestValidator.IsValidAPIRequest(requestSettings))
                return null;

            List<string> tablesName = ConvertToList(requestSettings.SelectedTables);
            tablesName = tablesName.Select(str => str.Replace(" ", string.Empty)).ToList();     // Remove all whitespace
            string commandQuery;

            if (tablesName.Count == 1)
                commandQuery = CreateSelectQuery(tablesName.First(), requestSettings);
            else
                commandQuery = CreateJoinQuery(tablesName, requestSettings);

            return commandQuery;
        }

        private static string CreateSelectQuery(string tableName, APIRequest requestSettings)
        {
            string columns = requestSettings.SelectedColumns;
            string commandQuery = $"SELECT {columns} FROM {tableName} ";

            if (!string.IsNullOrEmpty(requestSettings.WhereCondition))
            {
                string condition = string.Join(" AND ", ConvertToList(requestSettings.WhereCondition));
                commandQuery += ($"WHERE {condition} ");
            }
            commandQuery += "FOR JSON PATH";

            return commandQuery;
        }
        private static string CreateJoinQuery(List<string> tablesName, APIRequest requestSettings)
        {
            string columns = requestSettings.SelectedColumns;
            string joinCondition = requestSettings.JoinCondition;
            string firstTable = tablesName.ToArray()[0];
            string secondTable = tablesName.ToArray()[1];
            string commandQuery =
                $"SELECT {columns} " +
                $"FROM {firstTable} " +
                $"INNER JOIN {secondTable} " +
                $"ON {joinCondition} ";

            if (!string.IsNullOrWhiteSpace(requestSettings.WhereCondition))
            {
                string condition = string.Join(" AND ", ConvertToList(requestSettings.WhereCondition));
                commandQuery += $"WHERE {condition} ";
            }
            commandQuery += "FOR JSON PATH";

            return commandQuery;
        }

        private static List<string> ConvertToList(string str)
        {
            List<string> names = str.Split(",").ToList();

            int count = names.Count;
            for (int i = 0; i < count; i++)
                names[i] = names[i].Trim();

            return names;
        }
    }
}