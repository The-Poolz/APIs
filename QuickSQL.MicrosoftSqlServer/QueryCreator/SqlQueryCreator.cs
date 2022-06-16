using QuickSQL.QueryCreator;
using System.Collections.Generic;
using System.Linq;

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
            commandQuery.Add($"SELECT {string.Join(", ", request.SelectedColumns)}");
            commandQuery.Add($"FROM {request.TableName}");
            commandQuery.Add(CreateWhereCondition(request.WhereConditions));
            commandQuery.Add(CreateOrderByRules(request.OrderRules));
            commandQuery.Add("FOR JSON AUTO, WITHOUT_ARRAY_WRAPPER");
            return string.Join(" ", commandQuery.Where(T=>T != string.Empty));
        }
    }
}