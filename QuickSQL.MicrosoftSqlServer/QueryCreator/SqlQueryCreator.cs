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
            var commandQuery = new List<string>();
            commandQuery.add($"SELECT {string.Join(", ", request.SelectedColumns)} FROM {request.TableName}");
            commandQuery.add(CreateWhereCondition(request.WhereConditions));
            commandQuery.add(CreateOrderByRules(request.OrderRules));
            commandQuery.add("FOR JSON AUTO, WITHOUT_ARRAY_WRAPPER");
            return string.Join(" ", commandQuery.where(T=>T != string.Empty));
        }
    }
}