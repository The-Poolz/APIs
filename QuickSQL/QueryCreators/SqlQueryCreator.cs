using QuickSQL.Helpers;
using System.Linq;

namespace QuickSQL.QueryCreators
{
    /// <summary>
    /// Provides methods for creating SQL query string
    /// </summary>
    public static class SqlQueryCreator
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

            string commandQuery = $"SELECT {requestSettings.SelectedColumns} FROM {requestSettings.TableName}";
            if (!string.IsNullOrEmpty(requestSettings.WhereCondition))
            {
                string condition = string.Join(" AND ", requestSettings.WhereCondition.Split(",").ToList());
                commandQuery += ($" WHERE {condition}");
            }
            commandQuery += " FOR JSON PATH";
            return commandQuery;
        }
    }
}