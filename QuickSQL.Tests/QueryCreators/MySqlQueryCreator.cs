using System.Linq;
using System.Collections.Generic;

using QuickSQL.QueryCreator;

namespace QuickSQL.Tests.QueryCreators
{
    /// <summary>
    /// Provides methods for creating SQL query string
    /// </summary>
    public class MySqlQueryCreator : BaseQueryCreator
    {
        public override Providers Provider => Providers.MySql;

        /// <summary>
        /// Creates an SQL query string.
        /// </summary>
        /// <param name="request">Pass <see cref="Request"/> object with request settings.</param>
        /// <returns>Returns a SQL query string.</returns>
        protected override string OnCreateCommandQuery(Request request)
        {
            string tableName = request.TableName;
            List<string> columns = request.SelectedColumns.Split(",").ToList();
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

            if (!string.IsNullOrEmpty(request.WhereCondition))
            {
                string condition = string.Join(" AND ", request.WhereCondition.Split(",").ToList());
                commandQuery += ($" WHERE {condition}");
            }

            return commandQuery;
        }
    }
}