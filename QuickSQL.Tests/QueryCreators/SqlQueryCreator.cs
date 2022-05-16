using QuickSQL.QueryCreator;

namespace QuickSQL.Tests.QueryCreators
{
    /// <summary>
    /// Provides methods for creating SQL query string
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
            string commandQuery = $"SELECT {request.SelectedColumns} FROM {request.TableName}";

            if (request.WhereConditions != null)
            {
                commandQuery += $" {base.CreateWhereCondition(request.WhereConditions)}";
            }

            commandQuery += " FOR JSON PATH";
            return commandQuery;
        }
    }
}