using QuickSQL.QueryCreator;

namespace QuickSQL.Tests.QueryCreator
{
    public class SqlQueryCreator : BaseQueryCreator
    {
        protected override string OnCreateCommandQuery(Request request)
        {
            string commandQuery = $"SELECT {request.SelectedColumns} FROM {request.TableName}";

            if (request.WhereConditions != null)
            {
                commandQuery += $" {CreateWhereCondition(request.WhereConditions)}";
            }

            commandQuery += " FOR JSON PATH";
            return commandQuery;
        }
    }
}