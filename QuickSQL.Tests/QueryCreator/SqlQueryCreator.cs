using QuickSQL.QueryCreator;

namespace QuickSQL.Tests.QueryCreator
{
    public class SqlQueryCreator : BaseQueryCreator
    {
        protected override string OnCreateCommandQuery(Request request)
        {
            string selectedColumns = string.Join(", ", request.SelectedColumns);
            string commandQuery = $"SELECT {selectedColumns} FROM {request.TableName}";

            if (request.WhereConditions != null)
            {
                commandQuery += $" {CreateWhereCondition(request.WhereConditions)}";
            }
            if (request.OrderRules != null)
            {
                commandQuery += $" {CreateOrderByRules(request.OrderRules)}";
            }

            commandQuery += " FOR JSON PATH";
            return commandQuery;
        }
    }
}