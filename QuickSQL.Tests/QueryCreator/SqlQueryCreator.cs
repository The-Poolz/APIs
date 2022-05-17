using QuickSQL.QueryCreator;

namespace QuickSQL.Tests.QueryCreator
{
    public class SqlQueryCreator : BaseQueryCreator
    {
        protected override string OnCreateCommandQuery(Request request)
        {
            string commandQuery = $"SELECT {request.GetSelectedColumns()} FROM {request.GetTableName()}";

            if (request.GetWhereConditions() != null)
            {
                commandQuery += $" {CreateWhereCondition(request.GetWhereConditions())}";
            }

            commandQuery += " FOR JSON PATH";
            return commandQuery;
        }
    }
}