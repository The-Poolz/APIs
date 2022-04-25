using System.Collections.Generic;
using System.Linq;

namespace QuickSQL.Helpers
{
    /// <summary>
    /// Provides methods for creating SQL query string
    /// </summary>
    public static class QueryCreator
    {
        /// <summary>
        /// Creates an SQL query string.
        /// </summary>
        /// <param name="requestSettings">Pass <see cref="Request"/> object with request settings.</param>
        /// <returns>Returns a SQL query string.</returns>
        public static string CreateCommandQuery(Request requestSettings)
        {
            if (!RequestValidator.IsValidAPIRequest(requestSettings))
                return null;

            string tableName = requestSettings.SelectedTables;
            List<string> columns = ConvertToList(requestSettings.SelectedColumns);
            //Create parse columns to json data
            string jsonColumns = "JSON_ARRAYAGG(JSON_OBJECT(";
            foreach (var column in columns)
            {
                if (columns.Last() == column)
                {
                    jsonColumns += $"'{column}',{column}";
                }
                else
                {
                    jsonColumns += $"'{column}',{column}, ";
                }
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