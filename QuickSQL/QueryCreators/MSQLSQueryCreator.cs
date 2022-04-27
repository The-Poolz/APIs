using QuickSQL.Helpers;
using System.Collections.Generic;
using System.Linq;

namespace QuickSQL.QueryCreators
{
    /// <summary>
    /// Provides methods for creating SQL query string
    /// </summary>
    public static class MSQLSQueryCreator
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

            string commandQuery = $"SELECT {requestSettings.SelectedColumns} FROM {requestSettings.SelectedTable}";
            if (!string.IsNullOrEmpty(requestSettings.WhereCondition))
            {
                string condition = string.Join(" AND ", ConvertToList(requestSettings.WhereCondition));
                commandQuery += ($" WHERE {condition}");
            }
            commandQuery += " FOR JSON PATH";
            return commandQuery;
        }

        private static List<string> ConvertToList(string str)
            => str.Split(",").ToList();
    }
}