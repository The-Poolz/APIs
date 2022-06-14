using QuickSQL.QueryCreator;

namespace QuickSQL.Tests.QueryCreator
{
    public class SqlQueryCreator : BaseQueryCreator
    {
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