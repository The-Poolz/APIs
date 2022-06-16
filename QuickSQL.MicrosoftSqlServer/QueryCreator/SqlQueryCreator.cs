using QuickSQL.QueryCreator;

namespace QuickSQL.MicrosoftSqlServer
{
    /// <summary>
    /// Provides methods for creating SQL query string.
    /// </summary>
    public class SqlQueryCreator : BaseQueryCreator
    {
        /// <summary>
        /// Creates an SQL query string.
        /// </summary>
        /// <param name="request">Pass <see cref="Request"/> object with request settings.</param>
        /// <returns>Returns a SQL query string.</returns>
        protected override string OnCreateCommandQuery(Request request)
        {
            string commandQuery = $"SELECT {string.Join(", ", request.SelectedColumns)} FROM {request.TableName}";
            commandQuery += request.WhereConditions != null ? $" {CreateWhereCondition(request.WhereConditions)}" : string.Empty;
            commandQuery += request.OrderRules != null ? $" {CreateOrderByRules(request.OrderRules)}" : string.Empty;
            commandQuery += " FOR JSON AUTO, WITHOUT_ARRAY_WRAPPER";
            return commandQuery;
        }
    }
}