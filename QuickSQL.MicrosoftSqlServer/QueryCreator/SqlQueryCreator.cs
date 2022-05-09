using System.Linq;

using QuickSQL.QueryCreator;

namespace QuickSQL.MicrosoftSqlServer.QueryCreator
{
    /// <summary>
    /// Provides methods for creating SQL query string
    /// </summary>
    public class SqlQueryCreator : BaseQueryCreator
    {
        public override Providers Provider => Providers.MicrosoftSqlServer;

        /// <summary>
        /// Creates an SQL query string.
        /// </summary>
        /// <param name="request">Pass <see cref="Request"/> object with request settings.</param>
        /// <returns>Returns a SQL query string.</returns>
        protected override string OnCreateCommandQuery(Request request)
        {
            string commandQuery = $"SELECT {request.SelectedColumns} FROM {request.TableName}";
            if (!string.IsNullOrEmpty(request.WhereCondition))
            {
                string condition = string.Join(" AND ", request.WhereCondition.Split(",").ToList());
                commandQuery += ($" WHERE {condition}");
            }
            commandQuery += " FOR JSON PATH";
            return commandQuery;
        }
    }
}