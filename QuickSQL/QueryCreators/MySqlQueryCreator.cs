using QuickSQL.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace QuickSQL.QueryCreators
{
    /// <summary>
    /// Provides methods for creating SQL query string
    /// </summary>
    public static class MySqlQueryCreator
    {
        /// <summary>
        /// Creates an SQL query string.
        /// </summary>
        /// <param name="requestSettings">Pass <see cref="Request"/> object with request settings.</param>
        /// <returns>Returns a SQL query string.</returns>
        public static string CreateCommandQuery(Request requestSettings)
        {
            if (!RequestValidator.IsValidRequest(requestSettings))
                return null;

            string tableName = requestSettings.TableName;
            List<string> columns = ConvertToList(requestSettings.SelectedColumns);
            string jsonColumns = "JSON_ARRAYAGG(JSON_OBJECT(";
            foreach (var column in columns)
            {
                if (columns.Last() == column)
                    jsonColumns += $"'{column.Trim()}',{column.Trim()}";
                else
                    jsonColumns += $"'{column.Trim()}',{column.Trim()}, ";
            }
            jsonColumns += "))";

            string commandQuery = $"SELECT {jsonColumns} FROM {tableName}";

            if (!string.IsNullOrEmpty(requestSettings.WhereCondition))
            {
                string condition = string.Join(" AND ", ConvertToList(requestSettings.WhereCondition));
                commandQuery += ($" WHERE {condition}");
            }

            return commandQuery;
        }

        private static List<string> ConvertToList(string str) => str.Split(",").ToList();
    }
}