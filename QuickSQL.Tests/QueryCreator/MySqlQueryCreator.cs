using System.Linq;
using System.Collections.Generic;

using QuickSQL.QueryCreator;

namespace QuickSQL.Tests.QueryCreator
{
    public class MySqlQueryCreator : BaseQueryCreator
    {
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

            return commandQuery;
        }
    }
}
