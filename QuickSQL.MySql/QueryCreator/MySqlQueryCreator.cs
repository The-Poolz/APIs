using System.Linq;

using QuickSQL.QueryCreator;

namespace QuickSQL.MySql
{
    /// <summary>
    /// Provides methods for creating SQL query string
    /// </summary>
    public class MySqlQueryCreator : BaseQueryCreator
    {
        /// <summary>
        /// Creates an SQL query string.
        /// </summary>
        /// <param name="request">Pass <see cref="Request"/> object with request settings.</param>
        /// <returns>Returns a SQL query string.</returns>
        protected override string OnCreateCommandQuery(Request request)
        {
            string tableName = request.TableName;
            string jsonColumns = "JSON_ARRAYAGG(JSON_OBJECT(";
            foreach (var column in request.SelectedColumns)
            {
                if (request.SelectedColumns.Last() == column)
                    jsonColumns += $"'{column.Trim()}',{column.Trim()}";
                else
                    jsonColumns += $"'{column.Trim()}',{column.Trim()}, ";
            }
            jsonColumns += "))";

            string commandQuery = $"SELECT {jsonColumns} FROM {tableName}";

            if (request.WhereConditions != null)
            {
                commandQuery += $" {CreateWhereCondition(request.WhereConditions)}";
            }
            if (request.OrderRules != null)
            {
                commandQuery += $" {CreateOrderByRules(request.OrderRules)}";
            }

            return commandQuery;
        }
    }
}